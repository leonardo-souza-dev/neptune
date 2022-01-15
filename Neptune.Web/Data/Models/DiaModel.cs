using Neptune.Domain;

namespace Neptune.Web.Data.Models
{
    public class DiaModel
    {
        public List<TransacaoModel> Transacoes { get; set; } = new List<TransacaoModel>();
        public decimal SaldoDoDia { get; set; }
        public DateTime Data { get; set; }

        public DiaModel(DiaOld dia)
        {
            dia.Transacoes.ForEach(t => Transacoes.Add(new TransacaoModel(t)));
            SaldoDoDia = dia.SaldoDoDia;
            Data = dia.Data;
        }
    }
}
