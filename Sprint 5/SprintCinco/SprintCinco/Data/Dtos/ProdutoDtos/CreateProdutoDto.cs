using SprintCinco.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SprintCinco.Data.Dtos.ProdutoDtos
{
    public class CreateProdutoDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(128, ErrorMessage = "Você excedeu o limite máximo de 128 caracteres")]
        public string Nome { get; set; }
        public int SubcategoriaId { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(512, ErrorMessage = "Você excedeu o limite máximo de 512 caracteres")]
        public string Descricao { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCadastro { get; set; }

        //public DateTime? DataEdicao { get; set; } = null;
        [Required(ErrorMessage = "O campo Peso é obrigatório.")] public double Peso { get; set; }
        [Required(ErrorMessage = "O campo Altura é obrigatório.")] public double Altura { get; set; }
        [Required(ErrorMessage = "O campo Largura é obrigatório.")] public double Largura { get; set; }
        [Required(ErrorMessage = "O campo Comprimento é obrigatório.")] public double Comprimento { get; set; }
        [Required(ErrorMessage = "O campo Valor é obrigatório.")] public double Valor { get; set; }
        [Required(ErrorMessage = "O campo Estoque é obrigatório.")] public int Estoque { get; set; }
        public int CentroId { get; set; }
    }
}
