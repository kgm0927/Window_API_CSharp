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
        // 생성자.
        public frmTicTacToe()
        {
            InitializeComponent();
        }


        #region 변수 선언 

        // 네트워크 프로그래밍에 사용할 변수들 .
        private Network _network;
        public bool _isNetworkPlay = false;
        public bool _isServer = false;
        public bool _isClient = false;

        // 입력할 기호 와 승자가 가질 기호를 선언한다. 
        public SignType _signTurn = SignType.Ball;
        public SignType _signWinner = SignType.None;  // 승자를 체크 

        // 프로그램적으로 사용할 가상적인 보드를 만들도록 하자. 
        public SignType[,] _signBoard = new SignType[4, 4] { {SignType.None, SignType.None, SignType.None,SignType.None},
                                                             {SignType.None, SignType.None, SignType.None,SignType.None},
                                                             {SignType.None, SignType.None, SignType.None,SignType.None},
                                                             {SignType.None, SignType.None, SignType.None,SignType.None}};

        #endregion


        #region 이벤트 처리

        private void frmTicTacToe_Load(object sender, EventArgs e)
        {
            // 네트워크 객체를 생성합니다. 
            _network = new Network(this);
            RestartGame();
        }

        // 픽처박스를 클릭했을때 실행되는 실행 메소드
        private void pic_Click(object sender, System.EventArgs e)
        {
            // 클릭된 그림의 이름으로 부터 행, 열의 위치를 알아냅니다. 
            string iNumber = ((System.Windows.Forms.Control)(((System.Windows.Forms.PictureBox)(sender)))).Name;
            int iRow = int.Parse(iNumber.Substring(3, 1));
            int iColumn = int.Parse(iNumber.Substring(4, 1));

            // 클릭된 곳에 기호를 표시합니다.
            RepresentSign(iRow, iColumn);
        }

        // 새게임 버튼을 누렀을때 실행됩니다. 
        private void btnNewGame_Click(object sender, System.EventArgs e)
        {
            // 네트워크 게임을 하고 있다면 새로 시작하고자 한다는 메세지를 전송합니다.
            if ((_isClient == true) || (_isServer == true))
                _network.SendsRestartPacket();
            
            //새로운 게임을 시작합니다.
            RestartGame();
        }

        // 서버로 실행할 버튼을 클릭했을때 실행됩니다. 
        private void btnServer_Click(object sender, System.EventArgs e)
        {
            _isServer = true;
            _network.StartServer();
            SetStatusMessage("연결을 기다립니다.");
            btnClient.Enabled = false;
            btnServer.Enabled = false;
            btnDisconn.Enabled = true;
        }

        // 서버에 접속 버튼을 누르면 아이피를 입력하도록 유도 합니다. 
        private void btnClient_Click(object sender, System.EventArgs e)
        {
            SetStatusMessage("접속할 IP를 입력하세요");
            // 입력 박스와 버튼을 보여지도록 합니다. 
            txtIP.Visible = true;
            btnOk.Visible = true;
        }

        // 아이피를 입력한후 오른쪽의 확인 버튼을 눌렀을때를 처리합니다. 
        private void btnOk_Click(object sender, EventArgs e)
        {
            // 아이피 입력상자와 확인 버튼을 숨깁니다.
            txtIP.Visible = false;
            btnOk.Visible = false;

            _isClient = true;
            SetStatusMessage("연결중...");
            // 입력한 IP로 서버로 접속을 합니다. 
            _network.ConnectServer(txtIP.Text);
            btnClient.Enabled = false;
            btnServer.Enabled = false;
            btnDisconn.Enabled = true;
        }

        // 연결을 끊기 버튼 입니다. 
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

        // 게임을 다시 시작하기 위해 모두 초기화 합니다. 
        public void RestartGame()
        {
            // 게임을 다시 시작합니다.
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

            // 표시되었던 기호들을 모두 초기화 합니다.
            _signBoard = new SignType[4, 4] { {SignType.None,SignType.None,SignType.None,SignType.None},
                                            {SignType.None,SignType.None,SignType.None,SignType.None},
                                            {SignType.None,SignType.None,SignType.None,SignType.None},
                                            {SignType.None,SignType.None,SignType.None,SignType.None}
                                          };

        }


        // 인자로 주어진 위치에 이미지를 
        public void RepresentSign(int row, int col)
        {
            int iRow = row;
            int iColumn = col;

            //-------------------------------------------------------------------------
            // 현재 클릭된 위치의 PictureBox를 가져 옮니다.
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
            // 내가 둔 위치를 상대방에게 보내도록 한다. 
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
            
            // 아무 표시가 없는 빈 칸을 클릭 했을때만 표시 설정 
            if (_signBoard[iRow, iColumn] == SignType.None)
            {
                _signBoard[iRow, iColumn] = _signTurn;

                // X 표시일 경우 
                if (_signTurn == SignType.Cross)
                { 
                    // X 표시로 바꾸어 준다. 
                    imgCurrent.Image = ilSign.Images[0];
                    imgCurrent.Refresh();
                    _signTurn = SignType.Ball;
                }
                // O 의 경우 
                else
                {
                    // 0 의 표시로 바꿔준다.
                    imgCurrent.Image = ilSign.Images[1];
                    imgCurrent.Refresh();
                    _signTurn = SignType.Cross;
                }

                // 아직 승자가 결정되지 않은 상태라면 
                if (_signWinner == SignType.None)
                {
                    VerifyWinner();
                }

            }// end if (_signBoard[iRow,iColumn]==SignType.None)

            _isNetworkPlay = false;

        }// end RepresentSign Method 


        private void VerifyWinner()
        {
            // 누가 이겼는지를 check 하기 위해 칸의 값을 더함 

            // 행의 합 
            int iSum1 = (int)_signBoard[1, 1] + (int)_signBoard[1, 2] + (int)_signBoard[1, 3];
            int iSum2 = (int)_signBoard[2, 1] + (int)_signBoard[2, 2] + (int)_signBoard[2, 3];
            int iSum3 = (int)_signBoard[3, 1] + (int)_signBoard[3, 2] + (int)_signBoard[3, 3];

            // 열의 합
            int iSum4 = (int)_signBoard[1, 1] + (int)_signBoard[2, 1] + (int)_signBoard[3, 1];
            int iSum5 = (int)_signBoard[1, 2] + (int)_signBoard[2, 2] + (int)_signBoard[3, 2];
            int iSum6 = (int)_signBoard[1, 3] + (int)_signBoard[2, 3] + (int)_signBoard[3, 3];

            // 대각선 의 합
            int iSum7 = (int)_signBoard[1, 1] + (int)_signBoard[2, 2] + (int)_signBoard[3, 3];
            int iSum8 = (int)_signBoard[3, 1] + (int)_signBoard[2, 2] + (int)_signBoard[1, 3];


            // 0이 이겼을 경우 
            if ((iSum1 == 3) || (iSum2 == 3) || (iSum3 == 3) ||
                (iSum4 == 3) || (iSum5 == 3) || (iSum6 == 3) ||
                (iSum7 == 3) || (iSum8 == 3))
            {
                _signWinner = SignType.Ball;
                SetStatusMessage("O가 이겼습니다.");
                return;
            }

            // X 가 이겼을 경우 
            if ((iSum1 == 30) || (iSum2 == 30) || (iSum3 == 30) ||
                (iSum4 == 30) || (iSum5 == 30) || (iSum6 == 30) ||
                (iSum7 == 30) || (iSum8 == 30))
            {
                _signWinner = SignType.Cross;
                SetStatusMessage("X가 이겼습니다.");
                return;

            }


            // 비겼을 경우 
            bool isDraw = true;

            // 모두 두었는지 체크합니다.  
            for ( int y = 1; y <= 3; y++)
                for (int x = 1; x <= 3; x++)
                    if (_signBoard[x, y] == SignType.None)
                    {
                        isDraw = false;
                    }

            if (isDraw == true)
            {
                SetStatusMessage("비겼습니다.");
                return;
            }
        }


        // 상태 라벨에 메시지를 표시합니다. 
        public void SetStatusMessage(string msg)
        {
            lblMsg.Text = msg;
        }

        #endregion





    }// end form 



    #region Data Structures

    //-------------------------------------------------------------------------
    // 칸에 쓰일수 있는 기호의 종류 입니다.
    //-------------------------------------------------------------------------
    public enum SignType
    {
        None = 0,
        Ball = 1,
        Cross = 10
    }
    #endregion
}