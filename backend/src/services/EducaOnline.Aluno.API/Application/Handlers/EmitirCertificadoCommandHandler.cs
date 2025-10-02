using EducaOnline.Aluno.API.Application.Commands;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace EducaOnline.Aluno.API.Application.Handlers
{
    public class EmitirCertificadoCommandHandler : CommandHandler,
        IRequestHandler<EmitirCertificadoCommand, ValidationResult>
    {
        private readonly IAlunoRepository _alunoRepository;

        public EmitirCertificadoCommandHandler(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<ValidationResult> Handle(EmitirCertificadoCommand command, CancellationToken cancellationToken)
        {
            if (!command.EhValido()) return command.ValidationResult;

            var aluno = await _alunoRepository.BuscarAlunoPorId(command.AlunoId, cancellationToken);
            if (aluno == null)
            {
                AdicionarErro("Aluno não encontrado.");
                return ValidationResult;
            }

            if (aluno.Matricula == null)
            {
                AdicionarErro("Aluno não possui matrícula.");
                return ValidationResult;
            }

            if (!aluno.Matricula.PodeEmitirCertificado())
            {
                AdicionarErro("Progresso insuficiente para emitir certificado.");
                return ValidationResult;
            }

            aluno.EmitirCertificado(new Certificado(aluno.Matricula.CursoNome));

            _alunoRepository.AtualizarAluno(aluno);
            return await PersistirDados(_alunoRepository.UnitOfWork);
        }
    }
}
