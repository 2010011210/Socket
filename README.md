# Socket
Socket  
1.服务短要先启动，监听建立的链接  
``` 
public static void Process() 
        {
            // 1.确定ip地址和端口
            int port = 2024;
            string host = "127.0.0.1";
            IPEndPoint iPEnd = GetIPEndPoint(host, port);  

            // 2.
            Socket sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sSocket.Bind(iPEnd);
            sSocket.Listen(1000);  //该参数指定可以排队等待接受的传入连接数
            Console.WriteLine("已经连接，等待监听");

            // 收到消息，接收一个socket链接
            Socket serverSocket = sSocket.Accept();
            Console.WriteLine("链接已经确立");
            while (true) 
            {
                string recStr = "";
                byte[] recByte = new byte[4096];
                int bytes = serverSocket.Receive(recByte, recByte.Length, 0);
                recStr += Encoding.UTF8.GetString(recByte, 0, bytes);
                Console.WriteLine($"服务端接收到消息：{recStr}");
                if (recStr == "stop") 
                {
                    serverSocket.Close();
                    Console.WriteLine($"关闭链接。。。{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                    break;
                }

                // 回发消息
                Console.WriteLine("请输入回发消息。。。。。");
                var sendMessage = Console.ReadLine();
                byte[] sendByte = Encoding.UTF8.GetBytes(sendMessage);
                serverSocket.Send(sendByte, sendByte.Length, 0);
            }
            sSocket.Close();

        }

        public static IPEndPoint GetIPEndPoint(string host, int port) 
        {
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint iPEnd = new IPEndPoint(ip, port);  // 1.确定ip地址和端口
            return iPEnd;
        }
 ```
2.客户端启动，链接服务端   

```
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
```

