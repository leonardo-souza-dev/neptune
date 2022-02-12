using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Domain;

namespace Neptune.Infra
{
    public interface ICategoriaRepository
    {
        List<Categoria> ObterTodas();
    }
}
