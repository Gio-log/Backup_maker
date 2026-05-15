using System.Windows;
using System.Windows.Controls;

namespace Backup_Maker.Views.Windows
{
    public enum PopupButtons
    {
        YesNo,
        OK
    }

    public partial class PopupWindow : Window
    {
        public string WindowTitle { get; set; }
        public string Message { get; set; }


        public PopupWindow(string title, string message, PopupButtons buttons)
        {
            InitializeComponent();
            DataContext = this;
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowTitle = title;
            Message = message;

            if (buttons == PopupButtons.YesNo)
            {
                Yes.Visibility = Visibility.Visible;
                No.Visibility = Visibility.Visible;
            }
            else if (buttons == PopupButtons.OK)
            {
                Ok.Visibility = Visibility.Visible;
            }
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
