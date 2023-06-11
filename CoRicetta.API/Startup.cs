using CoRicetta.Business.Services.ActionService;
using CoRicetta.Business.Services.CategoryService;
using CoRicetta.Business.Services.IngredientService;
using CoRicetta.Business.Services.MenuService;
using CoRicetta.Business.Services.RecipeService;
using CoRicetta.Business.Services.StepService;
using CoRicetta.Business.Services.UserService;
using CoRicetta.Data.Context;
using CoRicetta.Data.Repositories.ActionRepo;
using CoRicetta.Data.Repositories.CategoryRepo;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.Repositories.IngredientRepo;
using CoRicetta.Data.Repositories.MenuRepo;
using CoRicetta.Data.Repositories.RecipeRepo;
using CoRicetta.Data.Repositories.StepRepo;
using CoRicetta.Data.Repositories.UserRepo;
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
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            services.AddControllers();

            //Add Scoped For Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IStepService, StepService>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            //Add Transient For Repo
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IActionRepo, ActionRepo>();
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            services.AddTransient<IIngredientRepo, IngredientRepo>();
            services.AddTransient<IMenuRepo, MenuRepo>();
            services.AddTransient<IRecipeRepo, RecipeRepo>();
            services.AddTransient<IStepRepo, StepRepo>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoRicetta.API", Version = "v1" });
                c.EnableAnnotations();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
