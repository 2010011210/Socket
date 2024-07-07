namespace SocketServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            try 
            {
                Console.WriteLine("Socket服务端启动");
                SimpleSocketServer.Process();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}