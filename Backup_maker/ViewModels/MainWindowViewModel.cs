using Backup_Maker.Models;
using Backup_Maker.MVVM;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace Backup_Maker.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Backup_maker_data.ini");

        private string backupPath;
        public ObservableCollection<FileData> FileDatas { get; set; }
        public ObservableCollection<Backup> Backups { get; set; }

        public MainWindowViewModel()
        {
            backupPath = dataFileExists();
            FileDatas = new ObservableCollection<FileData>();
            Backups = new ObservableCollection<Backup>();
            mainWindow_Load();
        }

        private FileData selectedFileData;

        private Backup selectedBackup;

        private string filesLocation;

        public FileData SelectedFileData
        {
            get { return selectedFileData; }
            set 
            { 
                selectedFileData = value;
                OnPropertyChanged();
            }
        }

        public Backup SelectedBackup
        {
            get { return selectedBackup; }
            set
            {
                selectedBackup = value;
                OnPropertyChanged();
            }
        }

        public string FilesLocation
        {
            get { return filesLocation; }
            set
            {
                filesLocation = value;
                OnPropertyChanged();
            }
        }


        private void mainWindow_Load()
        {
            Data_Load(FileDatas, filesLocation);
            if (Directory.Exists(backupPath))
            {
                Backup_Load(Backups, backupPath);
            }
        }

        private void Data_Load(ObservableCollection<FileData> data, string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                FileDatas.Add(new FileData
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Date = Directory.GetLastWriteTime(file).ToString(),
                    Extension = Path.GetExtension(file)
                });
            }
        }

        private void Backup_Load(ObservableCollection<Backup> data, string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                FileDatas.Add(new FileData
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Date = Directory.GetLastWriteTime(file).ToString(),
                    Extension = Path.GetExtension(file)
                });
            }
        }






        private string dataFileExists()
        {
            if (File.Exists(appDataPath) == false)
            {
                string startingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                filesLocation = startingFolder;
                File.WriteAllText(appDataPath, startingFolder);
            }
            else
            {
                using (StreamReader reader = new StreamReader(appDataPath))
                {
                    filesLocation = reader.ReadLine();
                }
            }
            return Path.Combine(filesLocation, "backup");
        }
    }
}
