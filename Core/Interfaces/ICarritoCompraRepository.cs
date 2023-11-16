using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICarritoCompraRepository
    {
        Task<CarritoDeCompras> GetCarritoCompraAsync(string carritoId);
        Task<CarritoDeCompras> UpdateCarritoDeComprasAsync(CarritoDeCompras carritoDeCompras);

        Task<bool> DeleteCarritoComprasAsync(string carritoId);

    }
}
