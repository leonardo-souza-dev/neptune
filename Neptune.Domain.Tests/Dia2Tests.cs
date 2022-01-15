using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class Dia2Tests
    {
        [Test]
        public void QuandoUmaTransacao_DeveSerValido()
        {
            // arrange & act
            var sut = new Dia(
                new DateTime(2022, 1, 1),
                new List<Transacao> { new Transacao(1, new DateTime(2022, 1, 1), "Lorem", 1, 1) },
                100);

            // assert
            Assert.AreEqual(sut.Transacoes.Count, 1);
        }

        [Test]
        public void QuandoAdicionarTransacao_DeveSerValido()
        {
            // arrange 
            var sut = new Dia(
                new DateTime(2022, 1, 1),
                new List<Transacao> { new Transacao(1, new DateTime(2022, 1, 1), "Lorem1", 1, 1) },
                100);

            // act
            sut.AdicionarTransacao(new Transacao(2, new DateTime(2022, 1, 1), "Lorem2", 1, 1));

            // assert
            Assert.AreEqual(sut.Transacoes.Count, 2);
        }

        [Test]
        public void QuandoAdicionarTransacaoDesordenadas_DeveOrdernar()
        {
            // arrange 
            var sut = new Dia(
                new DateTime(2022, 1, 1),
                new List<Transacao> { new Transacao(1, new DateTime(2022, 1, 31), "Lorem1", 1, 1) },
                100);

            // act
            sut.AdicionarTransacao(new Transacao(2, new DateTime(2022, 1, 15), "Lorem2", 1, 1));
            sut.AdicionarTransacao(new Transacao(2, new DateTime(2022, 1, 1), "Lorem2", 1, 1));

            // assert
            Assert.AreEqual(sut.Transacoes[0].Data.Day, 1);
            Assert.AreEqual(sut.Transacoes[2].Data.Day, 31);
        }
    }
}