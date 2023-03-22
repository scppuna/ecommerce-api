﻿using System;
using System.ComponentModel.DataAnnotations;

namespace IEcommerceAPI.Data.Dtos.CategoriaDtos
{
    public class UpdateCategoriaDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(128, ErrorMessage = "Você excedeu o limite máximo de 128 caracteres")]
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime DataEdicao { get; set; } = DateTime.Now;
    }
}
