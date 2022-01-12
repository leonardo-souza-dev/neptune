using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neptune.Ui.Models;
using Neptune.Ui.Services;

namespace Neptune.Ui.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPagesService _pagesService;

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



        public List<int> ContasSelecionadas { get; set; }
        public List<Conta> Contas { get; set; }
        public Mes MesTransacoes { get; private set; }


        public IndexModel(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }

        public async Task OnGet()
        {
            Contas = await _pagesService.ObterContas();
            ContasSelecionadas = Contas.Where(x => x.Ativo).Select(x => x.Id).ToList();

            MesTransacoes = await _pagesService.ObterMes(Ano, Mes, ContasSelecionadas);
        }
    }
}