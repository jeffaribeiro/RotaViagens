namespace RotaViagem.Application.Features.Rotas.Dtos
{
    public class RotaDto
    {
        public string Origem { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
