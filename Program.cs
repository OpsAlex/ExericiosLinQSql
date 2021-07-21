using System;
using System.Collections.Generic;
using ExerciciosLINGsql.Entities;
using System.IO;
using System.Globalization;
using System.Linq;

namespace ExerciciosLINGsql
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            //Segundo Exercicio. Fazer um programa para ler os dados (Nome, Email e salario) de funcionarios a partir de um arquivo
            // Ler em ordem alfabética o email cujo salario seja superior aos demais, 
            // mostrar tambem a soma dos salarios dos funcionamentos cujo nome começa com "M"

            Console.Write("Digite o caminho do arquivo: ");
            string path2 = Console.ReadLine();
            Console.Write("Digite um salario para ordernar quem tem o maior: ");
            double limit = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            List<Dados> lista = new List<Dados>();

            try
            {
                using (StreamReader sr = File.OpenText(path2))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        string email = fields[1];
                        double salario = double.Parse(fields[2], CultureInfo.InvariantCulture);
                        lista.Add(new Dados(name, email, salario));
                    }

                    var emails = lista.Where(obj => obj.Salario > limit).OrderBy(obj => obj.Email).Select(obj => obj.Email);

                    var sum = lista.Where(obj => obj.Nome[0] == 'M').Sum(obj => obj.Salario);

                    Console.WriteLine("Email da pessoa que possui o maior salario: " + limit.ToString("F2", CultureInfo.InvariantCulture));
                    foreach (string email in emails)
                    {
                        Console.WriteLine(email);
                    }

                    Console.WriteLine("Soma dos salarios das pessoas que tem a inicial 'M' " + sum.ToString("F2", CultureInfo.InvariantCulture));
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }


            


            // Primeiro Exercicio. Ler um arquivo, onde contem produtos e preços, 
            // fazer a media dos preços e mostrar os produtos que estão abaixo da media.

            Console.WriteLine("Digite o caminho do arquivo: ");
            string path = Console.ReadLine();

            List<Product> list = new List<Product>();

            using (StreamReader sr = File.OpenText(path)) 
            {
                while(!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                    list.Add(new Product(name, price));
                }
            }

            var avg = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Preço medio = " + avg.ToString("F2", CultureInfo.InvariantCulture));

            var names = list.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);

            Console.WriteLine("Produtos com o valor abaixo da media");
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
