using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class MesOld
    {
        public DataMes MesTransacao { get; private set; }
        public SaldoUltimoDiaMesAnterior SaldoUltimoDiaMesAnterior { get; private set; }
        public DateTime UltimoDiaMesAnterior => MesTransacao.UltimoDiaDoMesAnterior;
        
        public List<DiaOld> Dias { get; private set; } = new List<DiaOld>();

        public MesOld(DataMes mesTransacao, SaldoUltimoDiaMesAnterior saldoUltimoDiaMesAnterior, List<Transacao> transacoes)
        {
            MesTransacao = mesTransacao;
            SaldoUltimoDiaMesAnterior = saldoUltimoDiaMesAnterior;

            var transacoesDiaGrupo = transacoes
                .Where(x => x.Data.Month == mesTransacao.Mes && x.Data.Year == mesTransacao.Ano)
                .GroupBy(x => x.Data.Day).ToList();

            decimal saldoDiaAnterior = 0;

            for(int i = 0; i < transacoesDiaGrupo.Count(); i++)
            {
                var transacoesDia = transacoesDiaGrupo[i];

                var dataDia = new DateTime(mesTransacao.Ano, mesTransacao.Mes, transacoesDia.Key);
                DiaOld dia = null;

                if (i == 0)
                {
                    dia = new DiaOld(dataDia, transacoesDia.ToList(), saldoUltimoDiaMesAnterior.Valor);
                }
                else
                {
                    dia = new DiaOld(dataDia, transacoesDia.ToList(), saldoDiaAnterior);    
                }
                saldoDiaAnterior = dia.SaldoDoDia;

                Dias.Add(dia);
            }
        }
    }
}
