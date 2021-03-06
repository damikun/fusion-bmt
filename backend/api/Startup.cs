using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

using HotChocolate;
using HotChocolate.AspNetCore;

using api.Context;
using api.Services;
using api.GQL;
using api.Swagger;


namespace api
{
    public class Startup
    {
        private readonly string _accessControlPolicyName = "AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddOptions();
            services.Configure<BmtDbOptions>(options => Configuration.GetSection("Database").Bind(options));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddCors(options =>
            {
                options.AddPolicy(_accessControlPolicyName,
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.WithOrigins(
                        "http://localhost:3000",
                        "https://*.equinor.com"
                    ).SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });

            services.AddDbContext<BmtDbContext>();

            services.AddScoped<ProjectService>();
            services.AddScoped<ParticipantService>();
            services.AddScoped<EvaluationService>();
            services.AddScoped<QuestionService>();
            services.AddScoped<AnswerService>();
            services.AddScoped<QuestionTemplateService>();

            services.AddGraphQL(s =>
                SchemaBuilder.New()
                    .AddServices(s)
                    .AddQueryType<GraphQuery>()
                    .AddMutationType<Mutation>()
                    .AddAuthorizeDirectiveType()
                    .Create()
                );

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()

                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            TokenUrl = new Uri($"{Configuration["AzureAd:Instance"]}/{Configuration["AzureAd:TenantId"]}/oauth2/token"),
                            AuthorizationUrl = new Uri($"{Configuration["AzureAd:Instance"]}/{Configuration["AzureAd:TenantId"]}/oauth2/authorize"),
                            Scopes = { { $"api://{Configuration["AzureAd:ClientId"]}/user_impersonation", "User Impersonation" } }
                        }
                    }

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        Array.Empty<string>()
                    }
                });
                c.DocumentFilter<GraphEndpoint>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(_accessControlPolicyName);

            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<BmtDbContext>();
            dbContext.InitializeIfInMem();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UsePlayground("/graphql");

                var option = new RewriteOptions();
                option.AddRedirect("^$", "graphql/playground");
                app.UseRewriter(option);
            }
            app.UseRouting();

            // Comment out for using playground locally without auth
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
                c.OAuthAppName("Fusion-BMT");
                c.OAuthClientId(Configuration["AzureAd:ClientId"]);
                c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>()
                    { { "resource", $"{Configuration["AzureAd:ClientId"]}" } });
            });


            app.UseGraphQL("/graphql");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
