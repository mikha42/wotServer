using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{
    static class ui
    {
        static string newlines(string s)
        {
            s = s.Replace("\n", "{}");
            s = s.Replace("\r", "");
            return s;
        }
        public static void print(string s, user u)
        {
            s = newlines(s);
            Program.rawPrint("p" + s, u);
        }
        public static void prompt(string s, user u)
        {
            s = newlines(s);
            Program.rawPrint("r" + s, u);
        }
        public static void clear(user u)
        {
            Program.rawPrint("c", u);
        }
        public static void interupt(string s, user u)
        {
            s = newlines(s);
            Program.rawPrint("i" + s, u);
        }
        public static string input(user u)
        {
            Program.rawPrint("k", u);
            return Program.rawInput(u);
        }
    }
}
