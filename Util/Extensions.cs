using System;

namespace Util
{
    public static class Extensions
    {
        public static int ObterMesAnterior(this DateTime data)
        {
            if (data.Month == 1)
                return 12;
            else
                return data.Month - 1;
        }

        public static int ObterMesAnterior(this int mes)
        {
            if (mes == 1)
                return 12;
            else
                return mes - 1;
        }

        public static string FormatarMoeda(this decimal valor)
        {
            return $"R$ {((valor.ToString().Contains('.') || valor.ToString().Contains(',')) ? valor.ToString() : valor + ",00")}";
        }
    }
}
