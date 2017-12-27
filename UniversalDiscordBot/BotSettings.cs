using Discord;
using Discord.WebSocket;
using MaterialSkin;
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
    public partial class BotSettings : MaterialForm
    {
        DiscordSocketClient _client;
        public BotSettings(DiscordSocketClient iclient)
        {
            _client = iclient;
            InitializeComponent();
            var mf = MaterialSkinManager.Instance;
            mf.AddFormToManage(this);
            //mf.Theme = MaterialSkinManager.Themes.DARK;
            mf.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            pictureBox1.ImageLocation = iclient.CurrentUser.GetAvatarUrl(ImageFormat.Auto, 128);
            textBox1.Text = _client.CurrentUser.Username;
            if (_client.CurrentUser.Game == null)
            {
                textBox2.Text = "Playing no game";
            }
            else
            {
                textBox2.Text = _client.CurrentUser.Game.ToString();
            }

        }

        private void BotSettings_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textBox1.Text = string.Empty;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {
                MessageBox.Show("Please choose a name");
            }
            else
            {
                _client.CurrentUser.ModifyAsync(delegate (SelfUserProperties u)
                    {
                        u.Username = textBox1.Text;
                    });

                _client.SetGameAsync(textBox2.Text, "https://twitch.tv/idk", StreamType.Twitch);
            }
        }
    }
}
