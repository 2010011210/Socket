using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("启动socket客户端!");
            try 
            {
                // 1.确定ip地址和端口
                int port = 2024;
                string host = "127.0.0.1";
                IPEndPoint iPEnd = GetIPEndPoint(host, port);  // 1.确定ip地址和端口
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(iPEnd);
                while (true)
                {
                    Console.WriteLine("请输入要发送到服务端的内容：");
                    string sendStr = Console.ReadLine();
                    sendStr = $"Client:{sendStr}";
                    if ("exist".Equals(sendStr))
                    {
                        Console.WriteLine("客户端退出");
                        break;
                    }
                    byte[] sendBytes = Encoding.UTF8.GetBytes(sendStr);
                    clientSocket.Send(sendBytes);

                    string recStr = "";
                    byte[] recBytes = new byte[4096];
                    int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                    recStr = Encoding.UTF8.GetString(recBytes, 0, bytes);

                    Console.WriteLine($"服务器返回：{recStr}");
                }

                clientSocket.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            


        }

        public static IPEndPoint GetIPEndPoint(string host, int port)
        {
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint iPEnd = new IPEndPoint(ip, port);  // 1.确定ip地址和端口
            return iPEnd;
        }
    }
}