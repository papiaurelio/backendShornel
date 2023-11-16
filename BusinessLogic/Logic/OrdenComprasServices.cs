using Core.Entities;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class OrdenComprasServices : IOrdenCompraServices
    {

        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrdenComprasServices(ICarritoCompraRepository carritoCompraRepository, IUnitOfWork unitOfWork)
        {

            _carritoCompraRepository = carritoCompraRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrdenCompras> AddOrdenCompraAsync(string idComprador, string emailComprador, bool envio)
        {

            var carritoCompra = await _carritoCompraRepository.GetCarritoCompraAsync(idComprador);
            var items = new List<OrdenItem>();

            if(carritoCompra == null)
            {
                return null;
            }
            foreach (var item in carritoCompra.items)
            {
                var productoItem = await _unitOfWork.Respository<Producto>().GetByIdAsync(item.Id);
                if (productoItem != null)
                {
                    var itemOrdenado = new ProductoOrdenado(productoItem.Id, productoItem.Nombre, productoItem.Imagen);
                    var ordenItem = new OrdenItem(itemOrdenado, productoItem.Precio, item.Cantidad);
                    items.Add(ordenItem);
                }

            }   

            var subtotal = items.Sum(item => item.Precio * item.cantidad);

            var ordenCompra = new OrdenCompras(idComprador, emailComprador, envio, items, subtotal);

            _unitOfWork.Respository<OrdenCompras>().AddEntity(ordenCompra);
            var resultado = await _unitOfWork.Complete();

            if (resultado <= 0)
            {
                return null;
            }

            await _carritoCompraRepository.DeleteCarritoComprasAsync(idComprador);

            return ordenCompra;
       

        }

        public async Task<OrdenCompras> GetOrdenCompraByIdAsync(int id, string email)
        {
            var spec = new OrdenCompraSpecification(id, email);

            return await _unitOfWork.Respository<OrdenCompras>().GetByIdWithSpec(spec);
        }

        public async Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasByUserEmailAsync(string email)
        {
            var spec = new OrdenCompraSpecification(email);

            return await _unitOfWork.Respository<OrdenCompras>().GetAllWithSpec(spec);

        }
    }
}
