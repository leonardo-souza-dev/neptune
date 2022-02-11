using Neptune.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Meses2
    {
        public List<Transacao> TodasTransacoes { get; private set; }
        public List<Conta> Contas { get; private set; }
        public List<Mes> MesList { get; set; } = new List<Mes>();        
        public Saldo SaldoInicial { get; set; }

        public Meses2(List<Transacao> todasTransacoes, List<Conta> contas)
        {
            TodasTransacoes = todasTransacoes;
            Contas = contas;            
        }

        public Mes ObterMes(DataMes dataMes)
        {
            var mes = MesList.FirstOrDefault(x => x.DataMes.Ano == dataMes.Ano && x.DataMes.Mes == dataMes.Mes);

            if (mes == null)
            {
                var mesesAnteriores = MesList.Where(x => x.DataMes.ObterData() < dataMes.ObterData());

                Saldo saldo = null;
                if (mesesAnteriores.Any())
                {
                    saldo = mesesAnteriores.Last().SaldoFinalUltimoDia;
                }
                else
                {
                    saldo = SaldoInicial;
                }

                mes = new Mes(dataMes, saldo);
                MesList.Add(mes);
            }

            return mes;
        }
    }
}
