using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace MTCG_Project_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(2000); // 2 sec so the server is up and running

            using TcpClient client = new TcpClient("localhost", 8000);
            using StreamReader reader = new StreamReader(client.GetStream());
            Console.WriteLine(reader.ReadLine());
            using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

            string input = null;
            Console.WriteLine("Enter command:");
            while ((input = Console.ReadLine()) != "X")
            {
                writer.WriteLine(input);
                Console.WriteLine("Enter command:");
            }
            writer.WriteLine("X");

        }
    }
}
