using System;
using System.Collections.Generic;
using System.Text;

namespace FS.NHibernate.Entidades
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
