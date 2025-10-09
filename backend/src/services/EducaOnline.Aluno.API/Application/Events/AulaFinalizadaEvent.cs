using EducaOnline.Core.Messages;

namespace EducaOnline.Aluno.API.Application.Events
{
    public class AulaFinalizadaEvent : Event
    {
        public Guid AlunoId { get; }
        public Guid MatriculaId { get; }
        public Guid AulaId { get; }

        public AulaFinalizadaEvent(Guid alunoId, Guid matriculaId, Guid aulaId)
        {
            AlunoId = alunoId;
            MatriculaId = matriculaId;
            AulaId = aulaId;
        }
    }

}
