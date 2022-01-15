using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Domain;
using System.Threading.Tasks;

namespace Neptune.Infra
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly List<Transacao> _transacoes = new();
        private readonly IContaRepository _contaRepository;

        public TransacaoRepository(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;

            var contas = _contaRepository.ObterTodas();
            var conta1 = contas[0];
            var conta2 = contas[1];

            // setembro
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-4), "Lorem SETEmbro 2021 😉", 1M, conta1));
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-4), "Lorem SETEMBRO 2021 😉", 1M, conta2));

            // novembro
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-2), "Lorem NOVEMBRO 2021", 1M, conta1));
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-2), "Lorem NOVEMBRO 2021", 1M, conta2));

            // dezembro
            _transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-1), "Lorem DEZEMBRO 2021 🎅", 1M, conta1));
            _transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-1), "Lorem DEZEMBRO 2021 🎅", 1M, conta2));

            // TEMP - janeiro
            // hoje
            _transacoes.Add(new Transacao(3, DateTime.Now, "Lorem JANEIRO", 1M, conta1));
            _transacoes.Add(new Transacao(5, DateTime.Now, "Lorem JANEIRO", 1M, conta2));
            
            // amanha
            _transacoes.Add(new Transacao(5, DateTime.Now.AddDays(1), "Lorem", 1M, conta1));
            _transacoes.Add(new Transacao(6, DateTime.Now.AddDays(1), "Lorem", 1M, conta2));

            // depois de amanha
            _transacoes.Add(new Transacao(7, DateTime.Now.AddDays(2), "Lorem", 1M, conta1));
            _transacoes.Add(new Transacao(7, DateTime.Now.AddDays(2), "Lorem", 1M, conta2));

            // fevereiro
            _transacoes.Add(new Transacao(7, DateTime.Now.AddMonths(1), "Lorem", 1M, conta1));
            _transacoes.Add(new Transacao(7, DateTime.Now.AddMonths(1), "Lorem", 1M, conta2));
        }

        public async Task<List<Transacao>> ObterTodas()
        {
            return _transacoes;
        }

        public Transacao Obter(int id)
        {
            return _transacoes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Transacao>> ObterMesContas(int ano, int mes, List<int> contaIds)
        {
            return _transacoes.Where(x => x.Data.Month == mes && x.Data.Year == ano && contaIds.Contains(x.Conta.Id)).ToList();
        }

        public Transacao Criar(Transacao transacao)
        {
            var novaEntidade = new Transacao(GetNextId(),
                                         transacao.Data,
                                         transacao.Descricao,
                                         transacao.Valor,
                                         transacao.Conta);

            _transacoes.Add(novaEntidade);

            return novaEntidade;
        }

        public Transacao Atualizar(Transacao transacao)
        {
            var transacaoEditada = _transacoes.FirstOrDefault(x => x.Id == transacao.Id);

            transacaoEditada.Data = transacao.Data;
            transacaoEditada.Descricao = transacao.Descricao;
            transacaoEditada.Valor = transacao.Valor;
            transacaoEditada.Conta = transacao.Conta;

            return transacaoEditada;
        }

        private int GetNextId()
        {
            return _transacoes.Max(x => x.Id) + 1;
        }
    }
}
