using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
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

        public int NumMesAnterior
        {
            get => Mes == 1 ? 12 : Mes - 1;
        }

        public int NumMesSeguinte
        {
            get => Mes == 12 ? 1 : Mes + 1;
        }

        public int NumAnoMesAnterior
        {
            get => new DateTime(Ano, Mes, 1).AddMonths(-1).Year;
        }

        public int NumAnoMesSeguinte
        {
            get => new DateTime(Ano, Mes, 1).AddMonths(1).Year;
        }

        public DateTime UltimoDiaDoMesAnterior 
        { 
            get => new DateTime(Ano, Mes, 1).AddDays(-1);
        }
    }
}
