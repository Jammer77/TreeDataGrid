using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TreeDataGrid
{
	public class SelectConverter : IValueConverter
	{
		static List<TestData> _selectedItems;
		public static List<TestData> SelectedItems => _selectedItems ?? (_selectedItems = new List<TestData>());

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var t = value as TestData;

			if (t == null)
			{
				return false;
			}

			if (SelectedItems.Any(i => i == t))
			{
				return true;
			}

			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
