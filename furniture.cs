using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{
    class furniture : module
    {
        public furniture(entity self, string name) : base(self, name) { }

        public string furniture_name;
        public string furniture_desc;

        public string get_name()
        {
            return furniture_name;
        }

        public string get_desc()
        {
            return furniture_desc;
        }

        public List<Tuple<entity, string, string>> get_look()
        {
            List<Tuple<entity, string, string>> look = new List<Tuple<entity, string, string>>();



            return look;
        }
    }
}
