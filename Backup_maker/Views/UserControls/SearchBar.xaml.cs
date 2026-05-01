using Backup_Maker.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Backup_Maker.Views.UserControls
{
    public partial class SearchBar : UserControl
    {
        public static readonly DependencyProperty SearchFileProperty =
            DependencyProperty.Register(
                nameof(SearchFile),
                typeof(string),
                typeof(SearchBar),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string SearchFile
        {
            get => (string) GetValue(SearchFileProperty);
            set => SetValue(SearchFileProperty, value); 
        }
        public SearchBar()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbPlaceholder.Visibility = string.IsNullOrEmpty(txtInput.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
