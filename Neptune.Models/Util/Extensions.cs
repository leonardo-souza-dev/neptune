using System;

namespace Neptune.Domain.Util
{
    public static class Extensions
    {
        public static DateTime ObterData(this DataMes dataMes)
        {
            return new DateTime(dataMes.Ano, dataMes.Mes, 1);
        }
    }
}
