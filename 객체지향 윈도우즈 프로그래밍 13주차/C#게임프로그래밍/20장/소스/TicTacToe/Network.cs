using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace TicTacToe
{

    public class Network
    {

        #region 변수 선언
        // TicTacToe 메인 모듈을 참고할수 있는 객체 생성 
        frmTicTacToe _TicTacToe = null;

        // 서버와 클라이언트를 위한 쓰레드 
        public Thread _threadReceiveClient;
        public Thread _threadReceiveServer;

        // 서버의 IP 와 Port 
        private string _serverIP;
        const int SERVERPORT = 2005;

        // 서버와 클라이언트의 Loop체크를 위한 변수 
        bool _isReceivingServer = true;
        bool _isReceivingClient = true;

        // 서버와 클라이언트를위한 NetworkStream
        NetworkStream _clientNetStream;
        NetworkStream _serverNetStream;

        TcpClient _tcpClient;
        TcpListener _tcpListener;
        Socket _sktServer;

        #endregion


        #region 생성자

        public Network(frmTicTacToe obj)
        {
            _TicTacToe = obj;
        }

        #endregion


        #region Client
        // 서버에 접속하여 처리할 쓰레드를 시작합니다.
        public void ConnectServer(string ip)
        {
            _serverIP = ip;
            byte[] buf = new byte[1];

            _threadReceiveClient = new Thread(new ThreadStart(ThreadReceivingClient));
            _threadReceiveClient.Start();
        }

        // 서버에게 보낼 Packet 전송 모듈 
        private void ThreadReceivingClient()
        {
            try
            {
                byte[] buf = new byte[512];
                int bytesReceived = 0;

                // 클라이언트 소켓을 생성하고 연결 합니다.     
                _tcpClient = new TcpClient(_serverIP, SERVERPORT);
                // 네트워크 스트림을 생성합니다. 
                _clientNetStream = _tcpClient.GetStream();
                // 게임을 재시작합니다. 
                _TicTacToe.RestartGame();
                _TicTacToe.SetStatusMessage("연결 되었습니다.");

                _isReceivingClient = true;

                while (_isReceivingClient)
                {
                    // 데이터를 받을때 까지 블락됨 
                    try
                    {
                        // 약속된 메세지의 크기는 2바이트는 넘지 않으므로
                        // 메세지의 크기는 2바이트 만큼만 받습니다. 
                        bytesReceived = _clientNetStream.Read(buf, 0, 2);
                    }
                    catch
                    {
                        return;
                    }

                    // 받은 패킷을 분석 
                    if (bytesReceived > 0)
                    {
                        // R 이란 문자는 게임을 새로 시작하라는 뜻입니다.
                        if (buf[0] == byte.Parse(Asc("R").ToString()))
                        {
                            _TicTacToe.RestartGame();
                            continue;
                        }

                        // 첫 문자가 R이 아닌 숫자로 왔을땐 row, colum 값이다. 
                        int wRow = int.Parse(Convert.ToChar(buf[0]).ToString());
                        int wColumn = int.Parse(Convert.ToChar(buf[1]).ToString());

                        if ((wRow > 0 && wRow < 4) && (wColumn > 0 && wColumn < 4))
                        {
                            _TicTacToe._isNetworkPlay = true;
                            _TicTacToe.RepresentSign(wRow, wColumn);
                        }

                    } //if (bytesReceived>0) 

                } //while (_isReceivingClient)

            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred: " + ex.Message + "\n" + ex.StackTrace);
                _TicTacToe.btnDisconn_Click(null, null);
                return;
            }
        }

        #endregion


        #region Server

        public void StartServer()
        {
            _threadReceiveServer = new Thread(new ThreadStart(ThreadReceivingServer));
            _threadReceiveServer.Start();
        }


        private void ThreadReceivingServer()
        {
            // 클라이언트로 부터 패킷을 받는 쓰레드 
            try
            {
                byte[] buf = new byte[512];
                IPHostEntry localHostEntry = Dns.GetHostByName(Dns.GetHostName());
                int bytesReceived = 0;

                _tcpListener = new TcpListener(localHostEntry.AddressList[0], SERVERPORT);

                _tcpListener.Start();

                // 클라이언트로 부터 접속될때까지 블락된다. 
                _sktServer = _tcpListener.AcceptSocket();
                _serverNetStream = new NetworkStream(_sktServer);

                _TicTacToe.RestartGame();
                _TicTacToe.SetStatusMessage("연결 되었습니다.");

                _isReceivingServer = true;

                while (_isReceivingServer)
                {

                    // 데이터를 받을때까지 블락됨 
                    try
                    {
                        bytesReceived = _serverNetStream.Read(buf, 0, 2);
                    }
                    catch
                    {
                        return;
                    }

                    // 패킷 처리 

                    if (bytesReceived > 0)
                    {

                        // R 이란 신호는 게임을 다시 시작하라는뜻 
                        if (buf[0] == byte.Parse(Asc("R").ToString()))
                        {
                            _TicTacToe.RestartGame();
                            continue;
                        }

                        // 숫자일 경우 row, column 이다. 
                        int wRow = int.Parse(Convert.ToChar(buf[0]).ToString());
                        int wColumn = int.Parse(Convert.ToChar(buf[1]).ToString());

                        if ((wRow > 0 && wRow < 4) && (wColumn > 0 && wColumn < 4))
                        {
                            _TicTacToe._isNetworkPlay = true;
                            _TicTacToe.RepresentSign(wRow, wColumn);
                        }

                    }	//if (bytesReceived>0) 

                }	//while (_isReceivingServer)
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred: " + ex.Message + "\n" + ex.StackTrace);
                _TicTacToe.btnDisconn_Click(null, null);
                return;
            }
        }

        #endregion



        #region 패킷을 쉽게 보내고 받기위한 Util과 연결 종료 함수

        public void SendPacketTCP(Byte[] data)
        {
            // TCP를 이용하여 데이터를 전송한다. 
            try
            {
                // 클라이언트라면 
                if (_TicTacToe._isClient == true)
                {
                    if (_clientNetStream == null)
                        return;

                    if (_clientNetStream.CanWrite)
                    {
                        _clientNetStream.Write(data, 0, 2);
                        _clientNetStream.Flush();
                    }
                }
                // 서버라면
                else  
                {
                    if (_serverNetStream == null)
                        return;

                    if (_serverNetStream.CanWrite)
                    {
                        _serverNetStream.Write(data, 0, 2);
                        _serverNetStream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred: " + ex.Message + "\n" + ex.StackTrace);
                _TicTacToe.btnDisconn_Click(null, null);
                return;
            }

        }

        // 게임판의 에서 위치정보를 전송한다. 
        public void SendPosition(int row, int col)
        {
            byte[] buf = new byte[2];
            buf[0] = byte.Parse(Asc(row.ToString()).ToString());
            buf[1] = byte.Parse(Asc(col.ToString()).ToString());

            SendPacketTCP(buf);

        }

        // 다시 시작하라는 신호(R)을 전송한다. 
        public void SendsRestartPacket()
        {
            byte[] buf = new byte[2];
            buf[0] = byte.Parse(Asc("R").ToString());
            buf[1] = 0;

            SendPacketTCP(buf);
        }

        public void Disconnect()
        {
            // 접속을 종료 합니다. 
            if (_TicTacToe._isClient == true)
            {
                _threadReceiveClient.Abort();

                _isReceivingClient = false;

                if (_clientNetStream != null)
                    _clientNetStream.Close();

                if (_tcpClient != null)
                    _tcpClient.Close();

            }

            if (_TicTacToe._isServer == true)
            {
                _threadReceiveServer.Abort();

                _isReceivingServer = false;

                if (_serverNetStream != null)
                    _serverNetStream.Close();

                if (_tcpListener != null)
                    _tcpListener.Stop();

                if (_sktServer != null)
                    _sktServer.Shutdown(SocketShutdown.Both);
            }

        }

        // 바이트 값으로 쉽게 변경하기 위한 모듈 
        private static int Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new ApplicationException("Character is not valid.");
            }

        }	//private static int Asc(string character)

        #endregion




    }	//public class Network

}   //namespace TicTacToe
