using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class DiaOld
    {
        public DateTime Data { get; private set; }
        public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();

        private decimal? _saldoDoDia;
        public decimal SaldoDoDia 
        { 
            get
            {
                if (!_saldoDoDia.HasValue)
                {
                    throw new NullReferenceException("SaldoDoDia");
                }
                else
                {
                    return _saldoDoDia.Value;
                }
            }
            set
            {
                _saldoDoDia = value;
            }
        }

        public DiaOld(DateTime data, List<Transacao> transacoes, decimal saldoDiaAnterior)
        {
            Data = data;
            Transacoes.AddRange(transacoes.Select(transacao => new Transacao(transacao.Id, transacao.Data, transacao.Descricao, transacao.Valor, transacao.ContaId)));
            Transacoes.Sort((x, y) => x.Data.CompareTo(y.Data));

            SaldoDoDia = saldoDiaAnterior - Transacoes.Sum(x => x.Valor);
        }
    }
}
