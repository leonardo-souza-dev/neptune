using Neptune.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Meses
    {
        public List<Transacao> TodasTransacoes { get; private set; }
        public List<Transacao> TransacoesExibicao { get; private set; }
        public List<Conta> Contas { get; private set; }
        public List<Mes> MesList { get; set; } = new List<Mes>();        
        public decimal TotalSaldoInicialContas { get; set; }

        public Meses(List<Transacao> todasTransacoes, List<Conta> contas)
        {
            TodasTransacoes = todasTransacoes;
            Contas = contas;

            TransacoesExibicao = TodasTransacoes;
            TransacoesExibicao.Sort((x, y) => x.Data.CompareTo(y.Data));

            TotalSaldoInicialContas = Contas.Where(x => x.Selecionada).Sum(x => x.SaldoInicial);

            foreach (var transacao in TransacoesExibicao)
            {
                var mes = MesList.FirstOrDefault(x => x.DataMes.Ano == transacao.Data.Year && x.DataMes.Mes == transacao.Data.Month);

                if (mes == null)
                {
                    decimal saldoUltimoDiaMesAnterior = 0;

                    if (MesList.Any())
                    {
                        saldoUltimoDiaMesAnterior = MesList.Last().SaldoFinalUltimoDia;
                    }
                    else
                    {
                        saldoUltimoDiaMesAnterior = TotalSaldoInicialContas;
                    }

                    var novoMes = new Mes(new DataMes(transacao.Data.Year, transacao.Data.Month), saldoUltimoDiaMesAnterior);
                    novoMes.AdicionarTransacao(transacao);
                    MesList.Add(novoMes);
                }
                else
                {
                    mes.AdicionarTransacao(transacao);
                }
            }
        }

        public Mes ObterMes(DataMes dataMes)
        {
            var mes = MesList.FirstOrDefault(x => x.DataMes.Ano == dataMes.Ano && x.DataMes.Mes == dataMes.Mes);

            if (mes == null)
            {
                var mesesAnteriores = MesList.Where(x => x.DataMes.ObterData() < dataMes.ObterData());
                decimal saldo = 0;
                if (mesesAnteriores.Any())
                {
                    saldo = mesesAnteriores.Last().SaldoFinalUltimoDia;
                }
                else
                {
                    saldo = TotalSaldoInicialContas;
                }

                mes = new Mes(dataMes, saldo);
                MesList.Add(mes);
            }

            return mes;
        }
    }
}
