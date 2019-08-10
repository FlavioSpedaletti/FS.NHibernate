using FS.NHibernate.DAO;
using FS.NHibernate.Entidades;
using FS.NHibernate.Infra;
using NHibernate;
using System;

namespace FS.NHibernate
{
    class Program
    {
        public Program()
        {
            //injeção de dependências
        }

        static void Main(string[] args)
        {
            ISession session = NHibernateHelper.AbreSession();

            var dao = new ProductsDAO(session);

            //recupera todos
            var products = dao.BuscaTodos();
            products = dao.BuscaTodosPorOrdemAlfabetica();
            products = dao.BuscaTodosPaginado(0, 1);
            products = dao.BuscaTodosPaginado(2, 1);

            //recupera por id
            //var product = dao.BuscaPorId(1);

            //recupera por nome
            //var products = dao.BuscaPorNome("Mini curso Docker");

            //recupera por categoria
            //var products = dao.BuscaPorCategoria("Course");
            //products = dao.BuscaPorCategoriaComFetchJoin("Course");

            //recupera qtde de produtos por categoria
            //var productsPerCategory = dao.BuscaQtdeProdutosPorCategoria();

            //recupera por nome, preço mínimo e/OutOfMemoryException categoria (Comparison criteria)
            //var products = dao.BuscaPorNomePrecoMinimoECategoria("", 0, "");

            //adiciona
            //var novoProduct = new Product()
            //{
            //    Name = "Curso NHibernate",
            //    Price = 580.55M
            //};
            //dao.Adiciona(novoProduct);

            //altera
            //product.Price = 689.55M;
            //dao.Atualiza(product);

            session.Close();

            Console.ReadKey();
        }
    }
}
