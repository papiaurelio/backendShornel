using AutoMapper;
using Core.Entities;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult<OrdenCompraResponseDto>> AddOrdenCompra(OrdenComprasDto ordenCompraDto)
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);

            if (usuario  == null)
            {
                return BadRequest(new CodeErrorResponse(400, "Error en la orden de compra, error al validar usuario "));
            }
            var email = usuario.Email;
            var id = usuario.Id;

            var ordenCompra = await _ordenCompraServices.AddOrdenCompraAsync(id, email, ordenCompraDto.Envio);

            if(ordenCompra == null)
            {
                return BadRequest(new CodeErrorResponse(400, "Error en la orden de compra, rebice sus productos"));
            }

            return Ok(_mapper.Map<OrdenCompras, OrdenCompraResponseDto>(ordenCompra));
        }

        [HttpGet("my-orders")]
        public async Task<ActionResult<IReadOnlyList<OrdenCompraResponseDto>>> GetOrdenCompras()
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);
            var ordenCompras = await _ordenCompraServices.GetOrdenComprasByUserEmailAsync(usuario.Email);
            //return Ok(ordenCompras);

            return Ok(_mapper.Map<IReadOnlyList<OrdenCompras>, IReadOnlyList< OrdenCompraResponseDto>>(ordenCompras));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompraResponseDto>> GetOrdenComprasById(int id)
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);
            var ordenCompra = await _ordenCompraServices.GetOrdenCompraByIdAsync(id, usuario.Email);
            if(ordenCompra== null)
            {
                return NotFound(new CodeErrorResponse(404, "No se encontraron ordenes de compra"));
            }

            //return ordenCompra;

            return _mapper.Map< OrdenCompras, OrdenCompraResponseDto > (ordenCompra);
        }
    }
}
