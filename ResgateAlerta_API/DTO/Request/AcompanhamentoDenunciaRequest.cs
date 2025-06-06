

using ResgateAlerta.Domain.Enums;
using ResgateAlerta.DTO.Request;
using ResgateAlerta.DTO.Response;
using ResgateAlerta.Infrastructure.Contexts;
using EcoDenuncia.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResgateAlerta.Domain.Enums;
using ResgateAlerta.DTO.Response;
using System.Net;

namespace EcoDenuncia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Acompanhamento-Denúncia")]
    public class AcompanhamentoDenunciaController : ControllerBase
    {

        private readonly ResgateAlertaContext _context;

        public AcompanhamentoDenunciaController(ResgateAlertaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista completa de acompanhamentos de denúncia.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna todos os acompanhamentos registrados no sistema, com status, data e observações.
        /// </remarks>
        /// <response code="200">Retorna a lista de acompanhamentos de denúncia</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AcompanhamentoDenunciaRequest>>> GetAcompanhamentos()
        {
            var acompanhamentos = await _context.acompanhamentoDenuncias
                .Select(a => new AcompanhamentoDenunciaResponse
                {
                    IdAcompanhamento = a.IdAcompanhamento,
                    Status = a.Status.ToString(),
                    DataAtualizacao = a.DataAtualizacao,
                    Observacao = a.Observacao,
                    IdDenuncia = a.IdDenuncia
                })
                .ToListAsync();

            return Ok(acompanhamentos);
        }

        /// <summary>
        /// Retorna um acompanhamento de denúncia específico pelo Id.
        /// </summary>
        /// <param name="id">Id do acompanhamento de denúncia</param>
        /// <remarks>
        /// Este endpoint retorna todos os dados detalhados do acompanhamento de uma denúncia com base no Id fornecido.
        /// </remarks>
        /// <response code="200">Retorna os dados do acompanhamento de denúncia</response>
        /// <response code="404">Acompanhamento não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AcompanhamentoDenunciaResponse>> GetAcompanhamento(Guid id)
        {
            var acompanhamento = await _context.acompanhamentoDenuncias.FindAsync(id);

            if (acompanhamento == null)
                return NotFound();

            var dto = new AcompanhamentoDenunciaResponse
            {
                IdAcompanhamento = acompanhamento.IdAcompanhamento,
                Status = acompanhamento.Status.ToString(),
                DataAtualizacao = acompanhamento.DataAtualizacao,
                Observacao = acompanhamento.Observacao,
                IdDenuncia = acompanhamento.IdDenuncia
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cria um novo acompanhamento de denúncia no sistema.
        /// </summary>
        /// <param name="request">Dados necessários para criar o acompanhamento de denúncia</param>
        /// <remarks>
        /// Este endpoint cria um novo acompanhamento de denúncia e retorna a resposta com os dados da nova entrada.
        /// </remarks>
        /// <response code="201">Acompanhamento de denúncia criado com sucesso</response>
        /// <response code="400">Status inválido fornecido</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<AcompanhamentoDenunciaResponse>> PostAcompanhamento(AcompanhamentoDenunciaRequest request)
        {
            if (!Enum.TryParse<StatusDenuncia>(request.Status, true, out var statusEnum))
            {
                return BadRequest("Status inválido.");
            }

            var acompanhamento = AcompanhamentoDenuncia.Create(statusEnum, request.DataAtualizacao, request.Observacao, request.DenunciaId);

            _context.acompanhamentoDenuncias.Add(acompanhamento);
            await _context.SaveChangesAsync();

            var response = new AcompanhamentoDenunciaResponse
            {
                IdAcompanhamento = acompanhamento.IdAcompanhamento,
                Status = acompanhamento.Status.ToString(),
                DataAtualizacao = acompanhamento.DataAtualizacao,
                Observacao = acompanhamento.Observacao,
                IdDenuncia = acompanhamento.IdDenuncia
            };

            return CreatedAtAction(nameof(GetAcompanhamento), new { id = acompanhamento.IdAcompanhamento }, response);
        }

        /// /// <summary>
        /// Atualiza os dados de um acompanhamento de denúncia existente.
        /// </summary>
        /// <param name="id">Id do acompanhamento de denúncia</param>
        /// <param name="request">Dados atualizados do acompanhamento de denúncia</param>
        /// <remarks>
        /// Este endpoint permite que um acompanhamento de denúncia existente seja atualizado com novas informações.
        /// </remarks>
        /// <response code="200">Acompanhamento de denúncia atualizado com sucesso</response>
        /// <response code="400">Dados inválidos fornecidos</response>
        /// <response code="404">Acompanhamento não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AcompanhamentoDenunciaResponse>> PutAcompanhamento(Guid id, AcompanhamentoDenunciaRequest request)
        {
            var acompanhamento = await _context.acompanhamentoDenuncias.FindAsync(id);
            if (acompanhamento == null)
                return NotFound();

            if (!Enum.TryParse<StatusDenuncia>(request.Status, true, out var statusEnum))
            {
                return BadRequest("Status inválido.");
            }

            acompanhamento.AtualizarAcompanhamento(statusEnum, request.DataAtualizacao, request.Observacao);
            _context.acompanhamentoDenuncias.Update(acompanhamento);
            await _context.SaveChangesAsync();

            var response = new AcompanhamentoDenunciaResponse
            {
                IdAcompanhamento = acompanhamento.IdAcompanhamento,
                Status = acompanhamento.Status.ToString(),
                DataAtualizacao = acompanhamento.DataAtualizacao,
                Observacao = acompanhamento.Observacao,
                IdDenuncia = acompanhamento.IdDenuncia
            };

            return Ok(response);
        }


        /// <summary>
        /// Remove um acompanhamento de denúncia existente.
        /// </summary>
        /// <param name="id">Id do acompanhamento de denúncia</param>
        /// <remarks>
        /// Este endpoint remove o acompanhamento de uma denúncia do sistema.
        /// </remarks>
        /// <response code="204">Acompanhamento de denúncia removido com sucesso</response>
        /// <response code="404">Acompanhamento não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAcompanhamento(Guid id)
        {
            var acompanhamento = await _context.acompanhamentoDenuncias.FindAsync(id);
            if (acompanhamento == null)
                return NotFound();

            _context.acompanhamentoDenuncias.Remove(acompanhamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
