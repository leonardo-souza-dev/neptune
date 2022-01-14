using Neptune.Application;
using Neptune.Domain;
using System;
using Neptune.Web.Data.Models;

namespace Neptune.Web.Data
{
    public class PagesService 
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IContaService _contaService;

        public PagesService(ITransacaoService transacaoService, IContaService contaService)
        {
            _transacaoService = transacaoService;
            _contaService = contaService;
        }

        public async Task<MesModel> ObterMes(int numAno, int numMes, List<ContaModel> contasModel)
        {
            var contas = new List<Conta>();
            contasModel.ForEach(x => contas.Add(x.ToDomain()));

            var mes = await _transacaoService.ObterMes(new MesTransacao(numAno, numMes), contas);
            var mesModel = new MesModel(mes);

            return mesModel;
        }

        public async Task<List<ContaModel>> ObterContas()
        {
            var contas = await _contaService.ObterTodas();

            var contasModel = new List<ContaModel>();
            contas.ForEach(x => contasModel.Add(new ContaModel(x.Id, x.Nome, x.SaldoInicial, true)));

            return contasModel;
        }

        public decimal ObterSaldoMesAnterior(int mes, int ano, List<ContaModel> contas, List<TransacaoModel> transacoes)
        {
            var saldoInicialContas = contas.Sum(x => x.SaldoInicial);

            var transacoesMesPassadoPraTras = transacoes.Where(x => x.Data.Month < mes && x.Data.Year <= ano);
            var saldoMesAnterior = saldoInicialContas - transacoesMesPassadoPraTras
                                                            .Where(x => contas.Select(x => x.Id).Contains(x.ContaId))
                                                            .Sum(x => x.Valor);

            return saldoMesAnterior;
        }
    }
}
