using Neptune.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Ui.Models
{
    public struct MesTransacaoModel
    {
        public int Ano { get; private set; }
        public int Mes { get; private set; }

        public MesTransacaoModel(int ano, int mes)
        {
            Ano = ano;
            Mes = mes;
        }

        public MesTransacaoModel(MesTransacao mesTransacao)
        {
            Ano = mesTransacao.Ano;
            Mes = mesTransacao.Mes;
        }

        public MesTransacaoModel ObterMesAnterior()
        {
            bool ehJaneiro = Mes == 1;
            return new MesTransacaoModel { Ano = ehJaneiro ? Ano - 1 : Ano, Mes = ehJaneiro ? 12 : Mes - 1 };
        }
    }
}
