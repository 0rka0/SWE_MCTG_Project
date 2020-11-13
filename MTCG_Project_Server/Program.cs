using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
//using System.Threading.Tasks;
//using System.Threading;

namespace MTCG_Project_Server
{
    class Program
    {
        static List<RequestContext> MessageList = new List<RequestContext>();
        static void Main(string[] args)
        {
            RequestContext request;
            RequestHandler requestHandler = new RequestHandler();
            TcpListener listener = new TcpListener(IPAddress.Loopback, 80);
            listener.Start(5);

            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for connection...");
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    using StreamReader reader = new StreamReader(client.GetStream());
                    request = requestHandler.ReadRequest(reader);

                    using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                    requestHandler.HandleRequest(writer, request, MessageList);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                }
            }
        }

        /*static void StartThread(TcpClient client)
        {
            var thread = new Thread(new ParameterizedThreadStart(EstablishConnection));
            thread.Start(client);
        }

        static void EstablishConnection(object cl)
        {
            RequestContext request;
            RequestHandler requestHandler = new RequestHandler();
            TcpClient client = (TcpClient)cl;

            using StreamReader reader = new StreamReader(client.GetStream());
            request = requestHandler.ReadRequest(reader);
            
            using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
            requestHandler.HandleRequest(writer, request, MessageList);
        }*/
    }
}
