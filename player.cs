using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{

    class player : module
    {
        public player(entity self, string name) : base(self, name) { }

        //inventory
        public entity rh;
        public entity lh;
        public bool both;

        public entity pants;
        public entity shirt;
        public entity shoes;
        public entity hat;
        public entity bag;

        //stats
        //abilities

        public string p_inv()
        {
            return "inventory";
        }

        public string get_name()
        {
            return "player name";
        }

        public string get_desc()
        {
            return "player desc";
        }

        public List<Tuple<entity, string, string>> get_look()
        {
            List<Tuple<entity, string, string>> look = new List<Tuple<entity, string, string>>();



            return look;
        }

        public void listen(object o)
        {
            string s = (string)o;
            user u = (user)self.parent;
            ui.interupt(s, u);
        }



    }
}
