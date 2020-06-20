using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using catGirlBot.Attributes;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using OurIdolBot.Commands.PicturesCommand;

namespace catGirlBot
{
    public partial class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<configjson>(json);
            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true,
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,


            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<RequireRoleAttribute>();


            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {

            Task Ping(CommandContext ctx)
            {
                var channelId = 459510746083229697;

                ctx.Channel.SendMessageAsync("Ready").ConfigureAwait(false);
                return Task.CompletedTask;

            }

            return Task.CompletedTask;

        }


    }
}
