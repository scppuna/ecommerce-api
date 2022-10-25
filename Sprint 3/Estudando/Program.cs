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
                Console.WriteLine("3 - Listar  produtos");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Cadastrar produto\n");
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
                            Console.WriteLine("Data e hora do cadastro: " + product.dataCadastro);
                        }
                        break;
                        case"2":
                        Console.Write("Digite o cógido do produto: ");
                        string  code = Console.ReadLine();

                        int indexProduct = list.FindIndex(product => product.codigo.ToString() == code);

                        if (indexProduct == -1)
                        {
                            Console.WriteLine("Produto não encontrado");
                        }
                        else
                        {
                            Console.WriteLine("Digite o novo nome do produto: ");
                            string newName = Console.ReadLine();
                            Console.Write("Status ativo? (SIM - NÃO) ");
                            string statusChecking = Console.ReadLine();
                            
                            //string novoStatus = Console.ReadLine();

                            list[indexProduct].produto = newName;
                            //list[indexProduct].status = novoStatus;

                            Console.WriteLine("\nProduto editado com sucesso\n");
                        }

                        break;
                    case "3":
                        foreach (Product product in list)
                        {
                            Console.WriteLine("CÓDIGO: 2022{0} PRODUTO: {1} CATEGORIA: {2} SUBCATEGORIA {3} STATUS {4}",
                                product.codigo.ToString(), product.produto, product.categoria, product.subcategoria, product.status);
                        }
                        break;
                }
            }

            Console.ReadLine();
        }

    }
}
