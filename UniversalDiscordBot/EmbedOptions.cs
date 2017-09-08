using Discord;
using Discord.WebSocket;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalDiscordBot
{
    public partial class EmbedOptions : MaterialForm
    {
        DiscordSocketClient _client;
        ColorDialog pickcolor;
        public EmbedOptions(DiscordSocketClient iclient)
        {
            _client = iclient;
            pickcolor = new ColorDialog();
            InitializeComponent();
            foreach (SocketGuild guild in iclient.Guilds)
            {
                comboBox1.Items.Add(guild.Name);
            }
        }

        private void EmbedOptions_Load(object sender, EventArgs e)
        {
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            EmbedBuilder eb = new EmbedBuilder
            {
                Title = textBox1.Text,
                Description = textBox2.Text,
                //ThumbnailUrl = ,
                Color = Discord.Color.Green
            };
            
            sendMessage($"", eb);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            pickcolor.ShowDialog();
        }

        public void sendMessage(string msg, EmbedBuilder embed = null)
        {
            foreach (SocketGuild guild in _client.Guilds)
            {
                if (guild.Name == comboBox1.Text)
                {
                    foreach (SocketTextChannel textchannel in guild.TextChannels)
                    {
                        if (textchannel.Name == comboBox2.Text)
                        {
                            textchannel.SendMessageAsync(msg, false, embed, null);
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            foreach (SocketGuild guild in _client.Guilds)
            {
                if (guild.Name == comboBox1.Text)
                {
                    foreach (SocketGuildChannel textChannel in guild.TextChannels)
                    {
                        comboBox2.Items.Add(textChannel.Name);
                    }
                }
            }

        }
    }
}
