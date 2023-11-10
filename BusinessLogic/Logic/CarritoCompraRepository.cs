using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class CarritoCompraRepository : ICarritoCompraRepository
    {
        private readonly IDatabase _database;

        public CarritoCompraRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCarritoComprasAsync(string carritoId)
        {
            return await _database.KeyDeleteAsync(carritoId);
        }

        public async Task<CarritoDeCompras> GetCarritoCompraAsync(string carritoId)
        {
            var data= await _database.StringGetAsync(carritoId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CarritoDeCompras>(data);
        }

        public async Task<CarritoDeCompras> UpdateCarritoDeComprasAsync(CarritoDeCompras carritoDeCompras)
        {
            var status = await _database.StringSetAsync(carritoDeCompras.Id, 
                JsonSerializer.Serialize(carritoDeCompras), TimeSpan.FromDays(30));

            if (!status) return null;
            return await GetCarritoCompraAsync(carritoDeCompras.Id);

        }
    }
}
