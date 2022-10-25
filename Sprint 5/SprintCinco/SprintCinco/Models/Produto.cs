using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SprintCinco.Models
{
    public class Produto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(128, ErrorMessage = "Você excedeu o limite máximo de 128 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo descrição é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(512, ErrorMessage = "Você excedeu o limite máximo de 512 caracteres")]
        public string Descricao { get; set; }
        public bool? Status { get; set; } = true;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataEdicao { get; set; } = null;
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Valor { get; set; }
        public int Estoque { get; set; }
        public virtual CentroDistribuicao CentroDistribuicao { get; set; }
        public int CentroId { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
        public int SubcategoriaId { get; set; }


    }
}
