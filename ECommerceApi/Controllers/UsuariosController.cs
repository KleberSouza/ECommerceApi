using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Services;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using ECommerceApi.Models.Entities;
using BC = BCrypt.Net.BCrypt;

namespace ECommerceApi.Controllers
{
    public class UsuariosController : BaseController<Usuario, IUsuarioService>
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _userService;

        public UsuariosController(IUsuarioService service, IMapper mapper) : base(service)
        {
            _mapper = mapper;
            _userService = service;
        }

        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public override async Task<ActionResult<PagedResultDto<Usuario>>> GetAll(int page = 1, int pageSize = 10)
        {
            return await base.GetAll(page, pageSize);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public override async Task<ActionResult<Usuario>> GetById(int id)
        {
            return await base.GetById(id);
        }

        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public override async Task<ActionResult<Usuario>> Create([FromBody] Usuario entity)
        {
            entity.Senha = BC.HashPassword(entity.Senha);
            return await base.Create(entity);
        }

        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public override async Task<ActionResult<Usuario>> Update(int id, [FromBody] Usuario entity)
        {
            return await base.Update(id, entity);
        }

        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Usuario>> Register([FromBody] UsuarioDto usuario)
        {
            try
            {
                var entity = _mapper.Map<Usuario>(usuario);
                entity.Senha = BC.HashPassword(entity.Senha);
                await _userService.RegisterAsync(entity);
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, GenerateHateoasLinks(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto usuario)
        {
            try
            {
                string token = await _userService.Authenticate(usuario.Email, usuario.Senha);

                if (token == null)
                {
                    return Unauthorized("E-mail e/ou Senha inválidos.");
                }

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
