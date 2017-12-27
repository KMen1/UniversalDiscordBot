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
        public EmbedOptions(DiscordSocketClient iclient)
        {
            _client = iclient;
            InitializeComponent();
            foreach (SocketGuild guild in iclient.Guilds)
            {
                comboBox1.Items.Add(guild.Name);
            }
            comboBox3.Items.Add("Blue");
            comboBox3.Items.Add("Red");
            comboBox3.Items.Add("Purple");
            comboBox3.Items.Add("Orange");
            comboBox3.Items.Add("Gold");
            comboBox3.Items.Add("Green");
            comboBox3.Items.Add("Magenta");
            comboBox3.Items.Add("Teal");
            //comboBox3.Items.Add("");
            //comboBox3.Items.Add("");
            //comboBox3.Items.Add("");

        }

        private void EmbedOptions_Load(object sender, EventArgs e)
        {
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

            var eb = new EmbedBuilder();
            eb.WithTitle(textBox1.Text);
            eb.WithDescription(textBox2.Text);
            eb.WithAuthor("");
            eb.WithFooter("");
            eb.WithImageUrl("");
            eb.WithThumbnailUrl("");
            eb.WithUrl("");
            string color = comboBox3.Text;

            if (color == "Blue")
            {
                eb.WithColor(Discord.Color.Blue);
            }
            else if (color == "Red")
            {
                eb.WithColor(Discord.Color.Red);
            }
            else if (color == "Purple")
            {
                eb.WithColor(Discord.Color.Purple);
            }
            else if (color == "Orange")
            {
                eb.WithColor(Discord.Color.Orange);
            }
            else if (color == "Gold")
            {
                eb.WithColor(Discord.Color.Gold);
            }
            else if (color == "Green")
            {
                eb.WithColor(Discord.Color.Green);
            }
            else if (color == "Magenta")
            {
                eb.WithColor(Discord.Color.Magenta);
            }
            else if (color == "Teal")
            {
                eb.WithColor(Discord.Color.Teal);
            }

            sendMessage($"", eb);
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
