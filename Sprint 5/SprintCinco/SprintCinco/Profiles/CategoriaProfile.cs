using AutoMapper;
using SprintCinco.Data.Dtos.CategoriaDtos;
using SprintCinco.Data.Dtos.SubcategoriaDtos;
using SprintCinco.Models;

namespace SprintCinco.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateSubcategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>();
            CreateMap<UpdateCategoriaDto, Categoria>();
        }
    }
}
