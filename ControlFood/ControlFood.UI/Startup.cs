using AutoMapper;
using ControlFood.Repository;
using ControlFood.Repository.Context;
using ControlFood.Repository.Mapping;
using ControlFood.UI.Helpers.Implementation;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Mapping;
using ControlFood.UI.Validation;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControlFood.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Injeção de dependencia
            // Use Case
            services.AddTransient<ICadastroCategoriaUseCase, CadastroCategoriaUseCase>();
            services.AddTransient<ICadastroSubCategoriaUseCase, CadastroSubCategoriaUseCase>();
            // Repository
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ISubCategoriaRepository, SubCategoriaRepository>();
            // UI
            services.AddScoped<ICategoriaHelper, CategoriaHelper>();
            services.AddScoped<ISubcategoriaHelper, SubcategoriaHelper>();

            services.AddControllersWithViews();

            services.AddMemoryCache();

            // Configuração banco de dados
            services.AddDbContext<ControlFoodContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ControlFood")));

            // Auto Mapper configuração
            var cfg = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfileUI());
                x.AddProfile(new MappingProfileRepository());
            });

            var mapper = cfg.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoriaValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SubCategoriaValidation>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ControlFoodContext context)
        {
            // Atualiza/Cria a estrutura definada no migration ao subir o projeto
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Categoria}/{action=Cadastrar}/{id?}");
            });
        }
    }
}
