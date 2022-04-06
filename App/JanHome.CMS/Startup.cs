using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using JanHome.CMS.Middleware;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Impl;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MI.Cache;
using Swashbuckle.AspNetCore.Swagger;
using Utils.Settings;

using Nest;
using JanHome.CMS.ExcelHelper;
using Microsoft.AspNetCore.Http;

namespace JanHome.CMS
{
    public class Startup
    {
        readonly string AllowSpecificOrigin = "MyAllowSpecificOrigins ";
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // config cache
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);


            services.InitRedisCache(Configuration);
           // services.AddResponseCaching();
            services.AddResponseCaching(options =>
            {
               // options.MaximumBodySize = 1024;
                //options.UseCaseSensitivePaths = true;
               
            });

            //MI.ES.ConfigES.Start();
            //services.AddSingleton(x => new ConnectionSettings(new Uri("http://127.0.0.1:9200")));
            //services.AddTransient<IAutocompleteService, AutocompleteService>();

            // cache end
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IArticlesRepository, ArticlesRepository>();
            services.AddTransient<IDashBoardRepository, DashBoardRepository>();
            services.AddTransient<IZoneArticleRepository, ZoneArticleRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
            services.AddTransient<IArticleInLanguageRepository, ArticleInLanguageRepository>();
            services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IProductSpecificationTemplateRepository, ProductSpecificationTemplateRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICouponRepository, CouponRepository>();

            services.AddTransient<IUserStore<AppUser>, UserStore>();
            services.AddTransient<IRoleStore<AppRole>, RoleStore>();
            services.AddTransient<IExportUtility, ExportUtility>();

            services.AddIdentity<AppUser, AppRole>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
                // Default Password settings.
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 1;
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); //clear default settings

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", builder =>
            //    {
            //        builder.WithOrigins(Configuration["Cors:WithOrigin"])
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowCredentials();
            //    });
            //});


            //services.AddMvc(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_0);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
            });

            // Add framework services.
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "MI Project",
                    Description = "MI API Swagger surface",
                    Contact = new Contact
                    {
                        Name = "AnhLBT",
                        Email = "tuananh300496@gmail.com",
                    },
                    //License = new License { Name = "MIT" }
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }}
                });
            });
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc();


            //.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            loggerFactory.AddDebug();


            //app.UseCors(
            //     options => options.WithOrigins("http://example.com").AllowAnyMethod()
            //);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseResponseCaching();


            //app.Use(async (context, next) =>
            //{
            //    context.Response.GetTypedHeaders().CacheControl =
            //        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            //        {
            //            Public = true,
            //            MaxAge = TimeSpan.FromHours(24)
            //        };
            //    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
            //        new string[] { "Accept-Encoding" };

            //    await next();
            //});
            //app.UseHttpsRedirection();
            app.ConfigureCustomExceptionMiddleware();
            app.UseStaticFiles();
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((document, request) =>
                {
                    var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
                    document.Paths.Clear();
                    foreach (var pathItem in paths)
                    {
                        document.Paths.Add(pathItem.Key, pathItem.Value);
                    }
                });
            });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "MI Rest Api v1"); });
            app.UseAuthentication(); //Add auth
            app.UseCors("MyPolicy");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
