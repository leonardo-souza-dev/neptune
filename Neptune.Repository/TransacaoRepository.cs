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

        public TransacaoRepository()
        {
            // setembro
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-4), "Lorem SETEmbro 2021 😉", 1M, 1));
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-4), "Lorem SETEMBRO 2021 😉", 1M, 2));

            // novembro
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-2), "Lorem NOVEMBRO 2021", 1M, 1));
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-2), "Lorem NOVEMBRO 2021", 1M, 2));

            // dezembro
            _transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-1), "Lorem DEZEMBRO 2021 🎅", 1M, 1));
            _transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-1), "Lorem DEZEMBRO 2021 🎅", 1M, 2));

            // TEMP - janeiro
            // hoje
            _transacoes.Add(new Transacao(3, DateTime.Now, "Lorem JANEIRO", 1M, 1));
            _transacoes.Add(new Transacao(5, DateTime.Now, "Lorem JANEIRO", 1M, 2));
            
            // amanha
            _transacoes.Add(new Transacao(5, DateTime.Now.AddDays(1), "Lorem", 1M, 1));
            _transacoes.Add(new Transacao(6, DateTime.Now.AddDays(1), "Lorem", 1M, 2));

            // depois de amanha
            _transacoes.Add(new Transacao(7, DateTime.Now.AddDays(2), "Lorem", 1M, 1));
            _transacoes.Add(new Transacao(7, DateTime.Now.AddDays(2), "Lorem", 1M, 2));

            // fevereiro
            _transacoes.Add(new Transacao(7, DateTime.Now.AddMonths(1), "Lorem", 1M, 1));
            _transacoes.Add(new Transacao(7, DateTime.Now.AddMonths(1), "Lorem", 1M, 2));
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
            return _transacoes.Where(x => x.Data.Month == mes && x.Data.Year == ano && contaIds.Contains(x.ContaId)).ToList();
        }

        public Transacao Criar(Transacao transacao)
        {
            var novaEntidade = new Transacao(GetNextId(),
                                         transacao.Data,
                                         transacao.Descricao,
                                         transacao.Valor,
                                         transacao.ContaId);

            _transacoes.Add(novaEntidade);

            return novaEntidade;
        }

        public Transacao Atualizar(Transacao transacao)
        {
            var transacaoEditada = _transacoes.FirstOrDefault(x => x.Id == transacao.Id);

            transacaoEditada.Data = transacao.Data;
            transacaoEditada.Descricao = transacao.Descricao;
            transacaoEditada.Valor = transacao.Valor;
            transacaoEditada.ContaId = transacao.ContaId;

            return transacaoEditada;
        }

        private int GetNextId()
        {
            return _transacoes.Max(x => x.Id) + 1;
        }
    }
}
