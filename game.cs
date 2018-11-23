using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace wotServer
{
    static class game
    {

        public static void loop(object o)
        {
            user u = (user)o;
            ui.prompt(cstr.brac(u.player.name) + "{14} ", u);
            path[] pathlook = world.lookPaths(u.player.position);
            entity lookSubject = null;
            string lookS_name = "";
            string lookS_desc = "";
            List<Tuple<entity, string, string>> look = world.get_look(u.player.position);
            while (true)
            {
                string inp = ui.input(u);
                if (inp[0] == '!')
                    Console.WriteLine(cstr.charrest(inp));
                string f = cstr.first(inp);
                string r = cstr.rest(inp);
                if (f == "go")
                {
                    if (r == "")
                    {
                        for (int i = 0; i < pathlook.Length; i++)
                            if (!pathlook[i].locked)
                                ui.print("{14}["+ cstr.selectable(i) +"]{11}" + pathlook[i].name + "{}", u);
                            else
                                ui.print("{14}["+ cstr.selectable(i) + "]{12}" + pathlook[i].name + "{}", u);
                    }
                    else if(cstr.selected(r) == -1)
                    {
                        ui.print("{12}Please input a valid ID{}", u);
                    }
                    else if (cstr.selected(r) >= pathlook.Length)
                    {
                        ui.print("{12}ID out of range{}", u);
                    }
                    else
                    {
                        if (pathlook[cstr.selected(r)].locked)
                            ui.print("{12}" + pathlook[cstr.selected(r)].lockeddesc + "{}", u);
                        else
                        {
                            place p = world.go(u.player.position, pathlook[cstr.selected(r)]);
                            place po = u.player.position;
                            foreach (entity e in p.entities)
                                e.invoke("listen", u.player.name + " {15}entered " + pathlook[cstr.selected(r)].descother + "{}");
                            u.player.position = p;
                            foreach (entity e in po.entities)
                                e.invoke("listen", u.player.name + " {15}exited " + pathlook[cstr.selected(r)].descother + "{}");
                            ui.print("{13}" + pathlook[cstr.selected(r)].desc + "{}", u);
                            pathlook = world.lookPaths(u.player.position);
                            look = world.get_look(u.player.position);
                            lookSubject = null;
                        }
                    }
                }
                else if (f == "look")
                {
                    bool succ = true;
                    if (r != "")
                    {
                        if (r == "back")
                        {
                            object back = lookSubject.parent;
                            if (back.GetType() == typeof(entity))
                                lookSubject = (entity)back;
                            else
                                lookSubject = null;
                        }
                        else if (cstr.selected(r) == -1)
                        {
                            ui.print("{12}Please input a valid ID{}", u);
                            succ = false;
                        }
                        else if (cstr.selected(r) >= look.Count())
                        {
                            succ = false;
                            ui.print("{12}ID out of range{}", u);
                        }
                        else
                        {
                            lookSubject = look[cstr.selected(r)].Item1;
                            lookS_name = look[cstr.selected(r)].Item2;
                            lookS_desc = look[cstr.selected(r)].Item3;
                            object[] looking = lookSubject.invoke("get_look");
                            look = new List<Tuple<entity, string, string>>();
                            if (looking.Length > 0)
                                look = (List<Tuple<entity, string, string>>)looking[0];
                        }
                    }
                    if (succ)
                    {
                        if (lookSubject == null)
                            ui.print(u.player.position.name + ":\n" + u.player.position.desc + "{}", u);
                        else
                            ui.print(cstr.trac(u.player.position.name) + " " + lookS_name + "{15}:{}" + lookS_desc + "{}", u);
                        if (look.Count() > 0)
                        {
                            string entitieslook = "";
                            int i = 0;
                            foreach (Tuple<entity, string, string> e in look)
                                if (e.Item1 != u.player)
                                    entitieslook += cstr.brac(cstr.selectable(i++)) + "{14} " + e.Item2 + "{}";
                            ui.print(entitieslook, u);
                        }
                        else
                        {
                            ui.print("{5}It looks like theres nothing else here to interact with.{}", u);
                        }
                    }
                }
                else if (f == "say")
                {
                    place p = u.player.position;
                    foreach (entity e in p.entities)
                        if (e != u.player)
                            e.invoke("listen", cstr.trac(u.player.name) + "{15} " + r + "{}");
                }
                else
                {
                    if (u.player.can("p_" + f))
                        u.player.invoke("p_" + f, r);
                    else
                    {
                        string helptext = "go{}look{}say{}";
                        foreach (string k in u.player.prefix('p'))
                            helptext += k.Substring(2) + "{}";
                        ui.print("Unrecognised command{}Valid Commands:{}" + helptext, u);

                    }
                }
            }
        }

        public static void ringtest(object o)
        {
            user u = (user)o;
            Thread.Sleep(3000);
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(750);
                ui.interupt("RING!{}", u);
            }
        }

        public static void login(TcpClient tc)
        {
            user u = new user();
            u.client = tc;

            string username = "";
            ui.prompt("{13}>{15} ", u);
            ui.clear(u);
            do
            {
                if (username != "")
                    ui.print("{12}Could not find user with that name!{}", u);
                ui.print("{14}Welcome!\nPlease input username:{}", u);
                username = ui.input(u);
            } while (!auth.existsUser(username));
            string pass = "";
            do
            {
                if (pass != "")
                    ui.print("{12}Password does not match!{}", u);
                ui.print("{14}Please enter password for " + username + ":{}", u);
                pass = ui.input(u);
            } while (!auth.existsLogin(username, pass));
            u = auth.getUser(username, tc);
            ui.clear(u);
            ui.print("Welcome!{}",u);
            world.place(u.player.position, u.player);
            Console.WriteLine("Client " + u.client.Client.RemoteEndPoint + " has logged into account " + username);
            Thread t = new Thread(loop);
            t.Start(auth.getUser(username, tc));
        }
    }
}
