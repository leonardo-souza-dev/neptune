using Neptune.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Mes
    {
        public DataMes DataMes { get; private set; }
        public decimal SaldoUltimoDiaMesAnterior { get; private set; }
        public string UltimoDiaMesAnterior => DataMes.UltimoDiaDoMesAnterior.ToString("dd/MM/yyyy");
        public List<Dia> Dias { get; private set; } = new List<Dia>();
        public decimal SaldoFinalUltimoDia
        {
            get
            {
                var ultimoDia = Dias.LastOrDefault();
                if (ultimoDia != null)
                    return ultimoDia.SaldoFinal;
                else
                    return SaldoUltimoDiaMesAnterior;
            }
        }

        public string NumMes => DataMes.Mes.ToString();
        public string NavMesAnterior => $"?ano={DataMes.NumAnoDoMesAnterior}&mes={DataMes.NumMesAnterior}";
        public string NavMesSeguinte => $"?ano={DataMes.NumAnoDoMesSeguinte}&mes={DataMes.NumMesSeguinte}";

        public Mes(DataMes dataMes, decimal saldoUltimoDiaMesAnterior)
        {
            DataMes = dataMes;
            SaldoUltimoDiaMesAnterior = saldoUltimoDiaMesAnterior;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            var dia = Dias.FirstOrDefault(x => 
                x.Data.Year == transacao.Data.Year && 
                x.Data.Month == transacao.Data.Month && 
                x.Data.Day == transacao.Data.Day);

            if (dia == null)
            {
                var diasAnteriores = Dias.Where(x => x.Data < transacao.Data);
                decimal saldo = 0;
                
                if (diasAnteriores.Any())
                {
                    saldo = diasAnteriores.Last().SaldoFinal;
                }
                else
                {
                    saldo = SaldoUltimoDiaMesAnterior;
                }

                var primeiroDia = new Dia(transacao.Data, 
                                          new List<Transacao> { transacao }, 
                                          saldo);
                Dias.Add(primeiroDia);
            }
            else
            {
                dia.AdicionarTransacao(transacao);
            }
        }
    }
}
