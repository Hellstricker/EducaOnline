﻿using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EducaOnline.WebAPI.Core.Controllers
{
    //[ApiController]
    //public abstract class MainController : Controller
    //{
    //    private readonly DomainNotificationHandler _notifications;
    //    protected readonly IMediatorHandler _mediatorHandler;

    //    protected MainController(INotificationHandler<DomainNotification> notifications,
    //                             IMediatorHandler mediatorHandler)
    //    {
    //        _notifications = (DomainNotificationHandler)notifications;
    //        _mediatorHandler = mediatorHandler;
    //    }

    //    protected bool OperacaoValida()
    //    {
    //        return !_notifications.TemNotificacao();
    //    }

    //    protected IEnumerable<string> ObterMensagensErro()
    //    {
    //        return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
    //    }

    //    protected void NotificarErro(string codigo, string mensagem)
    //    {
    //        _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    //    }
    //}


    [ApiController]
    public abstract class MainController : Controller
    {
        private readonly ICollection<string> Erros = [];
        protected ActionResult CustomResponse(object? result = null)
        {
            if (OperacaoValida())
                return Ok(result);

            return BadRequest(
                new ValidationProblemDetails(new Dictionary<string, string[]> { { "Messages", Erros.ToArray() } })
            );
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelStateDictionary)
        {
            foreach (var erro in modelStateDictionary.Values.SelectMany(v => v.Errors))
                Erros.Add(erro.ErrorMessage);
            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
                Erros.Add(erro.ErrorMessage);
            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return Erros.Count == 0;
        }

        protected void AdicionarErro(string erro)
        {
            Erros.Add(erro);
        }
    }
}
