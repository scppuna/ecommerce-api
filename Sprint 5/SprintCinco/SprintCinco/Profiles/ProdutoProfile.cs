using AutoMapper;
using SprintCinco.Data.Dtos.ProdutoDtos;
using SprintCinco.Models;

namespace SprintCinco.Profiles
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
