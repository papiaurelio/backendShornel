using Core.Entities.OrdenCompra;

namespace WebApi.Dtos
{
    public class OrdenItemResponseDto
    {
        public int ProductoId { get; set; }
        public string ProductoName { get; set; }
        public string ProductoImagen { get; set; }
        public decimal Precio { get; set; }
        public int cantidad { get; set; }
    }
}
