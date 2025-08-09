using EducaOnline.Aluno.API.Models;
using EducaOnline.Core.Data;
using EducaOnline.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Aluno.API.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<Models.Aluno> CriarAluno(string nome, string email)
        {
            var aluno = new Models.Aluno(Guid.NewGuid(), nome, email);
            await _alunoRepository.AdicionarAluno(aluno);
            return aluno;
        }

        public async Task<Models.Aluno> ObterAlunoPorId(Guid id)
        {
            var aluno = await _alunoRepository.BuscarAlunoPorId(id);
            if (aluno == null)
                throw new DomainException("Aluno não encontrado");

            return aluno;
        }
        public async Task<Models.Aluno> ObterAlunoPorRa(int ra)
        {
            var aluno = await _alunoRepository.BuscarAlunoPorRa(ra);
            if (aluno == null)
                throw new DomainException("Aluno não encontrado");

            return aluno;
        }

        public async Task AtualizarAluno(Models.Aluno aluno)
        {
            await _alunoRepository.AtualizarAluno(aluno);
        }

        public async Task RealizarMatricula(Guid alunoId, Guid cursoId, int totalAulas)
        {
            var aluno = await ObterAlunoPorId(alunoId);
            aluno.RealizarMatricula(cursoId, totalAulas);
            await _alunoRepository.AtualizarAluno(aluno);
        }

        public async Task ConcluirAula(Guid alunoId, Guid aulaId, int horas)
        {
            var aluno = await ObterAlunoPorId(alunoId);
            aluno.ConcluirAula(aulaId, horas);
            await _alunoRepository.AtualizarAluno(aluno);
        }

        public async Task EmitirCertificado(Guid alunoId, Certificado certificado)
        {
            var aluno = await ObterAlunoPorId(alunoId);
            aluno.EmitirCertificado(certificado);
            await _alunoRepository.AtualizarAluno(aluno);
        }
    }

}
