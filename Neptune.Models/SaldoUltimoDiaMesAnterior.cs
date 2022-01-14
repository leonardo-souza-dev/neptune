using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    //TODO: refatorar. Remover método ObterSaldoUltimoDiaMesAnterior da TransacaoService
    public class SaldoUltimoDiaMesAnterior
    {
        public decimal Valor => Contas.Sum(x => x.Valor);

        public List<SaldoUltimoDiaMesAnteriorConta> Contas { get; private set; } = new List<SaldoUltimoDiaMesAnteriorConta>();

        public SaldoUltimoDiaMesAnterior(List<SaldoUltimoDiaMesAnteriorConta> saldosUltimoDiaMesAnteriorConta) =>
            Contas = saldosUltimoDiaMesAnteriorConta;
    }

    public class SaldoUltimoDiaMesAnteriorConta
    {
        public int ContaId { get; private set; }
        public decimal Valor { get; private set; }
        public bool Selecionada { get; private set; }

        public SaldoUltimoDiaMesAnteriorConta(int contaId, decimal valor)
        {
            ContaId = contaId;
            Valor = valor;
        }
    }
}
