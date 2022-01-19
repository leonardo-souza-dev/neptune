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

            //// setembro
            //_transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-4), "Pgto conta", 0, corrente));
            //_transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-4).AddDays(1), "Deposito", 0, poupanca));
            //_transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-4).AddDays(2), "Compra", 0, cartaoCredito));

            ////// novembro
            //_transacoes.Add(new Transacao(3, DateTime.Now.AddMonths(-2), "Lorem NOVEMBRO 2021", 0, corrente));
            //_transacoes.Add(new Transacao(4, DateTime.Now.AddMonths(-2), "Lorem NOVEMBRO 2021", 0, poupanca));

            //// dezembro
            _transacoes.Add(new Transacao(5, DateTime.Now.AddMonths(-1), "conta", -10, corrente));
            _transacoes.Add(new Transacao(6, DateTime.Now.AddMonths(-1), "aplicacao", 10, poupanca));
            _transacoes.Add(new Transacao(6, DateTime.Now.AddMonths(-1), "compra", -10, cartaoCredito));

            ////// TEMP - janeiro
            ////// hoje
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now, "estorno conta", 1, corrente));
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now, "aplicacao", 1, poupanca));
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now, "compra", 0, cartaoCredito));


            ////// amanha
            //_transacoes.Add(new Transacao(9, DateTime.Now.AddDays(1), "Lorem", 0, corrente));
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
            if (_transacoes.Any())
                return _transacoes.Max(x => x.Id) + 1;
            else
                return 1;
        }
    }
}
