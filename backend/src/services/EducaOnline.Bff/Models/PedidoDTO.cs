using EducaOnline.Core.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EducaOnline.Bff.Models
{
    public class PedidoDTO
    {
        #region Pedido        
        // Autorizado = 1,
        // Pago = 2,
        // Recusado = 3,
        // Entregue = 4,
        // Cancelado = 5
        public int Status { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotal { get; set; }

        public List<ItemDTO>? PedidoItems { get; set; }

        #endregion

        #region Cartão        
        public string? NumeroCartao { get; set; }        
        public string? NomeCartao { get; set; }        
        public string? ExpiracaoCartao { get; set; }
        public string? CvvCartao { get; set; }
        #endregion
    }

    public class ItemDTO
    {
        public Guid ProdutoId { get; set; }
        public string? Nome { get; set; }
        public decimal Valor { get; set; }
    }

    public class MatriculaDto
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }        
        public int Status { get; set; }        
        public string? CursoNome { get; set; }
        public int TotalAulas { get; set; }
        public int CargaHorariaTotal { get; set; }
    }

    public class CursoDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public decimal Valor { get; set; }
        public int TotalAulas { get; set; }
        public int CargaHorariaTotal { get; set; }
    }
}
