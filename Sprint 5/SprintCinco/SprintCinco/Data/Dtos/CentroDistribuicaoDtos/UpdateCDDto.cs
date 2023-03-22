using System;

namespace IEcommerceAPI.Data.Dtos.CentroDistribuicaoDtos
{
    public class UpdateCDDto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataEdicao { get; set; } = DateTime.Now;
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }
    }
}
