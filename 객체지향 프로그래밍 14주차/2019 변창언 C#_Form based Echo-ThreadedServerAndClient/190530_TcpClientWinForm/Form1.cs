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

namespace _190530_TcpClientWinForm
{
    public partial class Form1 : Form
    {
        TcpClient tclient;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("연결"))
            {
                try
                {
                    tclient = new TcpClient(textBox1.Text, int.Parse(textBox2.Text));

                    if (tclient.Connected)
                    {
                        listBox1.Items.Add("소켓이 연결되었습니다.");
                        button1.Text = "연결 해제";
                    }
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add("Exception Thrown : " + ex.ToString());
                    listBox1.Items.Add("소켓이 연결되지 않았습니다.");
                    button1.Text = "연결";
                }
            }
            else
            {
                try
                {
                    button1.Text = "연결";
                    tclient.Close();
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add("Exception Thrown : " + ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!textBox3.Text.Equals("") && tclient.Connected)
                {
                    NetworkStream ns = tclient.GetStream();

                    Encoding ASCII = Encoding.ASCII;
                    Byte[] ByteGet = ASCII.GetBytes(textBox3.Text);     // 전송을 누르면 이걸 바이트 단위로 변환
                    Byte[] RecvBytes = new Byte[1024];

                    if (tclient == null)
                    {
                        listBox1.Items.Add("연결 실패");
                        button1.Text = "연결";
                    }
                    else
                    {
                        ns.Write(ByteGet, 0, ByteGet.Length);
                        listBox1.Items.Add("[TCP 클라이언트] " + ByteGet.Length + " 바이트를 보냈습니다.");

                        int bytes = ns.Read(RecvBytes, 0, RecvBytes.Length);
                        if (bytes == -1) listBox1.Items.Add("메시지 받기 실패");
                        else
                        {
                            listBox1.Items.Add("[TCP 클라이언트] " + bytes + " 바이트를 받았습니다.");
                            listBox1.Items.Add("[Server] : " + ASCII.GetString(RecvBytes, 0, bytes));
                            textBox3.Text = "";
                        }
                    }
                }
                else if (!tclient.Connected)
                {
                    listBox1.Items.Add("이전에 소켓이 연결되지 않았습니다.");
                    button1.Text = "연결";
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Exception Thrown : " + ex.ToString());
                button1.Text = "연결";
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) button2_Click(sender, e);
        }
    }
}
