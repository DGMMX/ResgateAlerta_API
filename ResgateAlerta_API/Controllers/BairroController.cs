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
    [Tags("Bairros")]
    public class BairroController : ControllerBase
    {
        private readonly ResgateAlertaContext _context;

        public BairroController(ResgateAlertaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna uma lista de todos os bairros cadastrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de Solicitação:
        /// GET api/bairros
        /// </remarks>
        /// <response code="200">Lista de bairros retornada com sucesso</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BairroResponse>>> GetBairros()
        {
            var bairros = await _context.Bairros
                .Select(b => new BairroResponse
                {
                    IdBairro = b.IdBairro,
                    Nome = b.Nome,
                    IdCidade = b.IdCidade
                })
                .ToListAsync();

            return Ok(bairros);
        }

        /// <summary>
        /// Retorna detalhes de um bairro específico pelo Id.
        /// </summary>
        /// <param name="id">ID do bairro a ser retornado</param>
        /// <response code="200">Detalhes do bairro retornados com sucesso</response>
        /// <response code="404">Bairro não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BairroResponse>> GetBairro(Guid id)
        {
            var bairro = await _context.Bairros.FindAsync(id);

            if (bairro == null)
                return NotFound();

            var dto = new BairroResponse
            {
                IdBairro = bairro.IdBairro,
                Nome = bairro.Nome,
                IdCidade = bairro.IdCidade
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cria um novo bairro no sistema.
        /// </summary>
        /// <param name="request">Dados do bairro a ser criado</param>
        /// <response code="201">Bairro criado com sucesso</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<BairroResponse>> PostBairro(BairroRequest request)
        {
            var bairro = Bairro.Create(request.Nome, request.IdCidade);

            _context.Bairros.Add(bairro);
            await _context.SaveChangesAsync();

            var response = new BairroResponse
            {
                IdBairro = bairro.IdBairro,
                Nome = bairro.Nome,
                IdCidade = bairro.IdCidade
            };

            return CreatedAtAction(nameof(GetBairro), new { id = bairro.IdBairro }, response);
        }

        /// <summary>
        /// Atualiza os dados de um bairro existente.
        /// </summary>
        /// <param name="id">ID do bairro a ser atualizado</param>
        /// <param name="request">Novos dados do bairro</param>
        /// <response code="200">Bairro atualizado com sucesso</response>
        /// <response code="404">Bairro não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BairroResponse>> PutBairro(Guid id, BairroRequest request)
        {
            var bairro = await _context.Bairros.FindAsync(id);
            if (bairro == null)
                return NotFound();

            bairro.AtualizarBairro(request.Nome, request.IdCidade);


            await _context.SaveChangesAsync();

            var response = new BairroResponse
            {
                IdBairro = bairro.IdBairro,
                Nome = bairro.Nome,
                IdCidade = bairro.IdCidade
            };

            return Ok(response);
        }


        /// <summary>
        /// Remove um bairro do sistema pelo ID.
        /// </summary>
        /// <param name="id">ID do bairro a ser removido</param>
        /// <response code="204">Bairro removido com sucesso</response>
        /// <response code="404">Bairro não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteBairro(Guid id)
        {
            var bairro = await _context.Bairros.FindAsync(id);
            if (bairro == null)
                return NotFound();

            _context.Bairros.Remove(bairro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}