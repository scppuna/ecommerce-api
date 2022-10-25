using SprintCinco.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SprintCinco.Data.Dtos.ProdutoDtos
{
    public class ReadProdutoDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(128, ErrorMessage = "Você excedeu o limite máximo de 128 caracteres")]
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCadastro { get; set; } 
        public DateTime? DataEdicao { get; set; } = null;
        [Required] public double Peso { get; set; }
        [Required] public double Altura { get; set; }
        [Required] public double Largura { get; set; }
        [Required] public double Comprimento { get; set; }
        [Required] public double Valor { get; set; }
        [Required] public int Estoque { get; set; }
        [Required] public double CentroDistribuicao { get; set; }

        public Subcategoria subcategoria { get; set; }
    }
}
