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
namespace File_Stream_Test
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string insert = @"c:\temp\fs.txt";
        
            byte[] data = { 65,66,67,68,69,70,71,72};
            record_file( insert,data);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[8];
            string insert = @"c:\temp\fs.txt";

            try
            {
                output_file(insert, data);
                string result = "";
                for (int i = 0; i < data.Length; i++)
                {
                    result += data[i].ToString() + ",";
                }
                MessageBox.Show(result, "파일 내용");
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("지정한 파일이 없다.");
            }
        }

        private void record_file(string insert,byte []data)
        {

            FileStream fs=new  FileStream(insert, FileMode.Create, FileAccess.Write);
           
            fs.Write(data, 0, data.Length);// fs.Write(data[i]); 로도 가능하다.
            fs.Close();
            MessageBox.Show(@"C:의 fs.txt 파일에 기록했습니다.");
        }

        private void output_file(string path, byte[] data)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            fs.Read(data, 0, data.Length);
            fs.Close();
        }


    }
}
