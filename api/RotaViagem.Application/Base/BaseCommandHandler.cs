using FluentValidation;
using FluentValidation.Results;
using RotaViagem.Application.Infra.Notification;

namespace RotaViagem.Application.Base
{
    public abstract class BaseCommandHandler
    {
        private readonly INotificador _notificador;

        protected BaseCommandHandler(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : class
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        protected bool TemNotificacao()
        {
            return _notificador.TemNotificacao();
        }

    }
}