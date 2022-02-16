using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Domain.Utils;

namespace Neptune.Domain
{
    public class Dia
    {
        public string Data { get; set; }
        public decimal SaldoDiaAnteriorMaisProximo { get; set; }
        public List<Transacao> Transacoes { get; set; } = new();
        //public decimal SaldoDoDia => Transacoes.Where(x => x.Conta.Selecionada && x.Categoria.Selecionada).Sum(x => x.Valor) + SaldoDiaAnteriorMaisProximo;

        public Dia(List<Transacao> transacoes, decimal saldoDiaAnteriorMaisProximo)
        {
            Data = transacoes?.First().Data.ToString("d");
            SaldoDiaAnteriorMaisProximo = saldoDiaAnteriorMaisProximo;
            Transacoes = transacoes;
        }

        public string FormatarMoeda(decimal valor)
        {
            return (valor.ToString().Contains(".") || valor.ToString().Contains(",")) ? valor.ToString() : valor + ",00";
        }
    }
}
