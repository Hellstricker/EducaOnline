using EducaOnline.Aluno.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducaOnline.Aluno.API.Data.Mappings
{
    public class AlunoMapping : IEntityTypeConfiguration<Models.Aluno>
    {
        public void Configure(EntityTypeBuilder<Models.Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(p => p.HistoricoAprendizado)
                .Property(p => p.TotalAulasConcluidas);

            builder.OwnsOne(p => p.HistoricoAprendizado)
               .Property(p => p.Progresso);

            builder.OwnsOne(p => p.HistoricoAprendizado)
              .Property(p => p.TotalAulas);              

            builder.HasOne(p => p.Matricula);

            builder.HasOne(p => p.Certificado);

            builder.HasMany(p => p.AulasConcluidas)
                .WithOne(p => p.Aluno)
                .HasForeignKey(p => p.AlunoId);
        }
    }

    public class MatriculaMapping : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matriculas");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Status);
        }
    }

    public class CertificadoMapping : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.ToTable("Certificados");

            builder.HasKey(x => x.Id);
        }
    }

    public class AulaConcluidaMapping : IEntityTypeConfiguration<AulaConcluida>
    {
        public void Configure(EntityTypeBuilder<AulaConcluida> builder)
        {
            builder.ToTable("AulaConcluidas");

            builder.HasKey(x => x.Id);
        }
    }
}
