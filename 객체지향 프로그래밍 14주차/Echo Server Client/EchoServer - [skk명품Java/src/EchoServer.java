import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;


public class EchoServer {
	
	public static void main(String[] args ) throws IOException {
		ServerSocket serverSock = new ServerSocket( 9999 ); 
		System.out.println(serverSock + ": 辑滚 家南 积己");
		try {
			while(true) {
				Socket client = serverSock.accept();
				MultiThreadEchoServer myServer = new MultiThreadEchoServer(client); 
				myServer.start();
			}
		}
		finally {
			serverSock.close();
		}
	}
}

class MultiThreadEchoServer extends Thread {
	protected Socket sock;
	
	//-------------Constructor
	MultiThreadEchoServer (Socket sock) { 
		this.sock = sock;
	}
	 
	public void run() {
		try {
			System.out.println(sock+":");
			InputStream fromClient = sock.getInputStream();
			OutputStream toClient = sock.getOutputStream();
			byte[] buf = new byte[1024]; 
			int count; 
			
			while( (count = fromClient.read(buf)) != -1){
				toClient.write(buf, 0, count); 
				System.out.write(buf, 0, count);
			}
			
			toClient.close();
			System.out.println(sock+":");
		}
		catch(IOException ex) {
			System.out.println(sock+": (" + ex + ")");
		}
			
		finally {
			try {
				if (sock != null) sock.close();
			}
			catch(IOException ex) {}
		}
	}
}

