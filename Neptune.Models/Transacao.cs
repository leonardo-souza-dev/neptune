using System;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Domain
{
    public class Transacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public Conta Conta { get; set; }
        public int ContaId => Conta.Id;

        public Guid Guid { get; set; } = Guid.NewGuid();

        public Transacao(int id, DateTime data, string descricao, decimal valor, Conta conta)
        {
            Id = id;
            Data = data;
            Descricao = descricao;
            Valor = valor;
            Conta = conta;
        }

        public Transacao(DateTime data, string descricao, decimal valor, Conta conta)
        {
            Data = data;
            Descricao = descricao;
            Valor = valor;
            Conta = conta;
        }

        public Transacao()
        {
        }

        public Transacao ObterNovaTransacao()
        {
            Descricao = "descricao";
            Conta = new Conta(0, "selecionada", 0, true);

            return this;
        }

        public bool NovaTransacaoEhValida()
        {
            return !string.IsNullOrEmpty(Descricao) && ContaId > 0;
        }
    }
}
