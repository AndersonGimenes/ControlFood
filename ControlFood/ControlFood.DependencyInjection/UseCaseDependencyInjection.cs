using ControlFood.Api.Helpers.Implementation;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Repository;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace ControlFood.DependencyInjection
{
    public static class UseCaseDependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
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
        }

    }
}
