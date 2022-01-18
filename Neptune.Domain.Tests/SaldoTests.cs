using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class SaldoTests
    {
        private Conta _contaCorrente = new Conta(1, "corrente", 100);
        private Conta _contaPoupanca = new Conta(2, "poupanca", 100);

        [Test]
        public void DeveCriarAPartirDeTransacoes()
        {
            // arrange & act
            var transacao1 = new Transacao(1, DateTime.Now, "pao", 2, _contaCorrente);
            var transacao2 = new Transacao(1, DateTime.Now, "rendimento", 2, _contaPoupanca);

            var actual = new Saldo(new List<Transacao> { transacao1, transacao2 });

            // assert
            Assert.AreEqual(2, actual.SaldoContas.Count, 2);
            Assert.AreEqual(4, actual.Valor);
        }

        [Test]
        [TestCase(5, 50, 55)]
        [TestCase(-1000, 2000, 1000)]
        public void DeveCriarAPartirDeListSaldoContas(int valor1, int valor2, int valorFinal)
        {
            // arrange & ac
            var saldosContas = new List<SaldoConta>
            {
                new SaldoConta(_contaCorrente, valor1),
                new SaldoConta(_contaPoupanca, valor2)
            };
            var actual = new Saldo(saldosContas);

            // assert
            Assert.AreEqual(2, actual.SaldoContas.Count);
            Assert.AreEqual(valorFinal, actual.Valor);
        }

        [Test]
        public void DeveSomar()
        {
            // arrange
            var transacao1 = new Transacao(1, DateTime.Now, "pao", 1, _contaCorrente);
            var transacao2 = new Transacao(1, DateTime.Now, "rendimento", 2, _contaPoupanca);
            var actual = new Saldo(new List<Transacao> { transacao1, transacao2 });

            var transacao3 = new Transacao(1, DateTime.Now, "pao", 1, _contaCorrente);
            var transacao4 = new Transacao(1, DateTime.Now, "rendimento", 2, _contaPoupanca);
            var actual2 = new Saldo(new List<Transacao> { transacao3, transacao4 });

            // act
            actual.Adicionar(actual2);


            // assert
            Assert.AreEqual(2, actual.SaldoContas.Count, 2);
            Assert.AreEqual(6, actual.Valor);
        }
    }
}