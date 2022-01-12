using Neptune.Ui.Models;
using System;

namespace Neptune.Ui.Infra.Response
{
    public record ObterMesResponse
    {
        public Mes? Result { get; set; }
    }
}