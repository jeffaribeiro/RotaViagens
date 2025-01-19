using MediatR;
using Microsoft.AspNetCore.Mvc;
using RotaViagem.Application.Features.Rotas.Commands;
using RotaViagem.Application.Features.Rotas.Dtos;
using RotaViagem.Application.Infra.Notification;

namespace RotaViagem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : BaseController
    {
        private readonly IMediator _mediator;

        public RotaController(INotificador notificador, IMediator mediator) : base(notificador)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadTxt(IFormFile file)
        {
            if (file == null)
            {
                NotificarErro("Nenhum arquivo enviado.");
                return CustomResponse();
            }

            if (file.Length == 0)
            {
                NotificarErro("O arquivo está vazio.");
                return CustomResponse();
            }

            if (Path.GetExtension(file.FileName)?.ToLower() != ".txt")
            {
                NotificarErro("Somente arquivos .txt são permitidos.");
                return CustomResponse();
            }

            var rotas = await LerArquivoTxt(file);

            if (!rotas.Any())
            {
                NotificarErro("A leitura do arquivo não foi bem sucedida.");
                return CustomResponse();
            }

            if (!OperacaoValida())
                return CustomResponse();

            var command = new CadastrarRotaCommand { Rotas = rotas };

            return CustomResponse(await _mediator.Send(command));
        }

        private async Task<IEnumerable<RotaDto>> LerArquivoTxt(IFormFile file)
        {
            var linhas = new List<string>();
            var rotas = new List<RotaDto>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var linha = await reader.ReadLineAsync();
                    if (!string.IsNullOrWhiteSpace(linha))
                        linhas.Add(linha.Trim());
                }
            }

            foreach (var linha in linhas)
            {
                var partes = linha.Split(',');

                if (partes.Length != 3 || !decimal.TryParse(partes[2], out var valor))
                {
                    NotificarErro($"Formato inválido na linha: '{linha}'. O formato esperado é 'Origem,Destino,Valor' com Valor sendo numérico.");
                    continue;
                }

                rotas.Add(new RotaDto
                {
                    Origem = partes[0].Trim(),
                    Destino = partes[1].Trim(),
                    Valor = valor
                });
            }

            return rotas;
        }
    }
}
