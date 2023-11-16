using System;
using System.Collections.Generic;

namespace WebApi.Dtos
{
    public class OrdenCompraResponseDto
    {
        public string Id { get; set; }
        public string CorreoComprador { get; set; }
        public DateTimeOffset FechaOrdenCompra { get; set; } = DateTimeOffset.Now;
        public bool Envio { get; set; } = false;

        public IReadOnlyList<OrdenItemResponseDto> OrderItems { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } 
    }
}
