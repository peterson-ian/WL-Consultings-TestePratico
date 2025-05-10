using WL_Consultings_TestePratico.Repositories.Implementations;
using WL_Consultings_TestePratico.Repositories.Interfaces;
using WL_Consultings_TestePratico.Services.Implementations;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositórios
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();

            // Serviços
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISenhaService, SenhaService>();
            services.AddScoped<ICarteiraService, CarteiraService>();
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();

            return services;
        }
    }

}
