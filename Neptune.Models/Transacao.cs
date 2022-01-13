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
        public int ContaId { get; set; }

        public Transacao(int id, DateTime data, string descricao, decimal valor, int contaId)
        {
            Id = id;
            Data = data;
            Descricao = descricao;
            Valor = valor;
            ContaId = contaId;
        }
    }
}
