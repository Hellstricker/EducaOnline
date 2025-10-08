using EducaOnline.Aluno.API.Models.Enum;
using EducaOnline.Aluno.API.Models.ValueObjects;
using EducaOnline.Core.DomainObjects;

namespace EducaOnline.Aluno.API.Models
{
    public class Aluno : Entity, IAggregateRoot
    {
        protected Aluno()
        {
            DataCadastro = DateTime.UtcNow;
            AulasConcluidas = new HashSet<AulaConcluida>();
        }

        public Aluno(Guid id, string? nome, string? email)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataCadastro = DateTime.UtcNow;
            AulasConcluidas = new HashSet<AulaConcluida>();
        }

        public string Nome { get; private set; }
        public int Ra { get; private set; }
        public string? Email { get; private set; }
        public DateTime? DataCadastro { get; private set; }
        public HistoricoAprendizado? HistoricoAprendizado { get; private set; }
        public ICollection<AulaConcluida> AulasConcluidas { get; private set; }
        public Matricula? Matricula { get; private set; }
        public Certificado? Certificado { get; private set; }


        public void AtualizarDados(string? nome, string? email)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome inválido.");
            if (string.IsNullOrWhiteSpace(email)) throw new DomainException("E-mail inválido.");
            Nome = nome;
            Email = email;
        }

        public void VincularRa(int ra) => Ra = ra;

        public void RealizarMatricula(Matricula nova)
        {
            if (nova is null) throw new DomainException("Matrícula inválida.");
            if (Matricula is not null && Matricula.Status != StatusMatriculaEnum.CANCELADO)
                throw new DomainException("Aluno já possui matrícula ativa.");

            if (nova.AlunoId != Id)
                throw new DomainException("Matrícula não pertence a este aluno.");

            Matricula = nova;

            HistoricoAprendizado = new HistoricoAprendizado(
                totalAulasConcluidas: Matricula.AulasConcluidas,
                totalAulas: Matricula.TotalAulas
            );
        }
        public void ConcluirAula(Guid aulaId)
        {
            if (Matricula is null) throw new DomainException("Aluno não possui matrícula.");
            if (Matricula.Status == StatusMatriculaEnum.PENDENTE_PAGAMENTO)
                throw new DomainException("Não é possível concluir aula sem pagamento.");
            if (Matricula.Status == StatusMatriculaEnum.CURSO_CONCLUIDO)
                throw new DomainException("Curso já concluído.");

            if (AulasConcluidas.Any(a => a.AulaId == aulaId))
                throw new DomainException("Aula já concluída.");

            AulasConcluidas.Add(new AulaConcluida(aulaId));

            Matricula.RegistrarConclusaoAula();

            HistoricoAprendizado = HistoricoAprendizado is null
                ? new HistoricoAprendizado(Matricula.AulasConcluidas, Matricula.TotalAulas)
                : HistoricoAprendizado.Atualizar(Matricula.AulasConcluidas, Matricula.TotalAulas);
        }
        public void AtualizarStatusMatricula(StatusMatriculaEnum status)
        {
            if (Matricula is null) throw new DomainException("Aluno não possui matrícula.");
            Matricula.AtualizarStatus(status);
        }


        public void EmitirCertificado(Certificado certificado)
        {
            if (Matricula is null) throw new DomainException("Aluno não possui matrícula.");
            var progresso = Matricula.ObterProgressoPercentual();

            if (progresso >= 100 && Matricula.AulasConcluidas >= Matricula.TotalAulas)
            {
                AtualizarStatusMatricula(StatusMatriculaEnum.CURSO_CONCLUIDO);
                Certificado = certificado;
            }
            else
            {
                throw new DomainException($"Curso não concluído. Progresso atual {progresso}%.");
            }
        }

        public void PagarMatricula(Guid cursoId)
        {
            if(Matricula is null) throw new DomainException("Aluno não possui matrícula.");
            if(Matricula.CursoId != cursoId) throw new DomainException("Aluno não matriculado neste curso.");

            Matricula?.Pagar();
        }

        public bool EstaMatriculado(Guid cursoId)
        {
            return Matricula is not null && Matricula.CursoId == cursoId;
        }

        public Matricula? ObterMatricula(Guid cursoId)
        {
            if (!EstaMatriculado(cursoId)) throw new DomainException($"Aluno não está matriculado no curso com id {cursoId}");
            return Matricula;
        }
    }
}
    
