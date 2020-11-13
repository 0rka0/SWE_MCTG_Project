using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MTCG_Project_Server
{
    public class RequestHandler
    {
        StreamReader Reader;
        StreamWriter Writer;

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
            Writer = writer;
            int position = 0;
            string[] ressourceElements = request.Ressource.Split("/", StringSplitOptions.RemoveEmptyEntries);

            //error when
            //- wrong amount of ressourceElements
            //- first element not "messages"
            //- second element not an valid int
            if (((ressourceElements.Length != 1) && (ressourceElements.Length != 2)) || (String.Compare(ressourceElements[0], HttpData.Ressource) != 0) ||
                ((ressourceElements.Length == 2) && ((int.TryParse(ressourceElements[1], out position) != true) || (position <= 0) || (position > messageList.Count))))
            {
                ResponseHandler.Status400(Writer);
                return;
            }

            if(RequestError(request))
            {
                return;
            }

            HandleRequestByVerb(request, ressourceElements, messageList, position);
        }

        void HandleRequestByVerb(RequestContext request, string[] ressourceElements, List<RequestContext> messageList, int position)
        {
            if (String.Compare(request.Verb, HttpData.Post) == 0)
            {
                HandlePost(request, ressourceElements, messageList, position);
            }
            else if (String.Compare(request.Verb, HttpData.Get) == 0)
            {
                HandleGet(ressourceElements, messageList, position);
            }
            else if ((String.Compare(request.Verb, HttpData.Put) == 0) && (ressourceElements.Length == 2))
            {
                HandlePut(request, messageList, position);
            }
            else if (String.Compare(request.Verb, HttpData.Delete) == 0)
            {
                HandleDelete(ressourceElements, messageList, position);
            }
            else
            {
                ResponseHandler.Status400(Writer);
            }
        }

        void HandlePost(RequestContext request, string[] ressourceElements, List<RequestContext>messageList, int position)
        {
            ResponseHandler.Status201(Writer);
            string message = ReadContent();

            request.addMessage(message);
            if (ressourceElements.Length == 1)
            {
                messageList.Add(request);
            }
            else if (ressourceElements.Length == 2)
            {
                messageList.Insert(position - 1, request);
            }
        }

        void HandleGet(string[] ressourceElements, List<RequestContext> messageList, int position)
        {
            string tmpString = "";
            if (ressourceElements.Length == 1)
            {
                foreach (RequestContext r in messageList)
                {
                    tmpString += r.Message + "\n";
                }
            }
            else if (ressourceElements.Length == 2)
            {
                tmpString += messageList[position - 1].Message + "\n";
            }
            ResponseHandler.Status200(Writer, tmpString);
        }

        void HandlePut(RequestContext request, List<RequestContext> messageList, int position)
        {
            ResponseHandler.Status201(Writer);
            string message = ReadContent();
            request.addMessage(message);

            messageList.RemoveAt(position - 1);
            messageList.Insert(position - 1, request);
        }

        void HandleDelete(string[] ressourceElements, List<RequestContext> messageList, int position)
        {
            if (ressourceElements.Length == 1)
            {
                messageList.Clear();
            }
            else if (ressourceElements.Length == 2)
            {
                messageList.RemoveAt(position - 1);
            }
            ResponseHandler.Status201(Writer);
        }

        bool RequestError(RequestContext request)//checks for errors in header
        {
            if (!ValidVersion(request.Version))
            {
                return true;
            }

            string len;
            request.Header_data.TryGetValue(HttpData.ContentLength, out len);
            if (request.Header_data.ContainsKey(HttpData.ContentLength) && PayloadTooLarge(int.Parse(len)))
            {
                return true;
            }

            return false;
        }

        bool ValidVersion(string version)
        {
            if(String.Compare(version, HttpData.Version) == 0)
            {
                return true;
            }
            ResponseHandler.Status505(Writer);
            return false;
        }

        bool PayloadTooLarge(int length)
        {
            if(length > 100)
            {
                ResponseHandler.Status413(Writer);
                return true;
            }
            return false;
        }

        string[] ReadFirstLine()//reads only first line of request
        {
            string tmpString;
            tmpString = Reader.ReadLine();
            Console.WriteLine("received: " + tmpString);

            string[] tmpStrList = tmpString.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return tmpStrList;
        }

        Dictionary<string, string> ReadHeader()//reads rest of header data and saves pairs in dict
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

        Tuple<string, string> ReadCurLine(string tmpString)//returns current line as key, value pair
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
