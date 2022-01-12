using Neptune.Ui.Models;
using System;

namespace Neptune.Ui.Infra.Response
{
    public record DiaDto
    {
        public int NumeroDia { get; set; }
        public DateTime Data { get; set; }
        public List<TransacaoDto> Transacoes { get; set; }
    }

    public class TransacaoDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int ContaId { get; set; }
        public decimal Valor { get; set; }
    }
}