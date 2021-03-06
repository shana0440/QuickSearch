﻿using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace QuickSearch.Models
{
    public class Crawler
    {
        public static string GetResponse(string url)
        {
            var isNetWorkAvailable = NetworkInterface.GetIsNetworkAvailable();
            if (!isNetWorkAvailable)
            {
                throw new WebException("沒有連接至網際網路");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            request.Headers.Set("Accept-Language", "zh-TW,zh;q=0.8,en-US;q=0.6,en;q=0.4");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string html = stream.ReadToEnd();
                return html;
            }
        }
    }
}
