using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;


namespace MTCG_Project_Server.Test
{
    public class TestClient
    {
        string Address = "localhost";
        int Port = 8000;
        TcpClient Client;
        public void sendRequest(string inputString)
        {
            Client = new TcpClient(Address, Port);
            using StreamWriter writer = new StreamWriter(Client.GetStream()) { AutoFlush = true };
            writer.WriteLine(inputString);
        }

        public string readResponse()
        {
            using StreamReader reader = new StreamReader(Client.GetStream());

            string[] firstLine = new string[3];
            Dictionary<string, string> header = new Dictionary<string, string>();

            firstLine = ReadFirstLine(reader);
            header = ReadHeader(reader);

            string headerstring = "";

            foreach(var item in header)
            {
                headerstring += item.Key + " " + item.Value + " ";
            }

            string responseString = String.Format("{0} {1} {2} {3}", firstLine[0], firstLine[1], firstLine[2], headerstring);

            return responseString;
        }

        string[] ReadFirstLine(StreamReader reader)//reads only first line of request
        {
            string tmpString;
            tmpString = reader.ReadLine();
            Console.WriteLine("received: " + tmpString);

            string[] tmpStrList = tmpString.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return tmpStrList;
        }

        Dictionary<string, string> ReadHeader(StreamReader reader)//reads rest of header data and saves pairs in dict
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            Tuple<string, string> valuePair;
            string tmpString = "";
            while (((tmpString = reader.ReadLine()) != null) && (tmpString.Length != 0))
            {
                valuePair = ReadCurLine(tmpString);
                header.Add(valuePair.Item1, valuePair.Item2);
            }
            return header;
        }

        Tuple<string, string> ReadCurLine(string tmpString)//returns current line as key, value pair
        {
            string[] tmpStrList = tmpString.Split(": ", StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("received: " + tmpString);

            return new Tuple<string, string>(tmpStrList[0], tmpStrList[1]);
        }
    }
}
