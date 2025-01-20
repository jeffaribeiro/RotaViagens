using FluentValidation.TestHelper;
using RotaViagem.Application.Features.Rotas.Dtos;
using RotaViagem.Application.Features.Rotas.Validators;

namespace RotaViagem.UnitTests.Tests.Application.Features.Rotas.Validators
{
    public class RotaDtoValidatorTests
    {
        private readonly RotaDtoValidator _validator;

        public RotaDtoValidatorTests()
        {
            _validator = new RotaDtoValidator();
        }

        [Fact(DisplayName = "Falha na validação quando origem não for informada")]
        [Trait("Rotas", "RotaDtoValidator")]
        public void Validator_DeveFalhar_QuandoOrigemNaoForInformada()
        {
            // Arrange
            var rotaDto = new RotaDto { Origem = "", Destino = "SFO", Valor = 100 };

            // Act
            var result = _validator.TestValidate(rotaDto);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Origem)
                  .WithErrorMessage("Origem deve ser informada.");
        }

        [Fact(DisplayName = "Falha na validação quando origem não tiver 3 caracteres")]
        [Trait("Rotas", "RotaDtoValidator")]
        public void Validator_DeveFalhar_QuandoOrigemNaoTiverTresCaracteres()
        {
            // Arrange
            var rotaDto = new RotaDto { Origem = "RI", Destino = "SFO", Valor = 100 };

            // Act
            var result = _validator.TestValidate(rotaDto);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Origem)
                  .WithErrorMessage("Origem deve ter exatamente 3 caracteres.");
        }

        [Fact(DisplayName = "Falha na validação quando destino não for informado")]
        [Trait("Rotas", "RotaDtoValidator")]
        public void Validator_DeveFalhar_QuandoDestinoNaoForInformado()
        {
            // Arrange
            var rotaDto = new RotaDto { Origem = "RIO", Destino = "", Valor = 100 };

            // Act
            var result = _validator.TestValidate(rotaDto);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Destino)
                  .WithErrorMessage("Destino deve ser informado.");
        }

        [Fact(DisplayName = "Falha na validação quando destino não tiver 3 caracteres")]
        [Trait("Rotas", "RotaDtoValidator")]
        public void Validator_DeveFalhar_QuandoDestinoNaoTiverTresCaracteres()
        {
            // Arrange
            var rotaDto = new RotaDto { Origem = "RIO", Destino = "SF", Valor = 100 };

            // Act
            var result = _validator.TestValidate(rotaDto);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Destino)
                  .WithErrorMessage("Destino deve ter exatamente 3 caracteres.");
        }

        [Fact(DisplayName = "Falha na validação quando valor for menor ou igual a zero")]
        [Trait("Rotas", "RotaDtoValidator")]
        public void Validator_DeveFalhar_QuandoValorForMenorOuIgualAZero()
        {
            // Arrange
            var rotaDto = new RotaDto { Origem = "RIO", Destino = "SFO", Valor = 0 };

            // Act
            var result = _validator.TestValidate(rotaDto);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Valor)
                  .WithErrorMessage("Valor deve ser maior que 0.");
        }

        [Fact(DisplayName = "Sucesso")]
        [Trait("Rotas", "RotaDtoValidator")]
        public void Validator_DevePassar_QuandoTodosOsCamposForemValidos()
        {
            // Arrange
            var rotaDto = new RotaDto { Origem = "RIO", Destino = "SFO", Valor = 150 };

            // Act
            var result = _validator.TestValidate(rotaDto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
