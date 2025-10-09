using EducaOnline.Aluno.API.Application.Events;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Aluno.API.Models.Enum;
using EducaOnline.Core.Communication;
using MediatR;

namespace EducaOnline.Aluno.API.Application.EventHandlers
{
    public class CursoFinalizadoEventHandler : INotificationHandler<CursoFinalizadoEvent>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CursoFinalizadoEventHandler(IAlunoRepository alunoRepository, IMediatorHandler mediatorHandler)
        {
            _alunoRepository = alunoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(CursoFinalizadoEvent notification, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.BuscarAlunoPorId(notification.AlunoId, cancellationToken);
            if (aluno == null) return;

            var matricula = aluno.Matricula;
            if (matricula == null || matricula.Id != notification.MatriculaId) return;

            aluno.AtualizarStatusMatricula(StatusMatriculaEnum.CURSO_CONCLUIDO);

            var certificado = new Certificado(matricula.CursoNome);
            aluno.EmitirCertificado(certificado);

            _alunoRepository.AtualizarAluno(aluno);
            await _alunoRepository.UnitOfWork.Commit();

            await _mediatorHandler.PublicarEvento(
                new CertificadoEmitidoEvent(aluno.Id, matricula.CursoId, certificado.Id)
            );
        }
    }

}
