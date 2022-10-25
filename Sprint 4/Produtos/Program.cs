using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Produtos;

namespace Estudando
{
    internal class Program
    {
        static void Main(string[] args)
        {


            List<Produto> list = new List<Produto>();
            Regex regex = new Regex(@"^\w+$");

            //Menu

            bool start = true;
            while (start)
            {

                Console.WriteLine("\nSeja bem vindo.\nSelecione uma opção abaixo: \n");
                Console.WriteLine("1 - Cadastrar produto");
                Console.WriteLine("2 - Editar produto");
                Console.WriteLine("3 - Listar  produtos");

                string? input = Console.ReadLine();
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
                        Console.WriteLine("========= CADASTRAR PRODUTO =========");

                        Console.WriteLine("Nome do produto: ");
                        string? name = Console.ReadLine();

                        Console.WriteLine("Descrição do produto: ");
                        string? description = Console.ReadLine();

                        double weight = 0;
                        double heigth = 0;
                        double width = 0;
                        double lenght = 0;
                        double price = 0;
                        double amount = 0;

                        try
                        {
                            Console.WriteLine("Peso do produto: ");
                            weight = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("Altura do produto: ");
                            heigth = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("Largura do produto: ");
                            width = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("Comprimento do produto: ");
                            lenght = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("Preço do produto: ");
                            price = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("Quantidade do produto: ");
                            amount = Convert.ToDouble(Console.ReadLine());

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Apenas números! Tente novamente.");
                            break;
                        }

                        Console.WriteLine("Categoria do produto: ");
                        string category = Console.ReadLine();

                        Console.WriteLine("Subcategoria do produto: ");
                        string subcategory = Console.ReadLine();

                        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(subcategory))
                        {
                            Console.WriteLine("Nome nulo ou vazio.\nFavor Verificar.");
                        }

                        if (name.Length > 128 || description.Length > 512)
                        {
                            Console.WriteLine("Ultrapassou número de caracteres!\nO limite é 128.");
                        }
                        else if (!regex.IsMatch(name) || !regex.IsMatch(description))
                        {
                            Console.WriteLine("Somente letras são permitidas");
                        }
                        else
                        {
                            Produto novoproduto = new Produto(name, description, weight, heigth, width, lenght, price, amount, list.Count + 1, category, subcategory);
                            list.Add(novoproduto);

                            Console.WriteLine("Produto cadastrado com sucesso!\n");
                            Console.WriteLine("Nome: " + novoproduto.Name);
                            Console.WriteLine("Descrição: " + novoproduto.description);
                            Console.WriteLine("Peso: " + novoproduto.weight);
                            Console.WriteLine("Altura: " + novoproduto.heigth);
                            Console.WriteLine("Largura: " + novoproduto.width);
                            Console.WriteLine("Preço R$: " + novoproduto.Price);
                            Console.WriteLine("Quantidade: " + novoproduto.amount);
                            Console.WriteLine("Centro de Distruição: " + novoproduto.Distribution);
                            Console.WriteLine("Subcategoria: " + novoproduto.Categoria.Subcategory);
                            Console.WriteLine("Categoria: " + novoproduto.Categoria.CategoryName);
                            Console.WriteLine("Status: Ativo");
                            Console.WriteLine("Data de cadastro: " + novoproduto.dataCadastro);
                            Console.WriteLine("Código: " + novoproduto.codigo);
                        }

                        break;
                    case 2:
                        Console.WriteLine("========= EDITAR PRODUTO =========");
                        Console.WriteLine("CÓDIGO DO PRODUTO: ");
                        string code = Console.ReadLine();

                        int indexProduct = list.FindIndex(novoproduto => novoproduto.codigo.ToString() == code);

                        if (indexProduct == -1)
                        {
                            Console.WriteLine("PRODUTO NÃO ENCONTRADO!");
                        }
                        else
                        {
                            Console.WriteLine("Nome do produto: ");
                            string? newname = Console.ReadLine();

                            Console.WriteLine("Descrição do produto: ");
                            string? newdescription = Console.ReadLine();

                            double newweight;
                            double newheigth;
                            double newwidth;
                            double newlenght;
                            double newprice;
                            double newamount;

                            try
                            {
                                Console.WriteLine("Peso do produto: ");
                                newweight = Convert.ToDouble(Console.ReadLine());

                                Console.WriteLine("Altura do produto: ");
                                newheigth = Convert.ToDouble(Console.ReadLine());

                                Console.WriteLine("Largura do produto: ");
                                newwidth = Convert.ToDouble(Console.ReadLine());

                                Console.WriteLine("Comprimento do produto: ");
                                newlenght = Convert.ToDouble(Console.ReadLine());

                                Console.WriteLine("Preço do produto: ");
                                newprice = Convert.ToDouble(Console.ReadLine());

                                Console.WriteLine("Quantidade do produto: ");
                                newamount = Convert.ToDouble(Console.ReadLine());

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Apenas números! Tente novamente.");
                                break;
                            }
                            Console.WriteLine("Status ativo? (S - N) ");
                            string statusChecking = Console.ReadLine();

                            if (string.IsNullOrEmpty(statusChecking))
                            {
                                statusChecking = "Status não selecionado";
                            }
                            else if (statusChecking == "S")
                            {
                                list[indexProduct].status = "Ativo";
                            }
                            else if (statusChecking == "N")
                            {
                                list[indexProduct].status = "Inativo";
                            }
                            list[indexProduct].Name = newname;
                            list[indexProduct].dataEdicao = DateTime.Now.ToString("dd-MM-yyyy - HH:mm");

                            Console.WriteLine("\nProduto editado com sucesso!\n" + list[indexProduct].dataEdicao);


                        }
                        break;
                    case 3:
                        foreach (Produto product in list)
                        {
                            Console.WriteLine("CÓDIGO: {0}\nPRODUTO: {1}\nSTATUS: {2}\nVALOR: {3}\nQUANTIDADE: {4}\n" +
                                "CATEGORIA: {5}\nDATA CRIAÇÃO: {6}\nDATA MODIFICAÇÃO: {7}\n", product.codigo.ToString(), product.Name, product.status,
                                product.Price, product.amount, product.Categoria, product.dataCadastro, product.dataEdicao);
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