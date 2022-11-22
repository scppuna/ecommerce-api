using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Localidade { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataModificacao { get; set; }
    }
}
