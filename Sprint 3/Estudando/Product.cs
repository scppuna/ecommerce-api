using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Estudando
{
    public class Product
    {
        public string produto;
        public string categoria;
        public string subcategoria;
        public string status;
        public string dataCadastro;
        public string dataEdicao;
        //public int codigo;
        public int Codigo { get; set; }

        public Product(string _produto, string _categoria, string _subcategoria, int _codigo)
        {
            produto = _produto;
            categoria = _categoria;
            subcategoria = _subcategoria;
            dataCadastro = DateTime.Now.ToString("dd-MM-yyyy - HH:mm");
            dataEdicao = null;
            status = "ativo";
            Codigo = _codigo;
        }
    }
}

