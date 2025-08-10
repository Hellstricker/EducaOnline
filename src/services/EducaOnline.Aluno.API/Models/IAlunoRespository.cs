using EducaOnline.Core.Data;

namespace EducaOnline.Aluno.API.Models
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task AdicionarAluno(Aluno aluno);
        Task AtualizarAluno(Aluno aluno);
        IQueryable<Aluno?> BuscarAlunos();
        Task<Aluno?> BuscarAlunoPorRa(int ra);
        Task<Aluno?> BuscarAlunoPorId(Guid id);
        Task<int> BuscarUltimoRa();
    }
}
