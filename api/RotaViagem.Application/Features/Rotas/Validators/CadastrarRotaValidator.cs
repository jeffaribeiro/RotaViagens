using FluentValidation;
using RotaViagem.Application.Features.Rotas.Commands;

namespace RotaViagem.Application.Features.Rotas.Validators
{
    public class CadastrarRotaValidator : AbstractValidator<CadastrarRotaCommand>
    {
        public CadastrarRotaValidator()
        {
            RuleFor(command => command.Rotas)
                .NotEmpty()
                .WithMessage("Rotas não pode estar sem valor preenchido.");

            RuleForEach(command => command.Rotas)
                .NotEmpty()
                .WithMessage("Rotas possui valor não preenchido na linha {CollectionIndex}.");

            RuleForEach(command => command.Rotas)
                .SetValidator(new RotaDtoValidator())
                .When(command => command.Rotas is not null)
                .WithMessage("Erro na validação da linha {CollectionIndex}.");
        }
    }
}
