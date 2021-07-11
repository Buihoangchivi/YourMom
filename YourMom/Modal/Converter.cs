using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.IO;
using System.Windows.Media.Imaging;

namespace YourMom.Modal
{
    class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string relative = (string)value;
            string absolutePath = "";
            if (relative != null && !relative.Contains(":\\"))
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                absolutePath = $"{folder}{relative}";
            }
            else if (relative != null)
            {
                absolutePath = $"{relative}";
            }
            if (relative != null)
            {
                var image = ConvertToImage(absolutePath);
                if (image != null)
                {
                    return image;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return absolutePath;
            }
        }

        public static BitmapImage ConvertToImage(string path)
        {

            BitmapImage bitmapImage = null;
            if (File.Exists(path))
            {
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new FileStream(path, FileMode.Open, FileAccess.Read);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.StreamSource.Dispose();
            }

            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
