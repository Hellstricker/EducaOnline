using EducaOnline.Aluno.API.Application.Commands;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Aluno.API.Models.Enum;
using EducaOnline.Aluno.API.Models.ValueObjects;
using EducaOnline.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace EducaOnline.Aluno.API.Application.Handlers
{
    public class FinalizarCursoCommandHandler : CommandHandler,
        IRequestHandler<FinalizarCursoCommand, ValidationResult>
    {
        private readonly IAlunoRepository _alunoRepository;

        public FinalizarCursoCommandHandler(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<ValidationResult> Handle(FinalizarCursoCommand command, CancellationToken cancellationToken)
        {
            if (!command.EhValido()) return command.ValidationResult;

            var aluno = await _alunoRepository.BuscarAlunoPorId(command.AlunoId, cancellationToken);
            if (aluno == null)
            {
                AdicionarErro("Aluno não encontrado.");
                return ValidationResult;
            }

            var matricula = aluno.Matricula;
            if (matricula == null || matricula.Id != command.MatriculaId)
            {
                AdicionarErro("Matrícula não encontrada para o aluno informado.");
                return ValidationResult;
            }

            if (matricula.AulasConcluidas < matricula.TotalAulas)
            {
                AdicionarErro("O aluno ainda não concluiu todas as aulas do curso.");
                return ValidationResult;
            }

            try
            {
                aluno.AtualizarStatusMatricula(StatusMatriculaEnum.CURSO_CONCLUIDO);

                var certificado = new Certificado(command.CursoNome);
                aluno.EmitirCertificado(certificado);

                aluno.HistoricoAprendizado.AtualizarProgresso(
                     matricula.AulasConcluidas,
                     matricula.TotalAulas
                 );

                _alunoRepository.AtualizarAluno(aluno);
            }
            catch (Exception ex)
            {
                AdicionarErro(ex.Message);
                return ValidationResult;
            }

            return await PersistirDados(_alunoRepository.UnitOfWork);
        }
    }
}
