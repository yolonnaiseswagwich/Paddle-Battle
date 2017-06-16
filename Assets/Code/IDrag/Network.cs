using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;


public class StateObject
{
    // Client socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 256;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();
}
public class NetworkP2P
{
    // Thread signal.
    private static IPAddress ipAddress;
    private static int aPort = 25001;
    private static IPAddress OtheripAddress;
    private static ManualResetEvent allDone = new ManualResetEvent(false);
    private static ManualResetEvent connectDone = new ManualResetEvent(false);
    private static ManualResetEvent sendDone = new ManualResetEvent(false);
    private static ManualResetEvent receiveDone = new ManualResetEvent(false);
    private static Socket listener;
    private static Vector2 PosData;
    private static bool OtherConnect = false;
    private static bool Connected = false;
    public static bool CheckIP(string ip)
    {
        int k = 0;
        foreach (char i in ip)
        {
            if (i == '.')
            {
                ++k;
            }
        }
        if (k == 3)
        {
            return true;
        }
        return false;
    }
    public static byte[] GetIP(IPAddress ipAddress)
    {
        byte[] Data = new byte[4];
        int k = 0;
        int j = 0;
        int i = 0;
        string ip = ipAddress.ToString();
        for (; i < ip.Length; ++i)
        {
            if (ip[i] == '.')
            {
                Data[k++] = byte.Parse(ip.Substring(j, i - j));
                j = i + 1;
            }
        }
        Data[k++] = (byte)int.Parse(ip.Substring(j, i - j));
        return Data;
    }

    private static byte[] MakeData(Vector2 aPos)
    {
        String aString;
        aString = aPos.x.ToString() + "," + aPos.y.ToString() + "e";
        return Encoding.ASCII.GetBytes(aString);
    }
    private static void GeneratePos(String aString)
    {
        int k = 0;
        for (int i = 0; i < aString.Length; ++i)
        {
            if (aString[i] == ',')
            {
                PosData.x = 1.0f - float.Parse(aString.Substring(0, i - 1));
                k = i + 1;
                i = aString.Length;
            }
        }
        PosData.y = 1.0f - float.Parse(aString.Substring(k));
    }
    public static Vector2 GetPos()
    {
        return PosData;
    }
    public static bool Init()
    {
        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        bool ip = false;
        ipAddress = ipHostInfo.AddressList[0];
        foreach (IPAddress k in ipHostInfo.AddressList)
        {
            if (CheckIP(k.ToString()))
            {
                ipAddress = k;
                ip = true;
            }
        }
        if (!ip)
        {
            return false;
        }
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, aPort);

        // Create a TCP/IP socket.
        listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        listener.Bind(localEndPoint);
        listener.Listen(100);
        StartListening();
        
        byte[] IpAdds2 = new byte[4] { 192, 168, 2, 240 };
        OtheripAddress = new IPAddress(IpAdds2);
        //try to connect to other and send connect message
        IPEndPoint remoteEP = new IPEndPoint(OtheripAddress, aPort);

        // Create a TCP/IP socket.
        Socket client = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // Connect to the remote endpoint.
            client.BeginConnect(remoteEP,
                new AsyncCallback(ConnectCallback), client);
            if (!connectDone.WaitOne(5000))
            {
                return false;
            }
            byte[] byteData = Encoding.ASCII.GetBytes("h");//send hello message
                                                             // Send test data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
            sendDone.WaitOne();

