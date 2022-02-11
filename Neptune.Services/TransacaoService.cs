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

        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<List<Transacao>> ObterTodas()
        {
            return await _transacaoRepository.ObterTodas();
        }

        public async Task<Transacao> AdicionarTransacao(Transacao transacao)
        {
            return await _transacaoRepository.Criar(transacao);
        }
    }
}
