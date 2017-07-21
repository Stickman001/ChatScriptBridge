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
        private ChatScriptClient _cs;
        static void Main(string[] args)
        {
            string token = GetToken();
            new Program().MainAsync(token).GetAwaiter().GetResult();
        }
            

        public async Task MainAsync(string token)
        {
            _cs = new ChatScriptClient("GodBot", "localhost");
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += MessageRecieved;
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            //Block this task untill the program is closed
            await SimpleConsoleQuit();
            await _client.LogoutAsync();
        }

        /// <summary>
        /// Handel Discord msg recieved.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private async Task MessageRecieved(SocketMessage msg)
        {
            Console.WriteLine(msg.Content);
            //Limits the bot to operating only in the channel mentioned.
            //if (msg.Channel.Name.Equals("bot-shennanigans"))
            if (msg.Content == "!ping")
            {
                //await msg.Channel.SendMessageAsync(_cs.GetBotResponse(msg.Content, msg.Author.Username));
                await msg.Channel.SendMessageAsync("!pong");
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
        /// Simple exit for App, waits for the q key to be pressed.
        /// </summary>
        /// <returns></returns>
        private Task SimpleConsoleQuit()
        {
            while(true)
            {
                if(Console.ReadLine() == "q")
                    return Task.CompletedTask;
            }
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