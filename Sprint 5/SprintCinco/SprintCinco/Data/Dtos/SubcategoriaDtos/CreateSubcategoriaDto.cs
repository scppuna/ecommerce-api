using System;
using System.ComponentModel.DataAnnotations;

namespace SprintCinco.Data.Dtos.SubcategoriaDtos
{
    public class CreateSubcategoriaDto
    {
        //Mapeamento de dados dentro do banco de dados.

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(128, ErrorMessage = "Você excedeu o limite máximo de 128 caracteres")]
        public string Nome { get; set; }
        public int CategoriaID { get; set; }
    }
}
