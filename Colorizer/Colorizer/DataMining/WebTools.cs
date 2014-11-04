using Colorizer.Imaging;
using System;
using System.Collections.Generic;
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

                if (bitmap != null)
                {
                    return new LockBitmap(bitmap);
                }
            }
            throw new ArgumentNullException("Didn't download file");
        }
        public static Image DownloadImage(string _URL)
        {
            Image _tmpImage = null;

            try
            {
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);
                httpWebRequest.AllowWriteStreamBuffering = true;

                // You can also specify additional header values like the user agent or the referer: (Optional)
                httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                httpWebRequest.Referer = "http://www.google.com/";

                // set timeout for 20 seconds (Optional)
                httpWebRequest.Timeout = 20000;

                // Request response:
                System.Net.WebResponse webResponse = httpWebRequest.GetResponse();

                // Open data stream:
                System.IO.Stream webStream = webResponse.GetResponseStream();

                // convert webstream to image
                var tmpImage = Image.FromStream(webStream);

                // Cleanup
                webResponse.Close();
                webResponse.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                return null;
            }

            return _tmpImage;
        }

    }
}
