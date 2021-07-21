using System;
using System.Collections.Generic;
using System.Text;

namespace ExerciciosLINGsql.Entities
{
    class Dados
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public double Salario { get; set; }

        public Dados(string nome, string email, double salario)
        {
            Nome = nome;
            Email = email;
            Salario = salario;
        }
    }
}
