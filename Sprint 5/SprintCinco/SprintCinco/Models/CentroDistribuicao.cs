using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace IEcommerceAPI.Models
{
    public class CentroDistribuicao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public bool? Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEdicao { get; internal set; }
        public int Numero { get; set; }

        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }

    }
}
