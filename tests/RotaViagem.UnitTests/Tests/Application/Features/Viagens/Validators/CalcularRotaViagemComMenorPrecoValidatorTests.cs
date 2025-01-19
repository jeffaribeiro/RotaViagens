using FluentValidation.TestHelper;
using RotaViagem.Application.Features.Viagens.Commands;
using RotaViagem.Application.Features.Viagens.Validators;

namespace RotaViagem.UnitTests.Tests.Application.Features.Viagens.Validators
{
    public class CalcularRotaViagemComMenorPrecoValidatorTests
    {
        private readonly CalcularRotaViagemComMenorPrecoValidator _validator;

        public CalcularRotaViagemComMenorPrecoValidatorTests()
        {
            _validator = new CalcularRotaViagemComMenorPrecoValidator();
        }

        [Fact]
        public void Validator_DeveFalhar_QuandoOrigemEstiverVazia()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "", Destino = "SFO" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Origem)
                  .WithErrorMessage("Origem deve ser informada.");
        }

        [Fact]
        public void Validator_DeveFalhar_QuandoOrigemNaoTiver3Caracteres()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "AB", Destino = "SFO" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Origem)
                  .WithErrorMessage("Origem deve ter exatamente 3 caracteres.");
        }

        [Fact]
        public void Validator_DevePassar_QuandoOrigemTiver3Caracteres()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "RIO", Destino = "SFO" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(command => command.Origem);
        }

        [Fact]
        public void Validator_DeveFalhar_QuandoDestinoEstiverVazio()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "RIO", Destino = "" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Destino)
                  .WithErrorMessage("Destino deve ser informado.");
        }

        [Fact]
        public void Validator_DeveFalhar_QuandoDestinoNaoTiver3Caracteres()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "RIO", Destino = "SF" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Destino)
                  .WithErrorMessage("Destino deve ter exatamente 3 caracteres.");
        }

        [Fact]
        public void Validator_DevePassar_QuandoDestinoTiver3Caracteres()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "RIO", Destino = "SFO" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(command => command.Destino);
        }
    }
}
