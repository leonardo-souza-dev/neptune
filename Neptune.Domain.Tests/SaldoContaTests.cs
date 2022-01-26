using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class SaldoContaTests
    {

        [Test]
        public void DeveaAdicionarValor()
        {
            // arrange 
            var contaCorrente = new Conta(1, "corrente", 100000, true);
            var valor = 100;
            var sut = new SaldoConta(contaCorrente, valor);

            // act
            sut.Adicionar(9);

            // assert
            Assert.AreEqual(109, sut.Valor);
        }
    }
}