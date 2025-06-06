using Microsoft.AspNetCore.Mvc;
using ResgateAlerta.DTO.Request;
using ResgateAlerta.DTO.Response;
using ResgateAlerta.Infrastructure.Contexts;
using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ResgateAlerta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Localizacoes")]
    public class LocalizacaoController: ControllerBase
    {

        private readonly ResgateAlertaContext _context;

        public LocalizacaoController(ResgateAlertaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todas as localizações cadastradas no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     GET /api/localizacoes
        /// </remarks>
        /// <response code="200">Retorna a lista de localizações</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<LocalizacaoResponse>>> GetLocalizacoes()
        {
            var localizacoes = await _context.Localizacoes
                .Include(l => l.Bairro)
                .Include(l => l.Cidade)
                .Include(l => l.Estado)
                .Select(l => new LocalizacaoResponse
                {
                    IdLocalizacao = l.IdLocalizacao,
                    Logradouro = l.Logradouro,
                    Numero = l.Numero,
                    Complemento = l.Complemento,
                    Bairro = l.Bairro.Nome,
                    Cidade = l.Cidade.Nome,
                    Estado = l.Estado.Nome
                })
                .ToListAsync();
            return localizacoes;
        }

        /// <summary>
        /// Retorna uma localização específica a partir do Id informado.
        /// </summary>
        /// <param name="id">Id da localização</param>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     GET /api/localizacoes/{id}
        /// </remarks>
        /// <response code="200">Retorna a localização solicitada</response>
        /// <response code="404">Localização não encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<LocalizacaoResponse>> GetLocalizacao(Guid id)
        {
            var localizacao = await _context.Localizacoes
                .Include(l => l.Bairro)
                .Include(l => l.Cidade)
                .Include(l => l.Estado)
                .FirstOrDefaultAsync(l => l.IdLocalizacao == id);

            if (localizacao == null)
            {
                return NotFound();
            }

            return new LocalizacaoResponse
            {
                IdLocalizacao = localizacao.IdLocalizacao,
                Logradouro = localizacao.Logradouro,
                Numero = localizacao.Numero,
                Complemento = localizacao.Complemento,
                Bairro = localizacao.Bairro.Nome,
                Cidade = localizacao.Cidade.Nome,
                Estado = localizacao.Estado.Nome
            };
        }

        /// <summary>
        /// Cria uma nova localização no sistema.
        /// </summary>
        /// <param name="request">Dados da localização a ser criada</param>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     POST /api/localizacoes
        ///     {
        ///         "logradouro": "Rua das Flores",
        ///         "numero": "123",
        ///         "complemento": "Apto 101",
        ///         "cep": "50000000",
        ///         "idBairro": "bairro-id"
        ///     }
        /// </remarks>
        /// <response code="201">Localização criada com sucesso</response>
        /// <response code="400">Requisição inválida, dados incompletos ou malformados</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LocalizacaoResponse>> PostLocalizacao(LocalizacaoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bairro = await _context.Bairros.FindAsync(request.IdBairro);
            var cidade = await _context.Cidades.FindAsync(request.IdCidade);
            var estado = await _context.Estados.FindAsync(request.IdEstado);

            if (bairro == null || cidade == null || estado == null)
            {
                return BadRequest("Bairro, Cidade ou Estado não encontrado.");
            }

            var localizacao = Localizacao.Create(bairro, cidade, estado, request.Logradouro, request.Numero, request.Complemento);
            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();

            return new LocalizacaoResponse
            {
                IdLocalizacao = localizacao.IdLocalizacao,
                Logradouro = localizacao.Logradouro,
                Numero = localizacao.Numero,
                Complemento = localizacao.Complemento,
                Bairro = bairro.Nome,
                Cidade = cidade.Nome,
                Estado = estado.Nome
            };
        }

        /// <summary>
        /// Atualiza os dados de uma localização existente.
        /// </summary>
        /// <param name="id">Id da localização a ser atualizada</param>
        /// <param name="request">Dados atualizados da localização</param>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     PUT /api/localizacoes/{id}
        ///     {
        ///         "logradouro": "Rua das Flores Atualizada",
        ///         "numero": "123A",
        ///         "complemento": "Apto 102",
        ///         "cep": "50000001",
        ///         "idBairro": "bairro-id"
        ///     }
        /// </remarks>
        /// <response code="200">Localização atualizada com sucesso</response>
        /// <response code="404">Localização não encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutLocalizacao(Guid id, LocalizacaoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairros.FindAsync(request.IdBairro);
            var cidade = await _context.Cidades.FindAsync(request.IdCidade);
            var estado = await _context.Estados.FindAsync(request.IdEstado);

            if (bairro == null || cidade == null || estado == null)
            {
                return BadRequest("Bairro, Cidade ou Estado não encontrado.");
            }

            localizacao.AtualizaLocalizacao(bairro, cidade, estado, request.Logradouro, request.Numero, request.Complemento);
            _context.Localizacoes.Update(localizacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Remove uma localização do sistema pelo Id.
        /// </summary>
        /// <param name="id">Id da localização a ser removida</param>
        /// <response code="204">Localização removida com sucesso</response>
        /// <response code="404">Localização não encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteLocalizacao(Guid id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
            {
                return NotFound();
            }

            _context.Localizacoes.Remove(localizacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
