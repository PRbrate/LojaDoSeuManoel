
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LojaDoSeuManoel.Core.Base.Controller
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private readonly INotifier _notifier;
        public readonly IUser _appUser;
        protected long ContaId { get; set; }
        protected string UserId { get; set; }
        protected bool AuthenticatedUser { get; set; }
        public ApiControllerBase(INotifier notifier,
                              IUser appUser)
        {
            _notifier = notifier;
            _appUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UserId = appUser.GetUserId();
                //ContaId = appUser.GetAccountId();
                AuthenticatedUser = true;
            }
            //_auditoria = auditoria;
        }
        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(new
                {
                    Success = true,
                    Data = result
                });

            return BadRequest(new
            {
                Success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!ModelState.IsValid) NotifyErrorModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotifyErrorModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}
