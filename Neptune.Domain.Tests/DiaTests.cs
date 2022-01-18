using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class DiaTests
    {
        private Conta _cartao = new Conta(1, "cartao", 100000000);
        private Conta _poupanca = new Conta(2, "poupanca", 200000000);

        [Test]
        public void QuandoUmaContaUmaTransacao_DeveSerValido()
        {
            // arrange & act
            var dia = new DateTime(2022, 1, 1);
            var transacoes = new List<Transacao> { new Transacao(1, dia, "viagem", -2000, _poupanca) };
            var saldoContaPoupanca = new SaldoConta(_poupanca, 5000M);
            var sut = new Dia(
                dia,
                transacoes,
                new Saldo(new List<SaldoConta> { saldoContaPoupanca }));

            // assert
            Assert.AreEqual(sut.Transacoes.Count, 1);
            Assert.AreEqual(3000, sut.SaldoFinalDoDia.Valor);
        }

        [Test]
        public void QuandoUmaContaDuasTransacoes_DeveSerValido()
        {
            // arrange & act
            var dia = new DateTime(2022, 1, 1);
            var transacoes = new List<Transacao> { new Transacao(1, dia, "viagem", -2000, _poupanca) };
            var saldoContaPoupanca = new SaldoConta(_poupanca, 5000M);
            var sut = new Dia(
                dia,
                transacoes,
                new Saldo(new List<SaldoConta> { saldoContaPoupanca }));

            //act
            var transacao2 = new Transacao(1, dia, "livro", -1000, _poupanca);
            sut.AdicionarTransacao(transacao2);

            // assert
            Assert.AreEqual(sut.Transacoes.Count, 2);
            Assert.AreEqual(2000, sut.SaldoFinalDoDia.Valor);
        }

        //[Test]
        //public void QuandoAdicionarTransacao_DeveSerValido()
        //{
        //    // arrange 
        //    var sut = new Dia(
        //        new DateTime(2022, 1, 1),
        //        new List<Transacao> { new Transacao(1, new DateTime(2022, 1, 1), "Lorem1", 1, _conta1) },
        //        100);

        //    // act
        //    sut.AdicionarTransacao(new Transacao(2, new DateTime(2022, 1, 1), "Lorem2", 1, _conta1));

        //    // assert
        //    Assert.AreEqual(sut.Transacoes.Count, 2);
        //}

        //[Test]
        //public void QuandoAdicionarTransacaoDesordenadas_DeveOrdernar()
        //{
        //    // arrange 
        //    var sut = new Dia(
        //        new DateTime(2022, 1, 1),
        //        new List<Transacao> { new Transacao(1, new DateTime(2022, 1, 31), "Lorem1", 1, _conta1) },
        //        100);

        //    // act
        //    sut.AdicionarTransacao(new Transacao(2, new DateTime(2022, 1, 15), "Lorem2", 1, _conta1));
        //    sut.AdicionarTransacao(new Transacao(2, new DateTime(2022, 1, 1), "Lorem2", 1, _conta1));

        //    // assert
        //    Assert.AreEqual(sut.Transacoes[0].Data.Day, 1);
        //    Assert.AreEqual(sut.Transacoes[2].Data.Day, 31);
        //}
    }
}