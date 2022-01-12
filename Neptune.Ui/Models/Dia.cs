using Neptune.Ui.Infra.Response;

namespace Neptune.Ui.Models
{
    public class Dia
    {
        public List<Transacao>? Transacoes { get; set; } 
        private decimal SaldoDoDiaAnterior { get; set; }
        public decimal SaldoDoDia { get; set; }
        public DateTime Data { get; set; }

      
        public decimal? ObterSaldoDoDia()
        {
            return SaldoDoDiaAnterior - Transacoes?.Sum(x => x.Valor);
        }
    }
}
