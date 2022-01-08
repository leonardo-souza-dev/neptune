using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Json;
using Neptune.Web.ViewModel;
using System;
using System.Collections;

namespace Neptune.Web.Data
{
    public class ContasResponse : List<ContaResponse>
    {
    }

    public class ContaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal SaldoInicial { get; set; }
    }
}