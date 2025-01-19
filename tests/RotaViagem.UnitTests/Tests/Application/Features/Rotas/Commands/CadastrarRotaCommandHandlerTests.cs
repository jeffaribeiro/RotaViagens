using Moq;
using RotaViagem.Application.Features.Rotas.Commands;
using RotaViagem.Application.Features.Rotas.Domain;
using RotaViagem.Application.Features.Rotas.Dtos;
using RotaViagem.Application.Infra.DatabaseSession;
using RotaViagem.Application.Infra.Notification;

namespace RotaViagem.UnitTests.Tests.Application.Features.Rotas.Commands
{
    public class CadastrarRotaCommandHandlerTests
    {
        private readonly Mock<IUoW> _mockUoW;
        private readonly Mock<INotificador> _mockNotificador;
        private readonly CadastrarRotaCommandHandler _handler;

        public CadastrarRotaCommandHandlerTests()
        {
            _mockUoW = new Mock<IUoW>();
            _mockNotificador = new Mock<INotificador>();
            _handler = new CadastrarRotaCommandHandler(_mockUoW.Object, _mockNotificador.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalse_QuandoValidacaoFalhar()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto> { new RotaDto { Origem = "BS", Destino = "SDU", Valor = 100 } }
            };

            _mockNotificador
                .Setup(n => n.TemNotificacao())
                .Returns(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mockUoW.Verify(u => u.BeginTransaction(), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalse_QuandoRotaJaCadastrada()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto> { new RotaDto { Origem = "BSB", Destino = "SDU", Valor = 100 } }
            };

            _mockUoW.Setup(u => u.RotaRepository.BuscarRota("BSB", "SDU"))
                    .ReturnsAsync(new Rota { Origem = "BSB", Destino = "SDU" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mockUoW.Verify(u => u.Rollback(), Times.Once);
            _mockNotificador.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarTrue_QuandoCadastroBemSucedido()
        {
            // Arrange
            var command = new CadastrarRotaCommand
            {
                Rotas = new List<RotaDto>
            {
                new RotaDto { Origem = "GIG", Destino = "GRU", Valor = 100 },
                new RotaDto { Origem = "BSB", Destino = "SDU", Valor = 200 }
            }
            };

            _mockUoW.Setup(u => u.RotaRepository.BuscarRota(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync((RotaViagem.Application.Features.Rotas.Domain.Rota)null);

            _mockUoW.Setup(u => u.Commit()).Returns(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mockUoW.Verify(u => u.BeginTransaction(), Times.Once);
            _mockUoW.Verify(u => u.Commit(), Times.Once);
        }
    }
}
