using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Domain.Tests
{
    public class MesesTests
    {
        private Conta _corrente = new Conta(1, "corrente", 100, true);
        private Conta _poupanca = new Conta(1, "poupanca", 100, true);

        [Test]
        public void QuandoUmaTransacaoJaneiro_DeveTerMesJaneiro()
        {
            // arrange
            var transacao1 = new Transacao(1, new DateTime(2022, 1, 1), "Lorem", 1, _corrente);
            var transacao2 = new Transacao(1, new DateTime(2022, 1, 1), "Lorem", 1, _corrente);
            var transacoes = new List<Transacao>
            {
                transacao1,
                transacao2
            };
            var contas = new List<Conta> { new Conta(1, "corrente", 100, true) };

            // act
            var sut = new Meses(transacoes, contas);


            // assert
            Assert.NotNull(sut.ObterMes(new DataMes(2022, 1)));
        }

        [Test]
        public void DadoQueJaTenhaUmaTransacaoJaneiro_QuandoNavegarParaFevereiro_DeveApresentarMesFevereiroVazio()
        {
            // arrange
            var transacoes = new List<Transacao> { new Transacao(1, new DateTime(2022, 1, 1), "Lorem", 1, _corrente) };
            var contas = new List<Conta> { new Conta(1, "corrente", 100, true) };
            var sut = new Meses(transacoes, contas);

            // act
            var actual = sut.ObterMes(new DataMes(2022, 2));

            // assert
            Assert.NotNull(actual);
            Assert.AreEqual("2", actual.NumMes);
            Assert.AreEqual(0, actual.Dias.Count);
        }

        [Test]
        public void DadoQueNaoTenhaNenhumaTransacao__QuandoNavegarParaFevereiro_DeveApresentarMesFevereiroVazio()
        {
            // arrange
            var transacoes = new List<Transacao>();
            var contas = new List<Conta> { new Conta(1, "corrente", 100, true) };
            var sut = new Meses(transacoes, contas);

            // act
            var actual = sut.ObterMes(new DataMes(2022, 2));

            // assert
            Assert.NotNull(actual);
            Assert.AreEqual("2", actual.NumMes);
        }

        [Test]
        public void QuandoDuasTransacoesComUmaDelasFevereiro_DeveTerMesFevereiro()
        {
            // arrange
            var transacoes = new List<Transacao>
            {
                new Transacao(1, new DateTime(2022, 1, 1), "Lorem1", 1, _corrente),
                new Transacao(2, new DateTime(2022, 2, 1), "Lorem2", 1, _corrente)
            };
            var contas = new List<Conta> { new Conta(1, "corrente", 100, true) };

            // act
            var sut = new Meses(transacoes, contas);


            // assert
            Assert.NotNull(sut.ObterMes(new DataMes(2022, 2)));
        }

        [Test]
        public void QuandoTransacoesDesordenadas_DeveOrdenar()
        {
            // arrange
            var conta1 = new Conta(1, "corrente", 100, true);
            var transacoes = new List<Transacao>
            {
                new Transacao(1, new DateTime(2022, 2, 1), "Lorem1", 1, conta1),
                new Transacao(2, new DateTime(2022, 1, 15), "Lorem2", 1, conta1),
                new Transacao(2, new DateTime(2022, 3, 2), "Lorem2", 1, conta1), //ultima
                new Transacao(2, new DateTime(2021, 11, 15), "Lorem2", 1, conta1), //primeira
                new Transacao(2, new DateTime(2022, 1, 1), "Lorem2", 1, conta1)
            };
            var contas = new List<Conta> { new Conta(1, "corrente", 100, true) };

            // act
            var sut = new Meses(transacoes, contas);


            // assert
            Assert.NotNull(sut.TodasTransacoes.FirstOrDefault(x => x.Data.Year == 2021 && x.Data.Month == 11 && x.Data.Day == 15));
            Assert.NotNull(sut.TodasTransacoes.LastOrDefault(x => x.Data.Year == 2022 && x.Data.Month == 3 && x.Data.Day == 2));
        }
    }
}