using Microsoft.AspNetCore.Mvc;
using ResgateAlerta.Domain.Enums;
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
    [Tags("Usuários")]
    public class UsuarioController : ControllerBase
    {
        private readonly ResgateAlertaContext _context;

        public UsuarioController(ResgateAlertaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todos os usuários cadastrados
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// GET /api/usuarios
        /// </remarks>
        /// <response code="200">Lista de usuários retornada com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetUsuarios()
        {
            var usuariosDto = await _context.Usuarios
                .Select(u => new UsuarioResponse
                {
                    IdUsuario = u.IdUsuario,
                    Nome = u.Nome,
                    Email = u.Email,
                    TipoUsuario = u.TipoUsuario.ToString()
                })
                .ToListAsync();

            return Ok(usuariosDto);
        }

        /// <summary>
        /// Retorna os dados de um usuário específico pelo ID
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <remarks>
        /// Exemplo de solicitação:
        /// GET /api/usuarios/{id}
        /// </remarks>
        /// <response code="200">Usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UsuarioResponse>> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var dto = new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario.ToString()
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cria um novo usuário no sistema
        /// </summary>
        /// <param name="request">Dados do usuário a ser criado</param>
        /// <remarks>
        /// Exemplo de solicitação:
        /// POST /api/usuarios
        /// </remarks>
        /// <response code="201">Usuário criado com sucesso</response>
        /// <response code="400">Email já cadastrado</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UsuarioResponse>> PostUsuario(UsuarioRequest request)
        {
            // Verificar se o email já está cadastrado
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            // Se o usuário já existe, retorna um erro com status 400
            if (existingUser != null)
            {
                return BadRequest("Email já cadastrado.");
            }

            // Criar o novo usuário
            var usuario = Usuario.Create(request.Nome, request.Email, request.Senha,
                Enum.TryParse(request.TipoUsuario, true, out TipoUsuario tipo) ? tipo : TipoUsuario.USER);

            // Adicionar o usuário ao banco de dados
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Criar a resposta com os dados do usuário
            var response = new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario.ToString()
            };

            // Retorna a resposta com o usuário criado
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, response);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="request">Dados atualizados do usuário</param>
        /// <remarks>
        /// Exemplo de solicitação:
        /// PUT /api/usuarios/{id}
        /// </remarks>
        /// <response code="200">Usuário atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UsuarioResponse>> PutUsuario(Guid id, UsuarioRequest request)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            usuario.AtualizarUsuario(request.Nome, request.Email, request.Senha,
                Enum.TryParse(request.TipoUsuario, true, out TipoUsuario tipo) ? tipo : TipoUsuario.USER);

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            var response = new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario.ToString()
            };

            return Ok(response);
        }


        /// <summary>
        /// Remove um usuário do sistema pelo ID
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <remarks>
        /// Exemplo de solicitação:
        /// DELETE /api/usuarios/{id}
        /// </remarks>
        /// <response code="204">Usuário deletado com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}