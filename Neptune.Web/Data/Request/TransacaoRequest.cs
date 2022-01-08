using System;

namespace Neptune.Web.Data.Request
{
    public class TransacaoRequest
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int ContaId { get; set; }

        public TransacaoRequest(int id,
                                string descricao,
                                decimal valor,
                                DateTime data,
                                int contaId)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            ContaId = contaId;
        }

        public TransacaoRequest(string descricao,
                                decimal valor,
                                DateTime data,
                                int contaId)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            ContaId = contaId;
        }
    }
}