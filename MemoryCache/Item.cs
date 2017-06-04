using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCacheDefault
{
    public class Item
    {
        public string Name { get; set; }
        public virtual Category Category { get; set; }
    }
}
