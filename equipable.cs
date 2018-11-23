using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{
    class bag : module
    {
        public bag(entity self, string name) : base(self, name) { }

        public List<entity> storage = new List<entity>();

        

    }
}
