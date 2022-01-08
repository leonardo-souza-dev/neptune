using Neptune.Web.ViewModel;
using System;

namespace Neptune.Web.Data.Response
{
    public record TransacaoResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Conta { get; set; }
        public int ContaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public TransacaoResponse(int id, 
            string descricao, 
            string conta, 
            int contaId, 
            decimal valor, 
            DateTime data)
        {
            Id = id;
            Descricao = descricao;
            Conta = conta;
            ContaId = contaId;
            Valor = valor;
            Data = data;
        }

        public Transacao Converter()
        {
            return new Transacao(this);
        }
    }
}