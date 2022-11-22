using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dto.Usuario;
using UsuariosAPI.Models;

namespace UsuariosAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<CustomIdentityUser, ReadUsuarioDto>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
            
        }
    }
}
