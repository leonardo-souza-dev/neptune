using Neptune.Domain;
using Neptune.Infra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application
{
    public class CategoriaService : ICategoriaService
    {
        public async Task<List<Categoria>> ObterTodas()
        {
            var categorias = new List<Categoria>();
            categorias.Add(new Categoria(1, null, "base"));
            categorias.Add(new Categoria(2, null, "sem categoria"));

            categorias.Add(new Categoria(3, 1, "basico"));
            categorias.Add(new Categoria(4, 3, "alimentacao"));

            categorias.Add(new Categoria(5, 1, "livre"));
            categorias.Add(new Categoria(6, 5, "lazer"));
            categorias.Add(new Categoria(7, 5, "casa"));

            return categorias;
        }
    }
}
