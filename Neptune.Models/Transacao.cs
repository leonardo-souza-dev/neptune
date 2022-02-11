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

        public Transacao(int id, DateTime data, string descricao, decimal valor, Conta conta)
        {
            Id = id;
            Data = data;
            Descricao = descricao;
            Valor = valor;
            Conta = conta;
        }

        public Transacao()
        {
        }

        public bool NovaTransacaoEhValida()
        {
            return !string.IsNullOrEmpty(Descricao) && ContaId > 0;
        }
    }
}
