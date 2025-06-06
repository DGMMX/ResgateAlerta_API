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
    [Tags("Estados")]
    public class EstadoController: ControllerBase
    {
        private readonly ResgateAlertaContext _context;

        public EstadoController(ResgateAlertaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna uma lista de todos os estados cadastrados no sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna uma lista com todos os estados já registrados no banco de dados, com suas respectivas informações como nome e UF.
        /// </remarks>
        /// <response code="200">Lista de estados retornada com sucesso</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<EstadoResponse>>> GetEstados()
        {
            var estados = await _context.Estados
                .Select(e => new EstadoResponse
                {
                    IdEstado = e.IdEstado,
                    Nome = e.Nome,
                    Uf = e.Uf
                })
                .ToListAsync();

            return Ok(estados);
        }

        // <summary>
        /// Retorna os detalhes de um estado específico pelo seu Id.
        /// </summary>
        /// <param name="id">Id do estado</param>
        /// <remarks>
        /// Este endpoint permite buscar um estado específico no sistema através do seu ID único.
        /// </remarks>
        /// <response code="200">Estado encontrado e retornado com sucesso</response>
        /// <response code="404">Estado não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<EstadoResponse>> GetEstado(Guid id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado == null)
                return NotFound();

            var dto = new EstadoResponse
            {
                IdEstado = estado.IdEstado,
                Nome = estado.Nome,
                Uf = estado.Uf
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cria um novo estado no sistema.
        /// </summary>
        /// <param name="request">Dados do estado a ser criado</param>
        /// <remarks>
        /// Este endpoint cria um novo estado, que será registrado no banco de dados com informações como nome e UF.
        /// </remarks>
        /// <response code="201">Estado criado com sucesso</response>
        /// <response code="400">Erro nos dados fornecidos</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<EstadoResponse>> PostEstado(EstadoRequest request)
        {
            var estado = Estado.Create(request.Nome, request.Uf);

            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            var response = new EstadoResponse
            {
                IdEstado = estado.IdEstado,
                Nome = estado.Nome,
                Uf = estado.Uf
            };

            return CreatedAtAction(nameof(GetEstado), new { id = estado.IdEstado }, response);
        }

        /// <summary>
        /// Atualiza os dados de um estado existente no sistema.
        /// </summary>
        /// <param name="id">Id do estado a ser atualizado</param>
        /// <param name="request">Dados atualizados do estado</param>
        /// <remarks>
        /// Este endpoint permite atualizar um estado já registrado no sistema com novos dados de nome e UF.
        /// </remarks>
        /// <response code="200">Estado atualizado com sucesso</response>
        /// <response code="404">Estado não encontrado</response>
        /// <response code="400">Erro nos dados fornecidos</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<EstadoResponse>> PutEstado(Guid id, EstadoRequest request)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
                return NotFound();

            estado.AtualizarEstado(request.Nome, request.Uf); // supondo que você tem esse método na entidade

            _context.Estados.Update(estado);
            await _context.SaveChangesAsync();

            var response = new EstadoResponse
            {
                IdEstado = estado.IdEstado,
                Nome = estado.Nome,
                Uf = estado.Uf
            };

            return Ok(response);
        }


        /// <summary>
        /// Remove um estado do sistema pelo Id.
        /// </summary>
        /// <param name="id">Id do estado a ser removido</param>
        /// <remarks>
        /// Este endpoint permite excluir um estado do sistema com base no seu ID único.
        /// </remarks>
        /// <response code="204">Estado removido com sucesso</response>
        /// <response code="404">Estado não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteEstado(Guid id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
                return NotFound();

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
