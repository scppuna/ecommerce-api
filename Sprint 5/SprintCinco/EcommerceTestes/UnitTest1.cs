using IEcommerceAPI.Data.Dtos.CategoriaDtos;
using System;
using Xunit;

namespace EcommerceTestes
{
    public class UnitTest1
    {
        [Fact]
        public void TestaCadastrarCategoria()
        {
            var categoria = new CreateCategoriaDto();
            {
               /// Nome = "Frutas"
            };

        }
    }
}


//Nome da categoria (obrigat�rio - 128 caracteres - somente alfabeto)
//Status booleano
//Data de cria��o
//Data da altera��o
