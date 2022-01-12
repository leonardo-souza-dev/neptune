using Neptune.Ui.Infra.Response;

namespace Neptune.Ui.Models
{
    public class Mes
    {
        public MesTransacao MesTransacao { get;  set; }
        public decimal SaldoUltimoDiaMesAnterior { get; set; }
        public List<Dia>? Dias { get; set; } 
    }
}
