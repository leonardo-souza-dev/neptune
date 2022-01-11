using Neptune.Ui.Models;

namespace Neptune.Ui.Services
{
    public class PagesServices
    {
        public List<Transacao> ObterTodasTransacoes()
        {
            return new List<Transacao> 
            { 
                new Transacao(1, "cafee", "alimentacao", 4.5M, DateTime.Now.AddDays(-1), 1),
                new Transacao(2, "uber", "transporte", 15M, DateTime.Now.AddDays(-1), 2),
                new Transacao(3, "frutas", "alimentacao", 28M, DateTime.Now.AddDays(-1), 1)
            };
        }
    }
}
