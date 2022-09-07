using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using ZXing.QrCode;
using ZXing.Common;
using System.Runtime.InteropServices;
using ZXing;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using ZXing.CoreCompat.System.Drawing;

namespace BiliSpirit.Common
{
    public class QRCode
    {
        public static ImageSource CreateQRCode(string content, int width, int height)
        {
            EncodingOptions options;//包含一些编码、大小等的设置
            BarcodeWriter write = null;//用来生成二维码，对应的BarcodeReader用来解码
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
                Margin = 0
            };

            write = new BarcodeWriter();
            write.Format = BarcodeFormat.QR_CODE;
            write.Options = options;
            IntPtr ip = write.Write(content).GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                ip, IntPtr.Zero, Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            return bitmapSource;
        }

        // 注销对象方法API
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
    }
}
