using AutoMapper;
using ControlFood.Domain.Entidades;
using ControlFood.Repository;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.Repository.Mapping;
using ControlFood.UI.Mapping;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Configuração banco de dados
            services.AddDbContext<ControlFoodContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ControlFood")));

            // Injeção de dependencia
            // Use Case
            services.AddTransient<ICadastroCategoriaUseCase, CadastroCategoriaUseCase>();
            // Repository
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();

            // Auto Mapper configuração
            var cfg = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfileUI());
                x.AddProfile(new MappingProfileRepository());
            });

            var mapper = cfg.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ControlFoodContext context)
        {
            // atualiza / cria a estrutura definada no migration ao subir o projeto
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
