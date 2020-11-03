using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project_Server
{
    public struct Message
    {
        public string HttpVerb { get; private set; }
        public string Endpoint { get; private set; }
        public string Protocol { get; private set; }
        public string Host { get; private set; }
        public string User_Agent { get; private set; }
        public string Accept { get; private set; }
        public string Content_Length { get; private set; }
        public string Content { get; private set; }

        public Message(string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8)
        {
            HttpVerb = p1;
            Endpoint = p2;
            Protocol = p3;
            Host = p4;
            User_Agent = p5;
            Accept = p6;
            Content_Length = p7;
            Content = p8;
        }
    }
}
