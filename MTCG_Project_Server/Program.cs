using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MTCG_Project_Server
{
    class Program
    {
        static List<string> MessageList = new List<string>();
        static string[] inputMsg = new string[8];
        static async Task Main(string[] args)
        {
            //List<string> tmpInputMsg = new List<string>();
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
                    
                    ReadHeader(reader);

                    if (String.Compare(inputMsg[0], "DELETE") != 0)
                    {
                        reader.ReadLine();
                        ReadContent(reader);
                    }
                    
                    for (int i = 0; i < 8; i++)
                        Console.WriteLine(inputMsg[i]);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                }
            }
        }

        static void ReadHeader(StreamReader reader)
        {
            ReadFirstLine(reader);

            for (int i = 3; i < 6; i++)
            {
                ReadCurLine(reader, i);
            }

            if (String.Compare(inputMsg[0], "DELETE") != 0)
            {
                ReadCurLine(reader, 6);
            }
            else
            {
                inputMsg[6] = "";
                inputMsg[7] = "";
            }
        }

        static void ReadFirstLine(StreamReader reader)
        {
            string tmpString;
            int counter = 0;
            tmpString = reader.ReadLine();
            Console.WriteLine("received: " + tmpString);

            string[] tmpStrList = tmpString.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach(string s in tmpStrList)
            {
                inputMsg[counter] = s;
                counter++;
            }
        }

        static void ReadCurLine(StreamReader reader, int counter)
        {
            string tmpString = "";
            tmpString = reader.ReadLine();
            int index = tmpString.IndexOf(": ");
            inputMsg[counter] = tmpString.Substring(index + 2);
            Console.WriteLine("received: " + tmpString);
        }

        static void ReadContent(StreamReader reader)
        {
            string tmpString = "";
            while(!reader.EndOfStream)
            {
                tmpString += reader.ReadLine();
            }
            inputMsg[7] = tmpString;
            Console.WriteLine("received: " + tmpString);
        }
    }
}
