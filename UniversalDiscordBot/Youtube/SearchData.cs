using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using YoutubeSearch;

namespace UniversalDiscordBot.Helpers.YouTube
{
    public class SearchData
    {
        public static string SearchTitle(string querystring)
        {
            List<VideoInformation> items;
            WebClient webclient;
            string title;
            items = new List<VideoInformation>();
            webclient = new WebClient();
            string html = webclient.DownloadString("https://www.youtube.com/results?search_query=" + querystring);
            string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
            MatchCollection result = Regex.Matches(html, pattern, RegexOptions.Singleline);
            title = result[0].Groups[1].Value;
            return title;
        }
        public static string SearchTitleFromLink(string link)
        {
            List<VideoInformation> items;
            WebClient webclient;
            string title;
            items = new List<VideoInformation>();
            webclient = new WebClient();
            string html = webclient.DownloadString(link);
            string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
            MatchCollection result = Regex.Matches(html, pattern, RegexOptions.Singleline);
            title = result[0].Groups[1].Value;
            return title;
        }
        public static string SearchUrl(string querystring)
        {
            string url;
            List<VideoInformation> items;
            WebClient webclient;
            items = new List<VideoInformation>();
            webclient = new WebClient();
            string html = webclient.DownloadString("https://www.youtube.com/results?search_query=" + querystring);
            string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
            MatchCollection result = Regex.Matches(html, pattern, RegexOptions.Singleline);
            url = string.Concat("http://www.youtube.com/watch?v=", VideoItemHelper.cull(result[0].Value, "watch?v=", "\""));
            return url;
        }
        public static string SearchDuration(string querystring)
        {
            List<VideoInformation> items;
            WebClient webclient;
            string duration;
            items = new List<VideoInformation>();
            webclient = new WebClient();
            string html = webclient.DownloadString("https://www.youtube.com/results?search_query=" + querystring);
            string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
            MatchCollection result = Regex.Matches(html, pattern, RegexOptions.Singleline);
            duration = VideoItemHelper.cull(VideoItemHelper.cull(result[0].Value, "id=\"description-id-", "span"), ": ", "<");
            return duration;
        }
    }
}
