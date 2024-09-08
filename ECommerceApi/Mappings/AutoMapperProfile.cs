using AutoMapper;
using ECommerceApi.Models;
using ECommerceApi.Models.Entities;

namespace UserService.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<CarrinhoProdutos, PedidoProdutos>().ReverseMap();
        }
    }
}