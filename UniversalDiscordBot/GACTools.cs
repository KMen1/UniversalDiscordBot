using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace UniversalDiscordBot
{
    public partial class GACTools : MaterialForm
    {
        DiscordSocketClient _client;
        public GACTools(DiscordSocketClient iclient)
        {
            _client = iclient;
            InitializeComponent();
            if (Properties.Settings.Default.isConnected == true)
            {
                foreach (SocketGuild guild in iclient.Guilds)
                {
                    comboBox1.Items.Add(guild.Name);
                }
            }
            else
            {
                MessageBox.Show("Please connect first", "UDB");
            }
        }

        private void GACTools_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            foreach (SocketGuild guild in _client.Guilds)
            {
                if (guild.Name == comboBox1.Text)
                {
                    foreach (SocketGuildChannel textChannel in guild.TextChannels)
                    {
                        comboBox2.Items.Add(textChannel.Name);
                    }

                    foreach (SocketGuildChannel voiceChannel in guild.VoiceChannels)
                    {
                        comboBox3.Items.Add(voiceChannel.Name);
                    }
                }
            }

        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
           sendMessage(textBox1.Text, null);
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
                            textchannel.SendMessageAsync(msg, false);
                        }
                    }
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            EmbedOptions eo = new EmbedOptions(_client);
            eo.Show();
        }
    }
}
