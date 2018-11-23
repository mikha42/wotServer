using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{
    static class world
    {
        public static List<place> _world = new List<place>();
        public static List<path> paths = new List<path>();

        static public place fromId(int i)
        {
            foreach (place p in _world)
                if (p.id == i)
                    return p;
            return null;
        }

        static public path[] lookPaths(place i)
        {
            List<path> pths = new List<path>();
            foreach (path p in paths)
                if (p.a == i || p.b == i)
                    pths.Add(p);
            if (pths.Count() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Warning: A player has looked and found no paths.");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            return pths.ToArray();
        }

        static public void move(place p1, place p2, entity e)
        {
            if (p1 != null && p2 != null && p1.entities.Contains(e))
            {
                if (e.position != p2)
                    e.position = p2;
                p2.entities.Add(e);
                p1.entities.Remove(e);
            }
        }

        static public void place(place p1, entity e)
        {
            if (p1 != null && !p1.entities.Contains(e))
                p1.entities.Add(e);
        }

        static public void remove(place p1, entity e)
        {
            if (p1 != null && p1.entities.Contains(e))
                p1.entities.Remove(e);
        }

        

        static public List<Tuple<entity, string, string>> get_look(place p1)
        {
            List<Tuple<entity, string, string>> outp = new List<Tuple<entity, string, string>>();
            foreach (entity e in p1.entities)
            {
                object[] names = e.invoke("get_name");
                object[] descs = e.invoke("get_desc");
                string name = e.name;
                string desc = "";
                if (names.Length > 0)
                    name = (string)names[0];
                if (descs.Length > 0)
                    desc = (string)descs[0];
                outp.Add(new Tuple<entity, string, string>(e, name, desc));
            }
            return outp;
        }

        static public place go(place p, path i)
        {
            if (i.locked)
                return p;
            else if (i.a == p)
                return i.b;
            else if (i.b == p)
                return i.a;
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Warning: A player has gone down a path that they arent at");
                Console.ForegroundColor = ConsoleColor.Yellow;
                return p;
            }
        }


    }
    class path
    {
        public bool locked = false;
        public place a;
        public place b;
        public string desc;
        public string lockeddesc;
        public string descother;
        public string name;
    }
    class place
    {
        public string name;
        public string desc;
        public int id;
        public List<entity> entities = new List<entity>();


        public override bool Equals(object obj)
        {
            return id == ((place)obj).id;
        }
        public override int GetHashCode()
        {
            return id;
        }

    }
}
