using EducaOnline.Aluno.API.Data;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Core.Enums;
using EducaOnline.WebAPI.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace EducaOnline.Aluno.API.Configuration
{
    public class DbMigrationHelper
    {
        public static async Task EnsureSeedData(WebApplication app, CancellationToken cancellationToken)
        {
            var services = app.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services, cancellationToken);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var alunoContext = scope.ServiceProvider.GetRequiredService<AlunoDbContext>();

            await DbHealthChecker.TestConnection(alunoContext);

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await alunoContext.Database.MigrateAsync(cancellationToken);

                var alunoId = Guid.Parse("40640fec-5daf-4956-b1c0-2fde87717b66");
                var alunoDb = await alunoContext.Alunos.FirstOrDefaultAsync(x => x.Id == alunoId, cancellationToken);

                if (alunoDb is null)
                {
                    var aluno = new Models.Aluno(alunoId, "Jairo Bionez", "aluno@educaonline.com.br");

                    await alunoContext.Alunos.AddAsync(aluno, cancellationToken);
                    await alunoContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
