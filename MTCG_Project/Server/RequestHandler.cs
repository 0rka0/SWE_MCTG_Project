using System;
using System.Collections.Generic;
using System.IO;
using MTCG_Project.Interaction;

namespace MTCG_Project.Server
{
    public class RequestHandler
    {
        StreamWriter Writer;
        StreamReader Reader;
        public void HandleRequestByVerb(StreamReader reader, StreamWriter writer, RequestContext request, string[] ressourceElements, List<RequestContext> messageList)
        {
            Writer = writer;
            Reader = reader;
            if (String.Compare(request.Verb, HttpData.Post) == 0)
            {
                HandlePost(request, ressourceElements, messageList);
            }
            else if (String.Compare(request.Verb, HttpData.Get) == 0)
            {
                HandleGet(request, ressourceElements, messageList);
            }
            else if ((String.Compare(request.Verb, HttpData.Put) == 0) && (ressourceElements.Length == 2))
            {
                HandlePut(request, messageList);
            }
            else if (String.Compare(request.Verb, HttpData.Delete) == 0)
            {
                HandleDelete(ressourceElements, messageList);
            }
            else
            {
                ResponseHandler.Status400(Writer);
            }
        }

        void HandlePost(RequestContext request, string[] ressourceElements, List<RequestContext>messageList)
        {
            ResponseHandler.Status201(Writer);
            string message = ReadContent();
            request.addMessage(message);

            PostHandler.HandleByCommand(request);

            messageList.Add(request);
        }

        void HandleGet(RequestContext request, string[] ressourceElements, List<RequestContext> messageList)
        {
            string tmpString = GetHandler.HandleByCommand(request);
            Console.WriteLine(tmpString);
            
            ResponseHandler.Status200(Writer, tmpString);
        }

        void HandlePut(RequestContext request, List<RequestContext> messageList)
        {
            ResponseHandler.Status201(Writer);
            string message = ReadContent();
            request.addMessage(message);

            //messageList.RemoveAt(position - 1);
            //messageList.Insert(position - 1, request);
        }

        void HandleDelete(string[] ressourceElements, List<RequestContext> messageList)
        {
            messageList.Clear();
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
