using CoRicetta.Business.Services.ActionService;
using CoRicetta.Business.Services.CategoryService;
using CoRicetta.Business.Services.IngredientService;
using CoRicetta.Business.Services.MenuService;
using CoRicetta.Business.Services.RecipeService;
using CoRicetta.Business.Services.ReportService;
using CoRicetta.Business.Services.StepService;
using CoRicetta.Business.Services.UserService;
using CoRicetta.Data.Context;
using CoRicetta.Data.Repositories.ActionRepo;
using CoRicetta.Data.Repositories.CategoryDetailRepo;
using CoRicetta.Data.Repositories.CategoryRepo;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.Repositories.IngredientRepo;
using CoRicetta.Data.Repositories.MenuDetailRepo;
using CoRicetta.Data.Repositories.MenuRepo;
using CoRicetta.Data.Repositories.RecipeDetailRepo;
using CoRicetta.Data.Repositories.RecipeRepo;
using CoRicetta.Data.Repositories.ReportRepo;
using CoRicetta.Data.Repositories.StepRepo;
using CoRicetta.Data.Repositories.UserRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoRicetta.API
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
            services.AddDbContext<CoRicettaDBContext>(options =>
                             options.UseSqlServer(Configuration.GetConnectionString("CoRicettaDB")));
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            //Add Scoped For Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IStepService, StepService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            //Add Transient For Repo
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IActionRepo, ActionRepo>();
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            services.AddTransient<IIngredientRepo, IngredientRepo>();
            services.AddTransient<IMenuDetailRepo, MenuDetailRepo>();
            services.AddTransient<IMenuRepo, MenuRepo>();
            services.AddTransient<IRecipeRepo, RecipeRepo>();
            services.AddTransient<IStepRepo, StepRepo>();
            services.AddTransient<IRecipeDetailRepo, RecipeDetailRepo>();
            services.AddTransient<ICategoryDetailRepo, CategoryDetailRepo>();
            services.AddTransient<IReportRepo, ReportRepo>();

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder
                        .SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders(new string[] { "Authorization", "authorization" });
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoRicetta.API", Version = "v1" });
                c.EnableAnnotations();
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                    "\n\nEnter 'Bearer' [space] and then your token in the text input below. " +
                                      "\n\nExample: 'Bearer 12345abcde'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        securityScheme,
                        new string[]{ }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoRicetta.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
