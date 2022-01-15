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
        public List<Conta> Contas { get; private set; }
        private List<Mes> MesList { get; } = new List<Mes>();
        private decimal TotalSaldoInicialContas { get; }

        public Meses(List<Transacao> todasTransacoes, List<Conta> contas)
        {
            TodasTransacoes = todasTransacoes;
            Contas = contas;

            TodasTransacoes.Sort((x, y) => x.Data.CompareTo(y.Data));


            TotalSaldoInicialContas = Contas.Sum(x => x.SaldoInicial);

            foreach (var t in TodasTransacoes)
            {
                var mes = MesList.FirstOrDefault(x => x.DataMes.Ano == t.Data.Year && x.DataMes.Mes == t.Data.Month);
                
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

                    var novoMes = new Mes(new DataMes(t.Data.Year, t.Data.Month), saldoUltimoDiaMesAnterior);
                    novoMes.AdicionarTransacao(t);
                    MesList.Add(novoMes);
                }
                else
                {
                    mes.AdicionarTransacao(t);
                }
            }
        }

        public Mes ObterMes(DataMes mesTransacao)
        {
            var mes = MesList.FirstOrDefault(x => x.DataMes.Ano == mesTransacao.Ano && x.DataMes.Mes == mesTransacao.Mes);

            if (mes == null)
            {
                var mesesAnteriores = MesList.Where(x => x.DataMes.ObterData() < mesTransacao.ObterData());
                decimal saldo = 0;
                if (mesesAnteriores.Any())
                {
                    saldo = mesesAnteriores.Last().SaldoFinalUltimoDia;
                }
                else
                {
                    saldo = TotalSaldoInicialContas;
                }

                mes = new Mes(mesTransacao, saldo);
                MesList.Add(mes);
            }

            return mes;
        }
    }
}
