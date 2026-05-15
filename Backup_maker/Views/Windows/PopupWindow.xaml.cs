using System.Windows;
using System.Windows.Controls;

namespace Backup_Maker.Views.Windows
{
    public partial class PopupWindow : Window
    {
        private string Title;
        private string Message;
        public PopupWindow(string title, string message)
        {
            InitializeComponent();
            Title = title;
            Message = message;
        }

        public void YesNo()
        {
            Yes.Visibility = Visibility.Visible;
            No.Visibility = Visibility.Visible;
        }

        public void OK()
        {
            Ok.Visibility = Visibility.Visible;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
