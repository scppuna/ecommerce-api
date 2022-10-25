using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dto.Usuario
{
    public class CreateUsuarioDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(250, ErrorMessage = "Você excedeu o limite máximo de 250 caracteres")]
        public string Name { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        [RegularExpression(@"^\d+$")]
        [StringLength(11, ErrorMessage = "Você excedeu o limíte máximo de 11 caracteres")]
        public string Cpf { get; set; }

        [Required]
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        [Required]
        public int Numero { get; set; }

        public string Complemento { get; set; }

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

        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
