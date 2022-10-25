using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos
{
    public class Categoria
    {
        private string? _categoryName;
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                _categoryName = value;
            }
        }
        private string? _subcategory;
        public string Subcategory
        {
            get
            {
                return _subcategory;
            }
            set
            {
                _subcategory = value;

            }
        }
    }
}
