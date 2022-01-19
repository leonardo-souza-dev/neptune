using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Neptune.Domain.Tests
{
    public class MesTests
    {
        [Test]
        public void QuandoUmaContaEUmaTransacaoNova_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var corrente = new Conta(1, "corrente", 1000000, true);
            var saldoFinalUltimoDiaMesAnterior = new Saldo(new List<SaldoConta> { new SaldoConta(corrente, -1000) });
            var sut = new Mes(new DataMes(2022, 1), saldoFinalUltimoDiaMesAnterior);

            // act
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "pao", -10, corrente));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia.Valor, -1010);
        }

        [Test]
        public void QuandoUmaContaEDuasTransacoes_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var corrente = new Conta(1, "corrente", 1000000, true);
            var saldoFinalUltimoDiaMesAnterior = new Saldo(new List<SaldoConta> { new SaldoConta(corrente, -1000) });
            var sut = new Mes(new DataMes(2022, 1), saldoFinalUltimoDiaMesAnterior);
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "Lorem", -10, corrente));

            // act
            sut.AdicionarTransacao(new Transacao(2, new DateTime(2022, 1, 2), "Ipsum", -10, corrente));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia.Valor, -1020);
        }

        [Test]
        public void QuandoDuasContasEUmaTransacaoNova_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var corrente = new Conta(1, "corrente", 1000000, true);
            var poupanca = new Conta(2, "poupanca", 2000000, true);
            var saldoFinalUltimoDiaMesAnterior = new Saldo(new List<SaldoConta>
            {
                new SaldoConta(corrente, -1000),
                new SaldoConta(poupanca, -1000)
            });
            var sut = new Mes(new DataMes(2022, 1), saldoFinalUltimoDiaMesAnterior);

            // act
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "pao", -10, corrente));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia.Valor, -2010);
        }

        [Test]
        public void QuandoTresContasETresTransacoesNovas_DeveTerSaldoFinalUltimoDiaValido()
        {
            // arrange 
            var corrente = new Conta(1, "corrente", 1000000, true);
            var poupanca = new Conta(2, "poupanca", 2000000, true);
            var cartao = new Conta(2, "carta de credito", 0, true);
            var saldoFinalUltimoDiaMesAnterior = new Saldo(new List<SaldoConta>
            {
                new SaldoConta(corrente, -1000),
                new SaldoConta(poupanca, -1000),
                new SaldoConta(cartao, 0)
            });
            var sut = new Mes(new DataMes(2022, 1), saldoFinalUltimoDiaMesAnterior);

            // act
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "pao", -10, corrente));
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "aplicacao", 100, poupanca));
            sut.AdicionarTransacao(new Transacao(1, new DateTime(2022, 1, 1), "compra", -100, cartao));

            // assert
            Assert.AreEqual(sut.SaldoFinalUltimoDia.Valor, -2010);
        }

        [Test]
        public void DadoMesSemDias_QuandoPedirSaldoFinalUltimoDia_EntaoDeveSerSaldoInicialContas()
        {
            // arrange 
            var corrente = new Conta(1, "corrente", 1000000, true);
            var poupanca = new Conta(2, "poupanca", 2000000, true);
            var cartao = new Conta(2, "carta de credito", 0, true);
            var saldoFinalUltimoDiaMesAnterior = new Saldo(new List<SaldoConta>
            {
                new SaldoConta(corrente, -1000),
                new SaldoConta(poupanca, -1000),
                new SaldoConta(cartao, 0)
            });
            var mes = new Mes(new DataMes(2022, 1), saldoFinalUltimoDiaMesAnterior);

            // act
            var actual = mes.SaldoFinalUltimoDia.Valor;

            // assert
            Assert.AreEqual(actual, -2000);
        }
    }
}