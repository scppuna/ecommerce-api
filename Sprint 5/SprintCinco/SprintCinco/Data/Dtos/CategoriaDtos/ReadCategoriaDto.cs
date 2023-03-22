using System;
using System.ComponentModel.DataAnnotations;

namespace IEcommerceAPI.Data.Dtos.CategoriaDtos
{
    public class ReadCategoriaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(128, ErrorMessage = "Você excedeu o limite máximo de 128 caracteres")]
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime? DataEdicao { get; set; } = null;
    }
}
