﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Net;
using TheresaBot.Main.Common;
using TheresaBot.Main.Exceptions;
using TheresaBot.Main.Helper;
using TheresaBot.Main.Model.Ascii2d;
using TheresaBot.Main.Model.Pixiv;
using TheresaBot.Main.Type;

namespace TheresaBot.Main.Business
{
    internal class Ascii2dBusiness
    {
        public async Task<Ascii2dResult> getAscii2dResultAsync(string imgHttpUrl)
        {
            DateTime startTime = DateTime.Now;
            string colorUrl = await GetColorUrlAsync(imgHttpUrl);
            string bovwUrl = colorUrl.Replace("/color/", "/bovw/");
            var headerDic = new Dictionary<string, string>() { { "User-Agent", "Mozilla/5.0" } };
            string ascii2dHtml = await HttpHelper.GetHtmlAsync(bovwUrl, headerDic);
            HtmlParser htmlParser = new HtmlParser();
            IHtmlDocument document = await htmlParser.ParseDocumentAsync(ascii2dHtml);
            IEnumerable<IElement> domList = document.All.Where(m => m.ClassList.Contains("detail-box"));

            List<Ascii2dItem> itemList = new List<Ascii2dItem>();
            if (domList is null || domList.Count() == 0) return new Ascii2dResult(itemList, startTime, 0);
            foreach (IElement resultElement in domList)
            {
                IHtmlCollection<IElement> linkList = resultElement.GetElementsByTagName("a");
                if (linkList is null || linkList.Length == 0) continue;
                foreach (IElement linkElement in linkList)
                {
                    Ascii2dItem saucenaoItem = getAscii2dItem(linkElement);
                    if (saucenaoItem is null) continue;
                    if (itemList.Any(o => o.SourceUrl == saucenaoItem.SourceUrl)) continue;
                    itemList.Add(saucenaoItem);
                }
            }
            return new Ascii2dResult(itemList, startTime, domList.Count());
        }


        public Ascii2dItem getAscii2dItem(IElement linkElement)
        {
            string href = linkElement.GetAttribute("href");
            if (string.IsNullOrWhiteSpace(href)) return null;

            href = href.Trim();
            string hrefLower = href.ToLower();

            //https://www.pixiv.net/artworks/100378274
            if (hrefLower.Contains("www.pixiv.net/artworks"))
            {
                string illustId = href.SplitHttpUrl().LastOrDefault() ?? string.Empty;
                return new Ascii2dItem(SetuSourceType.Pixiv, href, illustId);
            }

            //https://twitter.com/1_tri_pic/status/1560897111624802304
            if (hrefLower.Contains("twitter.com") && hrefLower.Contains("/status/"))
            {
                string illustId = href.SplitHttpUrl().LastOrDefault() ?? string.Empty;
                return new Ascii2dItem(SetuSourceType.Twitter, href, illustId);
            }
            return null;
        }

        public async Task<List<Ascii2dItem>> getBestMatchAsync(List<Ascii2dItem> itemList)
        {
            if (itemList is null || itemList.Count == 0) return null;
            List<Ascii2dItem> matchList = new List<Ascii2dItem>();
            for (int i = 0; i < itemList.Count; i++)
            {
                try
                {
                    Ascii2dItem ascii2dItem = itemList[i];
                    if (ascii2dItem.SourceType == SetuSourceType.Pixiv)
                    {
                        PixivWorkInfo pixivWorkInfo = await PixivHelper.GetPixivWorkInfoAsync(ascii2dItem.SourceId);
                        if (pixivWorkInfo is null) continue;
                        ascii2dItem.PixivWorkInfo = pixivWorkInfo;
                    }
                    if (ascii2dItem.SourceType == SetuSourceType.Twitter)
                    {
                        //TODO
                    }
                    matchList.Add(ascii2dItem);
                }
                catch (Exception)
                {
                }
            }
            return matchList;
        }

        /// <summary>
        /// 请求ascii2d,获取返回结果
        /// </summary>
        /// <param name="imgHttpUrl"></param>
        /// <returns></returns>
        /// <exception cref="PixivException"></exception>
        private async static Task<string> GetColorUrlAsync(string imgHttpUrl)
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>() { { "uri", imgHttpUrl } };
            Dictionary<string, string> headerDic = new Dictionary<string, string>() { { "User-Agent", "Mozilla/5.0" } };
            string ascii2dUrl = BotConfig.SaucenaoConfig.Ascii2dWithIp ? HttpUrl.Ascii2dIpUrl : HttpUrl.Ascii2dDomainUrl;
            HttpResponseMessage response = await HttpHelper.PostFormForHtml(ascii2dUrl, paramDic, headerDic);
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Redirect)
            {
                string contentString = await response.GetContentStringAsync();
                Exception innerException = new Exception(contentString);
                throw new PixivException(innerException, $"ascii2d返回StatusCode：{(int)response.StatusCode}");
            }
            return response.RequestMessage.RequestUri.AbsoluteUri;
        }

    }
}
