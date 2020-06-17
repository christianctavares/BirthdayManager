using System;
using System.Collections.Generic;
using System.Linq;
using PessoaNiver.Dados;
using PessoaNiver.Model;


namespace PessoaNiver.Principal
{
    class Program
    {
        public static IBancoDeDados bancoDeDados = new BancoDeDadosEmArquivo();

        public static void Main(string[] args)
        {

            string op;
            VerificaNiver();
            do
            {
                Console.WriteLine("-=Gerenciador de Aniversarios=-");
                Console.WriteLine("Selecione uma das opções abaixo: ");
                Console.WriteLine("1 - Pesquisar pessoas");
                Console.WriteLine("2 - Adicionar nova pessoa");
                Console.WriteLine("3 - Deletar uma pessoa");
                Console.WriteLine("4 - Atualizar pessoa");
                Console.WriteLine("5 - Listar Todas as pessoa");
                Console.WriteLine("6 - Sair");
                op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        BuscarPessoa();

                        break;
                    case "2":
                        AdicionarPessoa();

                        break;
                    case "3":
                        DeletaPessoa();

                        break;
                    case "4":
                        AtualizarPessoa();

                        break;
                    case "5":
                        ListarTodos();

                        break;
                    case "6":
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção Invalida");
                        break;
                }

            } while (!op.Equals("6"));

        }


        static void BuscarPessoa()
        {

            Console.WriteLine("Digite o nome, ou parte do nome, da pessoa que deseja encontrar:");
            string nomeBusca = Console.ReadLine();


            var pessoasEncontradas = bancoDeDados.PessoasRegistradas(nomeBusca);


            if (pessoasEncontradas.Count > 0)
            {

                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados de uma das pessoas encontadas:");

                int sum = 0;

                foreach (Pessoa pessoa in pessoasEncontradas)
                {
                    Console.Write($"{pessoa.Nome} {pessoa.Sobrenome} - {sum} ");
                    sum += 1;
                }
                Console.WriteLine();
                int pessoaEscolhida = int.Parse(Console.ReadLine());

                Console.WriteLine("");
                Console.WriteLine("Dados da Pessoa:");
                Console.WriteLine("Nome Completo: " + pessoasEncontradas[pessoaEscolhida].Nome + " " + pessoasEncontradas[pessoaEscolhida].Sobrenome);
                Console.WriteLine("Data de Aniversario: " + pessoasEncontradas[pessoaEscolhida].Data.ToShortDateString());

                Console.WriteLine(pessoasEncontradas[pessoaEscolhida].CalcularNiver(pessoasEncontradas, pessoaEscolhida));
                Console.WriteLine();


            }
            else
            {
                Console.WriteLine("Não foi localizado nenhum Funcionario.");
                Console.WriteLine();
            }

        }

        static void AdicionarPessoa()
        {
            Console.WriteLine("Digite o nome da pessoa que deseja adicionar: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o sobrenome da pessoa que deseja adicionar: ");
            string sobrenome = Console.ReadLine();
            Console.WriteLine("Digite a data do aniversario no formato dd/MM/yyyy ");
            DateTime data = DateTime.Parse(Console.ReadLine());




            Console.WriteLine("Os dados abaixo estao corretos?");
            Console.WriteLine("Nome: " + nome + " " + sobrenome);
            Console.WriteLine("Data do Aniversairo: " + data.ToShortDateString());
            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Nao");
            Console.Write("opcao: ");
            string op2 = Console.ReadLine();

            if (op2.Equals("1"))
            {
                bancoDeDados.Salvar(new Pessoa(nome, sobrenome, data));
                Console.WriteLine("Dados Adicionados com sucesso");
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Voltando ao Menu...");
                Console.WriteLine();
            }
        }

        static void DeletaPessoa()
        {

            Console.WriteLine("Digite o nome, ou parte do nome, da pessoa que deseja encontrar:");
            string nomeBusca = Console.ReadLine();


            var pessoasEncontradas = bancoDeDados.PessoasRegistradas(nomeBusca);


            if (pessoasEncontradas.Count > 0)
            {

                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados de uma das pessoas encontadas:");

                int sum = 0;

                foreach (Pessoa pessoa in pessoasEncontradas)
                {
                    Console.Write($"{pessoa.Nome} - {sum} ");
                    sum += 1;
                }
                Console.WriteLine();
                int pessoaEscolhida = int.Parse(Console.ReadLine());
                Pessoa pessoaEscolha = new Pessoa(pessoasEncontradas[pessoaEscolhida].Nome, pessoasEncontradas[pessoaEscolhida].Sobrenome, pessoasEncontradas[pessoaEscolhida].Data);
                bancoDeDados.DeletarPessoa(pessoaEscolha);
                Console.WriteLine();
                Console.WriteLine("Pessoa Deletada.");

            }
            else
            {
                Console.WriteLine("Não foi localizado nenhum Funcionario.");
                Console.WriteLine();
            }
        }

        static void AtualizarPessoa()
        {
            Console.WriteLine("Digite o nome, ou parte do nome, da pessoa que deseja encontrar:");
            string nomeBusca = Console.ReadLine();


            var pessoasEncontradas = bancoDeDados.PessoasRegistradas(nomeBusca);


            if (pessoasEncontradas.Count > 0)
            {

                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados de uma das pessoas encontadas:");

                int sum = 0;

                foreach (Pessoa pessoa in pessoasEncontradas)
                {
                    Console.Write($"{pessoa.Nome} - {sum} ");
                    sum += 1;
                }
                Console.WriteLine();
                int pessoaEscolhida = int.Parse(Console.ReadLine());
                Pessoa pessoaEscolha = new Pessoa(pessoasEncontradas[pessoaEscolhida].Nome, pessoasEncontradas[pessoaEscolhida].Sobrenome, pessoasEncontradas[pessoaEscolhida].Data);
                bancoDeDados.AtualizarPessoa(pessoaEscolha);
                Console.WriteLine();
                Console.WriteLine("Pessoa Atualizar");

            }
            else
            {
                Console.WriteLine("Não foi localizado nenhum Funcionario.");
                Console.WriteLine();
            }
        }

        static void VerificaNiver()
        {

            var pessoasEncontradas = bancoDeDados.PessoasRegistradas();

            var pessoasEncontradasAux = pessoasEncontradas.Where(x => x.Data.Day == DateTime.Now.Day && x.Data.Month == DateTime.Now.Month);
            List<Pessoa> listaAniversariantes = new List<Pessoa>();
            foreach (Pessoa p in pessoasEncontradasAux)
            {
                listaAniversariantes.Add(p);
            }

            if (listaAniversariantes.Count > 0)
            {
                Console.WriteLine("-=Lista de Pessoas que fazem Aniversario Hj=-");
                foreach (Pessoa p in pessoasEncontradasAux)
                {
                    Console.WriteLine($"{p.Nome} {p.Sobrenome}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Voce nao tem nenhum amigo que faz aniversario hj.");
            }
        }

        static void ListarTodos()
        {

            var pessoasEncontradas = bancoDeDados.PessoasRegistradas();

            List<Pessoa> lista = new List<Pessoa>();
            foreach (Pessoa p in pessoasEncontradas)
            {
                lista.Add(p);
            }

            if (lista.Count > 0)
            {
                Console.WriteLine("-=Lista de Pessoas Cadastradas=-");
                foreach (Pessoa p in lista)
                {
                    Console.WriteLine($"{p.Nome} {p.Sobrenome}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Voce nao tem nenhum amigo que faz aniversario hj.");
            }
        }
    }
}
