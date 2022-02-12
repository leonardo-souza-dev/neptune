using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Domain;
using System.Threading.Tasks;

namespace Neptune.Infra
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly List<Categoria> _categorias = new();

        public CategoriaRepository()
        {
            _categorias.Add(new Categoria(1, null, "sem categoria", true));
            _categorias.Add(new Categoria(2, null, "base", true));

            _categorias.Add(new Categoria(3, 2, "basico55", true));
            _categorias.Add(new Categoria(4, 3, "alimentacao", true));

            _categorias.Add(new Categoria(5, 2, "livre20", true));
            _categorias.Add(new Categoria(6, 5, "lazer", true));
            _categorias.Add(new Categoria(7, 5, "casa", true));
            _categorias.Add(new Categoria(10, 7, "cozinha", true));

            _categorias.Add(new Categoria(8, 2, "reserva25", true));
        }

        public List<Categoria> ObterTodas()
        {
            return _categorias;
        }
    }
}
