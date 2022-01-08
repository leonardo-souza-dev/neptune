using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.ViewModel
{
    public class Dia
    {
        public List<Transacao> Transacoes { get; } = new();
        private decimal SaldoDoDiaAnterior { get; }
        public DateTime Data { get; }

        public Dia(DateTime data, List<Transacao> transacoes, decimal saldoDoDiaAnterior)
        {
            Data = data;
            SaldoDoDiaAnterior = saldoDoDiaAnterior;
            Transacoes = transacoes;
        }

        public decimal ObterSaldoDoDia()
        {
            return SaldoDoDiaAnterior - Transacoes.Sum(x => x.Valor);
        }

        public void AdicionarTransacao(Transacao transacaoViewModel)
        {
            Transacoes.Add(transacaoViewModel);
        }
    }
}
