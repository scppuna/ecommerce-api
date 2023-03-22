using System;
using System.ComponentModel.DataAnnotations;

namespace IEcommerceAPI.Data.Dtos.ProdutoDtos
{
    public class UpdateProdutoDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(128, ErrorMessage = "Você excedeu o limite máximo de 128 caracteres")]
        public string Nome { get; set; }
        public DateTime? DataEdicao { get; set; }
        [Required] public double Peso { get; set; }
        [Required] public double Altura { get; set; }
        [Required] public double Largura { get; set; }
        [Required] public double Comprimento { get; set; }
        [Required] public double Valor { get; set; }
        [Required] public int Estoque { get; set; }
        [Required] public double CentroDistribuicao { get; set; }
    }
}
