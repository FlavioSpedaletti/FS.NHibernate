using System;
using System.Collections.Generic;
using System.Text;

namespace FS.NHibernate.Entidades
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
    }
}
