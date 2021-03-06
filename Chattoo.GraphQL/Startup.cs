using System.Linq;
using Chattoo.Application;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Services;
using Chattoo.GraphQL.Subscription.CalendarEvent;
using Chattoo.GraphQL.Subscription.CommunicationChannel;
using Chattoo.GraphQL.Subscription.CommunicationChannelMessage;
using Chattoo.Infrastructure;
using Chattoo.Infrastructure.Persistence;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chattoo.GraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureNonBreakingSameSiteCookies();
            
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddSingleton<ICurrentUserIdService, CurrentUserIdService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            
            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services
                .AddSingleton<ICommunicationChannelSubscriptionProvider, CommunicationChannelSubscriptionProvider>()
                .AddSingleton<ICommunicationChannelMessageSubscriptionProvider, CommunicationChannelMessageSubscriptionProvider>()
                .AddSingleton<ICalendarEventSubscriptionProvider, CalendarEventSubscriptionProvider>()
                .AddSingleton<GraphQLSchema>()
                .AddGraphQL((options, provider) =>
                {
                    options.EnableMetrics = true;
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx =>
                    {
                        if (ctx.Exception is ValidationException validationException)
                        {
                            foreach (var error in validationException.Errors)
                            {
                                var executionErrors =
                                    error.Value.Select(e => new ExecutionError(e));
                                
                                ctx.Context.Errors.AddRange(executionErrors);
                            }
                        }
                        else if (ctx.Exception is ForbiddenAccessException forbiddenAccessException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Nedostate??n?? opr??vn??n??."));
                        }
                        else if (ctx.Exception is ChannelNotFoundException channelNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Komunika??n?? kan??l nebyl nalezen."));
                        }
                        else if (ctx.Exception is ChannelRoleNotFoundException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("U??ivatelsk?? role komunika??n??ho kan??lu nebyla nalezena.")
                            );
                        }
                        else if (ctx.Exception is GroupNotFoundException groupNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Skupina nebyla nalezena."));
                        }
                        else if (ctx.Exception is GroupRoleNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("U??ivatelsk?? role skupiny nebyla nalezena."));
                        }
                        else if (ctx.Exception is UserNotFoundException userNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("U??ivatel nebyl nalezen."));
                        }
                        else if (ctx.Exception is MessageNotFoundException messageNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Zpr??va nebyla nalezena."));
                        }
                        else if (ctx.Exception is CalendarEventNotFoundException eventNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Ud??lost nebyla nalezena."));
                        }
                        else if (ctx.Exception is CalendarEventTypeNotFoundException eventTypeNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Typ ud??losti nebyl nalezen."));
                        }
                        else if (ctx.Exception is CalendarEventWishNotFoundException wishNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("P????n?? nebylo nalezeno."));
                        }
                        else if (ctx.Exception is AttachmentNotFoundException attachmentNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("P????loha zpr??vy nebyla nalezena."));
                        }
                        else if (ctx.Exception is CalendarEventCapacityInsufficientException eventCapacityInsufficientException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("Do ud??losti ji?? nelze p??idat dal???? u??ivatele.")
                            );
                        }
                        else if (ctx.Exception is DuplicateUserInCalendarEventException duplicateUserInCalendarEventException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("U??ivatel m????e ve skupin?? existovat pouze jedinkr??t.")
                            );
                        }
                        else if (ctx.Exception is DuplicitCalendarEventTypeException duplicitCalendarEventTypeException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("Typ ud??losti m????e b??t v p????n?? pouze jedinkr??t.")
                            );
                        }
                        else if (ctx.Exception is DuplicitChannelRoleNameException duplicitChannelRoleNameException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("V komunika??n??m kan??lu ji?? existuje role se stejn??m n??zvem.")
                            );
                        }
                        else if (ctx.Exception is DateIntervalTooShortException dateIntervalTooShortException)
                        {
                            var intervalLength =
                                dateIntervalTooShortException.DateInterval.EndsAt -
                                dateIntervalTooShortException.DateInterval.StartsAt;
                            
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError($"Interval s d??lkou {intervalLength} je moc kr??tk??.")
                            );
                        }
                        
                        logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                    };
                })
                // Add required services for GraphQL request/response de/serialization
                .AddSystemTextJson() // For .NET Core 3+
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddWebSockets() // Add required services for web socket support
                .AddGraphTypes(typeof(GraphQLSchema)) // Add all IGraphType implementors in assembly which ChatSchema exists 
                .AddUserContextBuilder(httpContext => new GraphQLUserContext(httpContext.User))
                .AddDataLoader(); // Add required services for DataLoader support
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }

            app.UseHealthChecks("/health");

            app.UseCors("MyPolicy");

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSpaStaticFiles();
            
            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            
            // this is required for websockets support
            app.UseWebSockets();

            // use websocket middleware for ChatSchema at default path /graphql
            app.UseGraphQLWebSockets<GraphQLSchema>();

            // use HTTP middleware for ChatSchema at default path /graphql
            app.UseGraphQL<GraphQLSchema>();
            
            // use graphiQL middleware at default path /ui/graphiql
            app.UseGraphQLVoyager();
            app.UseGraphQLPlayground(options: new PlaygroundOptions()
            {
                EditorTheme = EditorTheme.Light
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });
        }
    }
}