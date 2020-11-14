using System;
using System.Collections.Generic;
using System.IO;

namespace MTCG_Project_Server
{
    class RequestPreparer
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

        public void PrepareRequest(StreamWriter writer, RequestContext request, List<RequestContext> messageList)
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

            if (RequestError(request))
            {
                return;
            }

            RequestHandler requestHandler = new RequestHandler();
            requestHandler.HandleRequestByVerb(Reader, Writer, request, ressourceElements, messageList, position);
        }

        bool RequestError(RequestContext request)//checks for errors in header
        {
            if (!ValidVersion(request.Version))
            {
                return true;
            }

            string len;
            request.HeaderData.TryGetValue(HttpData.ContentLength, out len);
            if (request.HeaderData.ContainsKey(HttpData.ContentLength) && PayloadTooLarge(int.Parse(len)))
            {
                return true;
            }

            return false;
        }

        bool ValidVersion(string version)
        {
            if (String.Compare(version, HttpData.Version) == 0)
            {
                return true;
            }
            ResponseHandler.Status505(Writer);
            return false;
        }

        bool PayloadTooLarge(int length)
        {
            if (length > 100)
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

    }
}
