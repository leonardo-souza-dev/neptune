using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Domain.Utils;

namespace Neptune.Domain
{
    public class Mes
    {
        public DataMes DataMes { get; set; }
        public decimal SaldoUltimoDiaMesAnterior { get; set; }
        public DateTime UltimoDiaMesAnterior => DataMes.UltimoDiaDoMesAnterior;
        public List<Dia> Dias { get; set; } = new();

        public Mes(List<Transacao> transacoes, List<Conta> contas, DataMes dataMes, decimal saldoFinalUltimoDiaMesAnterior)
        {
            DataMes = dataMes;
            
            SaldoUltimoDiaMesAnterior = saldoFinalUltimoDiaMesAnterior;

            var transacoesDia = transacoes.GroupBy(x => new { x.Data.Year, x.Data.Month, x.Data.Day }).ToList();

            transacoesDia.ForEach(t => Dias.Add(new Dia(t.ToList(), SaldoUltimoDiaMesAnterior)));
        }

        public string FormatarMoeda(decimal valor)
        {
            return (valor.ToString().Contains(".") || valor.ToString().Contains(",")) ? valor.ToString() : valor + ",00";
        }
    }
}
