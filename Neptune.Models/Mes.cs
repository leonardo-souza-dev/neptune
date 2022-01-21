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
        public string UltimoDiaMesAnterior => DataMes.UltimoDiaDoMesAnterior.ToString("dd/MM/yyyy");
        public List<Dia> Dias { get; private set; } = new List<Dia>();
        public List<Dia> DiasExibicao => Dias.Where(x => x.Transacoes.Where(t => t.Conta.Selecionada).Any()).ToList(); 
        public string NumMes => DataMes.Mes.ToString();
        public string NavMesAnterior => $"?ano={DataMes.NumAnoDoMesAnterior}&mes={DataMes.NumMesAnterior}";
        public string NavMesSeguinte => $"?ano={DataMes.NumAnoDoMesSeguinte}&mes={DataMes.NumMesSeguinte}";
        public Saldo SaldoFinalUltimoDiaMesAnterior { get; private set;}
        public Saldo SaldoFinalUltimoDia
        {
            get
            {
                var ultimoDia = Dias.LastOrDefault();
                if (ultimoDia != null)
                    return ultimoDia.SaldoFinalDoDia;
                else
                    return SaldoFinalUltimoDiaMesAnterior;
            }
        }
        public Transacao NovaTransacao { get; set; } = new();

        public Mes(DataMes dataMes, Saldo saldoFinalUltimoDiaMesAnterior)
        {
            DataMes = dataMes;
            SaldoFinalUltimoDiaMesAnterior = saldoFinalUltimoDiaMesAnterior;
        }

        public void LimparNovaTransacao()
        {
            NovaTransacao = new();
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
                Saldo saldoFinalDiaAnterior = null;
                
                if (diasAnteriores.Any())
                {
                    saldoFinalDiaAnterior = diasAnteriores.Last().SaldoFinalDoDia;
                }
                else
                {
                    saldoFinalDiaAnterior = SaldoFinalUltimoDiaMesAnterior;
                }

                var diaNovo = new Dia(transacao.Data, 
                                      new List<Transacao> { transacao },
                                      saldoFinalDiaAnterior);
                Dias.Add(diaNovo);
            }
            else
            {
                dia.AdicionarTransacao(transacao);
            }
        }
    }
}
