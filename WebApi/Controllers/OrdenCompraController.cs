using AutoMapper;
using Core.Entities;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Authorize]
    public class OrdenCompraController : BaseApiController
    {
        private readonly IOrdenCompraServices _ordenCompraServices;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        public OrdenCompraController(IOrdenCompraServices ordenCompraServices, IMapper mapper, 
            UserManager<Usuario> userManager)
        {
            _userManager = userManager;
            _ordenCompraServices = ordenCompraServices;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrdenCompras>> AddOrdenCompra(OrdenComprasDto ordenCompraDto)
        {
            var usuario = await _userManager.BuscarUsarioById(User);
            var email = usuario.Email;
            var id = usuario.Id;

            var direccion = _mapper.Map<DireccionDto, Core.Entities.OrdenCompra.Direccion>(ordenCompraDto.Direccion);

            var ordenCompra = await _ordenCompraServices.AddOrdenCompraAsync(id, email, ordenCompraDto.Envio, direccion);

            if(ordenCompra == null)
            {
                return BadRequest(new CodeErrorResponse(400, "Error en la orden de compra"));
            }

            return Ok(ordenCompra);
        }
    }
}
