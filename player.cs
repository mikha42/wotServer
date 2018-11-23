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
            return self.name;
        }

        public string get_desc()
        {
            return "Player desc";
        }

        public List<Tuple<entity, string, string>> get_look()
        {
            List<Tuple<entity, string, string>> look = new List<Tuple<entity, string, string>>();

            if (both)
            {
                if (rh != null)
                    look.Add(new Tuple<entity, string, string>(rh, "[in both hands] " + rh.get_name(), rh.get_desc()));
            }
            else
            {
                if (rh != null)
                    look.Add(new Tuple<entity, string, string>(rh, "[in right hand] " + rh.get_name(), rh.get_desc()));
                if (lh != null)
                    look.Add(new Tuple<entity, string, string>(lh, "[in left hand] " + lh.get_name(), lh.get_desc()));
            }

            if (pants != null)
                look.Add(new Tuple<entity, string, string>(pants, "[legs] " + pants.get_name(), pants.get_desc())); //pants
            if (shirt != null)
                look.Add(new Tuple<entity, string, string>(shirt, "[torso] " + shirt.get_name(), shirt.get_desc())); //shirt
            if (shoes != null)
                look.Add(new Tuple<entity, string, string>(shoes, "[feet] " + shoes.get_name(), shoes.get_desc())); //shoes
            if (hat != null)
                look.Add(new Tuple<entity, string, string>(hat, "[head] " + hat.get_name(), hat.get_desc())); //hat
            if (bag != null)
                look.Add(new Tuple<entity, string, string>(bag, "[bag] " + bag.get_name(), bag.get_desc())); //bag

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
