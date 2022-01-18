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
        public Saldo SaldoDiaAnterior { get; private set; }
        public Saldo SaldoFinalDoDia { get; }

        public Dia(DateTime data, List<Transacao> transacoes, Saldo saldoDiaAnterior)
        {
            Data = data;
            Transacoes = transacoes;
            SaldoDiaAnterior = saldoDiaAnterior;

            SaldoFinalDoDia = new Saldo(saldoDiaAnterior);
            SaldoFinalDoDia.AdicionarValor(transacoes);
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
            Transacoes.Sort((x, y) => x.Data.CompareTo(y.Data));

            SaldoFinalDoDia.AdicionarValor(transacao);
        }
    }
}
