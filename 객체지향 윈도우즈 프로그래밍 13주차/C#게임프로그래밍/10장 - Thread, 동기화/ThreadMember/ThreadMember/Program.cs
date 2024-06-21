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
            //������ �����ϱ�
            Program tsObject = new Program();
            Thread spThread = new Thread(new ThreadStart(tsObject.ThreadSample));

            Console.WriteLine("���� ������ ���� : {0}", spThread.ThreadState);

            //������ �����ϱ�
            spThread.Start();
            Console.WriteLine("���� ������ ���� : {0}", spThread.ThreadState);
            Console.WriteLine("���� ������ IsAlive? : {0}", spThread.IsAlive);
            
            //������ �����ϱ�
            spThread.Abort();
            Console.WriteLine("���� ������ ���� : {0}", spThread.ThreadState);

        }
        public void ThreadSample()
        {
            //�����带 1�ʰ� �����Ѵ�.
           // Thread.Sleep(1000);
        }
    }
}
