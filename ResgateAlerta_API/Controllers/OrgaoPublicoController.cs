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
    [Tags("Órgãos-Públicos")]
    public class OrgaoPublicoController: ControllerBase
    {
        private readonly ResgateAlertaContext _context;

        public OrgaoPublicoController(ResgateAlertaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todos os órgãos públicos cadastrados no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// GET /api/orgaopublico
        /// </remarks>
        /// <response code="200">Lista de órgãos públicos retornada com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrgaoPublicoResponse>>> GetOrgaosPublicos()
        {
            var orgaos = await _context.OrgaosPublicos
                .Select(o => new OrgaoPublicoResponse
                {
                    IdOrgaoPublico = o.IdOrgaoPublico,
                    Nome = o.Nome,
                    Sigla = o.Sigla,
                    Descricao = o.Descricao
                })
                .ToListAsync();

            return Ok(orgaos);
        }

        /// <summary>
        /// Retorna um órgão público específico, identificado pelo Id.
        /// </summary>
        /// <param name="id">Id do órgão público a ser recuperado.</param>
        /// <response code="200">Órgão público encontrado e retornado com sucesso.</response>
        /// <response code="404">Órgão público não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrgaoPublicoResponse>> GetOrgaoPublico(Guid id)
        {
            var orgao = await _context.OrgaosPublicos.FindAsync(id);

            if (orgao == null)
                return NotFound();

            var dto = new OrgaoPublicoResponse
            {
                IdOrgaoPublico = orgao.IdOrgaoPublico,
                Nome = orgao.Nome,
                Sigla = orgao.Sigla,
                Descricao = orgao.Descricao
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cria um novo órgão público no sistema.
        /// </summary>
        /// <param name="request">Dados do órgão público a ser criado.</param>
        /// <response code="201">Órgão público criado com sucesso.</response>
        /// <response code="400">Dados inválidos no corpo da requisição.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<OrgaoPublicoResponse>> PostOrgaoPublico(OrgaoPublicoRequest request)
        {
            var orgao = OrgaoPublico.Create(request.Nome, request.Sigla, request.Descricao);

            _context.OrgaosPublicos.Add(orgao);
            await _context.SaveChangesAsync();

            var response = new OrgaoPublicoResponse
            {
                IdOrgaoPublico = orgao.IdOrgaoPublico,
                Nome = orgao.Nome,
                Sigla = orgao.Sigla,
                Descricao = orgao.Descricao
            };

            return CreatedAtAction(nameof(GetOrgaoPublico), new { id = orgao.IdOrgaoPublico }, response);
        }

        /// <summary>
        /// Atualiza os dados de um órgão público existente.
        /// </summary>
        /// <param name="id">Id do órgão público a ser atualizado.</param>
        /// <param name="request">Dados atualizados do órgão público.</param>
        /// <response code="200">Órgão público atualizado com sucesso.</response>
        /// <response code="404">Órgão público não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrgaoPublicoResponse>> PutOrgaoPublico(Guid id, OrgaoPublicoRequest request)
        {
            var orgao = await _context.OrgaosPublicos.FindAsync(id);
            if (orgao == null)
                return NotFound();

            orgao.AtualizaOrgaoPublico(request.Nome, request.Sigla, request.Descricao);

            _context.OrgaosPublicos.Update(orgao);
            await _context.SaveChangesAsync();

            var response = new OrgaoPublicoResponse
            {
                IdOrgaoPublico = orgao.IdOrgaoPublico,
                Nome = orgao.Nome,
                Sigla = orgao.Sigla,
                Descricao = orgao.Descricao
            };

            return Ok(response);
        }


        /// <summary>
        /// Remove um órgão público pelo Id.
        /// </summary>
        /// <param name="id">Id do órgão público a ser removido.</param>
        /// <response code="204">Órgão público removido com sucesso.</response>
        /// <response code="404">Órgão público não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOrgaoPublico(Guid id)
        {
            var orgao = await _context.OrgaosPublicos.FindAsync(id);
            if (orgao == null)
                return NotFound();

            _context.OrgaosPublicos.Remove(orgao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
