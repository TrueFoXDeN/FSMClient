using System;
using System.Net;
using System.Net.Sockets;

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
            Console.ReadKey();
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
                    _clientSocket.Connect(new IPEndPoint(ipAddress, 0));

                }
                catch (SocketException e)
                {
                    Console.Clear();
                    Console.WriteLine(e);
                    Console.WriteLine("Attempt: " + attempts.ToString());

                }
            }

            Console.Clear();
            Console.WriteLine("Connected");




        }
    }
}
