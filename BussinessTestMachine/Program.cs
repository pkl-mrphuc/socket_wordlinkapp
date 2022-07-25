using System;
using WebSocketSharp;
using Newtonsoft.Json;
using WordLinkApp.SocketModel;

namespace BussinessTestMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of a ws client
            WebSocket socket = new WebSocket("ws://127.0.0.1:1011/Echo?username=lkphuc");
            socket.OnMessage += Ws_OnMessage;
            socket.Connect();

            Console.ReadKey();

        }


        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
        }
    }
}
