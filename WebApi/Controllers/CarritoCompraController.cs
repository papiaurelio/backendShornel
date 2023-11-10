﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CarritoCompraController : BaseApiController
    {
        private readonly ICarritoCompraRepository _carritoRepository;

        public CarritoCompraController(ICarritoCompraRepository carritoRepository)
        {
            _carritoRepository = carritoRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CarritoDeCompras>> GetCarritoById(string id)
        {
            var carrito = await _carritoRepository.GetCarritoCompraAsync(id);

            return Ok(carrito ?? new CarritoDeCompras(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CarritoDeCompras>> UpdateCarritoCompra(CarritoDeCompras carritoParametro)
        {
           var carritoActualizado = await _carritoRepository.UpdateCarritoDeComprasAsync(carritoParametro);
            return Ok(carritoActualizado);
        }

        [Authorize]
        [HttpDelete]

        public async Task DeleteCarritoCompora(string id)
        {
           await _carritoRepository.DeleteCarritoComprasAsync(id);
        }
    }
}
