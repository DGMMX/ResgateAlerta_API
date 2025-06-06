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
    [Tags("Cidades")]
    public class CidadeController: ControllerBase
    {
        private readonly ResgateAlertaContext _context;

        public CidadeController(ResgateAlertaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todas as cidades cadastradas no sistema.
        /// </summary>
        /// <remarks>
        /// Esta operação busca todas as cidades, incluindo o estado relacionado, 
        /// e retorna uma lista com os detalhes de cada cidade.
        /// </remarks>
        /// <response code="200">Lista de cidades retornada com sucesso.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CidadeResponse>>> GetCidades()
        {
            var cidades = await _context.Cidades
                .Include(c => c.Estado) // INCLUI o estado vinculado
                .ToListAsync();

            var cidadesDto = cidades.Select(c => new CidadeResponse
            {
                IdCidade = c.IdCidade,
                Nome = c.Nome,
                IdEstado = c.IdEstado,
                NomeEstado = c.Estado?.Nome // agora preenche corretamente
            }).ToList();

            return Ok(cidadesDto);
        }

        /// <summary>
        /// Retorna os detalhes de uma cidade específica através do seu ID.
        /// </summary>
        /// <param name="id">ID da cidade a ser buscada.</param>
        /// <remarks>
        /// Retorna os dados completos de uma cidade, incluindo nome e estado associado.
        /// </remarks>
        /// <response code="200">Cidade encontrada com sucesso.</response>
        /// <response code="404">Cidade não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CidadeResponse>> GetCidade(Guid id)
        {
            var cidade = await _context.Cidades.FindAsync(id);

            if (cidade == null)
                return NotFound();

            var dto = new CidadeResponse
            {
                IdCidade = cidade.IdCidade,
                Nome = cidade.Nome,
                IdEstado = cidade.IdEstado
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cria uma nova cidade no sistema.
        /// </summary>
        /// <param name="request">Dados da nova cidade a ser cadastrada.</param>
        /// <remarks>
        /// Este método permite criar uma cidade associando-a a um estado, com os dados fornecidos.
        /// </remarks>
        /// <response code="201">Cidade criada com sucesso.</response>
        /// <response code="400">Requisição inválida (dados incorretos).</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<CidadeResponse>> PostCidade(CidadeRequest request)
        {
            var cidade = Cidade.Create(request.Nome, request.IdEstado);

            _context.Cidades.Add(cidade);
            await _context.SaveChangesAsync();

            var response = new CidadeResponse
            {
                IdCidade = cidade.IdCidade,
                Nome = cidade.Nome,
                IdEstado = cidade.IdEstado
            };

            return CreatedAtAction(nameof(GetCidade), new { id = cidade.IdCidade }, response);
        }

        /// <summary>
        /// Atualiza os dados de uma cidade existente no sistema.
        /// </summary>
        /// <param name="id">ID da cidade a ser atualizada.</param>
        /// <param name="request">Novos dados da cidade.</param>
        /// <remarks>
        /// Atualiza os detalhes de uma cidade existente, permitindo alteração do nome e estado.
        /// </remarks>
        /// <response code="200">Cidade atualizada com sucesso.</response>
        /// <response code="400">Requisição inválida (dados incorretos).</response>
        /// <response code="404">Cidade não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CidadeResponse>> PutCidade(Guid id, CidadeRequest request)
        {
            var cidade = await _context.Cidades.FindAsync(id);
            if (cidade == null)
                return NotFound();

            cidade.SetNome(request.Nome);
            cidade.SetIdEstado(request.IdEstado);

            _context.Cidades.Update(cidade);
            await _context.SaveChangesAsync();

            var estado = await _context.Estados.FindAsync(cidade.IdEstado);

            var response = new CidadeResponse
            {
                IdCidade = cidade.IdCidade,
                Nome = cidade.Nome,
                IdEstado = cidade.IdEstado,
                NomeEstado = estado?.Nome
            };

            return Ok(response);
        }



        /// <summary>
        /// Remove uma cidade do sistema pelo seu ID.
        /// </summary>
        /// <param name="id">ID da cidade a ser removida.</param>
        /// <remarks>
        /// Este método exclui a cidade especificada, removendo-a permanentemente do banco de dados.
        /// </remarks>
        /// <response code="204">Cidade removida com sucesso.</response>
        /// <response code="404">Cidade não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCidade(Guid id)
        {
            var cidade = await _context.Cidades.FindAsync(id);
            if (cidade == null)
                return NotFound();

            _context.Cidades.Remove(cidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
