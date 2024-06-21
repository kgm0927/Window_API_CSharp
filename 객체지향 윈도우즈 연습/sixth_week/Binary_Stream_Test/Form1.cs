using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Binary_Stream_Test
{
    public partial class Form1 : Form
    {
        Human kim = new Human("김상형", 28);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = kim.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string path=@"c:\temp\jack.bin";
            save_file(path, kim);
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string title="파일을 읽는 중";
            string path=@"c:\temp\jack.bin";
  
            open_file(label1,title,path,kim);
  
        }

        private void save_file(string path,Human h)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            h.Write(bw);
            fs.Close();
        }

        private void open_file(Label label1, string label_text,string path ,Human h)
        {
            label1.Text = label_text;
            label1.Refresh();
            System.Threading.Thread.Sleep(1000);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);


            h = Human.Read(br);
            fs.Close();
            label1.Text = kim.ToString();
        }



    }

    class Human
    {
        private string Name;
        private int Age;
        private float Temp;
        public Human(string aName, int aAge)
        {
            Name = aName;
            Age = aAge;
            Temp = 1.23f;
        }

        public override string ToString()
        {
            Temp += 1;
            return "이름 :" + Name + ", 나이:" + Age;
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(Name);
            bw.Write(Age);// 메소드를 따로 읽어놓는 것 뿐이다.
        }
        public static Human Read(BinaryReader br){// 파일을 읽는다.
            return new Human(br.ReadString(), br.ReadInt32());
        }

    }
}
