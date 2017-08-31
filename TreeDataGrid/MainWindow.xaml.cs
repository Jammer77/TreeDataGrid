using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TreeDataGrid
{
    public class TestData : INotifyPropertyChanged
    {
        private string name;
        private string description = "Test";

        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }

        public string Description
        {
            get { return description; }
            set { description = value; NotifyPropertyChanged("Description"); }
        }

        public IEnumerable<TestData> Children { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

	public partial class MainWindow : Window
	{
		private List<TestData> _testDataCollection;

		public MainWindow()
		{
			InitializeComponent();
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _testDataCollection = new List<TestData>();
            for (int i = 0; i < 10; i++)
            {
                var newItem = new TestData()
                {
                    Name = "Parent",
                    Description = i.ToString()
                };
                var children = new List<TestData>();
                for (int j = 0; j < 10; j++)
                {
                    var child = new TestData()
                    {
                        Name = "Child",
                        Description = i.ToString() + j.ToString()
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
		//				SelectConverter.SelectedItems.Add(child);
					}
					
					children.Add(child);
				}
				newItem.Children = children;
				_testDataCollection.Add(newItem);
			}
	//		SelectConverter.SelectedItems.Add(_testDataCollection.FirstOrDefault());
			dataGrid.ItemsSource = ListHelper.GetList(_testDataCollection);
		}

        public static class ListHelper
        {
            public static IEnumerable<TestData> GetList(IEnumerable<TestData> testDataCollection)
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

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			var testData = (sender as CheckBox).DataContext as TestData;
			if (SelectConverter.SelectedItems.Any(o => o == testData))
			{
				SelectConverter.SelectedItems.Remove(testData);
                dataGrid.ItemsSource = ListHelper.GetList(_testDataCollection);
            }
			else
			{
				SelectConverter.SelectedItems.Add(testData);
                var newValues = dataGrid.ItemsSource as List<TestData>;
                if(newValues!=null)
                    dataGrid.ItemsSource = ListHelper.GetList(newValues);
			}
            
		}
	}
}
