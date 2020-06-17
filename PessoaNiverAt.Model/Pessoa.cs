using System;
using System.Collections.Generic;

namespace PessoaNiver.Model
{
    public class Pessoa
    {

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime Data { get; set; }

        public Pessoa()
        {
        }

        public Pessoa(string nome, string sobrenome, DateTime data)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Data = data;
        }

        public string CalcularNiver(List<Pessoa> listaDePessoas2, int pessoaEscolhida)
        {
            DateTime today = DateTime.Today;
            DateTime next = listaDePessoas2[pessoaEscolhida].Data.AddYears(today.Year - listaDePessoas2[pessoaEscolhida].Data.Year);

            if (next < today)
                next = next.AddYears(1);
            TimeSpan sub = next.Subtract(today);

            if (sub.Days == 0)
            {
                return ("É HOJE!!! UHULL");
            }
            else
            {
                return ("Faltam " + sub.Days + " dias para esse aniversario");

            }

        }

        public override string ToString()
        {
            return Nome + Sobrenome + Data;
        }
    }
}
