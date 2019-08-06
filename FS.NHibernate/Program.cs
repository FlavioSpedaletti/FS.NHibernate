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

            //recupera
            var product = dao.BuscaPorId(3);

            //adiciona
            //var novoProduct = new Product()
            //{
            //    Name = "Curso NHibernate",
            //    Price = 580.55M
            //};
            //dao.Adiciona(novoProduct);

            //altera
            product.Price = 689.55M;
            dao.Atualiza(product);

            session.Close();

            Console.ReadKey();
        }
    }
}
