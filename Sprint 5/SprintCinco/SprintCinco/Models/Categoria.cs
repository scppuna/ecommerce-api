using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SprintCinco.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÁ-Ù' '\s]{1,40}$", ErrorMessage = "Somente letras são aceitas.")]
        [StringLength(128, ErrorMessage = "Você excedeu o limite máximo de 128 caracteres")]
        public string Nome { get; set; }
        public bool? Status { get; set; } = true;
        public string Datacriacao { get; set; } = DateTime.Now.ToString("dd-MM-yy - HH:mm:ss");
        public string DataEdicao { get; set; } = null;

        [JsonIgnore]
        public virtual List<Subcategoria> Subcategoria { get; set; }
    }
}
