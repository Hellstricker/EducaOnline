namespace EducaOnline.Conteudo.API.Dtos
{
    public class AdicionarCursoDto
    {
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int CargaHoraria { get; set; }
        public string Objetivos { get; set; } = string.Empty;
    }
}
