using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Json;
using System;
using System.Collections;

namespace Neptune.Ui.Infra.Response
{
    public class ContasResponse 
    {
        public List<ContaDto> Result { get; set; }
    }

    public class ContaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal SaldoInicial { get; set; }
    }
}