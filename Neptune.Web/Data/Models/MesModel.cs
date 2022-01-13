using Neptune.Domain;

namespace Neptune.Web.Data.Models
{
    public class MesModel
    {
        public MesTransacaoModel MesTransacao { get; set; }
        public SaldoUltimoDiaMesAnterior SaldoUltimoDiaMesAnterior { get; set; }
        public List<DiaModel> Dias { get; set; } = new List<DiaModel>();

        public MesModel(Mes mes)
        {
            MesTransacao = new MesTransacaoModel(mes.MesTransacao);
            SaldoUltimoDiaMesAnterior = mes.SaldoUltimoDiaMesAnterior;
            mes.Dias.ForEach(dia => Dias.Add(new DiaModel(dia)));
        }
    }
}
