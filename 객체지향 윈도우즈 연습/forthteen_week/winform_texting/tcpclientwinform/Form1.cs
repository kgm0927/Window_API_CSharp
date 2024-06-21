using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace tcpclientwinform
{
    public partial class Form1 : Form
    {
        TcpClient tclient;

      private void  clicking_the_connecting_btn(ref Button btn_conn,ref TextBox ip,ref TextBox port,ref ListBox listbox){
            if(button1.Text.Equals("연결")){
                try{
                    tclient=new TcpClient(ip.Text,int.Parse(textBox2.Text));

                    if (tclient.Connected)
                    {
                        listbox.Items.Add("소켓이 연결되었습니다.");
                        btn_conn.Text = "연결해제";
                    }
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add("Exception Thrown : "+ ex.ToString());
                    listBox1.Items.Add("소켓이 연결되지 않았습니다.");
                    btn_conn.Text = "연결";
                }
            }

            else
            {
                try
                {
                    btn_conn.Text = "연결";
                    tclient.Close();
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add("Exception Thrown : "+ ex.ToString());
                }
            }

        }


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clicking_the_connecting_btn(ref button1, ref textBox1, ref textBox2, ref listBox1);
        }

        private void sending_message(ref TcpClient tclient,ref TextBox text3,ref ListBox listbox,ref Button btn1)
        {
            try
            {
                if (!text3.Text.Equals("") && tclient.Connected)
                {
                    NetworkStream ns = tclient.GetStream();

                    Encoding ASCII = Encoding.ASCII;
                    Byte[] ByteGet = ASCII.GetBytes(text3.Text);
                    Byte[] RecvBytes = new Byte[1024];


                    if (tclient == null)
                    {
                        listbox.Items.Add("연결 실패");
                        btn1.Text = "연결";
                    }
                    else
                    {
                        ns.Write(ByteGet, 0, ByteGet.Length);
                        listbox.Items.Add("[TCP 클라이언트]"+ByteGet.Length+"바이트를 보냈습니다.");

                        int bytes = ns.Read(RecvBytes, 0, RecvBytes.Length);
                        if (bytes == -1) listBox1.Items.Add("메시지 받기 실패");
                        else
                        {
                            listBox1.Items.Add("[TCP 클라이언트]" + bytes + " 바이트를 받았습니다.");
                            listBox1.Items.Add("[Server] : " + ASCII.GetString(RecvBytes, 0, bytes));
                            text3.Text = "";
                        }
                    }

                }
                else if (!tclient.Connected)
                {
                    listBox1.Items.Add("이전에 소켓이 연결되지 않았습니다.");
                    btn1.Text = "연결";
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Exception Thrown : "+ ex.ToString());
                btn1.Text = "연결";
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            sending_message(ref tclient, ref textBox3, ref listBox1, ref button1);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) button2_Click(sender, e);

        }

        
    }
}
