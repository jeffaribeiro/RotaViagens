using Moq;
using RotaViagem.Application.Features.Rotas.Domain;
using RotaViagem.Application.Features.Viagens.Commands;
using RotaViagem.Application.Infra.Notification;
using RotaViagem.Application.Infra.Repositories;

namespace RotaViagem.UnitTests.Tests.Application.Features.Viagens.Commands
{
    public class CalcularRotaViagemComMenorPrecoCommandHandlerTests
    {
        private readonly Mock<IRotaRepository> _mockRotaRepository;
        private readonly Mock<INotificador> _mockNotificador;
        private readonly CalcularRotaViagemComMenorPrecoCommandHandler _handler;

        public CalcularRotaViagemComMenorPrecoCommandHandlerTests()
        {
            _mockRotaRepository = new Mock<IRotaRepository>();
            _mockNotificador = new Mock<INotificador>();
            _handler = new CalcularRotaViagemComMenorPrecoCommandHandler(_mockRotaRepository.Object, _mockNotificador.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarNull_QuandoValidacaoFalhar()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = " ", Destino = "BSB" };
            _mockNotificador.Setup(n => n.TemNotificacao()).Returns(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _mockRotaRepository.Verify(r => r.OrigemEstaCadastrada(It.IsAny<string>()), Times.Never);
            _mockRotaRepository.Verify(r => r.DestinoEstaCadastrado(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarNull_QuandoOrigemNaoEstiverCadastrada()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "SDU", Destino = "BSB" };
            _mockRotaRepository.Setup(r => r.OrigemEstaCadastrada("SDU")).ReturnsAsync(false);
            _mockRotaRepository.Setup(r => r.DestinoEstaCadastrado("BSB")).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _mockNotificador.Verify(n => n.Handle(It.Is<Notificacao>(s => s.Mensagem.Contains("origem"))), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarNull_QuandoDestinoNaoEstiverCadastrado()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "SDU", Destino = "BSB" };
            _mockRotaRepository.Setup(r => r.OrigemEstaCadastrada("SDU")).ReturnsAsync(true);
            _mockRotaRepository.Setup(r => r.DestinoEstaCadastrado("BSB")).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _mockNotificador.Verify(n => n.Handle(It.Is<Notificacao>(s => s.Mensagem.Contains("destino"))), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarViagemDto_QuandoRotaMaisBarataForEncontrada()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "SDU", Destino = "GRU" };
            _mockRotaRepository.Setup(r => r.OrigemEstaCadastrada("SDU")).ReturnsAsync(true);
            _mockRotaRepository.Setup(r => r.DestinoEstaCadastrado("GRU")).ReturnsAsync(true);
            _mockRotaRepository.Setup(r => r.BuscarTodas()).ReturnsAsync(new List<Rota>
        {
            new Rota { Origem = "SDU", Destino = "BSB", Valor = 10 },
            new Rota { Origem = "BSB", Destino = "CWB", Valor = 15 },
            new Rota { Origem = "CWB", Destino = "GRU", Valor = 10 }
        });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(35, result.Custo);
            Assert.Equal(new List<string> { "SDU", "BSB", "CWB", "GRU" }, result.Conexoes);
        }

        [Fact]
        public async Task Handle_DeveRetornarNull_QuandoNaoExistirCaminhoDisponivel()
        {
            // Arrange
            var command = new CalcularRotaViagemComMenorPrecoCommand { Origem = "SDU", Destino = "GRU" };
            _mockRotaRepository.Setup(r => r.OrigemEstaCadastrada("SDU")).ReturnsAsync(true);
            _mockRotaRepository.Setup(r => r.DestinoEstaCadastrado("GRU")).ReturnsAsync(true);
            _mockRotaRepository.Setup(r => r.BuscarTodas()).ReturnsAsync(new List<RotaViagem.Application.Features.Rotas.Domain.Rota>
        {
            new Rota { Origem = "SDU", Destino = "BSB", Valor = 10 }
        });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
