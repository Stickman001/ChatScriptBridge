using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ChatScriptBridge
{
    class ChatScriptClient
    {
        public string Bot { get; private set; }
        public string ServerIP { get; private set; }
        private Func<string> MessageRecievedCallback;

        public ChatScriptClient(string Bot, string ServerIP)
        {
            this.Bot = Bot;
            this.ServerIP = ServerIP;
        }

        public string GetBotResponse(string ToBot, string Username = "Default")
        {
            //Format message
            string imMsg = FormatMessage(Username, ToBot);

            //Connect to chatscript
            try
            {
                byte[] response = new byte[1024];
                using(TcpClient clt = new TcpClient())
                {
                    clt.ConnectAsync("localhost", 1024).Wait();
                    NetworkStream stream = clt.GetStream();

                    byte[] message = Encoding.ASCII.GetBytes(ToBot);
                    stream.Write(message, 0, message.Length);

                    int rsp = stream.Read(response, 0, response.Length);
                    clt.Dispose();

                    return Encoding.ASCII.GetString(response, 0, rsp);
                }
            }
            catch (Exception e)
            {
                return "I'm borken!";
            }
        }

        private string FormatMessage(string username, string message)
        {
            return String.Format("{1}{0}{2}{0}{3}{0}", "\0", username, Bot, message);
        }
    }
}
