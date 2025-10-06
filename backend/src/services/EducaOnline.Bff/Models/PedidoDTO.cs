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

        [Required(ErrorMessage = "Informe o número do cartão")]
        [DisplayName("Número do Cartão")]
        public string? NumeroCartao { get; set; }

        [Required(ErrorMessage = "Informe o nome do portador do cartão")]
        [DisplayName("Nome do Portador")]
        public string? NomeCartao { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "O vencimento deve estar no padrão MM/AA")]
        [CartaoExpiracao(ErrorMessage = "Cartão Expirado")]
        [Required(ErrorMessage = "Informe o vencimento")]
        [DisplayName("Data de Vencimento MM/AA")]
        public string? ExpiracaoCartao { get; set; }

        [Required(ErrorMessage = "Informe o código de segurança")]
        [DisplayName("Código de Segurança")]
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
    }

    public class CursoDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
