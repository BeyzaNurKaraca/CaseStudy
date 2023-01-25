using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SanlabCase.Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any,1881);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client bağlandı.");
                NetworkStream stream = client.GetStream();
                StreamReader sr=new StreamReader(client.GetStream());
                StreamWriter sw=new StreamWriter(client.GetStream());

                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b!=0)
                        {
                            recv++;
                        }
                    }
                    string request=Encoding.UTF8.GetString(buffer,0,recv);
                    Console.WriteLine("İstek alındı: "+request);
                    sw.WriteLine($"Client mesajı: {request} {DateTime.Now}");
                    sw.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Hata oluştu.");
                    sw.WriteLine(e.ToString());
                }

            }

        }
    }
}
