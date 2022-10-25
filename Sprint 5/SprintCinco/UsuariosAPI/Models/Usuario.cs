using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Cpf { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public int Numero { get; set; }

        public string Complemento { get; set; }

        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataModificacao { get; set; } = DateTime.Now;
    }
}
