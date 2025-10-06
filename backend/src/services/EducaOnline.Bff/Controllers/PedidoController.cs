using EducaOnline.Bff.Models;
using EducaOnline.Bff.Services;
using EducaOnline.WebAPI.Core.Controllers;
using EducaOnline.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducaOnline.Bff.Controllers
{
    [Authorize]
    public class PedidoController : MainController
    {
        private readonly IPedidoService _pedidoService;
        private readonly IAlunoService _alunoService;
        private readonly IConteudoService _conteudoService;
        private readonly IAspNetUser _user;

        public PedidoController(IPedidoService pedidoService, IAlunoService alunoService, IConteudoService conteudoService, IAspNetUser user)
        {
            _pedidoService = pedidoService;
            _alunoService = alunoService;
            _conteudoService = conteudoService;
            _user = user;
        }


        [HttpPost]
        [Route("compras/pedido")]
        public async Task<IActionResult> AdicionarPedido(PedidoDTO pedido)
        {   
            var matricula = await _alunoService.ObterMatricula(_user.ObterUserId());
            if (matricula is null)
            {
                AdicionarErro("Aluno não possui matrícula ativa");
                return CustomResponse();
            }
            var curso = await _conteudoService.BuscarCurso(matricula.CursoId);

            if (curso is null)
            {
                AdicionarErro($"Curso inexistente  com id {matricula.CursoId}");
                return CustomResponse();
            }
            
            PopularDadosPedido(matricula, curso , pedido);

            var result = await _pedidoService.IniciarPedido(pedido);

            return CustomResponse(result);
        }

        private void PopularDadosPedido(MatriculaDto matricula, CursoDto curso, PedidoDTO pedido)
        {
            pedido.PedidoItems = new List<ItemDTO>()
            {
                new ItemDTO()
                {
                    ProdutoId = matricula.CursoId,
                    Nome = curso.Nome,
                    Valor = curso.Valor                    
                }
            };
            pedido.ValorTotal = curso.Valor;                        
        }
    }
}
