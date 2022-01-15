using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class Mes2Tests
    {
        [Test]
        public void QuandoUmaTransacaoNova_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var sut = new Mes(new DataMes(2022, 1), 100);

            // act
            sut.AdicionarTransacao(new Transacao(1, DateTime.Now, "Lorem", 1, 1));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia, 99);
        }

        [Test]
        public void QuandoAdicionarSegundaTransacao_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var sut = new Mes(new DataMes(2022, 1), 100);
            sut.AdicionarTransacao(new Transacao(1, DateTime.Now, "Lorem1", 1, 1));

            // act
            sut.AdicionarTransacao(new Transacao(2, DateTime.Now, "Lorem2", 1, 1));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia, 98);
        }

        [Test]
        public void QuandoTresTransacoesDiasDiferentes_DeveTerTresDiasValidos()
        {
            // arrange & act
            var sut = new Mes(new DataMes(2022, 1), 100);
            sut.AdicionarTransacao(new Transacao(1, DateTime.Now, "Lorem1", 1, 1));
            sut.AdicionarTransacao(new Transacao(2, DateTime.Now.AddDays(1), "Lorem2", 1, 1));
            sut.AdicionarTransacao(new Transacao(3, DateTime.Now.AddDays(2), "Lorem3", 1, 1));

            // assert
            Assert.AreEqual(sut.Dias.Count, 3);
        }
    }
}