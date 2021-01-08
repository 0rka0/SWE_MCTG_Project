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
        public void HandleRequestByVerb(StreamReader reader, StreamWriter writer, RequestContext request)
        {
            Writer = writer;
            Reader = reader;
            if (String.Compare(request.Verb, HttpData.Post) == 0)
            {
                HandlePost(request);
            }
            else if (String.Compare(request.Verb, HttpData.Get) == 0)
            {
                HandleGet(request);
            }
            else if (String.Compare(request.Verb, HttpData.Put) == 0)
            {
                HandlePut(request);
            }
            else if (String.Compare(request.Verb, HttpData.Delete) == 0)
            {
                HandleDelete(request);
            }
            else
            {
                ResponseHandler.Status400(Writer);
            }
        }

        void HandlePost(RequestContext request)
        {
            ResponseHandler.Status201(Writer);
            string message = ReadContent();
            request.addMessage(message);

            PostHandler.HandleByCommand(request);

            //messageList.Add(request);
        }

        void HandleGet(RequestContext request)
        {
            string tmpString = GetHandler.HandleByCommand(request);
            Console.WriteLine(tmpString);
            
            ResponseHandler.Status200(Writer, tmpString);
        }

        void HandlePut(RequestContext request)
        {
            ResponseHandler.Status201(Writer);
            string message = ReadContent();
            request.addMessage(message);

            PutHandler.HandleByCommand(request);

            //messageList.RemoveAt(position - 1);
            //messageList.Insert(position - 1, request);
        }

        void HandleDelete(RequestContext request)
        {
            string tmpString = DeleteHandler.HandleByCommand(request);
            ResponseHandler.Status200(Writer, tmpString);
        }

        string ReadContent()
        {
            string tmpString = Reader.ReadToEnd();
            Console.WriteLine("received: " + tmpString);

            return tmpString;
        }
    }
}
