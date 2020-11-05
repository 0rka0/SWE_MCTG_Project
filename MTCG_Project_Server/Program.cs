using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading.Tasks;

namespace MTCG_Project_Server
{
    class Program
    {
        static List<RequestContext> MessageList = new List<RequestContext>();
        static async Task Main(string[] args)
        {
            RequestContext request;
            RequestHandler requestHandler = new RequestHandler();
            TcpListener listener = new TcpListener(IPAddress.Loopback, 8000);
            listener.Start(5);

            Console.CancelKeyPress += (sender, e) => Environment.Exit(0);

            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for connection...");
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Connected!");

                    using StreamReader reader = new StreamReader(client.GetStream());
                    using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

                    request = requestHandler.ReadRequest(reader);
                    requestHandler.HandleRequest(writer, request, MessageList);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                }
            }
        }
    }
}
