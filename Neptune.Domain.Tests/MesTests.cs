using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class MesTests
    {
        private Conta _cc = new Conta(1, "corrente", 1000000);
        private Conta _cp = new Conta(2, "poupanca", 2000000);

        [Test]
        public void QuandoUmaContaEUmaTransacaoNova_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var saldoFinalUltimoDiaMesAnterior = new Saldo(new List<SaldoConta> { new SaldoConta(_cc, -1000) });
            var sut = new Mes(new DataMes(2022, 1), saldoFinalUltimoDiaMesAnterior);

            // act
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "pao", -10, _cc));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia.Valor, -1010);
        }

        [Test]
        public void QuandoUmaContaEDuasTransacoes_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var saldoFinalUltimoDiaMesAnterior = new Saldo(new List<SaldoConta> { new SaldoConta(_cc, -1000) });
            var sut = new Mes(new DataMes(2022, 1), saldoFinalUltimoDiaMesAnterior);
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "Lorem", -10, _cc));

            // act
            sut.AdicionarTransacao(new Transacao(2, new DateTime(2022, 1, 2), "Ipsum", -10, _cc));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia.Valor, -1020);
        }

        [Test]
        public void QuandoDuasContasEUmaTransacaoNova_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var saldoFinalUltimoDiaMesAnterior = new Saldo(new List<SaldoConta>
            {
                new SaldoConta(_cc, -1000),
                new SaldoConta(_cp, -1000)
            });
            var sut = new Mes(new DataMes(2022, 1), saldoFinalUltimoDiaMesAnterior);

            // act
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "pao", -10, _cc));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia.Valor, -2010);
        }
    }
}