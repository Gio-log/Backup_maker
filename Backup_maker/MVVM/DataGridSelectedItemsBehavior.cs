using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Backup_Maker.MVVM
{
    public static class DataGridSelectedItemsBehavior
    {
        public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.RegisterAttached(
            "SelectedItems",
            typeof(IList),
            typeof(DataGridSelectedItemsBehavior),
            new PropertyMetadata(null, OnSelectedItemsChanged));

        public static void SetSelectedItems(DependencyObject element, IList value)
            => element.SetValue(SelectedItemsProperty, value);

        public static IList GetSelectedItems(DependencyObject element)
            => (IList)element.GetValue(SelectedItemsProperty);

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not DataGrid grid) return;

            grid.SelectionChanged -= Grid_SelectionChanged;
            grid.SelectionChanged += Grid_SelectionChanged;
        }

        private static void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (DataGrid)sender;
            var selectedItems = GetSelectedItems(grid);

            if (selectedItems == null) return;

            selectedItems.Clear();

            foreach (var item in grid.SelectedItems)
            {
                selectedItems.Add(item);
            }
        }
    }
}
