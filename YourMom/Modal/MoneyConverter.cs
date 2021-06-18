using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YourMom.Modal
{
	class MoneyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			
			//Làm tròn 2 chữ số thập phân
			var money = (double)value;
			money = Math.Round(money, 2);

			const char dot = '.', hyphen = '-';
			const string comma = ",";
			var result = money.ToString();

			//Vị trí xuất hiện đầu tiên của dấu chấm
			var indexOfDot = result.IndexOf(dot);
			//Nếu không có phần thập phân thì xét vị trí cuối cùng của chuỗi
			if (indexOfDot == -1)
			{

				indexOfDot = result.Length;

			}

			do
			{

				indexOfDot -= 3;

				//Nếu ký tự phía trước không phải dấu âm thì thêm dấu phẩy vào
				if ((indexOfDot > 0) && (result[indexOfDot - 1] != hyphen))
				{

					result = result.Insert(indexOfDot, comma);

				}

			}
			while (indexOfDot > 0);

			return result;

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
