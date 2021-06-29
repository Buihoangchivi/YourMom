using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.IO;
using System.Windows.Media.Imaging;

namespace YourMom
{
    //Tách string date thành ngày - tháng - năm riêng biệt
    public class DelimiterConverter : IValueConverter
    {
        public object Convert(Object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] values = ((string)value).Split('/');
            int index = int.Parse((string)parameter);
            return values[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
