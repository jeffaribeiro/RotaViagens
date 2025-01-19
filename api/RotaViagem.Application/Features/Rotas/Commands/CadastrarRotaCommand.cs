using MediatR;
using RotaViagem.Application.Features.Rotas.Dtos;

namespace RotaViagem.Application.Features.Rotas.Commands
{
    public class CadastrarRotaCommand : IRequest<bool>
    {
        public required IEnumerable<RotaDto> Rotas { get; set; }
    }
}
