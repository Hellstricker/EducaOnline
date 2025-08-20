using EducaOnline.Aluno.API.Data;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Core.Enums;
using EducaOnline.WebAPI.Core.Configuration;
using Microsoft.AspNetCore.Identity;

namespace EducaOnline.Aluno.API.Configuration
{
    public class DbMigrationHelper
    {
        public static async Task EnsureSeedData(WebApplication app)
        {
            var services = app.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var alunoRepository = scope.ServiceProvider.GetRequiredService<IAlunoRepository>();

            var alunoContext = scope.ServiceProvider.GetRequiredService<AlunoDbContext>();

            await DbHealthChecker.TestConnection(alunoContext);

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await alunoContext.Database.EnsureCreatedAsync();


                var alunoDb = await alunoRepository.BuscarAlunoPorId(Guid.Parse("40640fec-5daf-4956-b1c0-2fde87717b66"));

                if (alunoDb == null)
                {
                    var aluno = new Models.Aluno(Guid.Parse("40640fec-5daf-4956-b1c0-2fde87717b66"), "Jairo Bionez", "aluno@educaonline.com.br");

                    alunoContext.Set<Models.Aluno>().Add(aluno);
                    alunoContext.SaveChanges();
                }
            }
        }
    }
}
