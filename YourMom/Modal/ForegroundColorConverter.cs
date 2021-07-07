using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace YourMom.Modal
{

	class ForegroundColorConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{

			var type = (string)value;
			var firstID = type[0] - '0';

			//Giao dịch tiền vào tương ứng màu chữ xanh nước biển
			if (firstID % 2 == 0)
            {

				return (SolidColorBrush)new BrushConverter().ConvertFromString("#039BE5");

			}
			else //Giao dịch tiền ra tương ứng màu chữ đỏ
			{

				return (SolidColorBrush)new BrushConverter().ConvertFromString("#E51C23");

			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{

			throw new NotImplementedException();

		}

	}

}

