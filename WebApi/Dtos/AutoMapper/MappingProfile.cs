using AutoMapper;
using Core.Entities;
using Core.Entities.OrdenCompra;

namespace WebApi.Dtos.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Producto, ProductoDto>()
                .ForMember(p => p.CategoriaNombre, x => x.MapFrom(a => a.Categoria.Nombre))
                .ForMember(p => p.MarcaNombre, x => x.MapFrom(a => a.Marca.Nombre));

            CreateMap<Direccion, DireccionDto>().ReverseMap();
            
            CreateMap<Usuario, UsuarioDto>().ReverseMap();



            CreateMap<OrdenCompras, OrdenCompraResponseDto>()
                .ForMember(o => o.Status, x=> x.MapFrom(y => y.Status.ToString()));

            CreateMap<OrdenItem, OrdenItemResponseDto>()
                .ForMember(o => o.ProductoId, x => x.MapFrom(y => y.ProductoOrdenado.ProductoOrdenadoId))
                .ForMember(o => o.ProductoName, x => x.MapFrom(y => y.ProductoOrdenado.ProductoNombre))
                .ForMember(o => o.ProductoImagen, x => x.MapFrom(y => y.ProductoOrdenado.ImagenUrl));
        }
    }
}
