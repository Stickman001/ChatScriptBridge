using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.IO;

namespace ChatScriptBridge
{
    class Program
    {
        private DiscordSocketClient _client;
        static void Main(string[] args)
        {
            string token = GetToken();
            new Program().MainAsync(token).GetAwaiter().GetResult();
        }
            

        public async Task MainAsync(string token)
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += MessageRecieved;
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            //Block this task untill the program is closed
            await Task.Delay(-1);
        }

        private async Task MessageRecieved(SocketMessage msg)
        {
            if (msg.Content.Equals("!ping"))
            {
                await msg.Channel.SendMessageAsync("Pong! Hellow World!");
            }
        }

        /// <summary>
        /// Handels Discord logging messages.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Retreives the secret from the appropriate file.
        /// </summary>
        /// <returns></returns>
        private static string GetToken()
        {
            string token = File.ReadAllText(@"C:\Secrets\DiscordBotToken.txt");
            Console.WriteLine("Token: {0}", token);
            return token;
        }
    }
}