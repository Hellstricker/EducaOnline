﻿using EducaOnline.Aluno.API.Models.Enum;
using EducaOnline.Core.DomainObjects;

namespace EducaOnline.Aluno.API.Models
{
    public class Matricula : Entity
    {
        protected Matricula()
        {
            DataMatricula = DateTime.Now;
        }

        public Matricula(Guid cursoId)
        {
            CursoId = cursoId;
            DataMatricula = DateTime.UtcNow;
            Status = StatusMatriculaEnum.PENDENTE_PAGAMENTO;
        }

        public Guid Id { get; private set; }
        public Guid CursoId { get; private set; }
        public DateTime DataMatricula { get; private set; }
        public StatusMatriculaEnum Status { get; private set; }

        public Guid AlunoId { get; private set; }
        public Aluno Aluno { get; private set; }

        public void AtualizarStatus(StatusMatriculaEnum status) => Status = status;
    }
}
