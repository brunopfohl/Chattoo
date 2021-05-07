using Chattoo.Application;
using Chattoo.Application.Common.Interfaces;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Services;
using Chattoo.GraphQL.Subscription.CommunicationChannelMessage;
using Chattoo.Infrastructure;
using Chattoo.Infrastructure.Persistence;
using GraphQL.Server;
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
                .AddSingleton<ICommunicationChannelMessageSubscriptionProvider, CommunicationChannelMessageSubscriptionProvider>()
                .AddSingleton<GraphQLSchema>()
                .AddGraphQL((options, provider) =>
                {
                    options.EnableMetrics = true;
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException.Message);
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

            app.UseHttpsRedirection();
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
            app.UseGraphQLPlayground();
            
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