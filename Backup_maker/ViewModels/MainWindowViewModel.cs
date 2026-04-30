using Backup_Maker.Models;
using Backup_Maker.MVVM;
using System.Collections.ObjectModel;

namespace Backup_Maker.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<File> Files { get; set; }

        public MainWindowViewModel()
        {
            Files = new ObservableCollection<File>();
            Files.Add(new File
            {
                Name="test",
                Date="21.1.2022",
                Extension="ini"
            });
            Files.Add(new File
            {
                Name = "test2",
                Date = "21.1.2024",
                Extension = "ini"
            });
            Files.Add(new File
            {
                Name = "test2",
                Date = "21.1.2024",
                Extension = "exe"
            });
        }

        private File selectedFile;

        public File SelectedFile
        {
            get { return selectedFile; }
            set 
            { 
                selectedFile = value;
                OnPropertyChanged();
            }
        }

        private string filesLocation;

        public string FilesLocation
        {
            get { return filesLocation; }
            set
            {
                filesLocation = value;
                OnPropertyChanged();
            }
        }

    }
}
