using MediatR;
using RotaViagem.Application.Base;
using RotaViagem.Application.Features.Rotas.Validators;
using RotaViagem.Application.Infra.DatabaseSession;
using RotaViagem.Application.Infra.Notification;
using RotaViagem.Domain.Entities;

namespace RotaViagem.Application.Features.Rotas.Commands
{
    public class CadastrarRotaCommandHandler : BaseCommandHandler, IRequestHandler<CadastrarRotaCommand, bool>
    {
        private readonly IUoW _uoW;

        public CadastrarRotaCommandHandler(IUoW uoW, INotificador notificador) : base(notificador)
        {
            _uoW = uoW;
        }

        public async Task<bool> Handle(CadastrarRotaCommand request, CancellationToken cancellationToken)
        {
            if (!ExecutarValidacao(new CadastrarRotaValidator(), request))
                return false;

            _uoW.BeginTransaction();

            foreach (var rotaDto in request.Rotas)
            {
                var rota = await _uoW.RotaRepository.BuscarRota(rotaDto.Origem, rotaDto.Destino);

                if (rota is not null)
                {
                    Notificar($"Rota {rota.Origem} - {rota.Destino} já está cadastrada.");
                    _uoW.Rollback();
                    return false;
                }

                var novaRota = new Rota
                {
                    Origem = rotaDto.Origem,
                    Destino = rotaDto.Destino,
                    Valor = rotaDto.Valor
                };

                await _uoW.RotaRepository.Cadastrar(novaRota);
            }

            return _uoW.Commit();
        }
    }
}
