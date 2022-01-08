using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Neptune.Web.ViewModel
{
    public class TransacoesMes 
    {
        public decimal SaldoUltimoDiaMesAnterior { get; private set; }
        public string UltimoDiaMesAnterior { get { return _ultimoDiaMesAnterior.ToString("dd/MM/yyyy"); } }
        public List<Dia> Dias { get; private set; } = new();

        public List<Conta> Contas { get; private set; } = new List<Conta>();

        public int Ano;
        public int Mes;

        private DateTime _ultimoDiaMesAnterior
        {
            get
            {
                return new DateTime(Ano, Mes, 1).AddDays(-1);
            }
        }

        public TransacoesMes(int ano, 
                             int mes, 
                             List<Transacao> transacoes, 
                             decimal saldoUltimoDiaMesAnterior, 
                             List<Conta> contasAtivasModel,
                             List<Conta> todasContasModel)
        {
            Ano = ano;
            Mes = mes;

            transacoes.ToList().Sort((x, y) => x.Data.CompareTo(y.Data));

            SaldoUltimoDiaMesAnterior = saldoUltimoDiaMesAnterior;

            var dias = transacoes
                .GroupBy(item => new {item.Data.Month, item.Data.Day}).Select(x => x.First())
                .Select(d => d.Data).ToList();

            var saldoDiaAnterior = 0M;
            for (var index = 0; index < dias.Count; index++)
            {
                var dia = dias[index];
                var transacoesDia = transacoes.Where(x => x.Data.Day == dia.Day).ToList();

                if (index == 0)
                {
                    var diaViewModel = new Dia(dia, transacoesDia, saldoUltimoDiaMesAnterior);
                    saldoDiaAnterior = diaViewModel.ObterSaldoDoDia();
                    Dias.Add(diaViewModel);
                }
                else
                {
                    var diaViewModel = new Dia(dia, transacoesDia, saldoDiaAnterior);
                    saldoDiaAnterior = diaViewModel.ObterSaldoDoDia();
                    Dias.Add(diaViewModel);
                }
            }

            todasContasModel.ForEach(x => 
            {
                var ativo = false;
                foreach (var contaAtiva in contasAtivasModel)
                {
                    if (x.Id == contaAtiva.Id)
                    {
                        ativo = true;
                    }
                }
                Contas.Add(new Conta(x.Id, x.Nome, ativo)); 
            });
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            var dia = Dias.FirstOrDefault(x => (x.Data.Day == transacao.Data.Day &&
                x.Data.Month == transacao.Data.Month &&
                x.Data.Year == transacao.Data.Year));

            if (dia == null)
            {
                var diaAnterior = Dias.FirstOrDefault(x => (x.Data.Day < transacao.Data.AddDays(-1).Day &&
                                                            x.Data.Month == transacao.Data.Month &&
                                                            x.Data.Year == transacao.Data.Year));

                var saldoDoDiaAnterior = diaAnterior.ObterSaldoDoDia();
                var transacoes = new List<Transacao>(); 
                transacoes.Add(transacao);
                
                var novoDia = new Dia(transacao.Data, transacoes, saldoDoDiaAnterior);

                Dias.Add(novoDia);                
            }
            else
                dia.AdicionarTransacao(transacao);

            Dias.Sort((x, y) => x.Data.CompareTo(y.Data));
        }

        public int ObterMesAnterior()
        {
            return new DateTime(Ano, Mes, 1).AddMonths(-1).Month;
        }

        public int ObterMesSeguinte()
        {
            return new DateTime(Ano, Mes, 1).AddMonths(1).Month;
        }

        public int ObterAnoDoMesAnterior()
        {
            return new DateTime(Ano, Mes, 1).AddMonths(-1).Year;
        }

        public int ObterAnoDoMesSeguinte()
        {
            return new DateTime(Ano, Mes, 1).AddMonths(1).Year;
        }
    }
}
