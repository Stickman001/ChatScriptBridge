using System;
using System.Collections.Generic;
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


            //Connect to chatscript

            //Send message

            //returne response

            return "Echo:  " + ToBot;
        }
    }
}
