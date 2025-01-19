using FluentValidation;
using RotaViagem.Application.Features.Viagens.Commands;

namespace RotaViagem.Application.Features.Viagens.Validators
{
    public class CalcularRotaViagemComMenorPrecoValidator : AbstractValidator<CalcularRotaViagemComMenorPrecoCommand>
    {
        public CalcularRotaViagemComMenorPrecoValidator()
        {
            RuleFor(command => command.Origem)
                .NotEmpty()
                .WithMessage("Origem deve ser informada.");

            RuleFor(command => command.Origem)
                .Must(x => x.Length == 3)
                .When(command => !string.IsNullOrWhiteSpace(command.Origem))
                .WithMessage("Origem deve ter exatamente 3 caracteres.");

            RuleFor(command => command.Destino)
                .NotEmpty()
                .WithMessage("Destino deve ser informado.");

            RuleFor(command => command.Destino)
                .Must(x => x.Length == 3)
                .When(command => !string.IsNullOrWhiteSpace(command.Destino))
                .WithMessage("Destino deve ter exatamente 3 caracteres.");
        }
    }
}
