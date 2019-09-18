using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace game_p
{
    public class AsynchronousSocketListener
    {
        public class StateObject
        {
            // Client  socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 1024;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();

            //  public  string player = "sarver";

        }

        private static AsynchronousSocketListener _singleInstance = new AsynchronousSocketListener();

        public static AsynchronousSocketListener GetInstance()
        {
            return _singleInstance;
        }

        // Thread signal.  
        public ManualResetEvent allDone = new ManualResetEvent(false);
        public List<StateObject> list;
        public List<String> sclist;
        public List<int> ralist;

        public int GameMode = 1;

        // 野登追加
        // 接続待ちが有効かどうかを表すフラグ
        public bool IsEnabled = false;
        public bool IsRunning = false;
        public Thread serverThread;

        //  public static List<string> lanlist;
        //  public static List<int> lalist;

        private AsynchronousSocketListener()
        {
            list = new List<StateObject>();
            sclist = new List<string>() { "0", "0", "0" }; //スコア処理に使用
            ralist = new List<int>() { 0, 0, 0, 0, 0 }; //乱数処理に使用
        }

        /// <summary>
        ///サーバーの受信待ちスレッドを開始します
        /// </summary>
        public void StartListeningThread()
        {
            IsEnabled = true;

            serverThread = new Thread(new ThreadStart(StartListening));
            serverThread.Start();
        }

        /// <summary>
        /// サーバーの受信待ちスレッドを終了します
        /// </summary>
        public async Task StopListeningThread()
        {
            // StartListening()の無限ループを抜けるためにfalseに変更
            IsEnabled = false;
            // 接続待ちのウェイトを終了させる
            allDone.Set();

            // スレッドが終わるまで少し待つ
            await Task.Run(() =>
            {
                do
                {
                    Thread.Sleep(100);
                } while (IsRunning);
            });
        }

        public void StartListening()
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                IsRunning = true;
                listener.Bind(localEndPoint);
                listener.Listen(100);

                // 野登修正
                // 受信待ちが有効な間のみループさせる
                // while (true)
                while (IsEnabled)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    //  Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }
                // 野登追加
                // スレッド終了時にリストの中身をクリアする
                foreach (var state in list)
                {
                    state.workSocket.Close();
                }
                list.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            IsRunning = false;
            listener.Close();

            // 野登修正
            // consoleプログラムではないため下記2行は不要
            /*
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
            */

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                allDone.Set();

                // Get the socket that handles the client request.  
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                // Create the state object.  
                StateObject state = new StateObject();

                //  if (sclist.Contains(state.player) == false)
                //  {
                //      sclist.Add(state.player);
                list.Add(state);
                //  }
                Console.WriteLine("Connect");
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // Signal the main thread to continue.  

        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            String Word = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            try
            {
                int bytesRead = handler.EndReceive(ar);
                Console.WriteLine("Server Read {0} bytes from client.", bytesRead);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read   
                    // more data.  
                    content = state.sb.ToString();
                    state.sb.Clear();
                    if (content.IndexOf("<EOF>") > -1)
                    {
                        //受信したバイト数と内容
                        // All the data has been read from the   
                        // client. Display it on the console.  

                        string hostname = Dns.GetHostName();
                        IPAddress[] adrList = Dns.GetHostAddresses(hostname);
                        foreach (IPAddress address in adrList)
                        {
                            //  Console.WriteLine(address.ToString());
                        }

                        //  Console.WriteLine("Data :{0}", content);

                        //  Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        //  content.Length, content);

                        int len = content.Length;
                        string score = string.Empty;

                        //送信
                        // Echo the data back to the client.

                        if (content == "CPU<EOF>") //ここで一人用か二人用を判断(二人用の場合の分岐)
                        {
                            GameMode = 1;
                        }

                        if (content == "VS<EOF>") //ここで一人用か二人用を判断(二人用の場合の分岐)
                        {
                            GameMode = 2;
                            if (list.Count == 2)
                            {
                                int i = 1;
                                foreach (var st in list)
                                {
                                    Send(st.workSocket, "OK." + i.ToString());
                                    i++;
                                }

                            }
                        }

                        if (content.IndexOf("<score>") > -1) //score送信の部分
                        {
                            if (content.IndexOf("<p1>") > -1)
                            {
                                score = (content.Replace("<p1>", ""));
                                sclist[0] = score;
                            }
                            else if (content.IndexOf("<p2>") > -1)
                            {
                                score = (content.Replace("<p2>", ""));
                                sclist[1] = score;
                            }
                            foreach (var st in list)
                            {
                                Send(st.workSocket, sclist[0] + ',' + sclist[1]);
                            }
                        }

                        if (content.IndexOf("<rand>") > -1) //乱数送信の部分
                        {
                            if (ralist[4] <= 0)
                            {
                                ralist[4] = GameMode;
                                Rand();
                            }
                            Send(handler, ralist[0].ToString() + "<rand><EOF>");
                            ralist[4] = ralist[4] - 1;
                        }

                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReadCallback), state);
                    }
                }
            }

            catch
            {
                list.Remove(state);
            }
        }

        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            try
            {
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
            }
            catch
            {

            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                //何バイトの情報を送ったか
                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                //  Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                Console.WriteLine("Server sent {0} bytes from client.", bytesSent);

                //  handler.Shutdown(SocketShutdown.Both);
                //  handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public string Rand()
        {

            int seed = Environment.TickCount;
            Random rnd = new System.Random(); // インスタンスを生成
            int r;
            do
            {
                r = rnd.Next(1, 25); //1～6の乱数を生成
            } while (ralist[0] == r || ralist[1] == r | ralist[2] == r || ralist[3] == r); //前回と重複したら再抽選 

            ralist[3] = ralist[2];
            ralist[2] = ralist[1];
            ralist[1] = ralist[0];
            ralist[0] = r;

            return r.ToString();
        }
    }
}
