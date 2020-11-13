using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MTCG_Project_Server
{
    public static class ResponseHandler
    {
        static string Version = "HTTP/1.1";
        static string Name = "Server: MTCG-Server\r\n";
        static string ContentType = "Content-Type: text/html\r\n";
        static string AcceptRanges = "Accept-Ranges: bytes\r\n";
        static string Status;
        static string Connection;

        public static void Status200(StreamWriter writer, string payload)
        {
            Status = "200 Ok\r\n";
            Connection = "Connection: closed\r\n";
            string response = string.Format("{0} {1}{2}{3}{4}{5}Content-Length: {6}\r\n\r\n{7}\r\n\r\n", Version, Status, Name, ContentType, AcceptRanges, Connection, payload.Length, payload);
            Console.WriteLine(response);
            writer.WriteLine(response);
        }

        public static void Status201(StreamWriter writer)
        {
            Status = "201 Created\r\n";
            SendWithoutPayload(writer);
        }

        public static void Status400(StreamWriter writer)
        {
            Status = "400 Bad Request\r\n";
            SendWithoutPayload(writer);
        }

        public static void Status413(StreamWriter writer)
        {
            Status = "413 Payload Too Large\r\n";
            SendWithoutPayload(writer);
        }

        public static void Status500(StreamWriter writer)
        {
            Status = "500 Internal Server Error\r\n";
            SendWithoutPayload(writer);
        }

        public static void Status505(StreamWriter writer)
        {
            Status = "505 HTTP Version not supported\r\n";
            SendWithoutPayload(writer);
        }

        static void SendWithoutPayload(StreamWriter writer)
        {
            Connection = "Connection: closed\r\n";
            string response = string.Format("{0} {1}{2}{3}{4}{5}Content-Length: {6}\r\n\r\n", Version, Status, Name, ContentType, AcceptRanges, Connection, 0);
            Console.WriteLine(response);
            writer.WriteLine(response);
        }
    }
}
