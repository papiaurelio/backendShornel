namespace WebApi.Dtos
{
    public class OrdenComprasDto
    {
        public string CarritoCompraId { get; set; }
        public bool Envio { get; set; }
        public DireccionDto Direccion { get; set; }
    }
}
