﻿using Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrdenCompraServices
    {
        Task<OrdenCompras> AddOrdenCompraAsync(string idComprador, string emailComprador,
            bool envio);

        Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasByUserEmailAsync(string email);

        Task<OrdenCompras> GetOrdenCompraByIdAsync(int id, string email);


    }
}