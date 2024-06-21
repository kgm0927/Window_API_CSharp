using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;//������ ���ӽ����̽� �߰�

namespace ThreadHorse
{
    class Horse
    {
        //����ȣ
        private int Number;

        public Horse(int Number)
        {
            //Ŭ���� ������ �� ��ȣ
            this.Number = Number;
        }
        public void Go()
        {
            //MITER �Դϴ�.
            int Miter = 0;

            while (true)
            {
                //�����Լ� ����
                Random rd = new Random(Number);

                //50~150 ������ ���� �߻�
                Miter += rd.Next(50, 150);

                //���� 500���͸� �����ٸ�
                if (Miter >= 500)
                    break;

                //���� ��Ȳ
                Console.WriteLine("{0}���� {1}���͸� ������ �ֽ��ϴ�.", Number, Miter);

                //������ �޽�
                Thread.Sleep(300);
            }
            Console.WriteLine("{0}���� ����!!", Number);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //5������ �� �����ϱ�
            Horse HorseOne= new Horse(1);
            Horse HorseTwo= new Horse(2);
            Horse HorseThree= new Horse(3);

            //������ �����ϱ�
            //Thread ThreadOne = new Thread(new ThreadStart(HorseOne.Go));
            ThreadStart ts = new ThreadStart(HorseOne.Go);
            Thread ThreadOne = new Thread(ts);

            Thread ThreadTwo = new Thread(new ThreadStart(HorseTwo.Go));
            Thread ThreadThree = new Thread(new ThreadStart(HorseThree.Go));
            
            //������ ����->���
            ThreadOne.Start();
            ThreadTwo.Start();
            ThreadThree.Start();
        }
    }
}