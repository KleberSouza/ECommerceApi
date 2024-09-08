using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Services;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using ECommerceApi.Models.Entities;

namespace ECommerceApi.Controllers
{
    public class ProdutosController : BaseController<Produto, IProdutoService>
    {
        public ProdutosController(IProdutoService service) : base(service)
        {

        }

        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public override async Task<ActionResult<Produto>> Create([FromBody] Produto entity)
        {
            return await base.Create(entity);
        }

        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public override async Task<ActionResult<Produto>> Update(int id, [FromBody] Produto entity)
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
    }
}
