using Moq;
using Neptune.Domain;
using Neptune.Infra;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application.Tests
{
    public class TransacaoServiceTests
    {
        private readonly Mock<ITransacaoRepository> _transacaoRepository = new();

        [Test]
        public async Task DeveObterMeses()
        {
            // arrange
            var numAno = 2022;
            var numMes = 1;

            var contaCorrente = new Conta(1, "corrente", 1000, true);
            var contaPoupanca = new Conta(2, "poupanca", 1000, true);
            var contas = new List<Conta>
            {
                contaCorrente,
                contaPoupanca
            };

            var transacoes = new List<Transacao>
            {
                new Transacao(2, new DateTime(2021, 12, 1), "compras", -500, contaPoupanca),

                new Transacao(1, new DateTime(numAno, numMes, 25), "boleto", -200, contaCorrente),
                new Transacao(2, new DateTime(numAno, numMes, 25), "rendimento", 100, contaPoupanca)
            };
            _transacaoRepository.Setup(x => x.ObterTodas()).Returns(Task.FromResult(transacoes));
            var sut = new TransacaoService(_transacaoRepository.Object);

            // act
            var meses = await sut.ObterMeses(contas);

            // assert
            Assert.AreEqual(2, meses.MesList.Count);
            Assert.AreEqual(2000, meses.SaldoInicial.Valor);

            var mesDez2021 = meses.ObterMes(new DataMes(2021, 12));
            Assert.AreEqual(2000, mesDez2021.SaldoFinalUltimoDiaMesAnterior.Valor);
            Assert.AreEqual(1500, mesDez2021.SaldoFinalUltimoDia.Valor);

            var mesJan2022 = meses.ObterMes(new DataMes(numAno, numMes));
            Assert.AreEqual(1500, mesJan2022.SaldoFinalUltimoDiaMesAnterior.Valor);
            Assert.AreEqual(1400, mesJan2022.SaldoFinalUltimoDia.Valor);
        }
    }
}