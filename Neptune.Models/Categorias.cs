using System;
using System.Linq;
using System.Collections.Generic;

namespace Neptune.Domain
{
    public class Categorias
    {
        public List<Categoria> Itens { get; set; } = new List<Categoria>();

        public List<Categoria> Nivel0 { get; set; } = new ();
        public List<Categoria> Nivel1 { get; set; } = new();
        public List<Categoria> Nivel2 { get; set; } = new();

        public Categorias(List<Categoria> todas)
        {
            var categoriasNivel0 = todas.Where(x => x.IdCategoriaPai == null).ToList();
            Nivel0.AddRange(categoriasNivel0);

            foreach (var categoriaNivel0 in categoriasNivel0)
            {
                var filhosNivel1 = todas.Where(x => x.IdCategoriaPai == categoriaNivel0.Id).ToList();
                categoriaNivel0.AdicionarFilhos(filhosNivel1);
                Nivel1.AddRange(filhosNivel1);

                foreach (var categoriaNivel1 in filhosNivel1)
                {
                    var filhosNivel2 = todas.Where(x => x.IdCategoriaPai == categoriaNivel1.Id).ToList();
                    categoriaNivel1.AdicionarFilhos(filhosNivel2);
                    Nivel2.AddRange(filhosNivel2);
                }
            }

            Itens.AddRange(categoriasNivel0);
        }

        public Categoria ObterCategoria(int id)
        {
            Categoria categoria = null;
            foreach (var item in Itens)
            {
                if (item.Id == id)
                    categoria = item;

                foreach (var item2 in item.Filhos)
                {
                    if (item2.Id == id)
                        categoria = item2;

                    foreach (var item3 in item2.Filhos)
                    {
                        if (item3.Id == id)
                            categoria = item3;
                    }
                }
            }

            return categoria;
        }

        public void SetarSelecionada(Categoria categoria, bool selecao)
        {
            var cat = Itens.FirstOrDefault(x => x.Id == categoria.Id);

            if (cat != null)
            {
                cat.SetarSelecionada(selecao);
            }
        }
    }
}
