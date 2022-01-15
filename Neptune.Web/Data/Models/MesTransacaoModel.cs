using Neptune.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Web.Data.Models
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

        public MesTransacaoModel(DataMes mesTransacao)
        {
            Ano = mesTransacao.Ano;
            Mes = mesTransacao.Mes;
        }
    }
}
