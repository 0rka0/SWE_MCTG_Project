using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MTCG_Project_Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 8000);
            listener.Start(5);

            Console.CancelKeyPress += (sender, e) => Environment.Exit(0);

            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for connection...");
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                    Console.WriteLine("Connected!");
                    writer.WriteLine("Welcome to the MTCG Server!");

                    using StreamReader reader = new StreamReader(client.GetStream());
                    string message;
                    do
                    {
                        message = reader.ReadLine();
                        Console.WriteLine("received: " + message);
                    } while (message != "X");
                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                }
            }
        }
    }
}
