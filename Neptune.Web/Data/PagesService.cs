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

        public async Task<string> AdicionarTransacao(Mes mes, Transacao novaTransacao)
        {
            string mensagem = "";
            Transacao transacaoPersistida = null;
            try
            {
                transacaoPersistida = await _transacaoService.AdicionarTransacao(novaTransacao);

                if (transacaoPersistida.Id > 0)
                {
                    mes.AdicionarTransacao(novaTransacao);

                    mes.LimparNovaTransacao();

                    return mensagem;
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }
            
            
            return mensagem;
        }

        public async Task<List<Conta>> ObterContas()
        {
            return await _contaService.ObterTodas();
        }
    }
}
