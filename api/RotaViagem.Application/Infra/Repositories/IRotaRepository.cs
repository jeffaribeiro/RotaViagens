using RotaViagem.Domain.Entities;

namespace RotaViagem.Application.Infra.Repositories
{
    public interface IRotaRepository
    {
        Task<Rota?> BuscarRota(string origem, string destino);
        Task<IEnumerable<Rota>> BuscarTodas();
        Task<bool> Cadastrar(Rota rota);
        Task<bool> OrigemEstaCadastrada(string origem);
        Task<bool> DestinoEstaCadastrado(string destino);
    }
}
