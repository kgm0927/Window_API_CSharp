using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
namespace SocketEx
{
  class Program
  {
    static void Main(string[] args)
    {
      // 소켓 생성 
      Socket sckTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
     
	  IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
      //IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("203.241.249.121"), 9000);

      try
      {
		// 소켓을 원격의 종점에 연결을 시도합니다.
        sckTcp.Connect(ipEndPoint);
				// 소켓이 연결되었다면 
        if (sckTcp.Connected)
        { 
          Console.WriteLine("소켓이 연결되었습니다. ");
          while (true)
          {
              // 소켓을 인자로 Http 프로토콜을 사용한 통신을 구현한 
              // socketSendReceive 메소드를 호출합니다.
              string str = Console.ReadLine();
              // 한글 입력도 받을수 있도록 인코딩 합니다.
              Byte[] ByteGet = Encoding.GetEncoding("ks_c_5601-1987").GetBytes(str);
              // 내용을 받을 바이트 배열을 선언합니다.
              Byte[] RecvBytes = new Byte[1024];
              // 서버에게 요청 명령을 보냅니다..
              sckTcp.Send(ByteGet);
              // 서버 홈페이지의 내용을 받습니다.
              Int32 bytes = sckTcp.Receive(RecvBytes, RecvBytes.Length, SocketFlags.None);
              // 리턴 받은 소스의 내용을 출력합니다.
              string receive = Encoding.GetEncoding("ks_c_5601-1987").GetString(RecvBytes, 0, bytes); 
              Console.WriteLine(receive);
          }
        }
        else
        {
		  // 연결되지 않았다면
          Console.WriteLine("이전에 소켓이 연결되지 않았습니다. ");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception Thrown: " + ex.ToString());
      }
      finally
      {
        sckTcp.Close();
      }
    }
  }// end Class 
}// end Namespace
