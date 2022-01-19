using Neptune.Domain;
using Neptune.Infra;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util;

namespace Neptune.Application
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IContaRepository _contaRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository, IContaRepository contaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
        }

        public async Task<List<Transacao>> ObterTodas()
        {
            return await _transacaoRepository.ObterTodas();
        }

        public async Task<Meses> ObterMeses(List<Conta> contas)
        {
            var todasTransacoes = await ObterTodas();

            var meses = new Meses(todasTransacoes, contas);

            return meses;
        }

        public async Task<Transacao> AdicionarTransacao(Transacao transacao)
        {
            return await _transacaoRepository.Criar(transacao);
        }
    }
}
