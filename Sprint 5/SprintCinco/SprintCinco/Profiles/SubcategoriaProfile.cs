using AutoMapper;
using SprintCinco.Data.Dtos.SubcategoriaDtos;
using SprintCinco.Models;

namespace SprintCinco.Profiles
{
    public class SubcategoriaProfile : Profile
    {
        public SubcategoriaProfile()
        {
            CreateMap<CreateSubcategoriaDto, Subcategoria>();
            CreateMap<Subcategoria, ReadSubcategoriaDto>();
            CreateMap<UpdateSubcategoriaDto, Subcategoria>();
        }

    }
}
