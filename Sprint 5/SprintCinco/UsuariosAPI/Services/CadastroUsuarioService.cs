using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dto.Usuario;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroUsuarioService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;

        public CadastroUsuarioService(IMapper mapper, UserManager<CustomIdentityUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result> EditarUsuario(int id, UpdateUsuarioDto updateUsuarioDto)
        {
            //NOME, DATANASCIMENTO, ENDEREÇO COMPLETO E EMAIL
            var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return Result.Fail("Usuário não encontrado");
            }

            var endereco = await PesquisaUsuarioCep(updateUsuarioDto.Cep);
            if(endereco.Cep == null || endereco.Cep == string.Empty)
            {
                return Result.Fail("CEP não pode ser nulo!");
            }
            usuario.Logradouro = endereco.Logradouro;
            usuario.Bairro = endereco.Bairro;
            usuario.UF = endereco.UF;
            usuario.Localidade = endereco.Localidade;
            usuario.Nome = updateUsuarioDto.Nome;
            usuario.Email = updateUsuarioDto.Email;
            usuario.DataNascimento = updateUsuarioDto.DataNascimento;
            usuario.DataModificacao = DateTime.Now;

            var mapeamento = _mapper.Map<CustomIdentityUser>(usuario);
            var resultado = _userManager.UpdateAsync(mapeamento);

            return Result.Ok();
        }
        //updateasync

        public async Task<List<ReadUsuarioDto>> PesquisarUsuarioComFiltro(string username, string cpf, string email, bool? status)
        {
            var usuarios = await _userManager.Users.ToListAsync();
            List<ReadUsuarioDto> listaUsuarioDto = new();

            foreach (var user in usuarios)
            {
                var readDto = _mapper.Map<ReadUsuarioDto>(user);
                listaUsuarioDto.Add(readDto);
            }
            if (username != null)
            {
                return listaUsuarioDto.Where(u => u.Username.ToLower().Contains(username.ToLower())).ToList();
            }
            if (cpf != null)
            {
                return listaUsuarioDto.Where(u => u.CPF.ToLower().Contains(cpf.ToLower())).ToList();
            }
            if (email != null)
            {
                return listaUsuarioDto.Where(u => u.Email.ToLower().Contains(email.ToLower())).ToList();
            }
            if (status != null)
            {
                return listaUsuarioDto.Where(u => u.Status == status).ToList();
            }
            return listaUsuarioDto;
        }

        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public async Task<Usuario> PesquisaUsuarioCep(string cep)
        {
            HttpClient client = new HttpClient();
            var resposta = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var conteudo = await resposta.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<Usuario>(conteudo);
            return endereco;
        }

        public static bool ValidaDataDeNascimento(CreateUsuarioDto createUsuarioDto)
        {
            if (createUsuarioDto.DataNascimento > DateTime.Now) { return false; }
            return true;
        }

        public async Task<Result> CadastrarUsuario(CreateUsuarioDto createDto)
        {
            if (ValidaCPF(createDto.CPF) == false)
            {
                return Result.Fail("Não foi possível cadastrar usuário devido CPF incorreto");
            }

            if (ValidaDataDeNascimento(createDto) == false)
            {
                return Result.Fail("Data de nascimento incorreta, favor verificar");
            }

            var endereco = await PesquisaUsuarioCep(createDto.Cep);
            if (endereco == null)
            {
                return Result.Fail("Endereço não pode ser nulo");
            }

            Usuario usuario = _mapper.Map<Usuario>(createDto);
            usuario.Logradouro = endereco.Logradouro;
            usuario.Bairro = endereco.Bairro;
            usuario.Localidade = endereco.Localidade;
            usuario.UF = endereco.UF;

            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            var resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);

            //var usuarioRoleResult = _userManager
            //    .AddToRoleAsync(usuarioIdentity, "admin").Result;
            
            //var createRoleResult = _roleManager
            //    .CreateAsync(new IdentityRole<int>("admin")).Result;

            if (resultadoIdentity.Result.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao cadastrar usuário");
        }

    }
}
