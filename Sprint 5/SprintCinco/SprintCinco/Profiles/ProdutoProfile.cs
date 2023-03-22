using AutoMapper;
using IEcommerceAPI.Data.Dtos.ProdutoDtos;
using IEcommerceAPI.Models;

namespace IEcommerceAPI.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CreateProdutoDto, Produto>();
            CreateMap<Produto, ReadProdutoDto>();
            CreateMap<UpdateProdutoDto, Produto>();
        }

    }
}
