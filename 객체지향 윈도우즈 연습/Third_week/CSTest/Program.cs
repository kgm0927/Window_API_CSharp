namespace CSTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            object[] ar=new object[3];
            ar[0] = 2.34;
            ar[1] = new DateTime(2005, 3, 1);
            ar[2] = "string";


            for (int i = 0; i < ar.Length; i++)
            {
                Console.WriteLine(ar[i].ToString());
            }

            //ar[3]=24;
            // Element의 개수가 미리 잡아둔 배열 크기를 넘어갈 때

            object[] t = ar;
            ar = new object[6];
            Array.Copy(t, ar, t.Length);

            ar[3] = 24;


            for (int i = 0; i < ar.Length; i++)
            {
                Console.WriteLine(ar[i].ToString());
                //다섯번째 접근할때는 null 이기에 error 발생 
                //ar.Count 같은 실제요소수를 리턴하는 property 구현 필요...
            }


        }
    }
}
