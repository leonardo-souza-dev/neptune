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

        public async Task<Meses> ObterMeses()
        {
            var meses = await _transacaoService.ObterMeses();

            return meses;
        }

        public async Task<List<Conta>> ObterContas() =>
            await _contaService.ObterTodas();
    }
}
