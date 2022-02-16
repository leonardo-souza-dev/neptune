using Neptune.Domain;
using Neptune.Domain.Utils;

namespace Neptune.Web.ViewModel
{
    public class DiaViewModel
    {
        
    }

    public class TransacaoViewModel
    {
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Conta { get; set; }
        public string Valor { get; set; }
        public DateTime Data { get; set; }
    }

    public class ContaViewModel
    {
        public string Nome { get; set; }
        public bool Selecionada { get; set; }
    }
}
