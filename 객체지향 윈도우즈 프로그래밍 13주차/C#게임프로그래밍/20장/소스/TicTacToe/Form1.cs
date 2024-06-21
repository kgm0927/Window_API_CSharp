using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class frmTicTacToe : Form
    {
        // ������.
        public frmTicTacToe()
        {
            InitializeComponent();
        }


        #region ���� ���� 

        // ��Ʈ��ũ ���α׷��ֿ� ����� ������ .
        private Network _network;
        public bool _isNetworkPlay = false;
        public bool _isServer = false;
        public bool _isClient = false;

        // �Է��� ��ȣ �� ���ڰ� ���� ��ȣ�� �����Ѵ�. 
        public SignType _signTurn = SignType.Ball;
        public SignType _signWinner = SignType.None;  // ���ڸ� üũ 

        // ���α׷������� ����� �������� ���带 ���鵵�� ����. 
        public SignType[,] _signBoard = new SignType[4, 4] { {SignType.None, SignType.None, SignType.None,SignType.None},
                                                             {SignType.None, SignType.None, SignType.None,SignType.None},
                                                             {SignType.None, SignType.None, SignType.None,SignType.None},
                                                             {SignType.None, SignType.None, SignType.None,SignType.None}};

        #endregion


        #region �̺�Ʈ ó��

        private void frmTicTacToe_Load(object sender, EventArgs e)
        {
            // ��Ʈ��ũ ��ü�� �����մϴ�. 
            _network = new Network(this);
            RestartGame();
        }

        // ��ó�ڽ��� Ŭ�������� ����Ǵ� ���� �޼ҵ�
        private void pic_Click(object sender, System.EventArgs e)
        {
            // Ŭ���� �׸��� �̸����� ���� ��, ���� ��ġ�� �˾Ƴ��ϴ�. 
            string iNumber = ((System.Windows.Forms.Control)(((System.Windows.Forms.PictureBox)(sender)))).Name;
            int iRow = int.Parse(iNumber.Substring(3, 1));
            int iColumn = int.Parse(iNumber.Substring(4, 1));

            // Ŭ���� ���� ��ȣ�� ǥ���մϴ�.
            RepresentSign(iRow, iColumn);
        }

        // ������ ��ư�� �������� ����˴ϴ�. 
        private void btnNewGame_Click(object sender, System.EventArgs e)
        {
            // ��Ʈ��ũ ������ �ϰ� �ִٸ� ���� �����ϰ��� �Ѵٴ� �޼����� �����մϴ�.
            if ((_isClient == true) || (_isServer == true))
                _network.SendsRestartPacket();
            
            //���ο� ������ �����մϴ�.
            RestartGame();
        }

        // ������ ������ ��ư�� Ŭ�������� ����˴ϴ�. 
        private void btnServer_Click(object sender, System.EventArgs e)
        {
            _isServer = true;
            _network.StartServer();
            SetStatusMessage("������ ��ٸ��ϴ�.");
            btnClient.Enabled = false;
            btnServer.Enabled = false;
            btnDisconn.Enabled = true;
        }

        // ������ ���� ��ư�� ������ �����Ǹ� �Է��ϵ��� ���� �մϴ�. 
        private void btnClient_Click(object sender, System.EventArgs e)
        {
            SetStatusMessage("������ IP�� �Է��ϼ���");
            // �Է� �ڽ��� ��ư�� ���������� �մϴ�. 
            txtIP.Visible = true;
            btnOk.Visible = true;
        }

        // �����Ǹ� �Է����� �������� Ȯ�� ��ư�� ���������� ó���մϴ�. 
        private void btnOk_Click(object sender, EventArgs e)
        {
            // ������ �Է»��ڿ� Ȯ�� ��ư�� ����ϴ�.
            txtIP.Visible = false;
            btnOk.Visible = false;

            _isClient = true;
            SetStatusMessage("������...");
            // �Է��� IP�� ������ ������ �մϴ�. 
            _network.ConnectServer(txtIP.Text);
            btnClient.Enabled = false;
            btnServer.Enabled = false;
            btnDisconn.Enabled = true;
        }

        // ������ ���� ��ư �Դϴ�. 
        public void btnDisconn_Click(object sender, System.EventArgs e)
        {
            _network.Disconnect();
            _isServer = false;
            _isClient = false;

            btnClient.Enabled = true;
            btnServer.Enabled = true;
            btnDisconn.Enabled = false;
        }

        #endregion



        #region Method

        // ������ �ٽ� �����ϱ� ���� ��� �ʱ�ȭ �մϴ�. 
        public void RestartGame()
        {
            // ������ �ٽ� �����մϴ�.
            pic11.Image = null;
            pic12.Image = null;
            pic13.Image = null;
            pic21.Image = null;
            pic22.Image = null;
            pic23.Image = null;
            pic31.Image = null;
            pic32.Image = null;
            pic33.Image = null;

            _signTurn = SignType.Ball;
            _signWinner = SignType.None;
            SetStatusMessage("");

            // ǥ�õǾ��� ��ȣ���� ��� �ʱ�ȭ �մϴ�.
            _signBoard = new SignType[4, 4] { {SignType.None,SignType.None,SignType.None,SignType.None},
                                            {SignType.None,SignType.None,SignType.None,SignType.None},
                                            {SignType.None,SignType.None,SignType.None,SignType.None},
                                            {SignType.None,SignType.None,SignType.None,SignType.None}
                                          };

        }


        // ���ڷ� �־��� ��ġ�� �̹����� 
        public void RepresentSign(int row, int col)
        {
            int iRow = row;
            int iColumn = col;

            //-------------------------------------------------------------------------
            // ���� Ŭ���� ��ġ�� PictureBox�� ���� �Ŵϴ�.
            //-------------------------------------------------------------------------
            System.Windows.Forms.PictureBox imgCurrent = pic11;

            if (iRow == 1 && iColumn == 1) imgCurrent = pic11;
            if (iRow == 1 && iColumn == 2) imgCurrent = pic12;
            if (iRow == 1 && iColumn == 3) imgCurrent = pic13;
            if (iRow == 2 && iColumn == 1) imgCurrent = pic21;
            if (iRow == 2 && iColumn == 2) imgCurrent = pic22;
            if (iRow == 2 && iColumn == 3) imgCurrent = pic23;
            if (iRow == 3 && iColumn == 1) imgCurrent = pic31;
            if (iRow == 3 && iColumn == 2) imgCurrent = pic32;
            if (iRow == 3 && iColumn == 3) imgCurrent = pic33;

            //-------------------------------------------------------------------------
            // ���� �� ��ġ�� ���濡�� �������� �Ѵ�. 
            if (((_isServer == true) && (_signTurn == SignType.Ball) && (_isNetworkPlay == false)) ||
                 ((_isClient == true) && (_signTurn == SignType.Cross) && (_isNetworkPlay == false)))
            {
                _network.SendPosition(iRow, iColumn);
            }
            else
            {
                if (((_isServer == true) && (_signTurn == SignType.Cross) && (_isNetworkPlay == false)) ||
                    ((_isClient == true) && (_signTurn == SignType.Ball) && (_isNetworkPlay == false)))
                    return;
            }
            
            // �ƹ� ǥ�ð� ���� �� ĭ�� Ŭ�� �������� ǥ�� ���� 
            if (_signBoard[iRow, iColumn] == SignType.None)
            {
                _signBoard[iRow, iColumn] = _signTurn;

                // X ǥ���� ��� 
                if (_signTurn == SignType.Cross)
                { 
                    // X ǥ�÷� �ٲپ� �ش�. 
                    imgCurrent.Image = ilSign.Images[0];
                    imgCurrent.Refresh();
                    _signTurn = SignType.Ball;
                }
                // O �� ��� 
                else
                {
                    // 0 �� ǥ�÷� �ٲ��ش�.
                    imgCurrent.Image = ilSign.Images[1];
                    imgCurrent.Refresh();
                    _signTurn = SignType.Cross;
                }

                // ���� ���ڰ� �������� ���� ���¶�� 
                if (_signWinner == SignType.None)
                {
                    VerifyWinner();
                }

            }// end if (_signBoard[iRow,iColumn]==SignType.None)

            _isNetworkPlay = false;

        }// end RepresentSign Method 


        private void VerifyWinner()
        {
            // ���� �̰������ check �ϱ� ���� ĭ�� ���� ���� 

            // ���� �� 
            int iSum1 = (int)_signBoard[1, 1] + (int)_signBoard[1, 2] + (int)_signBoard[1, 3];
            int iSum2 = (int)_signBoard[2, 1] + (int)_signBoard[2, 2] + (int)_signBoard[2, 3];
            int iSum3 = (int)_signBoard[3, 1] + (int)_signBoard[3, 2] + (int)_signBoard[3, 3];

            // ���� ��
            int iSum4 = (int)_signBoard[1, 1] + (int)_signBoard[2, 1] + (int)_signBoard[3, 1];
            int iSum5 = (int)_signBoard[1, 2] + (int)_signBoard[2, 2] + (int)_signBoard[3, 2];
            int iSum6 = (int)_signBoard[1, 3] + (int)_signBoard[2, 3] + (int)_signBoard[3, 3];

            // �밢�� �� ��
            int iSum7 = (int)_signBoard[1, 1] + (int)_signBoard[2, 2] + (int)_signBoard[3, 3];
            int iSum8 = (int)_signBoard[3, 1] + (int)_signBoard[2, 2] + (int)_signBoard[1, 3];


            // 0�� �̰��� ��� 
            if ((iSum1 == 3) || (iSum2 == 3) || (iSum3 == 3) ||
                (iSum4 == 3) || (iSum5 == 3) || (iSum6 == 3) ||
                (iSum7 == 3) || (iSum8 == 3))
            {
                _signWinner = SignType.Ball;
                SetStatusMessage("O�� �̰���ϴ�.");
                return;
            }

            // X �� �̰��� ��� 
            if ((iSum1 == 30) || (iSum2 == 30) || (iSum3 == 30) ||
                (iSum4 == 30) || (iSum5 == 30) || (iSum6 == 30) ||
                (iSum7 == 30) || (iSum8 == 30))
            {
                _signWinner = SignType.Cross;
                SetStatusMessage("X�� �̰���ϴ�.");
                return;

            }


            // ����� ��� 
            bool isDraw = true;

            // ��� �ξ����� üũ�մϴ�.  
            for ( int y = 1; y <= 3; y++)
                for (int x = 1; x <= 3; x++)
                    if (_signBoard[x, y] == SignType.None)
                    {
                        isDraw = false;
                    }

            if (isDraw == true)
            {
                SetStatusMessage("�����ϴ�.");
                return;
            }
        }


        // ���� �󺧿� �޽����� ǥ���մϴ�. 
        public void SetStatusMessage(string msg)
        {
            lblMsg.Text = msg;
        }

        #endregion





    }// end form 



    #region Data Structures

    //-------------------------------------------------------------------------
    // ĭ�� ���ϼ� �ִ� ��ȣ�� ���� �Դϴ�.
    //-------------------------------------------------------------------------
    public enum SignType
    {
        None = 0,
        Ball = 1,
        Cross = 10
    }
    #endregion
}