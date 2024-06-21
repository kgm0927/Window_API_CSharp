using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;//스레드 네임스페이스 추가

namespace ThreadHorse
{
    class Horse
    {
        //말번호
        private int Number;

        public Horse(int Number)
        {
            //클래스 생성시 말 번호
            this.Number = Number;
        }
        public void Go()
        {
            //MITER 입니다.
            int Miter = 0;

            while (true)
            {
                //랜던함수 생성
                Random rd = new Random(Number);

                //50~150 사이의 난수 발생
                Miter += rd.Next(50, 150);

                //만약 500미터를 지났다면
                if (Miter >= 500)
                    break;

                //현재 상황
                Console.WriteLine("{0}번마 {1}미터를 지나고 있습니다.", Number, Miter);

                //스레드 휴식
                Thread.Sleep(300);
            }
            Console.WriteLine("{0}번마 골인!!", Number);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //5마리의 말 생성하기
            Horse HorseOne= new Horse(1);
            Horse HorseTwo= new Horse(2);
            Horse HorseThree= new Horse(3);

            //스레드 생성하기
            //Thread ThreadOne = new Thread(new ThreadStart(HorseOne.Go));
            ThreadStart ts = new ThreadStart(HorseOne.Go);
            Thread ThreadOne = new Thread(ts);

            Thread ThreadTwo = new Thread(new ThreadStart(HorseTwo.Go));
            Thread ThreadThree = new Thread(new ThreadStart(HorseThree.Go));
            
            //스레드 실행->출발
            ThreadOne.Start();
            ThreadTwo.Start();
            ThreadThree.Start();
        }
    }
}