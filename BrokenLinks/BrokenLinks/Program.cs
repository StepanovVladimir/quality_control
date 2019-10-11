using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BrokenLinks
{
    class Program
    {
        public static void Main(string[] args)
        {
            Uri uri = new Uri("http://52.136.215.164/broken-links/");
            FileStream validLinks = new FileStream("../../validLinks.txt", FileMode.Create, FileAccess.Write);
            FileStream invalidLinks = new FileStream("../../invalidLinks.txt", FileMode.Create, FileAccess.Write);
            ISet<string> uris = new SortedSet<string> { uri.ToString() };

            RecursiveCheck(validLinks, invalidLinks, uris, uri);
        }

        private static void RecursiveCheck(FileStream validLinks, FileStream invalidLinks, ISet<string> uris, Uri uri)
        {
            WebRequest request;
            try
            {
                request = WebRequest.Create(uri);
            }
            catch (NotSupportedException)
            {
                return;
            }

            try
            {
                WriteLink(uri.ToString(), (HttpWebResponse)request.GetResponse(), validLinks);
                WebClient client = new WebClient();
                string source = client.DownloadString(uri);

                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.ParseDocument(source);

                foreach (IElement element in document.QuerySelectorAll("a"))
                {
                    string hrefTag = element.GetAttribute("href");

                    if (hrefTag == null || hrefTag == "#" || hrefTag.Substring(0, 8) == "https://")
                    {
                        continue;
                    }

                    Uri newUri = new Uri(uri, hrefTag);
                    if (uris.Add(newUri.ToString()))
                    {
                        RecursiveCheck(validLinks, invalidLinks, uris, newUri);
                    }
                }
            }
            catch (WebException exc)
            {
                WriteLink(uri.ToString(), (HttpWebResponse)exc.Response, invalidLinks);
            }
            request.Abort();
        }

        private static void WriteLink(string uri, HttpWebResponse response, FileStream fileStream)
        {
            string str = uri.ToString() + ' ' + (int)response.StatusCode + ' ' + response.StatusCode + '\n';
            byte[] bytes = Encoding.Default.GetBytes(str);
            fileStream.Write(bytes, 0, bytes.Length);
            response.Close();
        }
    }
}