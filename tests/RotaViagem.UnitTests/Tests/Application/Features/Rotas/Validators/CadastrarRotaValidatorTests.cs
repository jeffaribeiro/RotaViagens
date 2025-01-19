using FluentValidation.TestHelper;
using RotaViagem.Application.Features.Rotas.Commands;
using RotaViagem.Application.Features.Rotas.Dtos;
using RotaViagem.Application.Features.Rotas.Validators;

namespace RotaViagem.UnitTests.Tests.Application.Features.Rotas.Validators
{
    public class CadastrarRotaValidatorTests
    {
        private readonly CadastrarRotaValidator _validator;

        public CadastrarRotaValidatorTests()
        {
            _validator = new CadastrarRotaValidator();
        }

        [Fact]
        public void Validator_DeveFalhar_QuandoRotasEstiverVazio()
        {
            // Arrange
            var command = new CadastrarRotaCommand { Rotas = null };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Rotas)
                  .WithErrorMessage("Rotas não pode estar sem valor preenchido.");
        }

        [Fact]
        public void Validator_DeveFalhar_QuandoRotasContiverElementoVazio()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto> {
                new RotaDto()
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Rotas.ToList()[0].Origem)
                  .WithErrorMessage("Rotas possui valor não preenchido na linha 0.");
        }

        [Fact]
        public void Validator_DevePassar_QuandoRotasForValida()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto>
            {
                new RotaDto { Origem = "RIO", Destino = "SFO", Valor = 100 }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(command => command.Rotas);
        }

        [Fact]
        public void Validator_DeveFalhar_QuandoRotaForInvalida()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto>
            {
                new RotaDto { Origem = "RIO", Destino = "", Valor = 100 }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Rotas.ToList()[0].Destino)
                  .WithErrorMessage("Erro na validação da linha 0.");
        }
    }
}
