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
            var locais = await _context.Localizacoes
                .Select(l => new LocalizacaoResponse
                {
                    IdLocalizacao = l.IdLocalização,
                    Logradouro = l.Logradouro,
                    Numero = l.Numero,
                    Complemento = l.Complemento,
                    Cep = l.Cep,
                    //Latitude = l.Latitude,
                    //Longitude = l.Longitude,
                    IdBairro = l.IdBairro
                })
                .ToListAsync();

            return Ok(locais);
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
            var local = await _context.Localizacoes.FindAsync(id);

            if (local == null)
                return NotFound();

            var dto = new LocalizacaoResponse
            {
                IdLocalizacao = local.IdLocalização,
                Logradouro = local.Logradouro,
                Numero = local.Numero,
                Complemento = local.Complemento,
                Cep = local.Cep,
                //Latitude = local.Latitude,
                //Longitude = local.Longitude,
                IdBairro = local.IdBairro
            };

            return Ok(dto);
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
            var local = Localizacao.Create(request.Logradouro, request.Numero, request.Complemento, request.Cep,  request.IdBairro);

            _context.Localizacoes.Add(local);
            await _context.SaveChangesAsync();

            var response = new LocalizacaoResponse
            {
                IdLocalizacao = local.IdLocalização,
                Logradouro = local.Logradouro,
                Numero = local.Numero,
                Complemento = local.Complemento,
                Cep = local.Cep,
                //Latitude = local.Latitude,
                //Longitude = local.Longitude,
                IdBairro = local.IdBairro
            };

            return CreatedAtAction(nameof(GetLocalizacao), new { id = local.IdLocalização }, response);
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
        public async Task<ActionResult<LocalizacaoResponse>> PutLocalizacao(Guid id, [FromBody] LocalizacaoRequest request)
        {
            var local = await _context.Localizacoes.FindAsync(id);

            if (local == null)
                return NotFound();

            local.SetLogradouro(request.Logradouro);
            local.SetNumero(request.Numero);
            local.SetComplemento(request.Complemento);
            local.SetCep(request.Cep);
            local.SetIdBairro(request.IdBairro);

            await _context.SaveChangesAsync();

            var response = new LocalizacaoResponse
            {
                IdLocalizacao = local.IdLocalização,
                Logradouro = local.Logradouro,
                Numero = local.Numero,
                Complemento = local.Complemento,
                Cep = local.Cep,
                IdBairro = local.IdBairro
            };

            return Ok(response);
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
            var local = await _context.Localizacoes.FindAsync(id);

            if (local == null)
                return NotFound();

            _context.Localizacoes.Remove(local);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
