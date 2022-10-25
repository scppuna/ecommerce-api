using System;

namespace SprintCinco.Data.Dtos.CentroDistribuicaoDtos
{
    public class ReadCDDto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEdicao { get; set; }
        public string Localidade { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }

    }
}
