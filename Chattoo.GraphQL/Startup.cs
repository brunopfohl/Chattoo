using System.Linq;
using Chattoo.Application;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Services;
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
                            ctx.Context.Errors.Add(new ExecutionError("Nedostatečná oprávnění."));
                        }
                        else if (ctx.Exception is ChannelNotFoundException channelNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Komunikační kanál nebyl nalezen."));
                        }
                        else if (ctx.Exception is ChannelRoleNotFoundException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("Uživatelská role komunikačního kanálu nebyla nalezena.")
                            );
                        }
                        else if (ctx.Exception is GroupNotFoundException groupNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Skupina nebyla nalezena."));
                        }
                        else if (ctx.Exception is GroupRoleNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Uživatelská role skupiny nebyla nalezena."));
                        }
                        else if (ctx.Exception is UserNotFoundException userNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Uživatel nebyl nalezen."));
                        }
                        else if (ctx.Exception is MessageNotFoundException messageNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Zpráva nebyla nalezena."));
                        }
                        else if (ctx.Exception is CalendarEventNotFoundException eventNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Událost nebyla nalezena."));
                        }
                        else if (ctx.Exception is CalendarEventTypeNotFoundException eventTypeNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Typ události nebyl nalezen."));
                        }
                        else if (ctx.Exception is CalendarEventWishNotFoundException wishNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Přání nebylo nalezeno."));
                        }
                        else if (ctx.Exception is AttachmentNotFoundException attachmentNotFoundException)
                        {
                            ctx.Context.Errors.Add(new ExecutionError("Příloha zprávy nebyla nalezena."));
                        }
                        else if (ctx.Exception is CalendarEventCapacityInsufficientException eventCapacityInsufficientException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("Do události již nelze přidat další uživatele.")
                            );
                        }
                        else if (ctx.Exception is DuplicateUserInCalendarEventException duplicateUserInCalendarEventException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("Uživatel může ve skupině existovat pouze jedinkrát.")
                            );
                        }
                        else if (ctx.Exception is DuplicitCalendarEventTypeException duplicitCalendarEventTypeException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("Typ události může být v přání pouze jedinkrát.")
                            );
                        }
                        else if (ctx.Exception is DuplicitChannelRoleNameException duplicitChannelRoleNameException)
                        {
                            ctx.Context.Errors.Add
                            (
                                new ExecutionError("V komunikačním kanálu již existuje role se stejným názvem.")
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