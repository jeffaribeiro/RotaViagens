using FluentValidation;
using RotaViagem.Application.Features.Rotas.Dtos;

namespace RotaViagem.Application.Features.Rotas.Validators
{
    public class RotaDtoValidator : AbstractValidator<RotaDto>
    {
        public RotaDtoValidator()
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

            RuleFor(command => command.Valor)
                .GreaterThan(0)
                .WithMessage("Valor deve ser maior que 0.");
        }
    }
}
