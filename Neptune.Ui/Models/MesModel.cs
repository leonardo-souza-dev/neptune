using Neptune.Domain;
using Neptune.Ui.Infra.Response;

namespace Neptune.Ui.Models
{
    public class MesModel
    {
        public MesTransacaoModel MesTransacao { get; set; }
        public decimal SaldoUltimoDiaMesAnterior { get; set; }
        public List<DiaModel> Dias { get; set; } = new List<DiaModel>();

        public MesModel(Mes mes)
        {
            MesTransacao = new MesTransacaoModel(mes.MesTransacao);
            SaldoUltimoDiaMesAnterior = mes.SaldoUltimoDiaMesAnterior;
            mes.Dias.ForEach(dia => Dias.Add(new DiaModel(dia)));
        }
    }
}
