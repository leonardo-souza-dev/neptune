using Neptune.Application;
using Neptune.Domain;
using Neptune.Ui.Models;
using System;

namespace Neptune.Ui.Services
{
    public class PagesService : IPagesService
    {
        public IApiHelper _apiHelper;
        private readonly ITransacaoService _transacaoService;
        private readonly IContaService _contaService;

        public PagesService(/*IApiHelper apiHelper,*/
                            ITransacaoService transacaoService,
                            IContaService contaService)
        {
            //_apiHelper = apiHelper;
            _transacaoService = transacaoService;
            _contaService = contaService;
        }

        //public async Task<MesModel?> ObterMes(int numAno, int numMes, List<int> contas)
        //{
        //    var mesResponse = await _apiHelper.ObterMesResponse(numAno, numMes);

        //    return mesResponse?.Result;
        //}

        public async Task<MesModel> ObterMes2(int numAno, int numMes, List<int> contas)
        {
            var mes = await _transacaoService.ObterMes(new MesTransacao(numAno, numMes));

            var mesModel = new MesModel(mes);

            return mesModel;
        }

        public async Task<List<ContaModel>> ObterContas()
        {
            var obterContasResponse = await _apiHelper.ObterContas();

            var contas = new List<ContaModel>();
            obterContasResponse?.Result.ForEach(x => contas.Add(new ContaModel(x.Id, x.Nome, x.SaldoInicial, true)));

            return contas;
        }

        public async Task<List<ContaModel>> ObterContas2()
        {
            var contas = await _contaService.ObterTodas();

            var contasModel = new List<ContaModel>();
            contas.ForEach(x => contasModel.Add(new ContaModel(x.Id, x.Nome, x.SaldoInicial, true)));

            return contasModel;
        }

        public decimal ObterSaldoMesAnterior(int mes,
                                             int ano,
                                             List<ContaModel> contas,
                                             List<TransacaoModel> transacoes)
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

        //Task<MesModel?> ObterMes(int ano, int mes, List<int> contas);
        Task<MesModel?> ObterMes2(int ano, int mes, List<int> contas);
        Task<List<ContaModel>> ObterContas();
        Task<List<ContaModel>> ObterContas2();
        decimal ObterSaldoMesAnterior(int mes,
                                      int ano,
                                      List<ContaModel> contasId,
                                      List<TransacaoModel> transacoes);
    }
}
