using PessoaNiver.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PessoaNiver.Dados
{
    public interface IBancoDeDados
    {

        void Salvar(Pessoa pessoa);
        IEnumerable<Pessoa> PessoasRegistradas();
        List<Pessoa> PessoasRegistradas(string nome);
        void DeletarPessoa(Pessoa pessoa);
        void AtualizarPessoa(Pessoa pessoa);

    }
}
