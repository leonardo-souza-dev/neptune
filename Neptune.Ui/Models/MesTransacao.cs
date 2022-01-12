using Neptune.Ui.Infra.Response;

namespace Neptune.Ui.Models
{
    public struct MesTransacao
    {
        public int Ano { get; private set; }
        public int Mes { get; private set; }

        public MesTransacao(int ano, int mes)
        {
            Ano = ano;
            Mes = mes;
        }

        public MesTransacao ObterMesAnterior()
        {
            bool ehJaneiro = Mes == 1;
            return new MesTransacao { Ano = ehJaneiro ? Ano - 1 : Ano, Mes = ehJaneiro ? 12 : Mes - 1 };
        }
    }
}
