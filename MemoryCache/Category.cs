using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCacheDefault
{
    public class Category
    {
        public string Name { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public Category()
        {
            Items = new Collection<Item>();
        }
    }
}
