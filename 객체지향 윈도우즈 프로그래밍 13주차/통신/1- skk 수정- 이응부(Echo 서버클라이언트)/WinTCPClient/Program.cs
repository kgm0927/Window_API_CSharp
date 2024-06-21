using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
namespace WinTCPClient
{
    class Program
    {
        static string _strHostIP = "127.0.0.1";
        //static string _strHostIP = "192.168.56.121";
        static void Main(string[] args)
        {
            TcpClient tclient = new TcpClient(_strHostIP, 9000);
            string send_str;
            byte[] recev = new byte[513];

            try
            {
                if (tclient.Connected)
                {
                    Console.WriteLine("소켓이 연결되었습니다. ");
                    NetworkStream ns = tclient.GetStream();
  
                    while (true)
                    {
                        Console.Write("보낼 데이터 : ");
                        send_str = Console.ReadLine();
                        if (send_str == null)
                            break;
                        //byte[] sendbyte = Encoding.UTF8.GetBytes(send_str);  //OK
                        byte[] sendbyte = Encoding.ASCII.GetBytes(send_str);  //OK

                        //서버로 메시지내용을 바이트로 전송.
                        ns.Write(sendbyte, 0, sendbyte.Length);
                        Console.WriteLine("[TCP 클라이언트] {0}바이트를 보냈습니다.", sendbyte.Length);
                        int recevbyte = ns.Read(recev, 0, recev.Length);
                        if (recevbyte == -1)
                        {
                            Console.WriteLine("메시지 받기 실패");
                            break;
                        }
                        //string recevstr = Encoding.Default.GetString(recev, 0, recevbyte); //OK
                        string recevstr = Encoding.ASCII.GetString(recev, 0, recevbyte); //OK
                        Console.WriteLine("[TCP 클라이언트] {0}바이트를 받았습니다..", recevbyte);
                        Console.WriteLine("다시 받은 데이터 : {0}", recevstr);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Thrown : " + ex.ToString());
            }
            finally
            {
                tclient.Close();
            }
        }
    }
}
