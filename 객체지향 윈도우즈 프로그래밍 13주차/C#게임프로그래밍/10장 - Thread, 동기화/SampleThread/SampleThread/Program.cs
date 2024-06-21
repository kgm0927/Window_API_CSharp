using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;//스레드 네임스페이스

namespace SampleThread
{
    class Program
    {
        static void Main(string[] args)
        {
            //메서드 실행
            ThreadSample();
            
            //스레드 선언하기
            ThreadStart ts = new ThreadStart(ThreadSample);
            Thread spThread = new Thread(ts);

            //스레드 작동
            spThread.Start();

        }
        static void ThreadSample()
        {
            Console.WriteLine("Thread Called");
            
            //스레드 아이디 출력
            Console.WriteLine("현재 Thread ID : {0}",Thread.CurrentThread.ManagedThreadId);
        }
    }
}
