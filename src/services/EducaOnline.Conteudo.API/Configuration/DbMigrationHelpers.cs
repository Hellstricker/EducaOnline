using EducaOnline.Conteudo.API.Data;
using EducaOnline.Conteudo.API.Models;
using EducaOnline.Conteudo.API.Models.ValueObjects;
using EducaOnline.WebAPI.Core.Configuration;

namespace EducaOnline.Conteudo.API.Configuration
{
    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var conteudoContext = scope.ServiceProvider.GetRequiredService<ConteudoContext>();
            await DbHealthChecker.TestConnection(conteudoContext);

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await conteudoContext.Database.EnsureCreatedAsync();

                var curso = conteudoContext.Cursos.FirstOrDefault(p => p.Id == Guid.Parse("04effc8b-fa4a-415c-90eb-95cdfdaba1b2"));

                if (curso == null)
                {
                    var aulas = new List<Aula>()
                    {
                        new Aula("Aula 01", "Descrição 01", 10),
                        new Aula("Aula 02", "Descrição 02", 10)
                    };

                    var conteudoProgramatico = new ConteudoProgramatico("Conteúdo inicial", "Conteúdo inicial alimento pelo seed", 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry.");
                    curso = new Curso(Guid.Parse("04effc8b-fa4a-415c-90eb-95cdfdaba1b2"), "Curso Inicial", conteudoProgramatico, true);

                    aulas.ForEach(aula => curso.AdicionarAula(aula));

                    conteudoContext.Cursos.Add(curso);
                    conteudoContext.SaveChanges();
                }
            }
        }
    }
}
