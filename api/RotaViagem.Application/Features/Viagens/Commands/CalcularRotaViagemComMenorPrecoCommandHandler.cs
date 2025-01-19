using MediatR;
using RotaViagem.Application.Infra.Repositories;
using RotaViagem.Application.Infra.Notification;
using RotaViagem.Application.Base;
using RotaViagem.Application.Features.Viagens.Validators;
using RotaViagem.Application.Features.Viagens.Dtos;

namespace RotaViagem.Application.Features.Viagens.Commands
{
    public class CalcularRotaViagemComMenorPrecoCommandHandler : BaseCommandHandler, IRequestHandler<CalcularRotaViagemComMenorPrecoCommand, ViagemDto?>
    {
        private readonly IRotaRepository _rotaRepository;

        public CalcularRotaViagemComMenorPrecoCommandHandler(IRotaRepository rotaRepository, INotificador notificador) : base(notificador)
        {
            _rotaRepository = rotaRepository;
        }

        public async Task<ViagemDto?> Handle(CalcularRotaViagemComMenorPrecoCommand request, CancellationToken cancellationToken)
        {
            if (!ExecutarValidacao(new CalcularRotaViagemComMenorPrecoValidator(), request))
                return null;

            var origemEstaCadastrada = await _rotaRepository.OrigemEstaCadastrada(request.Origem);
            var destinoEstaCadastrado = await _rotaRepository.DestinoEstaCadastrado(request.Destino);

            if (!origemEstaCadastrada)
            {
                Notificar($"Não foi encontrada nenhuma rota com a origem {request.Origem}.");
            }

            if (!destinoEstaCadastrado)
            {
                Notificar($"Não foi encontrada nenhuma rota com o destino {request.Destino}.");
            }

            if (TemNotificacao())
                return null;

            var viagem = await EncontrarMelhorRotaViagem(request.Origem, request.Destino);

            return viagem;
        }

        private async Task<ViagemDto?> EncontrarMelhorRotaViagem(string origemViagem, string destinoViagem)
        {
            var conexoesVerificadas = new HashSet<string>();
            var conexoesAdicionadasViagem = new List<string>();
            var melhorRota = new ViagemDto { Custo = int.MaxValue, Conexoes = null };

            var conexoes = await BuscarConexoesCadastradas();

            void Buscar(string origemConexao, decimal custo)
            {
                if (custo >= melhorRota.Custo)
                    return;

                if (origemConexao == destinoViagem)
                {
                    melhorRota.Custo = custo;
                    melhorRota.Conexoes = new List<string>(conexoesAdicionadasViagem);
                    return;
                }

                if (conexoesVerificadas.Contains(origemConexao) || !conexoes.ContainsKey(origemConexao))
                    return;

                conexoesVerificadas.Add(origemConexao);

                foreach (var conexao in conexoes[origemConexao])
                {
                    conexoesAdicionadasViagem.Add(conexao.Destino);
                    Buscar(conexao.Destino, custo + conexao.Custo);
                    conexoesAdicionadasViagem.RemoveAt(conexoesAdicionadasViagem.Count - 1);
                }

                conexoesVerificadas.Remove(origemConexao);
            }

            conexoesAdicionadasViagem.Add(origemViagem);
            Buscar(origemViagem, 0);

            return melhorRota.Custo == int.MaxValue ? null : melhorRota;
        }

        private async Task<Dictionary<string, List<ConexaoDto>>> BuscarConexoesCadastradas()
        {
            var conexoes = new Dictionary<string, List<ConexaoDto>>();
            var rotas = await _rotaRepository.BuscarTodas();

            foreach (var rota in rotas)
            {
                if (!conexoes.ContainsKey(rota.Origem))
                {
                    conexoes[rota.Origem] = new List<ConexaoDto>();
                }

                conexoes[rota.Origem].Add(new ConexaoDto(rota.Destino, rota.Valor));
            }

            return conexoes;
        }
    }
}
