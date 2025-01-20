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

        [Fact(DisplayName = "Falha na validação quando rotas estiver vazio")]
        [Trait("Rotas", "CadastrarRotaValidator")]
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

        [Fact(DisplayName = "Falha na validação quando rotas possuir um elemento vazio")]
        [Trait("Rotas", "CadastrarRotaValidator")]
        public void Validator_DeveFalhar_QuandoRotasContiverElementoVazio()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto> {
                null,
                new RotaDto { Origem = "RIO", Destino = "SFO", Valor = 100 }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Rotas)
                  .WithErrorMessage("Rotas possui valor não preenchido na linha 0.");
        }

        [Fact(DisplayName = "Sucesso")]
        [Trait("Rotas", "CadastrarRotaValidator")]
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

        [Fact(DisplayName = "Falha na validação quando rotas possuir um elemento com origem vazia")]
        [Trait("Rotas", "CadastrarRotaValidator")]
        public void Deve_RetornarErro_Se_Origem_De_Uma_Rota_For_Vazia()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto>
            {
                new RotaDto { Origem = "", Destino = "ABC", Valor = 100 }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor("Rotas[0]")
                .WithErrorMessage("Erro na validação da linha Rotas[0]: Origem deve ser informada.");
        }

        [Fact(DisplayName = "Falha na validação quando rotas possuir um elemento com destino vazio")]
        [Trait("Rotas", "CadastrarRotaValidator")]
        public void Deve_RetornarErro_Se_Destino_De_Uma_Rota_For_Vazio()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto>
            {
                new RotaDto { Origem = "ABC", Destino = "", Valor = 100 }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor("Rotas[0]")
                .WithErrorMessage("Erro na validação da linha Rotas[0]: Destino deve ser informado.");
        }

        [Fact(DisplayName = "Falha na validação quando rotas possuir um elemento com valor menor ou igual a zero")]
        [Trait("Rotas", "CadastrarRotaValidator")]
        public void Deve_RetornarErro_Se_Valor_De_Uma_Rota_For_Menor_Ou_Igual_A_Zero()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto>
            {
                new RotaDto { Origem = "ABC", Destino = "DEF", Valor = 0 }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor("Rotas[0]")
                .WithErrorMessage("Erro na validação da linha Rotas[0]: Valor deve ser maior que 0.");
        }

    }
}
