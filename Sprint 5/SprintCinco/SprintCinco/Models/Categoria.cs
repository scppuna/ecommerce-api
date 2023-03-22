using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IEcommerceAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool? Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataEdicao { get; set; } = null;

        [JsonIgnore]
        public virtual List<Subcategoria> Subcategoria { get; set; }
    }
}
