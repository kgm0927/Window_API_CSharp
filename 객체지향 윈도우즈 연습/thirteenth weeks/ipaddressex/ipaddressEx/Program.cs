using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ipaddressEx
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. 읽기 전용 값들을 나열합니다.

            Console.WriteLine("IPAddress 의 일기전용 값들을 나열 한다.");
            Console.WriteLine("IPAddress.Any :{0}", IPAddress.Any.ToString());
            Console.WriteLine("IPAddress.Broadcase : {0}", IPAddress.Broadcast.ToString());
            Console.WriteLine("IPAddress.Loopback : {0}", IPAddress.Loopback.ToString());
            Console.WriteLine("IPAddress.None :{0}", IPAddress.None.ToString());


            // 2.  IPAddress 인스턴스 생성

            IPAddress ipAddress = IPAddress.Parse("203.241.249.126");

            // 3. 어떤 주소체계를 가지고 있는지 나타낸다.


            Console.WriteLine("AddressFamily : {0} ", ipAddress.AddressFamily.ToString());

            // 4. Loopback 인지를 체크

            if (IPAddress.IsLoopback(ipAddress))
                Console.WriteLine("{0} is Loopback.", ipAddress.ToString());
            else
                Console.WriteLine("{0} is not Loopback", ipAddress.ToString());
        }
    }
}
