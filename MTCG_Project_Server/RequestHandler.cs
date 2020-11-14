using System;
using System.Collections.Generic;
using System.IO;

namespace MTCG_Project_Server
{
    public class RequestHandler
    {
        StreamWriter Writer;
        StreamReader Reader;
        public void HandleRequestByVerb(StreamReader reader, StreamWriter writer, RequestContext request, string[] ressourceElements, List<RequestContext> messageList, int position)
        {
            Writer = writer;
            Reader = reader;
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

        string ReadContent()
        {
            string tmpString = Reader.ReadToEnd();
            Console.WriteLine("received: " + tmpString);
            return tmpString;
        }
    }
}
