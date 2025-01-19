using MediatR;
using Microsoft.AspNetCore.Mvc;
using RotaViagem.Application.Infra.Notification;
using RotaViagem.Application.Features.Viagens.Commands;

namespace RotaViagem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViagemController : BaseController
    {
        private readonly IMediator _mediator;

        public ViagemController(INotificador notificador, IMediator mediator) : base(notificador)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CalcularRotaComMenorPreco([FromQuery] CalcularRotaViagemComMenorPrecoCommand request)
        {
            return CustomResponse(await _mediator.Send(request));
        }

    }
}
