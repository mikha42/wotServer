using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace wotServer
{
    static class auth
    {
        static private Dictionary<string, Tuple<string, user>> users = new Dictionary<string, Tuple<string, user>>();

        static public bool existsUser(string username)
        {
            return users.ContainsKey(username);
        }
        static public bool existsLogin(string username, string password)
        {

            if (users.ContainsKey(username))
            {
                if (users[username].Item1 == hash(password))
                    return true;
            }
            return false;

        }
        static public user getUser(string username, TcpClient tc)
        {
            user getting = users[username].Item2;
            getting.client = tc;
            getting.username = username;
            return getting;
        }
        static public void createUser(string username, string password, user user)
        {
            users.Add(username, new Tuple<string, user>(hash(password), user));
        }
        static string hash(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            string hash = "";
            foreach (int i in md5data)
                hash += i.ToString("X");
            return hash;
        }

    }
    class user
    {
        public TcpClient client;
        public entity player;
        public string username;
    }

}
