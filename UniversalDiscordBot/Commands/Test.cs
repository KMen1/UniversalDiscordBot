using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalDiscordBot.Commands
{
    public class Test : ModuleBase
    {
        [Command("test")]
        public async Task test()
        {
            await ReplyAsync("test");
        }
    }
}
