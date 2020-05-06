using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server.Model
{
    public class TcpServer
    {
        private TcpListener listenerServer = null;

        private readonly Int32 PORT;
        private readonly string IPADRESS;

        public TcpServer(Int32 port = 25565, string ip = "localhost")
        {
            PORT = port;
            IPADRESS = ip;
        }

        public void StartServer()
        {
            IPAddress serverIP = IPAddress.Parse(IPADRESS);
            listenerServer = new TcpListener(serverIP, PORT);

            listenerServer.Start();

        }
    }
}