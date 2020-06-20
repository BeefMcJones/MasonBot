using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using catGirlBot.Attributes;
using catGirlBot.Helpers;
using System.Net;
using System.Threading.Tasks;
using catGirlBot.Const.PicturesConst;
using DSharpPlus.CommandsNext.Attributes;
using catGirlBot.containers;

namespace OurIdolBot.Commands.PicturesCommand
{



    [CommandsGroup("Pictures")]
    public class RequireRoleAttribute : BaseCommandModule
    {
        private const string footerText = "Powered by Mason";

        [Command("catgirl")]
        public async Task CatGirl(CommandContext ctx, [Description("Mention")] DiscordMember member = null)
        {
            await ctx.TriggerTypingAsync();
            await SendImage(ctx, NekosLifePicturesEndpoints.Neko, "My Man", member);
        }

        [Command("catboy")]
        public async Task CatBoy(CommandContext ctx, [Description("Mention")] DiscordMember member = null)
        {
            await ctx.TriggerTypingAsync();
            await SendImage(ctx, NekosLifePicturesEndpoints.Neko, "I swear its a boy", member);
        }








        public async Task SendImage(CommandContext ctx, string endpoint, string title, [Description("Mention")] DiscordMember member = null)
        {
            await ctx.TriggerTypingAsync();

            var client = new WebClient();
            var url = client.DownloadString(endpoint);
            var pictureContainer = JsonConvert.DeserializeObject<NekosFileImage>(url);

            await PostEmbedHelper.PostEmbed(ctx, title, member?.Mention, pictureContainer.Url, footerText);
        }

        public string GetExtension(string url)
        {
            var array = url.Split('.');
            return array[array.Length - 1];
        }

       
    }
}