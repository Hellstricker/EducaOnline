using EducaOnline.Aluno.API.Models;
using EducaOnline.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Aluno.API.Data.Repository
{

    public class AlunoRepository : IAlunoRepository
    {
        private readonly AlunoDbContext _context;

        public AlunoRepository(AlunoDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AdicionarAluno(Models.Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.Commit(); 
        }

        public async Task AtualizarAluno(Models.Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            await _context.Commit();  
        }

        public async Task<int> BuscarUltimoRa()
        {
            return await _context.Alunos.MaxAsync(p => p.Ra);
        }

        public IQueryable<Models.Aluno?> BuscarAlunos()
        {
            return _context.Alunos
                .Include(p => p.Matricula)
                .Include(p => p.AulasConcluidas)
                .Include(p => p.Certificado);
        }

        public async Task<Models.Aluno?> BuscarAlunoPorId(Guid id)
        {
            return await _context.Alunos
                .Include(p => p.Matricula)
                .Include(p => p.AulasConcluidas)
                .Include(p => p.Certificado)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Models.Aluno?> BuscarAlunoPorRa(int ra)
        {
            return await _context.Alunos
                .Include(p => p.Matricula)
                .Include(p => p.AulasConcluidas)
                .Include(p => p.Certificado)
                .FirstOrDefaultAsync(p => p.Ra == ra);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}

