using Neptune.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Dia
    {
        public DateTime Data { get; private set; }
        public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();
        public decimal SaldoDiaAnterior { get; private set; }
        public decimal SaldoFinal => SaldoDiaAnterior - Transacoes.Where(x => x.Conta.Selecionada).Sum(x => x.Valor);

        public Dia(DateTime data, List<Transacao> transacoes, decimal saldoDiaAnterior)
        {
            Data = data;
            Transacoes = transacoes;
            SaldoDiaAnterior = saldoDiaAnterior;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
            Transacoes.Sort((x, y) => x.Data.CompareTo(y.Data));
        }
    }
}
