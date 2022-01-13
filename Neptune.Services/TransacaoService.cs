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

        public async Task<Mes> ObterMes(MesTransacao mesTransacao)
        {
            var transacoes = await _transacaoRepository.Obter(mesTransacao.Ano, mesTransacao.Mes);
            
            var saldoUltimoDiaMesAnterior = await ObterSaldoUltimoDiaMesAnterior(mesTransacao);

            var mes = new Mes(mesTransacao, saldoUltimoDiaMesAnterior, transacoes);

            return mes;
        }

        public Transacao Criar(Transacao transacao)
        {
            return _transacaoRepository.Criar(transacao);
        }

        public Transacao Atualizar(Transacao transacao)
        {
            return _transacaoRepository.Atualizar(transacao);
        }

        private async Task<SaldoUltimoDiaMesAnterior> ObterSaldoUltimoDiaMesAnterior(MesTransacao mesTransacao)
        {
            var primeiroDiaDoMes = new DateTime(mesTransacao.Ano, mesTransacao.Mes, 1);

            var todasTransacoes = await _transacaoRepository.ObterTodas();
            var transacoesMesPassadoPraTras = todasTransacoes
                .Where(x => x.Data < primeiroDiaDoMes);

            var contas = await _contaRepository.ObterTodas();

            var saldoUltimoDiaMesAnteriorContas = new List<SaldoUltimoDiaMesAnteriorConta>();

            contas.ForEach(conta =>
            {
                var somaMes = transacoesMesPassadoPraTras.Where(t => t.ContaId == conta.Id).Sum(x => x.Valor);
                var valor = conta.SaldoInicial - somaMes;

                saldoUltimoDiaMesAnteriorContas.Add(new SaldoUltimoDiaMesAnteriorConta(conta.Id, valor));
            });

            var saldoUltimoDiaMesAnterior = new SaldoUltimoDiaMesAnterior(saldoUltimoDiaMesAnteriorContas);

            return saldoUltimoDiaMesAnterior;
        }
    }
}
