using Neptune.Ui.Models;
using System;

namespace Neptune.Ui.Services
{
    public class PagesService : IPagesService
    {
        public IApiHelper _apiHelper;

        public PagesService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<Mes?> ObterMes(int numAno, int numMes, List<int> contas)
        {
            var mesResponse = await _apiHelper.ObterMesResponse(numAno, numMes);

            return mesResponse?.Result;
        }

        public async Task<List<Conta>> ObterContas()
        {
            var obterContasResponse = await _apiHelper.ObterContas();

            var contas = new List<Conta>();
            obterContasResponse?.Result.ForEach(x => contas.Add(new Conta(x.Id, x.Nome, x.SaldoInicial, true)));

            return contas;
        }

        public decimal ObterSaldoMesAnterior(int mes,
                                             int ano,
                                             List<Conta> contas,
                                             List<Transacao> transacoes)
        {
            var saldoInicialContas = contas.Sum(x => x.SaldoInicial);

            var transacoesMesPassadoPraTras = transacoes.Where(x => x.Data.Month < mes && x.Data.Year <= ano);
            var saldoMesAnterior = saldoInicialContas - transacoesMesPassadoPraTras
                                                            .Where(x => contas.Select(x => x.Id).Contains(x.ContaId))
                                                            .Sum(x => x.Valor);

            return saldoMesAnterior;
        }
    }

    public interface IPagesService
    {

        Task<Mes?> ObterMes(int ano, int mes, List<int> contas);
        Task<List<Conta>> ObterContas();
        decimal ObterSaldoMesAnterior(int mes,
                                      int ano,
                                      List<Conta> contasId,
                                      List<Transacao> transacoes);
    }
}
