using EasyNetQ;
using EducaOnline.Aluno.API.Application.Commands;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Aluno.API.Models.Enum;
using EducaOnline.Core.Messages;
using EducaOnline.Core.Messages.Integration;
using EducaOnline.MessageBus;
using FluentValidation.Results;
using MediatR;

namespace EducaOnline.Aluno.API.Application.CommandHandlers
{
    public class PagarMatriculaCommandHandler : CommandHandler,
        IRequestHandler<PagarMatriculaCommand, ValidationResult>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMessageBus _bus;

        public PagarMatriculaCommandHandler(IAlunoRepository alunoRepository, IMessageBus bus)
        {
            _alunoRepository = alunoRepository;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(PagarMatriculaCommand command, CancellationToken cancellationToken)
        {
            if (!command.EhValido()) return command.ValidationResult!;

            var aluno = await _alunoRepository.BuscarAlunoPorId(command.AlunoId, cancellationToken);
            if (aluno is null)
            {
                AdicionarErro("Aluno não encontrado.");
                return ValidationResult;
            }

            var matricula = aluno.ObterMatricula(command.CursoId);

            if (matricula is null)
            {
                AdicionarErro($"Aluno não está matriculado no curso com id {command.CursoId}.");
                return ValidationResult;
            }

            // Processar pagamento
            //if (!await ProcessarPagamento(command)) return ValidationResult;

            aluno.PagarMatricula(matricula.CursoId);            
            _alunoRepository.AtualizarMatricula(matricula);
            return await PersistirDados(_alunoRepository.UnitOfWork);
        }


        //public async Task<bool> ProcessarPagamento(PagarMatriculaCommand command)
        //{
        //    var @event = new AlunoPagouMatriculaIntegrationEvent(command.AlunoId, command.CursoId, command.ValorCurso, command.NomeCartao, command.NumeroCartao, command.ExpiracaoCartao, command.CvvCartao);

        //    var result = await _bus
        //        .RequestAsync<AlunoPagouMatriculaIntegrationEvent, ResponseMessage>(@event);

        //    if (result.ValidationResult.IsValid) return true;

        //    foreach (var erro in result.ValidationResult.Errors)
        //    {
        //        AdicionarErro(erro.ErrorMessage);
        //    }

        //    return false;
        //}
    }
}
