using Neptune.Ui.Models;
using System;

namespace Neptune.Ui.Infra.Response
{
    public record ObterMesResponse
    {
        public MesModel? Result { get; set; }
    }
}