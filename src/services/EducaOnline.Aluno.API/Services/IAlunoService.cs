using EducaOnline.Aluno.API.Models;

namespace EducaOnline.Aluno.API.Services
{
    public interface IAlunoService
    {
        Task<Models.Aluno> CriarAluno(string nome, string email);
        Task<Models.Aluno> ObterAlunoPorId(Guid id);
        Task<Models.Aluno> ObterAlunoPorRa(int ra);
        Task AtualizarAluno(Models.Aluno aluno);
        Task RealizarMatricula(Guid alunoId, Guid cursoId, int totalAulas);
        Task ConcluirAula(Guid alunoId, Guid aulaId, int horas);
        Task EmitirCertificado(Guid alunoId, Certificado certificado);
    }

}
