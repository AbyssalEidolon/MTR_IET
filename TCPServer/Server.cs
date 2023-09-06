using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;
namespace TCPServer
{
    public delegate void OnCMDRx<T>(T cmd);
    internal class Server
    {
        public static bool ConsoleFree = true;
        TcpListener server;
        public IPAddress serverIP = IPAddress.Any;
        public static Server? i;
        int port = 12345;
        TcpConnectedClient client;
        public static JointData data;
        string[] Figners =
        {
            "Thumb", "Pointer", "Mi9dddle", "Ring", "Pinky"
        };
        string Palm = "Palm";
        Vector4 Rotation;
        public Server() {
            i = this;
            server = new(serverIP, port);
            server.Start();
            Console.WriteLine("Now accepting clients on " + serverIP.ToString() + ":" + port);
            server.BeginAcceptTcpClient(ConnectedClient, null);
            ReadInput();
        }
        void updateData(JointData jointData) { data = jointData; }
        void ConnectedClient(IAsyncResult ar)
        {
            TcpClient tcpClient = server.EndAcceptTcpClient(ar);
            client = new TcpConnectedClient(tcpClient);
            server.BeginAcceptTcpClient(ConnectedClient, null);
            //new TcpConnectedClient(tcpClient).writeSocket(CMDBase.Encode(new CMD(Convert.ToByte(clientList.Count), 0, (byte)CMDList.Connect, Args)));
            Console.WriteLine("New Connection online.");
        }
        internal void Disconnect(TcpConnectedClient client)
        {
            client.Close();
        }
        public void CloseServer()
        {
            server.Stop();
        }
        public void ReadInput()
        {
            Console.WriteLine("Press escape to close application/ s to capture values.");
            bool Exit = false;
            while(!Exit){
                if(Console.KeyAvailable)
                switch(Console.ReadKey().Key)
                {
                    default:
                        continue;
                    case ConsoleKey.Escape:
                        Exit = true;
                        break;
                    case ConsoleKey.S:
                        Snapshot();
                        continue;
                }
                else Thread.Sleep(1000);
            }
        }
        void Snapshot()
        {
            ConsoleFree = false;
            Console.WriteLine("Insert file name.");
            string? FileName = Console.ReadLine();
            if(FileName == null){
                Console.WriteLine("Empty Filename. Returning.");
            }else{
                Dictionary<string, object> FuckYou = new();
                for (int i = 0; i < data.Fingers.Length; i++){
                    Console.WriteLine(data.Fingers[i].ToString("F4"));
                    FuckYou.Add(Figners[i], data.Fingers[i]);
                };
                FuckYou.Add(Palm, Rotation);
                string jsonString = JsonConvert.SerializeObject(FuckYou, Formatting.Indented);
            File.WriteAllText($"saved\\{FileName}.json", jsonString);
            ConsoleFree = true;
            }
        }
    }
    internal class TcpConnectedClient
    {
        public static event OnCMDRx<JointData>? OnUpdate;
        readonly TcpClient connection = null;
        readonly byte[] readBuffer = new byte[1024];
        NetworkStream stream => connection.GetStream();
        public TcpConnectedClient(TcpClient client)
        {
            this.connection = client;
            this.connection.NoDelay = true;
            
            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
        }
        internal void Close() => connection.Close();
        bool isConnected() => connection == null ? false : connection.Connected;
        void OnRead(IAsyncResult ar)
        {
            if (!isConnected()) return;
            int len = stream.EndRead(ar);
            if (len <= 0) { Server.i.Disconnect(this); return; }
            JointData data = new(readBuffer);
            OnUpdate?.Invoke(data);
            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
        }
    }
    public class JointData
    {
        public Vector3[] Fingers = new Vector3[5];
        public Vector4 Rotation;
        public JointData(byte[] bytes)
        {
            string raw = Encoding.UTF8.GetString(bytes);
            string[] tokens = raw.Split(";");
            for(int i = 0; i < Fingers.Length; i++)
            {
                string[] splitToken = tokens[i].Split(",");
                for(int ii = 0; ii < 3; ii++)
                {
                    Fingers[i][ii] = float.Parse(splitToken[ii]);
                }
            }
            string[] split = tokens[5].Split(",");
            for(int i = 0; i < 4; i++)
            {
                Rotation[i] = float.Parse(split[i]);
            }
            Server.data = this;
            if(Server.ConsoleFree){
            Console.Clear();
            Console.WriteLine(raw);
            Console.WriteLine("Enter exit to close application. /Snapshot to capture values.");
        }}
        public JointData(){}
    }
}
