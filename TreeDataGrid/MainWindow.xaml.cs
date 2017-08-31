using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeDataGrid
{
	public class TestData
	{
		public string Name { get; set; }
		public string Description => "Test";
		public IEnumerable<TestData> Children{get;set;}
	}

	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var testDataCollection = new List<TestData>();
			for (int i = 0; i < 10; i++)
			{
				var newItem = new TestData()
				{
					Name = "Parent"
				};
				var children = new List<TestData>();
				for (int j = 0; j < 10; j++)
				{
					var child = new TestData()
					{
						Name = "Child"
					};

					if (j == 0)
					{
						child.Children = new List<TestData>()
						{
							new TestData()
							{
								Name = "SubChild"
							},

							new TestData()
							{
								Name = "SubChild"
							},
							new TestData()
							{
								Name = "SubChild"
							},
						};
						SelectConverter.SelectedItems.Add(child);
					}
					
					children.Add(child);
				}
				newItem.Children = children;
				testDataCollection.Add(newItem);
			}
			SelectConverter.SelectedItems.Add(testDataCollection.FirstOrDefault());
			dataGrid.ItemsSource = GetList(testDataCollection);
		}

		private IEnumerable<TestData> GetList(IEnumerable<TestData> testDataCollection)
		{
			var result = new List<TestData>();

			foreach (var item in testDataCollection)
			{
				result.Add(item);
				if (item.Children != null && SelectConverter.SelectedItems.Any(i => i == item))
				{
					var allchildren = GetList(item.Children);
					result.AddRange(allchildren);
				}
			}
			return result;
		}
	}
}
