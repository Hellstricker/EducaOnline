using EducaOnline.Conteudo.API.Models;
using EducaOnline.Conteudo.API.Models.ValueObjects;
using EducaOnline.Conteudo.API.Services;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Enums;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using EducaOnline.WebAPI.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducaOnline.Conteudo.API.Controllers
{
    [Authorize]
    [Route("api/curso")]
    public class ConteudoController : MainController
    {
        private readonly IConteudoService _conteudoService;

        //public ConteudoController(
        //    INotificationHandler<DomainNotification> notifications,
        //    IMediatorHandler mediatorHandler,
        //    IConteudoService conteudoService) : base(notifications, mediatorHandler)
        //{
        //    _conteudoService = conteudoService;
        //}

        ///// <summary>
        ///// Lista os cursos
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[ProducesResponseType(typeof(List<Curso>), 200)]
        ////[ProducesResponseType(typeof(InternalServerErrorModel), 500)]
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok(await _conteudoService.BuscarCursos());
        //}

        ///// <summary>
        ///// Busca um curso especifico por id
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[ProducesResponseType(typeof(Curso), 200)]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    return Ok(await _conteudoService.BuscarCurso(id));
        //}

        ///// <summary>
        ///// Adicionar um novo curso
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[Authorize(Roles = nameof(PerfilUsuarioEnum.ADM))]
        //[HttpPost]
        //public async Task<IActionResult> AdicionarCurso(Curso model)
        //{
        //    await _conteudoService.AdicionarCurso(model);
        //    return Ok();
        //}

        ///// <summary>
        ///// Altera o nome do curso
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[Authorize(Roles = nameof(PerfilUsuarioEnum.ADM))]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> AlterarNomeCurso(Guid id, Curso model)
        //{
        //    await _conteudoService.AlterarNomeCurso(id, model?.Nome);
        //    return Ok();
        //}

        ///// <summary>
        ///// Alterar o conteudo programtico do curso
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[Authorize(Roles = nameof(PerfilUsuarioEnum.ADM))]
        //[HttpPut("{id}/conteudo-programatico")]
        //public async Task<IActionResult> AlterarNomeCurso(Guid id, ConteudoProgramatico model)
        //{
        //    await _conteudoService.AlterarConteudoProgramaticoCurso(id, model);
        //    return Ok();
        //}

        ///// <summary>
        ///// Desatia um curso
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[Authorize(Roles = nameof(PerfilUsuarioEnum.ADM))]
        //[HttpPut("{id}/desativar")]
        //public async Task<IActionResult> DesativarCurso(Guid id)
        //{
        //    await _conteudoService.DesativarCurso(id);
        //    return Ok();
        //}

        ///// <summary>
        ///// Adiciona uma aula ao curso informado
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[Authorize(Roles = nameof(PerfilUsuarioEnum.ADM))]
        //[HttpPost("{id}/aula")]
        //[ProducesResponseType(typeof(List<Curso>), 200)]
        //public async Task<IActionResult> AdicionarAula(Guid id, Aula aula)
        //{
        //    return Ok(await _conteudoService.AdicionarAula(id, aula));
        //}

        ///// <summary>
        ///// Altera uma aula do curso informado
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[Authorize(Roles = nameof(PerfilUsuarioEnum.ADM))]
        //[HttpPut("{id}/aula/{aulaId}")]
        //[ProducesResponseType(typeof(List<Curso>), 200)]
        //public async Task<IActionResult> AlterarAula(Guid id, Guid aulaId, Aula aula)
        //{
        //    return Ok(await _conteudoService.AlterarAula(id, aulaId, aula));
        //}

        ///// <summary>
        ///// Remove uma aula do curso informado
        ///// </summary>
        ///// <response code="401">Token não autorizado. Favor contate o suporte</response>
        //[Authorize(Roles = nameof(PerfilUsuarioEnum.ADM))]
        //[HttpDelete("{id}/aula/{aulaId}")]
        //[ProducesResponseType(typeof(List<Curso>), 200)]
        //public async Task<IActionResult> RemoverAula(Guid id, Guid aulaId)
        //{
        //    return Ok(await _conteudoService.RemoverAula(id, aulaId));
        //}
    }
}
