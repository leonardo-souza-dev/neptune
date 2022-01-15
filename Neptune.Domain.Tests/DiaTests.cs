using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class DiaTests
    {
        [Test]
        public void QuandoUmaTransacao_DeveSerValido()
        {
            // arrange & act
            var data = new DateTime(2022, 1, 1);
            var valor = 2M;
            var transacoes = new List<Transacao>
            {
                new Transacao(1, data, "Pao", valor, 1)
            };
            var sut = new DiaOld(data, transacoes, 0);

            // assert
            Assert.AreEqual(sut.SaldoDoDia, -2M);
            Assert.AreEqual(sut.Transacoes.Count, 1);
            Assert.AreEqual(sut.Data.Day, 1);
            Assert.AreEqual(sut.Data.Month, 1);
            Assert.AreEqual(sut.Data.Year, 2022);
        }

        [Test]
        public void QuandoDuaTransacoes_DeveSerValido()
        {
            // arrange & act
            var data = new DateTime(2022, 1, 1);
            var transacoes = new List<Transacao>
            {
                new Transacao(1, data, "Pao", 2M, 1),
                new Transacao(1, data, "Cafe", 4M, 1)
            };
            var sut = new DiaOld(data, transacoes, 0);

            // assert
            Assert.AreEqual(sut.SaldoDoDia, -6M);
            Assert.AreEqual(sut.Transacoes.Count, 2);
            Assert.AreEqual(sut.Data.Day, 1);
            Assert.AreEqual(sut.Data.Month, 1);
            Assert.AreEqual(sut.Data.Year, 2022);
        }
    }
}