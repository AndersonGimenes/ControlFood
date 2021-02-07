using AutoMapper;
using ControlFood.Api.Helpers.Implementation;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Mapping;
using ControlFood.Repository;
using ControlFood.Repository.Context;
using ControlFood.Repository.Mapping;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControlFood.Api
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
            services.AddControllers();

            services.AddTransient<ICadastroCategoriaUseCase, CadastroCategoriaUseCase>();
            services.AddTransient<ICadastroSubCategoriaUseCase, CadastroSubCategoriaUseCase>();
            services.AddTransient<ICadastroProdutoUseCase, CadastroProdutoUseCase>();
            services.AddTransient<ICadastroEstoqueUseCase, CadastroEstoqueUseCase>();
            services.AddTransient<ICadastroClienteUseCase, CadastroClienteUseCase>();

            // Repository
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ISubCategoriaRepository, SubCategoriaRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IEstoqueRepository, EstoqueRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();

            // Api
            services.AddScoped<ICategoriaHelper, CategoriaHelper>();
            services.AddScoped<ISubCategoriaHelper, SubcategoriaHelper>();
            services.AddScoped<IProdutoHelper, ProdutoHelper>();
            services.AddScoped<IClienteHelper, ClienteHelper>();

            services.AddMemoryCache();

            // Configuração banco de dados
            var conn = Configuration.GetConnectionString("ControlFood");
            
            services.AddDbContext<ControlFoodContext>(options =>
                options.UseNpgsql(conn));

            // Auto Mapper configuração
            var cfg = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfileApi());
                x.AddProfile(new MappingProfileRepository());
            });

            var mapper = cfg.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ControlFoodContext context)
        {
            // Atualiza/Cria a estrutura definada no migration ao subir o projeto
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
