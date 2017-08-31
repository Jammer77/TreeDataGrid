using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using static TreeDataGrid.MainWindow;

namespace TreeDataGrid
{
    public class CustomSortingDataGrid : DataGrid
    {
        protected override void OnSorting(DataGridSortingEventArgs eventArgs)
        {
            var ListTestData = ItemsSource as List<TestData>;

            if (eventArgs.Column.CanUserSort && ListTestData != null)
            {
                if (!string.IsNullOrEmpty(eventArgs.Column.SortMemberPath))
                {
                    var rootLevelItems = ListTestData.Where(rootTestData => ListTestData.Any(testData => testData.Children != null && !testData.Children.Contains(rootTestData)));

                    IEnumerable<TestData> sortedList = null;
                    var currentColumn = this.Columns.FirstOrDefault(col => col == eventArgs.Column);
                    var newDirection = ListSortDirection.Descending;
                    
                    if (eventArgs.Column.SortDirection == ListSortDirection.Descending )
                    {
                        newDirection = ListSortDirection.Ascending;
                        sortedList = rootLevelItems.OrderBy(o => o.GetType().GetProperty(eventArgs.Column.SortMemberPath).GetValue(o, null));

                    }
                    else
                    {
                        newDirection = ListSortDirection.Descending;
                        sortedList = rootLevelItems.OrderByDescending(o => o.GetType().GetProperty(eventArgs.Column.SortMemberPath).GetValue(o, null));
                    }

                    this.ItemsSource = ListHelper.GetList(sortedList);

                    if (currentColumn != null)
                        currentColumn.SortDirection = newDirection;
                }
            }
            else
            {
                base.OnSorting(eventArgs);
            }
        }
    }
}
