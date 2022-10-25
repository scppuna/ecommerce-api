using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Estudando
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> list = new List<Product>();
            Regex regex = new Regex(@"^\w+$");

            bool start = true;
            while (start)
            {

                Console.WriteLine("\nSeja bem vindo.\nSelecione uma opção abaixo: \n");
                Console.WriteLine("1 - Cadastrar produto");
                Console.WriteLine("2 - Editar produto");
                Console.WriteLine("3 - Procurar  produtos");
                Console.WriteLine("4 - Listar  produtos");

                string input = Console.ReadLine();
                int option = 0;

                try
                {
                    option = int.Parse(input);
                }
                catch
                {
                    Console.WriteLine("Apenas números!");
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("CADASTRAR PRODUTO\n");
                        Console.WriteLine("Digite o nome do novo produto: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Digite a categoria do novo produto: ");
                        string category = Console.ReadLine();
                        Console.WriteLine("Digite a subcategoria do novo produto: ");
                        string subcategory = Console.ReadLine();

                        if (name.Length > 128)
                        {
                            Console.WriteLine("Ultrapassou número de caracteres!\nO limite é 128.");
                        }
                        else if (!regex.IsMatch(name))
                        {
                            Console.WriteLine("É permitido somente letras.");
                        }
                        else
                        {
                            Product product = new Product(name, category, subcategory, list.Count + 1);
                            list.Add(product);
                            Console.WriteLine("Produto cadastrado com sucesso!\n");
                            Console.WriteLine("Produto: " + product.produto);
                            Console.WriteLine("Categoria: " + product.categoria);
                            Console.WriteLine("Subcategoria: " + product.subcategoria);
                            Console.WriteLine("Status: " + product.status);
                            Console.WriteLine("Código: " + product.Codigo);
                            Console.WriteLine("Data e hora do cadastro: " + product.dataCadastro);
                        }
                        break;
                    case 2:
                        Console.WriteLine("EDITAR PRODUTO\n");
                        Console.WriteLine("Digite o cógido do produto: ");
                        string code = Console.ReadLine();

                        int indexProduct = list.FindIndex(product => product.Codigo.ToString() == code);

                        if (indexProduct == -1)
                        {
                            Console.WriteLine("Produto não encontrado");
                        }
                        else
                        {
                            Console.WriteLine("Digite o novo nome do produto: ");
                            string newName = Console.ReadLine();
                            Console.WriteLine("Status ativo? (S - N) ");
                            string statusChecking = Console.ReadLine();

                            if (string.IsNullOrEmpty(statusChecking)) { statusChecking = "Status não selecionado"; }
                            else if (statusChecking == "S") { list[indexProduct].status = "Ativo"; }
                            else if (statusChecking == "N") { list[indexProduct].status = "Inativo"; };
                            list[indexProduct].produto = newName;
                            list[indexProduct].dataEdicao = DateTime.Now.ToString("dd-MM-yyyy - HH:mm");
                            Console.WriteLine("\nProduto editado com sucesso\n");
                        }

                        break;
                    case 3:
                        Console.WriteLine("Digite o nome do produto que deseja encontrar:");
                        string nameSearch = Console.ReadLine();
                        List<Product> productFounded = list.FindAll(product => product.produto.ToUpper() == nameSearch.ToUpper());
                        if (productFounded.Count == 0)
                        {
                            Console.WriteLine("Produto Não Encontrado");
                        }
                        else
                        {
                            foreach (Product product in productFounded)
                            {
                                Console.WriteLine("CÓDIGO: {0}\nPRODUTO: {1}\nCATEGORIA: {2}\nSUBCATEGORIA: {3}\nSTATUS: {4}\n",
                                    product.Codigo.ToString(), product.produto, product.categoria, product.subcategoria, product.status);
                            }
                        }

                        break;
                    case 4:
                        foreach (Product product in list)
                        {
                            Console.WriteLine("CÓDIGO: {0}\nPRODUTO: {1}\nCATEGORIA: {2}\nSUBCATEGORIA: {3}\nSTATUS: {4}\n",
                                product.Codigo.ToString(), product.produto, product.categoria, product.subcategoria, product.status);
                        }
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }

            Console.ReadLine();
        }

    }
}


