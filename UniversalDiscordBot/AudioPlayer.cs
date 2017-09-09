using Discord;
using Discord.Audio;
using Discord.WebSocket;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalDiscordBot
{
    public partial class AudioPlayer : MaterialForm
    {
        DiscordSocketClient _client;
        string currentpath;
        Process currentsong;
        public AudioPlayer(DiscordSocketClient iclient)
        {
            _client = iclient;
            InitializeComponent();
            foreach (SocketGuild guild in iclient.Guilds)
            {
                comboBox1.Items.Add(guild.Name);
            }
        }

        private void AudioPlayer_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (SocketGuild guild in _client.Guilds)
            {
                if (guild.Name == comboBox1.Text)
                {
                    foreach (SocketVoiceChannel vchannel in guild.VoiceChannels)
                    {
                        comboBox2.Items.Add(vchannel.Name);
                    }
                }
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Please select a guild first!", "UDB");
                return;
            }
            if (comboBox2.Text == string.Empty)
            {
                MessageBox.Show("Please select a voice channel first!", "UDB");
                return;
            }

            joinchannel();
        }

        private void joinchannel()
        {
            var guildId = _client.Guilds.Where(x => x.Name == comboBox1.Text).Select(x => x.Id).FirstOrDefault();
            var Guild = _client.GetGuild(guildId);
            var VoiceChannels = Guild.VoiceChannels;
            var channelId = VoiceChannels.Where(x => x.Name == comboBox2.Text).Select(x => x.Id).FirstOrDefault();
            IVoiceChannel channel = (IVoiceChannel)_client.GetChannel(channelId);
            var client = channel.ConnectAsync();
            
            MessageBox.Show("Connected", "UDB");
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            folderBrowserDialog1.Description = "Select a folder containing songs";
            folderBrowserDialog1.ShowDialog();
            currentpath = folderBrowserDialog1.SelectedPath + "\\";
            if (folderBrowserDialog1.SelectedPath != "")
            {
                string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                for (int i = 0; i < files.Length; i++)
                {
                    string file = files[i];
                    if (file.EndsWith(".mp3") || file.EndsWith(".wav") || file.EndsWith(".m4a"))
                    {
                        comboBox3.Items.Add(file.Remove(0, folderBrowserDialog1.SelectedPath.Length + 1));
                    }
                }
            }
        }
        private Process CreateStream(string path)
        {
            currentsong = new Process();
            currentsong.StartInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = string.Format("-hide_banner -loglevel panic -i \"{0}\" -ac 2 -f s16le -ar 48000 pipe:1", currentpath + path),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            currentsong.Start();
            return currentsong;
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Select a Guild first!", "Error");
                return;
            }
            if (comboBox2.Text == string.Empty)
            {
                MessageBox.Show("Select a Voice Channel first!", "Error");
                return;
            }
            if (comboBox3.Text == string.Empty)
            {
                MessageBox.Show("Select a Song first!", "Error");
                return;
            }
            send();
        }
        private async Task send()
        {
           
            var guildId = _client.Guilds.Where(x => x.Name == comboBox1.Text).Select(x => x.Id).FirstOrDefault();
            var Guild = _client.GetGuild(guildId);
            var VoiceChannels = Guild.VoiceChannels;
            var channelId = VoiceChannels.Where(x => x.Name == comboBox2.Text).Select(x => x.Id).FirstOrDefault();
            IVoiceChannel channel = (IVoiceChannel)_client.GetChannel(channelId);
            var aclient = await channel.ConnectAsync();

            Stream stream = CreateStream(comboBox3.Text).StandardOutput.BaseStream;
            AudioOutStream astream = aclient.CreatePCMStream(AudioApplication.Mixed);
            await stream.CopyToAsync(astream);
            await astream.FlushAsync();
        }
    }
}
