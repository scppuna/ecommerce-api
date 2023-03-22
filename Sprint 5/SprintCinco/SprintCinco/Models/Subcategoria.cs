using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IEcommerceAPI.Models
{
    public class Subcategoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public bool Status { get; set; } = true;
        public DateTime DataCadastro { get; set; }

        public DateTime DataEdicao { get; set; }

        public virtual Categoria Categoria { get; set; }

        public int CategoriaId { get; set; }

        [JsonIgnore]
        public virtual List<Produto> Produto {get; set;}
    }
}
