using AutoMapper;
using SprintCinco.Data.Dtos.CentroDistribuicaoDtos;
using SprintCinco.Models;

namespace SprintCinco.Profiles
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
