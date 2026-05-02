using Backup_Maker.Models;
using Backup_Maker.MVVM;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Backup_Maker.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Backup_maker_data.ini");

        private string backupPath;

        public ObservableCollection<FileData> FileDatas { get; set; }
        public ObservableCollection<Backup> Backups { get; set; }

        public ObservableCollection<Backup> SelectedBackups { get; } = new();
        public ObservableCollection<FileData> SelectedFiles { get; } = new();

        public RelayCommand unselectCommand => new RelayCommand(execute => unselect());
        public RelayCommand deleteCommand => new RelayCommand(execute => deleteFiles(), canExecute => SelectedFiles.Count >= 1 || SelectedBackups.Count >= 1);
        public RelayCommand restoreCommand => new RelayCommand(execute => restoreFunction(), canExecute => SelectedBackups.Count >= 1);
        public RelayCommand backupCommand => new RelayCommand(execute => backupFunction(), canExecute => SelectedFiles.Count >= 1);
        public RelayCommand filesLocationChangeCommand => new RelayCommand(execute => filesLocationChange());

        public MainWindowViewModel()
        {
            dataFileExists();
            FileDatas = new ObservableCollection<FileData>();
            Backups = new ObservableCollection<Backup>();
            mainWindow_Load();
        }

        private FileData selectedFileData;

        private Backup selectedBackup;

        private string filesLocation;

        private string searchFile;

        private string searchBackup;


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

        public string SearchFile
        {
            get { return searchFile; }
            set
            {
                searchFile = value;
                OnPropertyChanged();
                Data_Load();
            }
        }

        public string SearchBackup
        {
            get { return searchBackup; }
            set
            {
                searchBackup = value;
                OnPropertyChanged();
                Backup_Load();
            }
        }



        private void mainWindow_Load()
        {
            Data_Load();
            if (Directory.Exists(backupPath))
            {
                Backup_Load();
            }
            else
            {
                Backups.Clear();
            }
        }

        public void Data_Load()
        {
            if (!Directory.Exists(filesLocation)) return;
            var all = Directory.GetFiles(filesLocation);
            FileDatas.Clear();
            foreach (var file in all.Where(f => string.IsNullOrEmpty(searchFile) || Path.GetFileName(f).Contains(searchFile, StringComparison.OrdinalIgnoreCase)))
            {
                FileDatas.Add(new FileData
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Date = Directory.GetLastWriteTime(file),
                    Extension = Path.GetExtension(file)
                });
            }
        }

        public void Backup_Load()
        {
            if (!Directory.Exists(backupPath)) return;
            var all = Directory.GetFiles(backupPath);
            Backups.Clear();
            foreach (var file in all.Where(f => string.IsNullOrEmpty(searchBackup) || Path.GetFileName(f).Contains(searchBackup, StringComparison.OrdinalIgnoreCase)))
            {
                Backups.Add(new Backup
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Date = Directory.GetLastWriteTime(file),
                    Extension = Path.GetExtension(file)
                });
            }
        }

        private void restoreFunction()
        {
            SelectedFileData = null;
            copyFiles(backupPath, filesLocation);
            Data_Load();
            SelectedBackup = null;
        }

        private void backupFunction()
        {
            SelectedBackup = null;
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }
            copyFiles(filesLocation, backupPath);
            Backup_Load();
            SelectedFileData = null;
        }

        private void copyFiles(string firstPath, string secondPath)
        {
            //bool replace = overwriteCB.Checked;
            foreach (var file in SelectedBackups)
            {
                string patha = Path.Combine(firstPath, file.Name + file.Extension);
                string pathb = Path.Combine(secondPath, file.Name + file.Extension);
                File.Copy(patha,pathb, true);
            }
            foreach (var file in SelectedFiles)
            {
                string patha = Path.Combine(firstPath, file.Name + file.Extension);
                string pathb = Path.Combine(secondPath, file.Name + file.Extension);
                File.Copy(patha, pathb, true);
            }
        }
        private void deleteFiles()
        {
            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var file in SelectedBackups)
                {
                    string path = Path.Combine(backupPath, file.Name + file.Extension);
                    File.Delete(path);
                }
                foreach (var file in SelectedFiles)
                {
                    string path = Path.Combine(filesLocation, file.Name + file.Extension);
                    File.Delete(path);
                }
            }
            unselect();
            mainWindow_Load();
        }

        private void filesLocationChange()
        {
            OpenFolderDialog dialog = new OpenFolderDialog
            {
                Title = "Select Folder",
                InitialDirectory = filesLocation
            };
            {
                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllText(appDataPath, dialog.FolderName);
                    filesLocation = dialog.FolderName;
                    backupPath = Path.Combine(filesLocation, "backup");
                }
            }
            mainWindow_Load();
        }

        private void dataFileExists()
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
            backupPath = Path.Combine(filesLocation, "backup");
        }
        private void unselect()
        {
            SelectedFileData = null;
            SelectedBackup = null;
        }
    }
}
