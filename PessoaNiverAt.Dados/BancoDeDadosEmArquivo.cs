using PessoaNiver.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PessoaNiver.Dados
{
    public class BancoDeDadosEmArquivo : IBancoDeDados
    {

        string sourceFilePath = @"C:\Users\User PC\Desktop\Arquivos\infnet\Bloco_.Net\projetos\AT_CONSOLE\testeFinal\PessoaNiverAt\PessoaNiverAt.Dados\arquivoDados.txt";
        FileStream fs = null;
        StreamReader sr = null;
        public void Salvar(Pessoa pessoa)
        {

            try
            {
                using (StreamWriter sw = File.AppendText(sourceFilePath))
                {
                    string formato = $"{pessoa.Nome},{pessoa.Sobrenome},{pessoa.Data.ToString()};";
                    sw.WriteLine(formato);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Um erro Ocorreu");
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();

            }

        }

        public IEnumerable<Pessoa> PessoasRegistradas()
        {
            FileStream arquivo;
            if (!File.Exists(sourceFilePath))
            {
                arquivo = File.Create(sourceFilePath);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(sourceFilePath);

            string[] pessoas = resultado.Split(';');

            List<Pessoa> pessoaList = new List<Pessoa>();

            for (int i = 0; i < pessoas.Length - 1; i++)
            {
                string[] dados = pessoas[i].Split(',');

                string nome = dados[0];
                string sobrenome = dados[1];
                DateTime dataDeCadastro = Convert.ToDateTime(dados[2]);

                Pessoa pessoa = new Pessoa(nome, sobrenome, dataDeCadastro);

                pessoaList.Add(pessoa);
            }
            if (sr != null) sr.Close();
            if (fs != null) fs.Close();
            return pessoaList;
        }


        public List<Pessoa> PessoasRegistradas(string nome)
        {
            FileStream arquivo;
            if (!File.Exists(sourceFilePath))
            {
                arquivo = File.Create(sourceFilePath);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(sourceFilePath);

            string[] pessoas = resultado.Split(';');

            List<Pessoa> pessoaList = new List<Pessoa>();


            for (int i = 0; i < pessoas.Length - 1; i++)
            {
                string[] dados = pessoas[i].Split(',');

                string nomeAux = dados[0];
                string sobrenome = dados[1];
                DateTime dataDeCadastro = Convert.ToDateTime(dados[2]);

                Pessoa pessoa = new Pessoa(nomeAux, sobrenome, dataDeCadastro);



                pessoaList.Add(pessoa);
            }

            List<Pessoa> listaDePessoasComONome = pessoaList.FindAll(x => x.Nome.Contains(nome));

            if (sr != null) sr.Close();
            if (fs != null) fs.Close();
            return listaDePessoasComONome;

        }

        public void DeletarPessoa(Pessoa pessoa)
        {
            var tempFile = Path.GetTempFileName();


            using (StreamWriter sw = File.AppendText(tempFile))
            {
                string resultado = File.ReadAllText(sourceFilePath);

                string[] pessoas = resultado.Split(';');

                List<Pessoa> pessoaList = new List<Pessoa>();


                for (int i = 0; i < pessoas.Length - 1; i++)
                {
                    string[] dados = pessoas[i].Split(',');

                    string nome = dados[0];
                    string sobrenome = dados[1];
                    DateTime dataDeCadastro = Convert.ToDateTime(dados[2]);



                    if (nome != pessoa.Nome && sobrenome != pessoa.Sobrenome && dataDeCadastro != pessoa.Data)
                    {
                        sw.WriteLine(nome + "," + sobrenome + "," + dataDeCadastro.ToString() + ";");
                    }

                }
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
                if (sw != null) sw.Close();
                File.Delete(sourceFilePath);
                File.Move(tempFile, sourceFilePath);
            }

        }

        public void AtualizarPessoa(Pessoa pessoa)
        {
            var tempFile = Path.GetTempFileName();
            Console.WriteLine("Digite o nome da pessoa que deseja Atualizar: ");
            string nomeNova = Console.ReadLine();
            Console.WriteLine("Digite o sobrenome da pessoa que deseja Atualizar: ");
            string sobrenomeNova = Console.ReadLine();
            Console.WriteLine("Digite a data do aniversario no formato dd/MM/yyyy para Atualizar");
            DateTime dataNova = DateTime.Parse(Console.ReadLine());

            using (StreamWriter sw = File.AppendText(tempFile))
            {
                string resultado = File.ReadAllText(sourceFilePath);

                string[] pessoas = resultado.Split(';');

                List<Pessoa> pessoaList = new List<Pessoa>();


                for (int i = 0; i < pessoas.Length - 1; i++)
                {
                    string[] dados = pessoas[i].Split(',');

                    string nome = dados[0];
                    string sobrenome = dados[1];
                    DateTime dataDeCadastro = Convert.ToDateTime(dados[2]);


                    //todo: adicionar ID unico para garantir que eh a pessoa desejada
                    if (nome != pessoa.Nome && sobrenome != pessoa.Sobrenome && dataDeCadastro != pessoa.Data)
                    {
                        sw.WriteLine(nome + "," + sobrenome + "," + dataDeCadastro.ToString() + ";");
                    }


                }
                sw.WriteLine(nomeNova + "," + sobrenomeNova + "," + dataNova.ToString() + ";");
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
                if (sw != null) sw.Close();
                File.Delete(sourceFilePath);
                File.Move(tempFile, sourceFilePath);
            }

        }


    }
}

