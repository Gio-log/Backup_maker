using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Backup_Maker.Views.UserControls
{
    public partial class TitleBar : UserControl
    {
        private Window ParentWindow => Window.GetWindow(this);

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(TitleBar),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TitleBar()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => ParentWindow.DragMove();

        private void Minimize_Click(object sender, RoutedEventArgs e) => ParentWindow.WindowState = WindowState.Minimized;

        private void Maximize_Click(object sender, RoutedEventArgs e) => ParentWindow.WindowState = ParentWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void Close_Click(object sender, RoutedEventArgs e) => ParentWindow.Close();
    }
}
