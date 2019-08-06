using FS.NHibernate.Entidades;
using NHibernate;

namespace FS.NHibernate.DAO
{
    public class ProductsDAO
    {
        private ISession _session;

        public ProductsDAO(ISession session)
        {
            _session = session;
        }

        public Product BuscaPorId(int id)
        {
            return _session.Get<Product>(id);
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
}
