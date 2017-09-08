using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversalDiscordBot;

namespace UniversalDBot
{
    public partial class Main : MaterialForm
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IServiceProvider services;
        private string token;
        private string game;

        public Main()
        {
            var mf = MaterialSkinManager.Instance;
            InitializeComponent();
            mf.AddFormToManage(this);
            //mf.Theme = MaterialSkinManager.Themes.DARK;
            mf.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private async void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (UniversalDiscordBot.Properties.Settings.Default.isConnected == false)
            {


                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Please enter a token!", "UDB");
                }
                else
                {
                    token = textBox1.Text;
                    commands = new CommandService(new CommandServiceConfig
                    {
                        LogLevel = LogSeverity.Debug,
                        CaseSensitiveCommands = false,
                        DefaultRunMode = RunMode.Async,
                    });
                    commands.Log += Logger;
                    client = new DiscordSocketClient(new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Debug
                    });

                    services = new ServiceCollection()
                    .BuildServiceProvider();

                    await InstallCommands();

                    client.Log += Logger;
                    await client.LoginAsync(TokenType.Bot, _token);
                    await client.StartAsync();
                    UniversalDiscordBot.Properties.Settings.Default.isConnected = true;
                    MessageBox.Show("Connected!");
                }
            }
            else
            {
                MessageBox.Show("Bot is already connected", "UDB");
            }

        }

        private async Task InstallCommands()
        {
            client.MessageReceived += HandleCommand;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommand(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;
            var context = new CommandContext(client, message);
            var result = await commands.ExecuteAsync(context, argPos, services);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);

            Invoke((Action)delegate
            {
                richTextBox2.AppendText($"[{DateTime.UtcNow}] [{messageParam.Channel}]  {messageParam.Author} -> {messageParam.Content}");
            });
        }

        private Task Logger(LogMessage lmsg)
        {
            Invoke((Action)delegate
            {
                richTextBox1.AppendText($"{DateTime.Now} [{lmsg.Severity,8}] {lmsg.Source}: {lmsg.Message}" + "\n");
            });
            
            return Task.CompletedTask;
        }

        private async void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            if (UniversalDiscordBot.Properties.Settings.Default.isConnected == true)
            {

                if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Please enter a game", "UDB");
                }
                else
                {
                    game = textBox2.Text;
                    await client.SetGameAsync(game, "https://twitch.tv", StreamType.Twitch);
                    MessageBox.Show($"Game set to {game}", "UDB");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (UniversalDiscordBot.Properties.Settings.Default.isConnected == true)
            {
                GACTools channelTools = new GACTools(client);
                channelTools.Show();
            }
        }
    }
}
