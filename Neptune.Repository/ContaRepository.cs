using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Domain;
using System.Threading.Tasks;

namespace Neptune.Infra
{
    public class ContaRepository : IContaRepository
    {
        private readonly List<Conta> _contas = new();

        public ContaRepository()
        {
            _contas.Add(new Conta(1, "Conta corrente", 0, true));
            _contas.Add(new Conta(2, "Poupanca", 0, true));
            _contas.Add(new Conta(3, "Cartao de Credito", 0, true));
        }        

        public List<Conta> ObterTodas()
        {
            return _contas;
        }

        public Conta Obter(int id)
        {
            return _contas.FirstOrDefault(x => x.Id == id);
        }

        public Conta Criar(Conta conta)
        {
            var novaEntidade = new Conta(GetNextId(), conta.Nome, conta.SaldoInicial, conta.Selecionada);

            _contas.Add(novaEntidade);

            return novaEntidade;
        }

        private int GetNextId()
        {
            return _contas.Max(x => x.Id) + 1;
        }
    }
}
