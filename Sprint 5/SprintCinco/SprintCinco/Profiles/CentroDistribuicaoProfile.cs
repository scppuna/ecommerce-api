using AutoMapper;
using IEcommerceAPI.Data.Dtos.CentroDistribuicaoDtos;
using IEcommerceAPI.Models;

namespace IEcommerceAPI.Profiles
{
    public class CentroDistribuicaoProfile : Profile
    {

        public CentroDistribuicaoProfile()
        {
            CreateMap<CreateCDDto, CentroDistribuicao>();
            CreateMap<CentroDistribuicao, ReadCDDto>();
            CreateMap<UpdateCDDto, CentroDistribuicao>();

        }
    }
}
