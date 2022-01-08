using Neptune.Web.Data.Response;
using System;

namespace Neptune.Web.ViewModel
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Conta { get; set; }
        public int ContaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public Transacao(TransacaoResponse transacao)
        {
            Id = transacao.Id;
            Descricao = transacao.Descricao;
            ContaId = transacao.ContaId;
            Valor = transacao.Valor;
            Data = transacao.Data;
        }

        public TransacaoResponse Converter()
        {       
            return new TransacaoResponse(Id, 
                                         Descricao, 
                                         Conta, 
                                         ContaId, 
                                         Valor, 
                                         Data);
        }
    }
}
