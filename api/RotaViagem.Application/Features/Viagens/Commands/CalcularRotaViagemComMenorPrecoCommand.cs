using MediatR;
using RotaViagem.Application.Features.Viagens.Dtos;

namespace RotaViagem.Application.Features.Viagens.Commands
{
    public class CalcularRotaViagemComMenorPrecoCommand : IRequest<ViagemDto?>
    {
        public string Origem { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
    }
}
