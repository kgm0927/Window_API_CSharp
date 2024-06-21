using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; //������ ���ӽ����̽� �߰�
namespace ThreadSync
{
    class Program
    {
        static void Main(string[] args)
        {
            //����ȭ ��ü�� �����մϴ�.
            SyncTest syncClass=new SyncTest();
            
            //������ �迭
            Thread[] syncThread=new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                //�����带 �����մϴ�.
                syncThread[i] = new Thread(new ThreadStart(syncClass.CountPlus));
                syncThread[i].Start();
            }

            //�����尡 ��� ���߱� ���� ���
            Thread.Sleep(100);

            //����� ����մϴ�.
            Console.WriteLine("Count : {0}",syncClass.Count);
            
        }
    }

    class SyncTest
    {
        //��ü ī��Ʈ
        public int Count=0;

public void CountPlus()
{
    lock (this)
    {
        //Count�� ���� �Ҵ�
        int temp = Count;

        //���� ������ ���̵�� temp�� ���
        Console.WriteLine("Thread{0} : {1} ", Thread.CurrentThread.ManagedThreadId, temp);

        //Count�� 1�� ���� 
        temp = temp + 1;
        Thread.Sleep(1);
        Count = temp;
    }
}
    }
}
