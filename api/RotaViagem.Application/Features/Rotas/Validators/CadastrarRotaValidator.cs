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
                .Custom((rotaDto, context) =>
                {
                    if (rotaDto == null)
                        return;

                    var validator = new RotaDtoValidator();
                    var validationResult = validator.Validate(rotaDto);

                    if (!validationResult.IsValid)
                    {
                        context.AddFailure($"Erro na validação da linha {context.PropertyName}: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}");
                    }
                });
        }
    }
}
