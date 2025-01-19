namespace RotaViagem.Application.Features.Rotas.Domain
{
    public class Rota
    {
        public int Id { get; set; }
        public string Origem { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
