using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UniversalDiscordBot.Commands
{
    public class Info : ModuleBase
    {
        [Command("info")]
        public async Task BAOInfo()
        {
            var eb = new EmbedBuilder();
            var application = await Context.Client.GetApplicationInfoAsync();
            eb.Color = Color.Orange;
            eb.WithDescription(
                           $"{Format.Bold("Info")}\n" +
                           $"- Author: {application.Owner.Username} (ID {application.Owner.Id})\n" +
                           $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                           $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                           $"- OS Version: {RuntimeInformation.OSDescription} {RuntimeInformation.OSArchitecture}\n" +
                           $"- Process Architecture: {RuntimeInformation.ProcessArchitecture}\n" +
                           $"- Uptime: {GetUptime()}\n\n" +

                           $"{Format.Bold("Stats")}\n" +
                           $"- Heap Size: {GetHeapSize()} MB\n" +
                           $"- Guilds: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                           $"- Channels: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}" +
                           $"- Users: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}"
                       );
            await Context.Channel.SendMessageAsync($"{Format.Bold("Bot Information")}", false, eb);

        }
        private static string GetUptime()
           => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
    }
  
}

