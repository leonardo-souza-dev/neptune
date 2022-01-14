using Neptune.Domain;

namespace Neptune.Web.Data.Models
{
    public class MesModel
    {
        public MesTransacaoModel MesTransacao { get; set; }
        public SaldoUltimoDiaMesAnterior SaldoUltimoDiaMesAnterior { get; set; }
        public decimal SaldoUltimoDiaMesAnterior2 { get; set; }
        public string UltimoDiaMesAnterior { get; set; }
        public List<DiaModel> Dias { get; set; } = new List<DiaModel>();

        public int AnoDoMesAnterior { get; }
        public int MesAnterior { get; }
        public int AnoDoMesSeguinte { get; }
        public int MesSeguinte { get; }

        public string NavMesAnterior => $"/{AnoDoMesAnterior}/{MesAnterior}";
        public string NavMesSeguinte => $"/{AnoDoMesSeguinte}/{MesSeguinte}";

        public MesModel(Mes mes)
        {
            MesTransacao = new MesTransacaoModel(mes.MesTransacao);
            SaldoUltimoDiaMesAnterior = mes.SaldoUltimoDiaMesAnterior;
            SaldoUltimoDiaMesAnterior2 = mes.SaldoUltimoDiaMesAnterior.Valor;
            mes.Dias.ForEach(dia => Dias.Add(new DiaModel(dia)));
            UltimoDiaMesAnterior = mes.UltimoDiaMesAnterior.ToString("dd/MM/yyyy");

            AnoDoMesAnterior = mes.MesTransacao.NumAnoMesAnterior;
            MesAnterior = mes.MesTransacao.NumMesAnterior;
            AnoDoMesSeguinte = mes.MesTransacao.NumAnoMesSeguinte;
            MesSeguinte = mes.MesTransacao.NumMesSeguinte;
        }
    }
}
