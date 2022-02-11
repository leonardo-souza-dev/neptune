using System;

namespace Neptune.Domain
{
    public class Categoria
    {
        public int Id { get; set; }
        public int? CategoriaPai { get; private set; }
        public string Descricao { get; private set; }

        public Categoria(int id, int? categoriaPai, string descricao)
        {
            Id = id;
            CategoriaPai = categoriaPai;
            Descricao = descricao;
        }
    }
}
