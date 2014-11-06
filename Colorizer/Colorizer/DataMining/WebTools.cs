using Colorizer.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.DataMining
{
    public static class WebTools
    {
        public static string DownloadHtml(string s)
        {
            s.NullCheck();

            using (WebClient client = new WebClient())
            {
                return client.DownloadString(s);
            }
        }

        public static LockBitmap DownloadLockBitmap(string s)
        {
            s.NullCheck();

            using (WebClient client = new WebClient())
            {

                Stream stream = client.OpenRead(s);

                var bitmap = new Bitmap(stream);

                stream.Flush();
                stream.Close();

                bitmap.NullCheck();

                return new LockBitmap(bitmap);
            }
        }
        public static Image DownloadImage(string url)
        {
            Image mpImage = null;

            try
            {
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                httpWebRequest.AllowWriteStreamBuffering = true;

                httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                httpWebRequest.Referer = "http://www.google.com/";

                httpWebRequest.Timeout = 20000;

                System.Net.WebResponse webResponse = httpWebRequest.GetResponse();

                System.IO.Stream webStream = webResponse.GetResponseStream();

                var tmpImage = Image.FromStream(webStream);

                webResponse.Close();
                webResponse.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception caught in process: {0}", ex.ToString());
                return null;
            }

            return mpImage;
        }

    }
}
