using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UdpSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("press any key to start");
            Console.ReadKey();
            ThreadPool.QueueUserWorkItem(_ =>
                {
                    UdpClient client = new UdpClient();
                    IPEndPoint destination = new IPEndPoint(new IPAddress(new byte[] {127, 0, 0, 1}), 9999);
                    while (true)
                    {
                        var payload = Guid.NewGuid().ToByteArray();
                        Console.WriteLine(payload[0] + "" + payload[1] + "" + payload[3] + "...");
                        client.Send(payload, payload.Length, destination);
                        Thread.Sleep(10);
                    }
                });
            Console.WriteLine("press any key to quit");
            Console.ReadKey();
        }
    }
}
