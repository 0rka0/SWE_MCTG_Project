using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project_Server
{
    public class RequestContext
    {
        public string Verb { get; private set; }
        public string Ressource { get; private set; }
        public string Version { get; private set; }
        public Dictionary<string, string> Header_data { get; private set; } = new Dictionary<string, string>();
        public string Message { get; private set; }

        public RequestContext(string verb, string ressource, string version, Dictionary<string,string> header, string message)
        {
            Verb = verb;
            Ressource = ressource;
            Version = version;
            Header_data = header;
            Message = message;
        }

        public void addMessage(string message)
        {
            Message = message;
        }

        public void WriteData()
        {
            Console.WriteLine(Verb);
            Console.WriteLine(Ressource);
            Console.WriteLine(Version);
            foreach(KeyValuePair<string, string> kvp in Header_data)
            {
                Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine(Message);
        }
    }
}
