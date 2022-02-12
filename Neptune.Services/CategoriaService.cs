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

        public async Task<List<Categoria>> ObterTodasComFilhos()
        {
            var todas = _categoriaRepository.ObterTodas();

            var categoriasNivel0 = todas.Where(x => x.EhNivel0).ToList();

            foreach (var categoriaNivel0 in categoriasNivel0)
            {
                var filhosNivel1 = todas.Where(x => x.IdCategoriaPai == categoriaNivel0.Id).ToList();
                categoriaNivel0.AdicionarFilhos(filhosNivel1);

                foreach (var categoriaNivel1 in filhosNivel1)
                {
                    var filhosNivel2 = todas.Where(x => x.IdCategoriaPai == categoriaNivel1.Id).ToList();
                    categoriaNivel1.AdicionarFilhos(filhosNivel2);
                }
            }

            var todasComFilhos = new List<Categoria>();
            todasComFilhos.AddRange(categoriasNivel0);
            
            return todasComFilhos;
        }
    }
}
