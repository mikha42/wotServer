using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{
    static class admin
    {
        public static void debuging()
        {
            return;
            //Debug stopper

            entity e = new entity(null);
            e.addModule(typeof(debugmodule));
            e.invoke("setvalue", 0.4f);
            Console.WriteLine((float)e.invoke("getvalue")[0]);
        }
        static public void startup()
        {
            debuging();

            place p = new place();
            p.name = "debug room";
            p.desc = "a room for debuging";
            p.id = 0;

            place p2 = new place();
            p2.name = "debug hallway";
            p2.desc = "a hallway leading to rooms for debuging";
            p2.id = 0;

            path p3 = new path();
            p3.a = p;
            p3.b = p2;
            p3.name = "northern door";
            p3.desc = "you go through the door";
            p3.descother = "through the door";

            path p4 = new path();
            p4.a = p;
            p4.b = null;
            p4.name = "western locked door";
            p4.desc = "you go through the door";
            p4.lockeddesc = "The door is locked";
            p4.descother = "through the door";
            p4.locked = true;



            addf("Sofa", "A blue sofa", p2);
            addf("Painting", "A painting of a man looking at a cow", p2);
            addf("Desk", "A dark wooden desk", p2);
            addf("Swirl chair", "It swirls!", p2);
            addf("Cabinet", "A wooden cabinet", p2);

            world._world.Add(p);
            world._world.Add(p2);
            world.paths.Add(p3);
            world.paths.Add(p4);


            createUser("admin", "{14}lord {99}admin", "adminpass", p);
            createUser("bob1", "{9}bob1", "password", p);
            createUser("bob2", "{9}bob2", "password", p);
            createUser("bob3", "{9}bob3", "password", p);
            createUser("bob4", "{9}bob4", "password", p);

        }
        public static void addf(string name, string desc, place p)
        {
            entity f = new entity(p);
            f.addModule(typeof(furniture));
            furniture _f = ((furniture)f.module("furniture"));
            _f.furniture_name = name;
            _f.furniture_desc = desc;
            p.entities.Add(f);
        }
        public static void createUser(string username, string handle, string password, place p)
        {
            user admin = new user();
            entity e = new entity(admin);
            e.position = p;
            e.name = handle;
            admin.player = e;
            admin.username = username;
            auth.createUser(username, password, admin);
            e.addModule(typeof(player));
        }
    }
}
