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

        #region ���� ����
        // TicTacToe ���� ����� �����Ҽ� �ִ� ��ü ���� 
        frmTicTacToe _TicTacToe = null;

        // ������ Ŭ���̾�Ʈ�� ���� ������ 
        public Thread _threadReceiveClient;
        public Thread _threadReceiveServer;

        // ������ IP �� Port 
        private string _serverIP;
        const int SERVERPORT = 2005;

        // ������ Ŭ���̾�Ʈ�� Loopüũ�� ���� ���� 
        bool _isReceivingServer = true;
        bool _isReceivingClient = true;

        // ������ Ŭ���̾�Ʈ������ NetworkStream
        NetworkStream _clientNetStream;
        NetworkStream _serverNetStream;

        TcpClient _tcpClient;
        TcpListener _tcpListener;
        Socket _sktServer;

        #endregion


        #region ������

        public Network(frmTicTacToe obj)
        {
            _TicTacToe = obj;
        }

        #endregion


        #region Client
        // ������ �����Ͽ� ó���� �����带 �����մϴ�.
        public void ConnectServer(string ip)
        {
            _serverIP = ip;
            byte[] buf = new byte[1];

            _threadReceiveClient = new Thread(new ThreadStart(ThreadReceivingClient));
            _threadReceiveClient.Start();
        }

        // �������� ���� Packet ���� ��� 
        private void ThreadReceivingClient()
        {
            try
            {
                byte[] buf = new byte[512];
                int bytesReceived = 0;

                // Ŭ���̾�Ʈ ������ �����ϰ� ���� �մϴ�.     
                _tcpClient = new TcpClient(_serverIP, SERVERPORT);
                // ��Ʈ��ũ ��Ʈ���� �����մϴ�. 
                _clientNetStream = _tcpClient.GetStream();
                // ������ ������մϴ�. 
                _TicTacToe.RestartGame();
                _TicTacToe.SetStatusMessage("���� �Ǿ����ϴ�.");

                _isReceivingClient = true;

                while (_isReceivingClient)
                {
                    // �����͸� ������ ���� ����� 
                    try
                    {
                        // ��ӵ� �޼����� ũ��� 2����Ʈ�� ���� �����Ƿ�
                        // �޼����� ũ��� 2����Ʈ ��ŭ�� �޽��ϴ�. 
                        bytesReceived = _clientNetStream.Read(buf, 0, 2);
                    }
                    catch
                    {
                        return;
                    }

                    // ���� ��Ŷ�� �м� 
                    if (bytesReceived > 0)
                    {
                        // R �̶� ���ڴ� ������ ���� �����϶�� ���Դϴ�.
                        if (buf[0] == byte.Parse(Asc("R").ToString()))
                        {
                            _TicTacToe.RestartGame();
                            continue;
                        }

                        // ù ���ڰ� R�� �ƴ� ���ڷ� ������ row, colum ���̴�. 
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
            // Ŭ���̾�Ʈ�� ���� ��Ŷ�� �޴� ������ 
            try
            {
                byte[] buf = new byte[512];
                IPHostEntry localHostEntry = Dns.GetHostByName(Dns.GetHostName());
                int bytesReceived = 0;

                _tcpListener = new TcpListener(localHostEntry.AddressList[0], SERVERPORT);

                _tcpListener.Start();

                // Ŭ���̾�Ʈ�� ���� ���ӵɶ����� ����ȴ�. 
                _sktServer = _tcpListener.AcceptSocket();
                _serverNetStream = new NetworkStream(_sktServer);

                _TicTacToe.RestartGame();
                _TicTacToe.SetStatusMessage("���� �Ǿ����ϴ�.");

                _isReceivingServer = true;

                while (_isReceivingServer)
                {

                    // �����͸� ���������� ����� 
                    try
                    {
                        bytesReceived = _serverNetStream.Read(buf, 0, 2);
                    }
                    catch
                    {
                        return;
                    }

                    // ��Ŷ ó�� 

                    if (bytesReceived > 0)
                    {

                        // R �̶� ��ȣ�� ������ �ٽ� �����϶�¶� 
                        if (buf[0] == byte.Parse(Asc("R").ToString()))
                        {
                            _TicTacToe.RestartGame();
                            continue;
                        }

                        // ������ ��� row, column �̴�. 
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



        #region ��Ŷ�� ���� ������ �ޱ����� Util�� ���� ���� �Լ�

        public void SendPacketTCP(Byte[] data)
        {
            // TCP�� �̿��Ͽ� �����͸� �����Ѵ�. 
            try
            {
                // Ŭ���̾�Ʈ��� 
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
                // �������
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

        // �������� ���� ��ġ������ �����Ѵ�. 
        public void SendPosition(int row, int col)
        {
            byte[] buf = new byte[2];
            buf[0] = byte.Parse(Asc(row.ToString()).ToString());
            buf[1] = byte.Parse(Asc(col.ToString()).ToString());

            SendPacketTCP(buf);

        }

        // �ٽ� �����϶�� ��ȣ(R)�� �����Ѵ�. 
        public void SendsRestartPacket()
        {
            byte[] buf = new byte[2];
            buf[0] = byte.Parse(Asc("R").ToString());
            buf[1] = 0;

            SendPacketTCP(buf);
        }

        public void Disconnect()
        {
            // ������ ���� �մϴ�. 
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

        // ����Ʈ ������ ���� �����ϱ� ���� ��� 
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
