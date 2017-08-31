using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TreeDataGrid
{
	public class MarginConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var itemsSource  = (values[0] as DataGrid).ItemsSource as IEnumerable<TestData> ;
			var currentItem = values[1] as TestData;
			int level = GetLevel(itemsSource, currentItem);
			Thickness margin = new Thickness(level * 10, 0, 0, 0);
			return margin;
		}

		private int GetLevel(IEnumerable<TestData> itemsSource, TestData currentItem)
		{
			int level = -1;
			var lastIlem = currentItem;
			do
			{
				level++;
				lastIlem = itemsSource.Where(_=>_.Children != null).FirstOrDefault(i => i.Children.Any(sub => sub == lastIlem));
			} while (lastIlem != null);

			return level;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
