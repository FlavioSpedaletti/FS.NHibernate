using FS.NHibernate.Entidades;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System.Collections.Generic;

namespace FS.NHibernate.DAO
{
    public class ProductsDAO
    {
        private ISession _session;

        public ProductsDAO(ISession session)
        {
            _session = session;
        }

        public ICollection<Product> BuscaTodos()
        {
            var query = _session.CreateQuery("from Product");
            return query.List<Product>();
        }

        public ICollection<Product> BuscaTodosPorOrdemAlfabetica()
        {
            var query = _session.CreateQuery("from Product p order by p.Name");
            return query.List<Product>();
        }

        public ICollection<Product> BuscaTodosPaginado(int comecaEm, int qtdePorPg)
        {
            var query = _session.CreateQuery("from Product p order by p.Name");
            query.SetFirstResult(comecaEm);
            query.SetMaxResults(qtdePorPg);
            return query.List<Product>();
        }

        public Product BuscaPorId(int id)
        {
            return _session.Get<Product>(id);
        }

        public ICollection<Product> BuscaPorNome(string nome)
        {
            //Forma 1 - substituindo parâmetros pela posição
            //var query = _session.CreateQuery("from Product p where p.Name = ?");
            //query.SetParameter(0, nome);

            //Forma 2 - substituindo parâmetros pelo nome
            var query = _session.CreateQuery("from Product p where p.Name = :nome");
            query.SetParameter("nome", nome);
            return query.List<Product>();
        }

        public ICollection<Product> BuscaPorCategoria(string nome)
        {
            var query = _session.CreateQuery("from Product p where p.Category.Name = :nome");
            query.SetParameter("nome", nome);
            return query.List<Product>();
        }

        //Dessa forma 
        public ICollection<Product> BuscaPorCategoriaComFetchJoin(string nome)
        {
            var query = _session.CreateQuery("from Product p join fetch p.Category where p.Category.Name = :nome");
            query.SetParameter("nome", nome);
            return query.List<Product>();
        }

        //isso aqui deveria estar no CategoriesDAO, mas é só um exemplo :)
        public ICollection<ProdutosPorCategoria> BuscaQtdeProdutosPorCategoria()
        {
            //Forma 1 - manipulando lista de Object[]
            //var query = _session.CreateQuery("select p.Category, count(p) from Product p group by p.Category");
            //var resultados = query.List<Object[]>();

            //foreach (var r in resultados)
            //{
            //    var p = new ProdutosPorCategoria()
            //    {
            //        Categoria = (Category)r[0],
            //        NumeroDeProdutos = (long)r[1]
            //    };
            //    yield return p; //yield return não está debugando =/
            //}

            //Forma 2 - Transformers.AliasToBean
            var query = _session.CreateQuery("select p.Category as Categoria, count(p) as NumeroDeProdutos from Product p group by p.Category");
            query.SetResultTransformer(Transformers.AliasToBean<ProdutosPorCategoria>());

            return query.List<ProdutosPorCategoria>();
        }

        public ICollection<Product> BuscaPorNomePrecoMinimoECategoria(string nome, decimal preco, string nomeCategoria)
        {
            ICriteria criteria = _session.CreateCriteria<Product>();

            if (!string.IsNullOrEmpty(nome))
            {
                criteria.Add(Restrictions.Eq("Name", nome));
            }

            if (preco > 0)
            {
                criteria.Add(Restrictions.Ge("Price", preco));
            }

            if (!string.IsNullOrEmpty(nomeCategoria))
            {
                ICriteria criteriaCat = criteria.CreateCriteria("Category");
                criteriaCat.Add(Restrictions.Eq("Name", nomeCategoria));
            }

            return criteria.List<Product>();
        }

        public void Adiciona(Product usuario)
        {
            ITransaction transacao = _session.BeginTransaction();
            _session.Save(usuario);
            transacao.Commit();
        }

        public void Atualiza(Product produto)
        {
            ITransaction transacao = _session.BeginTransaction();
            _session.Merge(produto);
            transacao.Commit();
        }
    }

    public class ProdutosPorCategoria
    {
        public Category Categoria { get; set; }
        public long NumeroDeProdutos { get; set; }
    }
}
