
using FluentValidation;
using FluentValidation.Results;

namespace LojaDoSeuManoel.Core.Base.Service
{
    public class ServiceBase
    {
        private readonly INotifier _notifier;
        public ServiceBase(INotifier notifier)
        {
            _notifier = notifier;
        }
        protected void Notifier(ValidationResult validationResult)
            => validationResult.Errors.ForEach(erro => Notifier(erro.ErrorMessage));

        protected void Notifier(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool ExecuteVatidation<TValidation, TEntity>(TValidation validation, TEntity entity)
            where TValidation : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notifier(validator);

            return false;
        }
    }
}
