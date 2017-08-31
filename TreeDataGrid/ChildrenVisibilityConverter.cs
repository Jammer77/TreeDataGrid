using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace TreeDataGrid
{
	public class ChildrenVisibilityConverter : IValueConverter
	{
		static List<TestData> _selectedItems;
		public static List<TestData> SelectedItems => _selectedItems ?? (_selectedItems = new List<TestData>());

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var t = value as TestData;

			if (t == null || t.Children == null || !t.Children.Any())
			{
				return Visibility.Hidden;
			}

			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
