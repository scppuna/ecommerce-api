using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false); //alteração para tentativa de login e bloqueio
            if (resultadoIdentity.Result.IsNotAllowed)
            {
                return Result.Fail("Login Falhou");
            }

            var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(UsuariosAPI =>
                    UsuariosAPI.NormalizedUserName == request.Username.ToUpper());
            Token token = _tokenService.CreateToken(identityUser);
            return Result.Ok().WithSuccess(token.Value);
        }
    }
}
