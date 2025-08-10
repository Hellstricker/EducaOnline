namespace EducaOnline.Aluno.API.DTO
{
    public class AlunoDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
    public class MatriculaDto
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public int TotalAulas { get; set; }
    }
    public class AulaDto
    {
        public Guid AlunoId { get; set; }
        public Guid AulaId { get; set; }
        public int Horas { get; set; }
    }
    public class CertificadoDto
    {
        public Guid AlunoId { get; set; }
        public string Curso { get; set; }
    }
}
