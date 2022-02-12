using Neptune.Domain;
using Neptune.Infra;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categorias> ObterTodasComFilhos()
        {
            return new Categorias(_categoriaRepository.ObterTodas());
        }
    }
}
