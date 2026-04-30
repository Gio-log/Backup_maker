using Backup_Maker.ViewModels;
using System.Windows;

namespace Backup_Maker
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            DataContext = viewModel;
        }

    }
}