using Neptune.Domain;
using Neptune.Ui.Infra.Response;

namespace Neptune.Ui.Models
{
    public class DiaModel
    {
        public List<TransacaoModel> Transacoes { get; set; } = new List<TransacaoModel>();
        private decimal SaldoDoDiaAnterior { get; set; }
        public decimal SaldoDoDia { get; set; }
        public DateTime Data { get; set; }

        public DiaModel(Dia dia)
        {
            dia.Transacoes.ForEach(t => Transacoes.Add(new TransacaoModel(t)));
            SaldoDoDia = dia.SaldoDoDia;
            Data = dia.Data;
        }
      
        public decimal? ObterSaldoDoDia()
        {
            return SaldoDoDiaAnterior - Transacoes?.Sum(x => x.Valor);
        }
    }
}
