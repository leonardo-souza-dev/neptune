using Neptune.Application;
using Neptune.Domain;
using System;

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

        public async Task<Meses> ObterMeses(List<Conta> contas)
        {
            var meses = await _transacaoService.ObterMeses(contas);

            return meses;
        }

        public async Task<List<Conta>> ObterContas()
        {
            return await _contaService.ObterTodas();
        }
    }
}
