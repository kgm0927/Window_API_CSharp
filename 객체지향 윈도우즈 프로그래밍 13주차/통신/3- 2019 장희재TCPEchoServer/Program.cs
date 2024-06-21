using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace _190529_TcpServerEx
{
    class Program
    {
        static byte[] recevbyte = new byte[513];
        static public Thread ThreadClient;
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 9000);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient(); // client 새로 접속
                ThreadClient = new Thread(new ParameterizedThreadStart(ThreadEx)); // 스레드 생성
                ThreadClient.Start(client); // 인자 전달
            }
        }
        public static void ThreadEx(object obj)
        {
            Encoding ASCII = Encoding.ASCII;
            TcpClient client = (TcpClient)obj;
            Socket s = client.Client;
            string address = s.RemoteEndPoint.ToString(); // 123.123.123.123:12345 (아이피:포트)
            string[] array = address.Split(new char[] { ':' }); // ':'으로 자름
            Console.WriteLine("[TCP 서버] 클라이언트 접속 : IP 주소 = {0}, 포트번호 = {1}", array[0], array[1]);
            try
            {
                if (client.Connected)
                {
                    NetworkStream ns = client.GetStream();
                    while (true)
                    {
                        int bytes = ns.Read(recevbyte, 0, recevbyte.Length); // Read
                        String strbyte = ASCII.GetString(recevbyte, 0, bytes);
                        Console.WriteLine("[TCP/{0}:{1}] {2}", array[0], array[1], strbyte);
                        ns.Write(recevbyte, 0, bytes); // Write
                    }
                }
                else
                {
                    Console.WriteLine("이전에 소켓이 연결되지 않았습니다. ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Thrown: " + ex.ToString());
            }
            finally
            {
                client.Close();
            }
        }
    }
}
