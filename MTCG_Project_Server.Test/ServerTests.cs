using NUnit.Framework;
using System;

namespace MTCG_Project_Server.Test
{
    [TestFixture]
    public class ServerTests
    {
        public string ExpectedResponse(string status, string payload)
        {
            string res = String.Format("HTTP/1.1 {0}\r\nServer: MTCG-Server\r\nContent-Type: text/html\r\n" +
                "Accept-Ranges: bytes\r\nConnection: closed\r\nContent-Length: {1}\r\n\r\n{2}\r\n\r\n", status, payload.Length, payload);
            return res;
        }

        public string HttpRequest(string verb, string ressource, string version, string host, string payload)
        {
            string req = String.Format("{0} {1} {2}\r\nHost: {3}\r\nUser-Agent: TestClient\r\nAccept: */*\r\nContent-Length: {4}\r\n\r\n{5}\r\n\r\n", 
                verb,ressource,version,host,payload.Length, payload);
            return req;
        }

        [SetUp]
        public void Setup()
        { 
        }

        [Test]
        public void EmptyGetRequest()
        {
            TestClient client = new TestClient();
            string req = HttpRequest("GET", "/messages", "HTTP/1.1", "localhost:8000", "");
            client.sendRequest(req);

            string res = ExpectedResponse("200 OK", "");
            string actualRes = client.readResponse();

            Assert.AreEqual(res, actualRes);
        }
    }
}