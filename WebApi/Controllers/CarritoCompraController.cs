using Core.Entities;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Errors;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class CarritoCompraController : BaseApiController
    {
        private readonly ICarritoCompraRepository _carritoRepository;
        private readonly UserManager<Usuario> _userManager;

        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly ICarritoCompraRepository _carritoCompraRepository;

        public CarritoCompraController(ICarritoCompraRepository carritoRepository,
            UserManager<Usuario> userManager, IGenericRepository<Producto> productoRepository,
            ICarritoCompraRepository carritoCompraRepository)
        {
            _carritoRepository = carritoRepository;
            _userManager = userManager;
            _productoRepository = productoRepository;
            _carritoCompraRepository = carritoCompraRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CarritoDeCompras>> GetCarritoById()
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);
            if (usuario == null)
            {
                return BadRequest(new CodeErrorResponse(404));
            }

            var carrito = await _carritoRepository.GetCarritoCompraAsync(usuario.Id);

            if (carrito != null)
            {
                //actulizar lista 
                var carroVerificado = await VerificarCarrito(usuario.Id);
                await UpdateCarritoCompra(carroVerificado);
                return Ok(carroVerificado ?? new CarritoDeCompras(usuario.Id));
                //
            }

            return Ok(carrito ?? new CarritoDeCompras(usuario.Id));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CarritoDeCompras>> UpdateCarritoCompra(CarritoDeCompras carritoParametro)
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);
            carritoParametro.Id = usuario.Id;
            await _carritoRepository.UpdateCarritoDeComprasAsync(carritoParametro);
            var carritoVerificado = await VerificarCarrito(carritoParametro.Id);
            return Ok(carritoVerificado ?? new CarritoDeCompras(usuario.Id));
        }

        [Authorize]
        [HttpDelete]

        public async Task DeleteCarritoCompora()
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);
            await _carritoRepository.DeleteCarritoComprasAsync(usuario.Id);
        }
        

        ///NUEVO
        public async Task<CarritoDeCompras> VerificarCarrito(string id)
        {

            var carritoCompra = await _carritoCompraRepository.GetCarritoCompraAsync(id);
            var itemsVerificados = new List<CarritoItem>();
            
            foreach (var item in carritoCompra.items)
            {
                var productoItem = await _productoRepository.GetByIdAsync(item.Id);
                if (productoItem != null)
                {
                    itemsVerificados.Add(new CarritoItem
                    {
                        Id = productoItem.Id,
                        NombreProducto = productoItem.Nombre,
                        Precio = productoItem.Precio,
                        Cantidad = item.Cantidad,
                        Imagen = productoItem.Imagen,
                        Marca = item.Marca,
                        Categoria = item.Categoria
                    });
                }
                
            }
            var carritoActualizado = new CarritoDeCompras
            {
                Id = id,
                items = itemsVerificados
            };
            
            return carritoActualizado;

        }
    }
}
