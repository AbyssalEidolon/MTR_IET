using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    internal class TestClient
    {
        internal Boolean socketReady = false;
        TcpClient? socket;
        NetworkStream? ns;
        StreamWriter? writer;
        StreamReader? reader;
        public String Host;
        private readonly Int32 Port;
        public static TestClient i;
        public Vector3[] Vectors {
            get {
                return vector3s;
            }
        }
        Vector3[] vector3s = new Vector3[5]{
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0)
        };
        Vector4 Rotation = new();
        public TestClient(string host = "127.0.0.1", Int32 port = 12345)
        {
            this.Host = host;
            this.Port = port;
            i = this;
            SetupSocket();
            Thread updateThread = new(new ThreadStart(Update));
            updateThread.Start();
            ReadInput();
        }
        static void Update()
        {
            TestClient client = i;
            while (true)
            {
                byte[] message = Encoder.JointPosBytes(i.Vectors, i.Rotation);
                client.WriteMessage(message);
                //i.vector3s[0][0]++;
                Thread.Sleep(1000);
            }
        }
        void ReadInput()
        {
            Console.WriteLine("Enter exit to close application. Enter number to change vector values");
            foreach (Vector3 vec in vector3s)
            {
                Console.WriteLine(vec.ToString("F4"));
            }
            bool Exit = false;
            while (!Exit)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey().Key)
                    {
                        default:
                            continue;
                        case ConsoleKey.Escape:
                            Exit = true;
                            break;
                        case ConsoleKey.D0:
                            ChangeValue(0);
                            break;
                        case ConsoleKey.D1:
                            ChangeValue(1);
                            break;
                        case ConsoleKey.D2:
                            ChangeValue(2);
                            break;
                        case ConsoleKey.D3:
                            ChangeValue(3);
                            break;
                        case ConsoleKey.D4:
                            ChangeValue(4);
                            break;
                    }
                    foreach (Vector3 vec in vector3s)
                    {
                        Console.WriteLine(vec.ToString("F4"));
                    }
                }
            }
        }
        void ChangeValue(int vecIndex)
        {
            Console.WriteLine("Currently Modifying Vector: " + vecIndex + " , Pick Target Position Between 0-2 Or Press Esc To Exit.");
            bool Finished = false;
            while(!Finished)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D0:
                            ChangeInsideValue(vecIndex, 0);
                            break;
                        case ConsoleKey.D1:
                            ChangeInsideValue(vecIndex, 1);
                            break;
                        case ConsoleKey.D2:
                            ChangeInsideValue(vecIndex, 2);
                            break;
                        case ConsoleKey.Escape:
                            Console.WriteLine("Returning to vector selection.");
                            Finished = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Key, Try Again.");
                            break;
                    }
                    foreach (Vector3 vec in vector3s)
                    {
                        Console.WriteLine(vec.ToString("F4"));
                    }
                }
            }
        }
        void ChangeInsideValue(int vecIndex, int floatIndex)
        {
            Console.WriteLine("Now modifying entry: " + floatIndex + "in" + vector3s[vecIndex].ToString("F4"));
            try
            {
                string? temp = Console.ReadLine();
                if (temp != null)
                {
                    float tempFloat = float.Parse(temp);
                    vector3s[vecIndex][floatIndex] = tempFloat;
                    Console.WriteLine($"Entry [{vecIndex}][{floatIndex}] changed to: {tempFloat}");
                }
            }
            catch (FormatException e) { Console.WriteLine("Invalid input, please try again.");} 
        }
        private void SetupSocket()
        {
            try
            {
                Console.WriteLine("Now trying to connect to: " + this.Host + ":" + this.Port);
                socket = new TcpClient(Host, Port);
                ns = socket.GetStream();
                writer = new StreamWriter(ns);
                reader = new StreamReader(ns);
                socketReady = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Socket error: " + e);
            }
        }

        public void CloseSocket()
        {
            if (!socketReady)
                return;
            writer.Close();
            reader.Close();
            socket.Close();
            socketReady = false;
        }
        public void WriteMessage(byte[] message)
        {
            if (socketReady == true) ns.Write(message);
        }
    }
    public class Encoder
    {
        public static byte[] JointPosBytes(Vector3[] vectors, Vector4 Rotation)
        {
            if (vectors.Length != 5) return null;
            List<byte> temp = new();
            for (int i = 0; i < vectors.Length; i++)
            {
                temp.AddRange(Encoding.UTF8.GetBytes((vectors[i].ToString("F4") + ";").Replace("<", "").Replace(">", "")));
            }
            temp.AddRange(Encoding.UTF8.GetBytes((Rotation.ToString("F4") + ";").Replace("<", "").Replace(">", "")));
            return temp.ToArray();
        }
    }
}
