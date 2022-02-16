using Neptune.Domain;
using Neptune.Infra;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util;
using Neptune.Domain.Utils;

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

        public async Task<List<Mes>> ObterTodosMeses(List<Conta> contas)
        {
            var transacoes =  await _transacaoRepository.ObterTodas();

            var meses = new List<Mes>();
            var transacoesMes = transacoes.GroupBy(x => new { x.Data.Year, x.Data.Month }).ToList();
            var saldoContas = contas.Where(x => x.Selecionada).Sum(x => x.SaldoInicial);

            foreach (var t in transacoesMes)
            {
                var dataMesAtual = new DataMes(t.Key.Year, t.Key.Month);
                var transacoesAnteriores = transacoes.Where(x => x.Data.EhAntes(dataMesAtual)).ToList();
                var somaTransacoesAnteriores = transacoesAnteriores.Sum(x => x.Valor);

                decimal? saldoFinalUltimoDiaMesAnterior = saldoContas + somaTransacoesAnteriores;

                meses.Add(new Mes(t.ToList(), contas, dataMesAtual, saldoFinalUltimoDiaMesAnterior.Value));
            }
            
            return meses;
        }

        public async Task<Transacao> AdicionarTransacao(Transacao transacao)
        {
            return await _transacaoRepository.Criar(transacao);
        }
    }
}
