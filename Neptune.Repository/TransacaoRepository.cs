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
            var corrente = contas[0];
            var poupanca = contas[1];
            var cartaoCredito = contas[2];

            //// dez
            //_transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-4), "Pgto conta", 0, corrente));
            //_transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-4).AddDays(1), "Deposito", 0, poupanca));
            //_transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-4).AddDays(2), "Compra", 0, cartaoCredito));

            ////// jan
            //_transacoes.Add(new Transacao(3, DateTime.Now.AddMonths(-2), "Lorem NOVEMBRO 2021", 0, corrente));
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now.AddMonths(-1), "Lorem JAN 2021", -10, corrente));

            //// fev
            //_transacoes.Add(new Transacao(5, DateTime.Now.AddMonths(-1), "conta", -10, corrente));
            //_transacoes.Add(new Transacao(6, DateTime.Now.AddMonths(-1), "aplicacao", 10, poupanca));
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now, "compra FEV", 5, corrente));

            ////// mar
            ////// ontem
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now.AddMonths(1).AddDays(-1), "estorno MAR", 11, corrente));

            ////// hoje
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now.AddMonths(1), "transacao x MAR", -7, corrente));
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now.AddMonths(1), "aplicacao MAR", 2, corrente));
            //_transacoes.Add(new Transacao(GetNextId(), DateTime.Now, "compra", 0, cartaoCredito));


            ////// amanha
            //_transacoes.Add(new Transacao(GetNextId(), DateTime.Now.AddDays(1), "Lorem", 1, corrente));
            //_transacoes.Add(new Transacao(10, DateTime.Now.AddDays(1), "Lorem", 0, poupanca));

            ////// depois de amanha
            //_transacoes.Add(new Transacao(11, DateTime.Now.AddDays(2), "Lorem", 0, corrente));
            //_transacoes.Add(new Transacao(12, DateTime.Now.AddDays(2), "Lorem", 0, poupanca));

            ////// fevereiro
            //_transacoes.Add(new Transacao(13, DateTime.Now.AddMonths(1), "Lorem", 0, corrente));
            //_transacoes.Add(new Transacao(14, DateTime.Now.AddMonths(1), "Lorem", 0, poupanca));
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

        public async Task<Transacao> Criar(Transacao transacao)
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
            if (_transacoes.Any())
                return _transacoes.Max(x => x.Id) + 1;
            else
                return 1;
        }
    }
}
