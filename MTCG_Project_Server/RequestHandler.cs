using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MTCG_Project_Server
{
    public class RequestHandler
    {
        StreamReader Reader;
        StreamWriter writer;

        public RequestContext ReadRequest(StreamReader reader)
        {
            Reader = reader;
            string[] firstLine = new string[3];
            Dictionary<string, string> header = new Dictionary<string, string>();
            RequestContext request;

            firstLine = ReadFirstLine();
            header = ReadHeader();
            request = new RequestContext(firstLine[0], firstLine[1], firstLine[2], header, "");

            return request;
        }

        public void HandleRequest(StreamWriter writer, RequestContext request, List<RequestContext> messageList)
        {
            int position = 0;
            string[] ressourceElements = request.Ressource.Split("/", StringSplitOptions.RemoveEmptyEntries);

            //error when
            //- wrong amount of ressourceElements
            //- first element not "messages"
            //- second element not an int
            if (((ressourceElements.Length != 1) && (ressourceElements.Length != 2)) || String.Compare(ressourceElements[0], HttpData.Ressource) != 0 || 
                ((ressourceElements.Length == 2) && ((int.TryParse(ressourceElements[1], out position) != true) || (position <= 0) || (position > messageList.Count))))
            {
                writer.WriteLine("400: Bad Request");
                if (String.Compare(request.Verb, HttpData.Delete) != 0)
                {
                    string message = ReadContent();
                }
                Console.WriteLine("Bad Ressource2");
                return;
            }

            if (String.Compare(request.Verb, HttpData.Post) == 0)
            {
                string message = ReadContent();
                request.addMessage(message);
                if (ressourceElements.Length == 1)
                {
                    messageList.Add(request);
                }
                else if (ressourceElements.Length == 2)
                {
                    messageList.Insert(position-1, request);
                }
            }
            if(String.Compare(request.Verb, HttpData.Get) == 0)
            {
                string message = ReadContent();
                request.addMessage(message);
                if (ressourceElements.Length == 1)
                {
                    foreach (RequestContext r in messageList)
                    {
                        Console.WriteLine(r.Message);
                    }
                }
                else if (ressourceElements.Length == 2)
                {
                    Console.WriteLine(messageList[position - 1].Message);
                }
            }
            if (String.Compare(request.Verb, HttpData.Put) == 0)
            {
                string message = ReadContent();
                request.addMessage(message);
            }
            if (String.Compare(request.Verb, HttpData.Delete) == 0)
            {
                
            }
        }

        Dictionary<string, string> ReadHeader()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            Tuple<string, string> valuePair;
            string tmpString = "";
            while (((tmpString = Reader.ReadLine()) != null) && (tmpString.Length != 0))
            {
                valuePair = ReadCurLine(tmpString);
                header.Add(valuePair.Item1, valuePair.Item2);
            }
            return header;
        }

        string[] ReadFirstLine()
        {
            string tmpString;
            tmpString = Reader.ReadLine();
            Console.WriteLine("received: " + tmpString);

            string[] tmpStrList = tmpString.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return tmpStrList;
        }

        Tuple<string, string> ReadCurLine(string tmpString)
        {
            string[] tmpStrList = tmpString.Split(": ", StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("received: " + tmpString);

            return new Tuple<string, string>(tmpStrList[0], tmpStrList[1]);
        }

        string ReadContent()
        {
            string tmpString = "";
            while (!Reader.EndOfStream)
            {
                tmpString += Reader.ReadLine();
            }
            Console.WriteLine("received: " + tmpString);
            return tmpString;
        }
    }
}
