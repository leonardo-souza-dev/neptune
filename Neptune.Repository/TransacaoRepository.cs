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
        private readonly ICategoriaRepository _categoriaRepository;

        public TransacaoRepository(IContaRepository contaRepository, ICategoriaRepository categoriaRepository)
        {
            _contaRepository = contaRepository;
            _categoriaRepository = categoriaRepository;

            var contas = _contaRepository.ObterTodas();
            var corrente = contas[0];
            var poupanca = contas[1];
            var cartaoCredito = contas[2];

            var categorias = _categoriaRepository.ObterTodas();
            var @base = categorias[0];
            var semCategoria = categorias[1];
            var basico = categorias[2];
            var alimentacao = categorias[3];
            var livre = categorias[4];
            var lazer = categorias[5];
            var casa = categorias[6];

            // MES PASSADO
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now.AddMonths(-1), "transacaoooo", casa, -10, corrente));

            // MES ATUAL
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now, "descricao", alimentacao, -10, corrente));
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now, "zxcvb", livre, 10, poupanca));
            _transacoes.Add(new Transacao(GetNextId(), DateTime.Now.AddDays(1), "transacao", lazer, 5, cartaoCredito));
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
                                         transacao.Categoria,
                                         transacao.Valor,
                                         transacao.Conta);

            _transacoes.Add(novaEntidade);
            _transacoes.Sort((x, y) => x.Data.CompareTo(y.Data));

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
