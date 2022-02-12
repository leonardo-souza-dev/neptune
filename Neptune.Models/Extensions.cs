using System;

namespace Neptune.Domain.Utils
{
    public static class Extensions
    {
        public static bool EhAntes(this DateTime data, DataMes data2)
        {
            if (data.Year < data2.Ano)
                return true;
            else if (data.Year > data2.Ano)
                return false;

            return data.Month < data2.Mes;
        }
    }
}
