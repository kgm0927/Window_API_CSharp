using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; //스레드 네임스페이스 추가
namespace ThreadSync
{
    class Program
    {
        static void Main(string[] args)
        {
            //동기화 객체를 생성합니다.
            SyncTest syncClass=new SyncTest();
            
            //스레드 배열
            Thread[] syncThread=new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                //스레드를 시작합니다.
                syncThread[i] = new Thread(new ThreadStart(syncClass.CountPlus));
                syncThread[i].Start();
            }

            //스레드가 모두 멈추기 까지 대기
            Thread.Sleep(100);

            //결과를 출력합니다.
            Console.WriteLine("Count : {0}",syncClass.Count);
            
        }
    }

    class SyncTest
    {
        //전체 카운트
        public int Count=0;

public void CountPlus()
{
    lock (this)
    {
        //Count의 값을 할당
        int temp = Count;

        //현재 스레드 아이디와 temp값 출력
        Console.WriteLine("Thread{0} : {1} ", Thread.CurrentThread.ManagedThreadId, temp);

        //Count에 1을 증가 
        temp = temp + 1;
        Thread.Sleep(1);
        Count = temp;
    }
}
    }
}
