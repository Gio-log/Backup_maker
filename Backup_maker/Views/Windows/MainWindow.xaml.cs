using Backup_Maker.ViewModels;
using System.Windows;

namespace Backup_Maker.Views.Windows
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