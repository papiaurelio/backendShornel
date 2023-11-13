using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class CarritoCompraController : BaseApiController
    {
        private readonly ICarritoCompraRepository _carritoRepository;
        private readonly UserManager<Usuario> _userManager;
        public CarritoCompraController(ICarritoCompraRepository carritoRepository,
            UserManager<Usuario> userManager)
        {
            _carritoRepository = carritoRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CarritoDeCompras>> GetCarritoById()
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);
            
            var carrito = await _carritoRepository.GetCarritoCompraAsync(usuario.Id);

            return Ok(carrito ?? new CarritoDeCompras(usuario.Id));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CarritoDeCompras>> UpdateCarritoCompra(CarritoDeCompras carritoParametro)
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);
            carritoParametro.Id = usuario.Id;
            var carritoActualizado = await _carritoRepository.UpdateCarritoDeComprasAsync(carritoParametro);
            return Ok(carritoActualizado);
        }

        [Authorize]
        [HttpDelete]

        public async Task DeleteCarritoCompora()
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);
            await _carritoRepository.DeleteCarritoComprasAsync(usuario.Id);
        }
    }
}
