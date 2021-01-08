﻿using MTCG_Project.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MTCG_Project
{
    class Program
    {
        public static async Task Main()
        {
            RequestContext request;
            RequestPreparer requestPreparer = new RequestPreparer();
            TcpListener listener = new TcpListener(IPAddress.Loopback, 10001);
            listener.Start(5);

            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for connection...");
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Connected!");

                    using StreamReader reader = new StreamReader(client.GetStream());
                    request = requestPreparer.ReadRequest(reader);

                    using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                    requestPreparer.PrepareRequest(writer, request);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                }
            }
        }
    }
}
