using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Produtos
{
    public class Produto
    { 
        public Categoria Categoria { get; set; }
        string nome;
        public string Name
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }

        public string description;
        public double weight;
        public double heigth;
        public double width;
        public double lenght;
        public int codigo;
        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public double amount;
        private string distribution = "Clearsale";
        public string Distribution
        {
            get
            {
                return distribution;
            }
        }
        public string status;
        public string dataCadastro;
        public string dataEdicao;

        public Produto(string _name, string _description, double _weight, double _heigth, double _width, double _lenght, double _price, double _amount, int _codigo, string _categoria, string _subcategoria)
        {
            Name = _name;
            description = _description;
            weight = _weight;
            heigth = _heigth;
            width = _width;
            lenght = _lenght;
            price = _price;
            amount = _amount;
            distribution = "Centro de Distribuição ClearTech";
            status = "Ativo";
            dataCadastro = DateTime.Now.ToString("dd-MM-yyyy - HH:mm");
            dataEdicao = null;
            codigo = _codigo;
            Categoria = new Categoria();
            Categoria.CategoryName = _categoria;
            Categoria.Subcategory = _subcategoria;
        }
    }
}

