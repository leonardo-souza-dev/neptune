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
            var mesTransacao = new DataMes(2022, 1);
            var saldoContas = new List<SaldoUltimoDiaMesAnteriorConta>() 
            { 
                new SaldoUltimoDiaMesAnteriorConta(1, 100), 
                new SaldoUltimoDiaMesAnteriorConta(2, 100) 
            };
            var saldo = new SaldoUltimoDiaMesAnterior(saldoContas);
            var transacoes = new List<Transacao>
            {
                new Transacao(1, DateTime.Now.AddMonths(-2), "Cafe", 5M, 1),

                new Transacao(1, DateTime.Now.AddMonths(-1), "Cafe", 5M, 1),

                new Transacao(1, DateTime.Now, "Cafe", 5M, 1),
                new Transacao(1, DateTime.Now.AddDays(1), "Cafe", 5M, 1),

                new Transacao(1, DateTime.Now.AddMonths(1), "Cafe", 5M, 1),

                new Transacao(1, DateTime.Now.AddMonths(2), "Cafe", 5M, 1)
            };
            var mes = new MesOld(mesTransacao, saldo, transacoes);

            // assert
            Assert.AreEqual(2, mes.Dias.Count);
            Assert.AreEqual(2022, mes.MesTransacao.Ano);
            Assert.AreEqual(1, mes.MesTransacao.Mes);
            Assert.AreEqual(200, mes.SaldoUltimoDiaMesAnterior.Valor);
            Assert.AreEqual(31, mes.UltimoDiaMesAnterior.Day);
        }

        [Test]
        public void QuandoNaoTiveremTransacoesDoMes_NaoDeveTerDiasDeTransacao()
        {
            // arrange & act
            var mesTransacao = new DataMes(2022, 1);
            var saldoConta1 = new SaldoUltimoDiaMesAnteriorConta(1, 100);
            var saldoConta2 = new SaldoUltimoDiaMesAnteriorConta(2, 100);
            var saldoContas = new List<SaldoUltimoDiaMesAnteriorConta> { saldoConta1, saldoConta2 };
            var saldo = new SaldoUltimoDiaMesAnterior(saldoContas);
            var transacoes = new List<Transacao>
            {
                new Transacao(1, DateTime.Now.AddMonths(-2), "Cafe", 1M, 1),
                new Transacao(2, DateTime.Now.AddMonths(-2), "Cafe", 1M, 2),

                new Transacao(3, DateTime.Now.AddMonths(-1), "Cafe", 1M, 1),
                new Transacao(4, DateTime.Now.AddMonths(-1), "Cafe", 1M, 2),

                new Transacao(5, DateTime.Now.AddMonths(1), "Cafe", 1M, 1),
                new Transacao(6, DateTime.Now.AddMonths(1), "Cafe", 1M, 2),

                new Transacao(7, DateTime.Now.AddMonths(2), "Cafe", 1M, 1),
                new Transacao(8, DateTime.Now.AddMonths(2), "Cafe", 1M, 2)
            };
            var mes = new MesOld(mesTransacao, saldo, transacoes);
         
            // assert
            Assert.AreEqual(0, mes.Dias.Count);
        }
    }
}