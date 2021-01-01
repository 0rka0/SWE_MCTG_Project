using System;
using System.Collections.Generic;
using System.IO;

namespace MTCG_Project.Server
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
            string[] ressourceElements = request.Ressource.Split("/", StringSplitOptions.RemoveEmptyEntries);


            //error when
            //- wrong amount of ressourceElements
            //- first element not "messages"
            //- second element not an valid int
            if ((ressourceElements.Length != 1) && (ressourceElements.Length != 2))
            {
                ResponseHandler.Status400(Writer);
                return;
            }

            if (RequestError(request))
            {
                return;
            }


            RequestHandler requestHandler = new RequestHandler();
            requestHandler.HandleRequestByVerb(Reader, Writer, request, ressourceElements, messageList);
        }

        bool RequestError(RequestContext request)//checks for errors in header
        {
            if (!ValidVersion(request.Version))
            {
                return true;
            }

            if (!ValidRessource(request.Ressource))
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

        bool ValidRessource(string ress)
        {
            if (String.Compare(ress, RequestCalls.users) == 0)
            {
                return true;
            }
            if (ress.Contains(RequestCalls.specific_user))
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.sessions) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.packages) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.transactions) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.trans_packs) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.cards) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.deck) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.deck_plain) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.stats) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.score) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.battles) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.tradings) == 0)
            {
                return true;
            }
            if (String.Compare(ress, RequestCalls.trade_coins) == 0)
            {
                return true;
            }
            if (ress.Contains(RequestCalls.specific_trade))
            {
                return true;
            }
            if (ress.Contains(RequestCalls.friends))
            {
                return true;
            }
            ResponseHandler.Status400(Writer);
            return false;
        }

        bool PayloadTooLarge(int length)
        {
            if (length > 1000)
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
