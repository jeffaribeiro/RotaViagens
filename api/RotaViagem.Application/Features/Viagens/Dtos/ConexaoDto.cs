namespace RotaViagem.Application.Features.Viagens.Dtos
{
    public class ConexaoDto
    {
        public string Destino { get; }
        public decimal Custo { get; }

        public ConexaoDto(string destino, decimal custo)
        {
            Destino = destino;
            Custo = custo;
        }
    }
}
