using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{
    static class cstr
    {
        public static string first(string s)
        {
            return s.Split(' ')[0];
        }
        public static string rest(string s)
        {
            string f = first(s);
            if (s == f)
                return "";
            else
                return s.Substring(f.Length + 1);
        }
        public static string charrest(string s)
        {
            if (s.Length <= 1)
                return "";
            return s.Substring(1);
        }
        public static string brac(string s)
        {
            return "{15}[" + s + "{15}]";
        }
        public static string trac(string s)
        {
            return "{15}<" + s + "{15}>";
        }

        public static string selectable(int i)
        {
            string vals = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (i < 0 || i > vals.Length)
                return "Err";
            return "" + vals[i];
        }
        public static int selected(string s)
        {
            string vals = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (s.Length == 0 || !vals.Contains(s[0]))
                return -1;
            return vals.IndexOf(s[0]);
        }

    }
}
