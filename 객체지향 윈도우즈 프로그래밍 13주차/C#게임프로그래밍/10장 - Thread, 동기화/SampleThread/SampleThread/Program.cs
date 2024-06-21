using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;//������ ���ӽ����̽�

namespace SampleThread
{
    class Program
    {
        static void Main(string[] args)
        {
            //�޼��� ����
            ThreadSample();
            
            //������ �����ϱ�
            ThreadStart ts = new ThreadStart(ThreadSample);
            Thread spThread = new Thread(ts);

            //������ �۵�
            spThread.Start();

        }
        static void ThreadSample()
        {
            Console.WriteLine("Thread Called");
            
            //������ ���̵� ���
            Console.WriteLine("���� Thread ID : {0}",Thread.CurrentThread.ManagedThreadId);
        }
    }
}
