using AutoMapper;
using EcommerceAPI.Data.Repository;
using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;
using IEcommerceAPI.Models;
using IEcommerceAPI.Repository;

namespace IEcommerceAPI.Profiles
{
    public class SubcategoriaProfile : Profile
    {
        public SubcategoriaProfile()
        {
            CreateMap<CreateSubcategoriaDto, Subcategoria>();
            CreateMap<Subcategoria, ReadSubcategoriaDto>();
            CreateMap<UpdateSubcategoriaDto, Subcategoria>();
            CreateMap<SubcategoriaRepository, Subcategoria>();
        }

    }
}
