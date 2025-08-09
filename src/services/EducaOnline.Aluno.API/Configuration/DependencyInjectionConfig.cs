using EducaOnline.Aluno.API.Data.Repository;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Aluno.API.Services;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Data;

namespace EducaOnline.Aluno.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyConfig(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Conteudo
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();

        }
    }
}
