using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace MTCG_Project.Server
{
    public class HTTP_Server
    {
        static List<RequestContext> MessageList = new List<RequestContext>();
        public static async Task Start()
        {
            RequestContext request;
            RequestPreparer requestHandler = new RequestPreparer();
            TcpListener listener = new TcpListener(IPAddress.Loopback, 8000);
            listener.Start(5);

            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for connection...");
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Connected!");

                    using StreamReader reader = new StreamReader(client.GetStream());
                    request = requestHandler.ReadRequest(reader);

                    using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                    requestHandler.PrepareRequest(writer, request, MessageList);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                }
            }
        }

        //Potential Setup for multithreading, not implemented yet
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
