using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FsmClient
{
    class Program
    {

        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static string ipAddressString;
        static void Main(string[] args)
        {
            ipAddressString = Console.ReadLine();
            LoopConnect();
            SendLoop();
            Console.ReadKey();
        }

        private static void SendLoop()
        {
            while (true)
            {
                Console.Write("Enter a request: ");
                string req = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(req);
                _clientSocket.Send(buffer);

                byte[] recBuffer = new byte[1024];
               int rec = _clientSocket.Receive(recBuffer);
                byte[] data = new byte[rec];
                Array.Copy(recBuffer, data, rec);
                Console.WriteLine("Received: " + Encoding.ASCII.GetString(data));

            }

        }


        private static void LoopConnect()
        {
            IPAddress ipAddress = IPAddress.Parse(ipAddressString);
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect(new IPEndPoint(ipAddress, 13000));

                }
                catch (SocketException e)
                {
                   // Console.Clear();
                    Console.WriteLine(e);
                    Console.WriteLine("Attempt: " + attempts.ToString());
                }
            }

            Console.Clear();
            Console.WriteLine("Connected");




        }
    }
}
