using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalDiscordBot.Helpers.Others
{
    public class TimeConverter
    {
        public static string ConvertMinuteToMilliseconds(string duration)
        {
            int minute = Convert.ToInt32(duration[0].ToString()) * 60;
            int firstSecond = Convert.ToInt32(duration[2].ToString());
            int secondSecond = Convert.ToInt32(duration[3].ToString());
            string seconds = firstSecond.ToString() + secondSecond.ToString();
            int second = Convert.ToInt32(seconds);
            int total = minute + second;
            double toConvert = Convert.ToDouble(total);
            string milliSeconds = TimeSpan.FromSeconds(total).TotalMilliseconds.ToString();
            return milliSeconds;
        }
    }
}
