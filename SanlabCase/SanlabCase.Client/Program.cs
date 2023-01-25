using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SanlabCase.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client=new TcpClient("127.0.0.1",1881);
                //string messageToSend = "Hello";
                Console.WriteLine("Mesajınızı giriniz: ");
                string messageToSend = Console.ReadLine();
              
                int byteCount=Encoding.ASCII.GetByteCount(messageToSend+1);
                byte[] sendData=Encoding.ASCII.GetBytes(messageToSend);

                NetworkStream stream=client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("Sunucuya veri gönderiliyor...");

                StreamReader sr=new StreamReader(stream);
                string response = sr.ReadLine();
                Console.WriteLine(response);

                stream.Close();
                client.Close();
                Console.ReadKey();


            }
            catch (Exception e)
            {

                Console.WriteLine("Bağlantı hatası.");
            }
        }
    }
}
