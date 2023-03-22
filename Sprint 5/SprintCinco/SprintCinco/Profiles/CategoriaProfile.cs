using AutoMapper;
using IEcommerceAPI.Repository;
using IEcommerceAPI.Data.Dtos.CategoriaDtos;
using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;
using IEcommerceAPI.Models;

namespace IEcommerceAPI.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>();
            CreateMap<UpdateCategoriaDto, Categoria>();
            CreateMap<CategoriaRepository, Categoria>();
        }
    }
}
