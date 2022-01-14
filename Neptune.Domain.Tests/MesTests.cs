using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class MesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeveSerValido()
        {
            // arrange & act
            var mesTransacao = new MesTransacao(2022, 1);
            var saldoConta1 = new SaldoUltimoDiaMesAnteriorConta(1, 100);
            var saldoConta2 = new SaldoUltimoDiaMesAnteriorConta(2, 100);
            var saldoContas = new List<SaldoUltimoDiaMesAnteriorConta>();
            saldoContas.Add(saldoConta1);
            saldoContas.Add(saldoConta2);
            var saldo = new SaldoUltimoDiaMesAnterior(saldoContas);
            var transacoes = new List<Transacao>
            {
                new Transacao(1, DateTime.Today.AddMonths(-2), "Cafe", 5M, 1),

                new Transacao(1, DateTime.Today.AddMonths(-1), "Cafe", 5M, 1),

                new Transacao(1, DateTime.Today, "Cafe", 5M, 1),
                new Transacao(1, DateTime.Today.AddDays(1), "Cafe", 5M, 1),

                new Transacao(1, DateTime.Today.AddMonths(1), "Cafe", 5M, 1),

                new Transacao(1, DateTime.Today.AddMonths(2), "Cafe", 5M, 1)
            };
            var mes = new Mes(mesTransacao, saldo, transacoes, new List<Conta> { new Conta(1, "conta corrente", 100) });

            // assert
            Assert.AreEqual(mes.Dias.Count, 2);
            Assert.AreEqual(mes.MesTransacao.Ano, 2022);
            Assert.AreEqual(mes.MesTransacao.Mes, 1);
            Assert.AreEqual(mes.SaldoUltimoDiaMesAnterior.Valor, 200);
            Assert.AreEqual(mes.UltimoDiaMesAnterior.Day, 31);
        }
    }
}