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
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(s);
            }
        }
        public static LockBitmap DownloadLockBitmap(string s)
        {
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
        }
    }
}
