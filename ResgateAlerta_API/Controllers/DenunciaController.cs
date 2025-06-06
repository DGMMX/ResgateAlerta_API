using ResgateAlerta.DTO.Request;
using ResgateAlerta.DTO.Response;
using ResgateAlerta.Infrastructure.Contexts;
using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ResgateAlerta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Denuncias")]
    public class DenunciaController : ControllerBase
    {
        private readonly ResgateAlertaContext _context;

        public DenunciaController(ResgateAlertaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna uma lista de todas as denúncias registradas no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// GET /api/denuncia
        /// </remarks>
        /// <response code="200">Lista de denúncias retornada com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<DenunciaResponse>>> GetDenuncias()
        {
            var denuncias = await _context.Denuncias
                .Include(d => d.Usuario)
                .Include(d => d.OrgaoPublico)
                .Include(d => d.Localizacao)
                    .ThenInclude(l => l.Bairro)
                        .ThenInclude(b => b.Cidade)
                            .ThenInclude(c => c.Estado)
                .Select(d => new DenunciaResponse
                {
                    IdDenuncia = d.IdDenuncia,
                    Titulo = d.Titulo,
                    Descricao = d.Descricao,
                    DataDenuncia = d.DataDenuncia,
                    Status = d.Status,
                    Usuario = d.Usuario.Nome,
                    Localizacao = $"{d.Localizacao.Logradouro}, {d.Localizacao.Numero}",
                    OrgaoPublico = d.OrgaoPublico.Nome
                })
                .ToListAsync();
            return denuncias;
        }

        /// <summary>
        /// Retorna os detalhes de uma denúncia específica pelo seu ID
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// GET /api/denuncia/{id}
        /// </remarks>
        /// <param name="id">Id da denúncia</param>
        /// <response code="200">Retorna a denúncia solicitada</response>
        /// <response code="404">Denúncia não encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DenunciaResponse>> GetDenuncia(Guid id)
        {
            var denuncia = await _context.Denuncias
                .Include(d => d.Usuario)
                .Include(d => d.OrgaoPublico)
                .Include(d => d.Localizacao)
                    .ThenInclude(l => l.Bairro)
                        .ThenInclude(b => b.Cidade)
                            .ThenInclude(c => c.Estado)
                .FirstOrDefaultAsync(d => d.IdDenuncia == id);

            if (denuncia == null)
            {
                return NotFound();
            }

            return new DenunciaResponse
            {
                IdDenuncia = denuncia.IdDenuncia,
                Titulo = denuncia.Titulo,
                Descricao = denuncia.Descricao,
                DataDenuncia = denuncia.DataDenuncia,
                Status = denuncia.Status,
                Usuario = denuncia.Usuario.Nome,
                Localizacao = $"{denuncia.Localizacao.Logradouro}, {denuncia.Localizacao.Numero}",
                OrgaoPublico = denuncia.OrgaoPublico.Nome
            };
        }

        /// <summary>
        /// Cria uma nova denúncia no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// POST /api/denuncia
        /// Corpo da requisição:
        /// {
        ///     "idUsuario": "guid_usuario",
        ///     "idLocalizacao": "guid_localizacao",
        ///     "idOrgaoPublico": "guid_orgao",
        ///     "dataHora": "2025-06-02T16:37:52",
        ///     "descricao": "Lixo acumulado na rua"
        /// }
        /// </remarks>
        /// <param name="request">Dados da denúncia</param>
        /// <response code="201">Denúncia criada com sucesso</response>
        /// <response code="400">Dados inválidos na requisição</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DenunciaResponse>> PostDenuncia(DenunciaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuarios.FindAsync(request.IdUsuario);
            var localizacao = await _context.Localizacoes.FindAsync(request.IdLocalizacao);
            var orgaoPublico = await _context.OrgaosPublicos.FindAsync(request.IdOrgaoPublico);

            if (usuario == null || localizacao == null || orgaoPublico == null)
            {
                return BadRequest("Usuário, Localização ou Órgão Público não encontrado.");
            }

            var denuncia = Denuncia.Create(usuario, localizacao, orgaoPublico, request.Titulo, request.Descricao);
            _context.Denuncias.Add(denuncia);
            await _context.SaveChangesAsync();

            return new DenunciaResponse
            {
                IdDenuncia = denuncia.IdDenuncia,
                Titulo = denuncia.Titulo,
                Descricao = denuncia.Descricao,
                DataDenuncia = denuncia.DataDenuncia,
                Status = denuncia.Status,
                Usuario = usuario.Nome,
                Localizacao = $"{localizacao.Logradouro}, {localizacao.Numero}",
                OrgaoPublico = orgaoPublico.Nome
            };
        }

        /// <summary>
        /// Atualiza os dados de uma denúncia existente
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// PUT /api/denuncia/{id}
        /// Corpo da requisição:
        /// {
        ///     "idUsuario": "guid_usuario",
        ///     "idLocalizacao": "guid_localizacao",
        ///     "idOrgaoPublico": "guid_orgao",
        ///     "dataHora": "2025-06-02T16:37:52",
        ///     "descricao": "Descrição atualizada"
        /// }
        /// </remarks>
        /// <param name="id">Id da denúncia</param>
        /// <param name="request">Dados atualizados da denúncia</param>
        /// <response code="200">Denúncia atualizada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Denúncia não encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutDenuncia(Guid id, DenunciaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var denuncia = await _context.Denuncias.FindAsync(id);
            if (denuncia == null)
            {
                return NotFound();
            }

            denuncia.AtualizarDenuncia(request.Titulo, request.Descricao, request.Status);
            _context.Denuncias.Update(denuncia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Remove uma denúncia pelo Id
        /// </summary>
        /// <param name="id">Id da denúncia</param>
        /// <response code="204">Denúncia removida com sucesso</response>
        /// <response code="404">Denúncia não encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDenuncia(Guid id)
        {
            var denuncia = await _context.Denuncias.FindAsync(id);
            if (denuncia == null)
            {
                return NotFound();
            }

            _context.Denuncias.Remove(denuncia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}



   