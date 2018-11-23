using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace wotServer
{
    class Program
    {
        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            admin.startup();

            int count = 1;

            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 3301);
            ServerSocket.Start();

            Console.WriteLine("Running WOT server at port 3301");
            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();
                lock (_lock) list_clients.Add(count, client);
                Console.WriteLine($"Client at {client.Client.RemoteEndPoint.ToString()} connected");

                Thread t = new Thread(handle_clients);
                t.Start(count);
                count++;
            }
        }


        public static string rawInput(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count;
                byte_count = stream.Read(buffer, 0, buffer.Length);
                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                return data;
            }
            catch
            {
                Console.WriteLine($"Client at {client.Client.RemoteEndPoint.ToString()} lost connection");
                Thread.CurrentThread.Abort();
                return "";
            }
        }
        public static string rawInput(user c)
        {
            TcpClient client = c.client;
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count;
                byte_count = stream.Read(buffer, 0, buffer.Length);
                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                return data;
            }
            catch
            {
                world.remove(c.player.position, c.player);
                Console.WriteLine($"User {c.username} lost connection");
                Thread.CurrentThread.Abort();
                return "";
            }
        }

        public static void rawPrint(string data, user c)
        {
            TcpClient client = c.client;
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);
                NetworkStream stream = client.GetStream();
                stream.Write(buffer, 0, buffer.Length);            
            }
            catch
            {
                world.remove(c.player.position, c.player);
                Console.WriteLine($"Client at {client.Client.RemoteEndPoint.ToString()} lost connection");
                Thread.CurrentThread.Abort();
            }
        }
        public static void rawPrint(string data, TcpClient client)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);
                NetworkStream stream = client.GetStream();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                Console.WriteLine($"Client at {client.Client.RemoteEndPoint.ToString()} lost connection");
                Thread.CurrentThread.Abort();
            }
        }


        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id];

            game.login(client);

            lock (_lock) list_clients.Remove(id);
        }
    }
}
