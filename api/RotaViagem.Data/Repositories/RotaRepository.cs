using Dapper;
using RotaViagem.Application.Features.Rotas.Domain;
using RotaViagem.Application.Infra.DatabaseSession;
using RotaViagem.Application.Infra.Repositories;
using System.Data;

namespace RotaViagem.Data.Repositories
{
    public class RotaRepository : IRotaRepository
    {
        private readonly IDBSession _session;

        public RotaRepository(IDBSession session)
            => _session = session;

        public async Task<Rota?> BuscarRota(string origem, string destino)
        {
            var query = @"SELECT Id, Origem, Destino, Valor
                              FROM Rota WITH (NOLOCK)
                              WHERE Origem = @Origem AND Destino = @Destino";

            var parms = new DynamicParameters();

            parms.Add("@Origem", origem.ToUpper());
            parms.Add("@Destino", destino.ToUpper());

            var result =
                await _session
                        .Connection
                        .QuerySingleOrDefaultAsync<Rota>(sql: query,
                                                         param: parms,
                                                         commandType: CommandType.Text,
                                                         transaction: _session.Transaction);

            return result;
        }

        public async Task<IEnumerable<Rota>> BuscarTodas()
        {
            var query = @"SELECT Id, Origem, Destino, Valor
                              FROM Rota WITH (NOLOCK)
                              ORDER BY Id";

            var result =
                await _session
                        .Connection
                        .QueryAsync<Rota>(sql: query,
                                          commandType: CommandType.Text,
                                          transaction: _session.Transaction);

            return result;
        }

        public async Task<bool> Cadastrar(Rota rota)
        {
            var query = @"INSERT INTO Rota(Origem, Destino, Valor)
                          VALUES(@Origem, @Destino, @Valor)";

            var parms = new DynamicParameters();

            parms.Add("@Origem", rota.Origem.ToUpper());
            parms.Add("@Destino", rota.Destino.ToUpper());
            parms.Add("@Valor", rota.Valor);

            var result =
                await _session
                        .Connection
                        .ExecuteAsync(sql: query,
                                      param: parms,
                                      commandType: CommandType.Text,
                                      transaction: _session.Transaction);

            return result > 0;
        }

        public async Task<bool> DestinoEstaCadastrado(string destino)
        {
            var query = @"SELECT count(1) FROM Rota WITH (NOLOCK) WHERE Destino = @Destino";

            var parms = new DynamicParameters();

            parms.Add("@Destino", destino.ToUpper());

            var result =
                await _session
                        .Connection
                        .QuerySingleAsync<int>(sql: query,
                                               param: parms,
                                               commandType: CommandType.Text,
                                               transaction: _session.Transaction);

            return result > 0;
        }

        public async Task<bool> OrigemEstaCadastrada(string origem)
        {
            var query = @"SELECT count(1) FROM Rota WITH (NOLOCK) WHERE Origem = @Origem";

            var parms = new DynamicParameters();

            parms.Add("@Origem", origem.ToUpper());

            var result =
                await _session
                        .Connection
                        .QuerySingleAsync<int>(sql: query,
                                               param: parms,
                                               commandType: CommandType.Text,
                                               transaction: _session.Transaction);

            return result > 0;
        }
    }
}
