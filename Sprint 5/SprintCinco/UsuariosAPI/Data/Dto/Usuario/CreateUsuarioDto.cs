using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dto.Usuario
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Repassword { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Cep { get; set; }

        //public string Logradouro { get; set; }
        //public string Localidade { get; set; }
        //public string Bairro { get; set; }

        [Required]
        public int Numero { get; set; }

        //public string Complemento { get; set; }
        //public string UF { get; set; }

        //public bool Status { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}

