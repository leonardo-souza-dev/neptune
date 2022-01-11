using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neptune.Ui.Models;
using Neptune.Ui.Services;

namespace Neptune.Ui.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PagesServices _pagesServices;

        public List<Transacao> Transacoes { get; set; }
        public List<Conta> Contas { get; private set; }
        public List<Dia> Dias { get; private set; }

        public decimal SaldoUltimoDiaMesAnterior { get; private set; }
        private DateTime _ultimoDiaMesAnterior
        {
            get
            {
                return new DateTime(Ano, Mes, 1).AddDays(-1);
            }
        }
        public string UltimoDiaMesAnterior { get { return _ultimoDiaMesAnterior.ToString("dd/MM/yyyy"); } }


        public string NavMesAnterior => $"/{AnoDoMesAnterior}/{MesAnterior}";
        public string NavMesSeguinte => $"/{AnoDoMesSeguinte}/{MesSeguinte}";
        public int Ano { get; set; } = DateTime.Now.Year;
        public int Mes { get; set; } = DateTime.Now.Month;

        public bool NovaTransacaoVisivel { get; set; }

        public string AnoDoMesAnterior { get; set; }
        public string MesAnterior { get; set; }
        public string AnoDoMesSeguinte { get; set; }
        public string MesSeguinte { get; set; }



        bool _novaTransacaoVisivel;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _pagesServices = new PagesServices();
        }

        public void OnGet()
        {
            Transacoes = _pagesServices.ObterTodasTransacoes();
        }
    }
}