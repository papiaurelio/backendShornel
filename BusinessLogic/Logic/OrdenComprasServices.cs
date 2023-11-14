using Core.Entities;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class OrdenComprasServices : IOrdenCompraServices
    {
        private readonly IGenericRepository<OrdenCompras> _ordenComprasRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly ICarritoCompraRepository _carritoCompraRepository;

        public OrdenComprasServices(IGenericRepository<OrdenCompras> ordenComprasRepository, IGenericRepository<Producto> productoRepository, ICarritoCompraRepository carritoCompraRepository)
        {
            _ordenComprasRepository = ordenComprasRepository;
            _productoRepository = productoRepository;
            _carritoCompraRepository = carritoCompraRepository;
        }

        public async Task<OrdenCompras> AddOrdenCompraAsync(string idComprador, string emailComprador, bool envio,
            Core.Entities.OrdenCompra.Direccion direccion)
        {
            var carritoCompra = await _carritoCompraRepository.GetCarritoCompraAsync(idComprador);
            var items = new List<OrdenItem>();
            foreach (var item in carritoCompra.items)
            {
                var productoItem = await _productoRepository.GetByIdAsync(item.Id);
                var itemOrdenado = new ProductoOrdenado(productoItem.Id, productoItem.Nombre, productoItem.Imagen);
                var ordenItem = new OrdenItem(itemOrdenado, productoItem.Precio, item.Cantidad);
                items.Add(ordenItem);
            }

          
            if (!envio == true)
            {
                direccion = null;
            }

            var subtotal = items.Sum(item => item.Precio * item.cantidad);

            var ordenCompra = new OrdenCompras(idComprador, emailComprador, envio, direccion, items, subtotal);

            return ordenCompra;


        }

        public Task<OrdenCompras> GetOrdenCompraByIdAsync(int id, string email)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasByUserEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
