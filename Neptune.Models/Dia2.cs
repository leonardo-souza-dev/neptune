using Neptune.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Dia2
    {
        public DateTime Data { get; private set; }
        public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();


        public Dia2(DateTime data, List<Transacao> transacoes)
        {
            Data = data;
            Transacoes = transacoes;
        }
    }
}
