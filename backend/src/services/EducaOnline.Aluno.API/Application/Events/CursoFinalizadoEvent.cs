using EducaOnline.Core.Messages;

namespace EducaOnline.Aluno.API.Application.Events
{
    public class CursoFinalizadoEvent : Event
    {
        public Guid AlunoId { get; }
        public Guid MatriculaId { get; }

        public CursoFinalizadoEvent(Guid alunoId, Guid matriculaId)
        {
            AlunoId = alunoId;
            MatriculaId = matriculaId;
        }
    }

}
