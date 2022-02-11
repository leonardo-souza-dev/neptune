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

        public async Task<Meses> ObterMeses_old(List<Conta> contas)
        {
            var meses = await _transacaoService.ObterMeses_old(contas);

            return meses;
        }

        public async Task<Meses2> ObterMeses(List<Conta> contas)
        {
            var meses = await _transacaoService.ObterMeses(contas);

            return meses;
        }
        public async Task<List<Transacao>> ObterTodasTransacoes()
        {
            var transacoes = await _transacaoService.ObterTodas();

            return transacoes;
        }

        public async Task<string> AdicionarTransacao_old(Meses meses, Transacao novaTransacao)
        {
            string mensagem = "";
            try
            {
                Transacao transacaoPersistida = await _transacaoService.AdicionarTransacao(novaTransacao);

                if (transacaoPersistida.Id > 0)
                {
                    var mes = meses.ObterMes(new DataMes(novaTransacao.Data.Year, novaTransacao.Data.Month));
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
        public async Task<string> AdicionarTransacao(Meses2 meses, Transacao novaTransacao)
        {
            string mensagem = "";
            try
            {
                Transacao transacaoPersistida = await _transacaoService.AdicionarTransacao(novaTransacao);

                if (transacaoPersistida.Id > 0)
                {
                    var mes = meses.ObterMes(new DataMes(novaTransacao.Data.Year, novaTransacao.Data.Month));
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
