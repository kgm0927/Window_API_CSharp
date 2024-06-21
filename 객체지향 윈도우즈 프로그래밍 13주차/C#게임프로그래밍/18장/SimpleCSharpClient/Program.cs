using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
//상단 메뉴 : 프로젝트-참조추가-System.Net.Http 쳌

namespace SimpleCSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "http://203.241.246.160");

            //HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "https://www.naver.com");
            //HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "https://www.inje.ac.kr");
            //HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "http://www.google.com");
           // HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");

            //var res = client.GetAsync("http://203.241.246.160").Result;
            var res = client.SendAsync(httpRequest).Result;
            Console.WriteLine("Response :" + " - " + res.Content.ReadAsStringAsync().Result);
        }
    }
}
