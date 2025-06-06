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
                .ToListAsync();

            var denunciasDto = denuncias.Select(d => new DenunciaResponse
            {
                IdDenuncia = d.IdDenuncia,
                DataHora = d.DataHora,
                Descricao = d.Descricao,
                NomeUsuario = d.Usuario?.Nome,
                NomeOrgaoPublico = d.OrgaoPublico?.Nome,
                Logradouro = d.Localizacao?.Logradouro,
                Numero = d.Localizacao?.Numero,
                Bairro = d.Localizacao?.Bairro?.Nome,
                Cidade = d.Localizacao?.Bairro?.Cidade?.Nome,
                Estado = d.Localizacao?.Bairro?.Cidade?.Estado?.Nome
            }).ToList();

            return Ok(denunciasDto);
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
                return NotFound();

            var dto = new DenunciaResponse
            {
                IdDenuncia = denuncia.IdDenuncia,
                DataHora = denuncia.DataHora,
                Descricao = denuncia.Descricao,
                NomeUsuario = denuncia.Usuario?.Nome,
                NomeOrgaoPublico = denuncia.OrgaoPublico?.Nome,
                Logradouro = denuncia.Localizacao?.Logradouro,
                Numero = denuncia.Localizacao?.Numero,
                Bairro = denuncia.Localizacao?.Bairro?.Nome,
                Cidade = denuncia.Localizacao?.Bairro?.Cidade?.Nome,
                Estado = denuncia.Localizacao?.Bairro?.Cidade?.Estado?.Nome
            };

            return Ok(dto);
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
            var usuario = await _context.Usuarios.FindAsync(request.IdUsuario);
            if (usuario == null) return BadRequest("Usuário não encontrado.");

            var localizacao = await _context.Localizacoes.FindAsync(request.IdLocalizacao);
            if (localizacao == null) return BadRequest("Localização não encontrada.");

            var orgao = await _context.OrgaosPublicos.FindAsync(request.IdOrgaoPublico);
            if (orgao == null) return BadRequest("Órgão público não encontrado.");

            var denuncia = Denuncia.Create(request.IdUsuario, request.IdLocalizacao, request.IdOrgaoPublico, request.DataHora, request.Descricao);

            _context.Denuncias.Add(denuncia);
            await _context.SaveChangesAsync();

            var response = new DenunciaResponse
            {
                IdDenuncia = denuncia.IdDenuncia,
                DataHora = denuncia.DataHora,
                Descricao = denuncia.Descricao
            };

            return CreatedAtAction(nameof(GetDenuncia), new { id = denuncia.IdDenuncia }, response);
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
        public async Task<ActionResult<DenunciaResponse>> PutDenuncia(Guid id, DenunciaRequest request)
        {
            var denuncia = await _context.Denuncias.FindAsync(id);
            if (denuncia == null)
                return NotFound();

            var usuario = await _context.Usuarios.FindAsync(request.IdUsuario);
            if (usuario == null) return BadRequest("Usuário não encontrado.");

            var localizacao = await _context.Localizacoes.FindAsync(request.IdLocalizacao);
            if (localizacao == null) return BadRequest("Localização não encontrada.");

            var orgao = await _context.OrgaosPublicos.FindAsync(request.IdOrgaoPublico);
            if (orgao == null) return BadRequest("Órgão público não encontrado.");

            denuncia.AtualizaDenuncia(request.IdUsuario, request.IdLocalizacao, request.IdOrgaoPublico, request.DataHora, request.Descricao);

            await _context.SaveChangesAsync();

            //  includes necessários
            var denunciaAtualizada = await _context.Denuncias
                .Include(d => d.Usuario)
                .Include(d => d.OrgaoPublico)
                .Include(d => d.Localizacao)
                    .ThenInclude(l => l.Bairro)
                        .ThenInclude(b => b.Cidade)
                            .ThenInclude(c => c.Estado)
                .FirstOrDefaultAsync(d => d.IdDenuncia == id);

            var response = new DenunciaResponse
            {
                IdDenuncia = denunciaAtualizada.IdDenuncia,
                DataHora = denunciaAtualizada.DataHora,
                Descricao = denunciaAtualizada.Descricao,
                NomeUsuario = denunciaAtualizada.Usuario.Nome,
                NomeOrgaoPublico = denunciaAtualizada.OrgaoPublico.Nome,
                Logradouro = denunciaAtualizada.Localizacao.Logradouro,
                Numero = denunciaAtualizada.Localizacao.Numero,
                Bairro = denunciaAtualizada.Localizacao.Bairro.Nome,
                Cidade = denunciaAtualizada.Localizacao.Bairro.Cidade.Nome,
                Estado = denunciaAtualizada.Localizacao.Bairro.Cidade.Estado.Nome
            };

            return Ok(response);
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



   