using Neptune.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class SaldoConta
    {
        public Conta Conta { get; set; }
        public decimal Valor { get; set; }

        public SaldoConta(Conta conta, decimal valor)
        {
            Conta = conta;
            Valor = valor;
        }

        public void Adicionar(SaldoConta saldoConta)
        {
            Valor += saldoConta.Valor;
        }

        public void Adicionar(decimal valor)
        {
            Valor += valor;
        }
    }
}