            // Receive the response from the remote device.
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,//try to receve that the connection was good
                    new AsyncCallback(ReceiveCallback), state);
                if (!receiveDone.WaitOne(5000))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                return false;
            }

            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            connectDone.Reset();
            sendDone.Reset();
            receiveDone.Reset();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }
        return true;
    }
    public static void StartListening()
    {
        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
            listener.BeginAccept(
                new AsyncCallback(AcceptCallback),
                listener);

            // Wait until a connection is made before continuing.
            //allDone.WaitOne();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public static void AcceptCallback(IAsyncResult ar)
    {
        // Signal the main thread to continue.
        allDone.Set();
        // Get the socket that handles the client request.
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
        // Create the state object.
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
        allDone.Reset();
    }
    public static void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;
        try
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf("e") > -1)//THIS IS THE END OF MESSAGE THING SOOO I CAN CHANGE THIS TO BE SOMETHING SMALLER
                {
                    GeneratePos(content.Substring(0, content.Length - 1));
                    byte[] byteData = Encoding.ASCII.GetBytes("k");
                    handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendReadCallback), handler);
                }
                else if (content.IndexOf("p") > -1)//THIS IS THE END OF MESSAGE THING SOOO I CAN CHANGE THIS TO BE SOMETHING SMALLER
                {
                    byte[] byteData = Encoding.ASCII.GetBytes("p");
                    handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendReadCallback), handler);
                }
                else if (content.IndexOf("h") > -1)//THIS IS THE END OF MESSAGE THING SOOO I CAN CHANGE THIS TO BE SOMETHING SMALLER
                {
                    OtherConnect = true;
                    byte[] byteData = Encoding.ASCII.GetBytes("o");
                    handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendReadCallback), handler);
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public static bool SendPos(Vector2 posData)
    {
        IPEndPoint remoteEP = new IPEndPoint(OtheripAddress, aPort);

        // Create a TCP/IP socket.
        Socket client = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // Connect to the remote endpoint.
            client.BeginConnect(remoteEP,
                new AsyncCallback(ConnectCallback), client);
            connectDone.WaitOne();
            byte[] byteData = MakeData(posData);
            // Send test data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
            sendDone.WaitOne();

            // Receive the response from the remote device.
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
                receiveDone.WaitOne();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                return false;
            }
            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            connectDone.Reset();
            sendDone.Reset();
            receiveDone.Reset();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }
        return true;
    }
    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;
            // Complete the connection.
            client.EndConnect(ar);
            Connected = true;
            // Signal that the connection has been made.
            connectDone.Set();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            connectDone.Set();
        }
    }
    private static void SendReadCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            handler.EndSend(ar);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            client.EndSend(ar);

            // Signal that all bytes have been sent.
            sendDone.Set();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            String content = String.Empty;
            // Retrieve the state object and the client socket 
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;

            // Read data from the remote device.
            int bytesRead = client.EndReceive(ar);
            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf("o") > -1)//THIS IS THE END OF MESSAGE THING SOOO I CAN CHANGE THIS TO BE SOMETHING SMALLER
                {
                    Connected = true;
                }
                else if(content.IndexOf("k") > -1)
                {
                    Debug.Log("Pos Sent Successful");
                }
                else
                {
                    // Not all data received. Get more.
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
            receiveDone.Set();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public static bool Ping()
    {
        IPEndPoint remoteEP = new IPEndPoint(OtheripAddress, aPort);

        // Create a TCP/IP socket.
        Socket client = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        try
        {

            // Connect to the remote endpoint.
            client.BeginConnect(remoteEP,
                new AsyncCallback(ConnectCallback), client);
            if (!connectDone.WaitOne(5000))
            {
                return false;
            }
            byte[] byteData = Encoding.ASCII.GetBytes("p");
            // Send test data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
            sendDone.WaitOne();
            // Receive the response from the remote device.
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
                if (!receiveDone.WaitOne(5000))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                return false;
            }
            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            connectDone.Reset();
            sendDone.Reset();
            receiveDone.Reset();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        return true;
    }
    public static bool IsReady()
    {
        return (Connected && OtherConnect);
    }
    private static float aTimer = 0;
    public static bool Update(ref Player aPlayer, ref Multi aMulti)
    {
        aTimer += Time.deltaTime;
        StartListening();
        if (!SendPos(IDrag.D2Camera.GetPosToPixel(aPlayer.GetPos())))
        {
            return false;
        }
        aMulti.SetPos(IDrag.D2Camera.GetPixelToPos(GetPos()));
        //if (aTimer > 1.0f)
        //{
        //    if (!Ping())
        //    {
        //        return false;
        //    }
        //}
        return true;
    }
    public static void Shudown()
    {
        listener.Close();
        allDone.Reset();
        connectDone.Reset();
        sendDone.Reset();
        receiveDone.Reset();
    }
}

