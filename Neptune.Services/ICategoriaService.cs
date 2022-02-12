using Neptune.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application
{
    public interface ICategoriaService
    {
        Task<Categorias> ObterTodasComFilhos();
    }
}
