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
            //스레드 선언하기
            Program tsObject = new Program();
            Thread spThread = new Thread(new ThreadStart(tsObject.ThreadSample));

            Console.WriteLine("현재 스레드 상태 : {0}", spThread.ThreadState);

            //스레드 시작하기
            spThread.Start();
            Console.WriteLine("현재 스레드 상태 : {0}", spThread.ThreadState);
            Console.WriteLine("현재 스레드 IsAlive? : {0}", spThread.IsAlive);
            
            //스레드 정지하기
            spThread.Abort();
            Console.WriteLine("현재 스레드 상태 : {0}", spThread.ThreadState);

        }
        public void ThreadSample()
        {
            //스레드를 1초간 쉬게한다.
           // Thread.Sleep(1000);
        }
    }
}
