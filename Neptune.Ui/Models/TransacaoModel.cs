﻿using Neptune.Domain;
using Neptune.Ui.Infra.Response;

namespace Neptune.Ui.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int ContaId { get; set; }
        public string NomeConta { get; set; }

        public TransacaoModel(Transacao transacao)
        {
            Id = transacao.Id;
            Descricao = transacao.Descricao;
            Categoria = transacao.Categoria;
            Valor = transacao.Valor;
            Data = transacao.Data;
            ContaId = transacao.ContaId;
        }
    }
}
