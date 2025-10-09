using EducaOnline.Aluno.API.Application.Events;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Core.Communication;
using MediatR;

namespace EducaOnline.Aluno.API.Application.EventHandlers
{
    public class AulaFinalizadaEventHandler : INotificationHandler<AulaFinalizadaEvent>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public AulaFinalizadaEventHandler(IAlunoRepository alunoRepository, IMediatorHandler mediatorHandler)
        {
            _alunoRepository = alunoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(AulaFinalizadaEvent notification, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.BuscarAlunoPorId(notification.AlunoId, cancellationToken);
            if (aluno == null) return;

            var matricula = aluno.Matricula;
            if (matricula == null || matricula.Id != notification.MatriculaId) return;

            if (matricula.AulasConcluidas >= matricula.TotalAulas)
            {
                await _mediatorHandler.PublicarEvento(
                    new CursoFinalizadoEvent(aluno.Id, matricula.Id)
                );
            }
        }
    }
}
