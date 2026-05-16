using Backup_Maker.Models;
using Backup_Maker.MVVM;
using Backup_Maker.Views.Windows;
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

        private bool overwrite;


        public FileData SelectedFileData
        {
            get { return selectedFileData; }
            set 
            { 
                selectedFileData = value;
                OnPropertyChanged();
                if (value != null)
                {
                    selectedBackup = null;
                    SelectedBackups.Clear();
                    OnPropertyChanged(nameof(SelectedBackup));
                }
            }
        }

        public Backup SelectedBackup
        {
            get { return selectedBackup; }
            set
            {
                selectedBackup = value;
                OnPropertyChanged();
                if (value != null)
                {
                    selectedFileData = null;
                    SelectedFiles.Clear();
                    OnPropertyChanged(nameof(SelectedFileData));
                }
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

        public bool Overwrite
        {
            get { return overwrite; }
            set
            {
                overwrite = value;
                OnPropertyChanged();
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
                    Date = File.GetLastWriteTime(file),
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
            if (folderCheck(backupPath))
            {
                Backups.Clear();
                return;
            }
            SelectedFileData = null;
            copyFiles(backupPath, filesLocation, SelectedBackups);
            Data_Load();
            SelectedBackup = null;
        }

        private void backupFunction()
        {
            if (folderCheck(filesLocation))
            {
                FileDatas.Clear();
                return;
            }
            SelectedBackup = null;
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }
            copyFiles(filesLocation, backupPath, SelectedFiles);
            Backup_Load();
            SelectedFileData = null;
        }

        private void copyFiles(string firstPath, string secondPath, IEnumerable<IFileEntry> files)
        {
            List<string> missingFiles = new List<string>();
            foreach (var file in files)
            {
                string patha = Path.Combine(firstPath, file.Name + file.Extension);
                if (!File.Exists(patha))
                {
                    missingFiles.Add(file.Name);
                    continue;
                }
                string pathb = Path.Combine(secondPath, file.Name + file.Extension);
                if (File.Exists(pathb) == false || overwrite == true)
                {
                    File.Copy(patha, pathb, true);
                    continue;
                }
                int count = 1;
                do
                {
                    pathb = Path.Combine(secondPath, file.Name + $"({count})" + file.Extension);
                    count++;
                } while (File.Exists(pathb));
                File.Copy(patha, pathb);
            }
            if (missingFiles.Any())
            {
                string missingMsg = missingFiles.Count() == 1
                    ? $"File not found {missingFiles[0]}."
                    : $"File not found {missingFiles[0]} and {missingFiles.Count() - 1} more.";
                new PopupWindow("Backup", missingMsg, PopupButtons.OK).ShowDialog();
                new PopupWindow("Backup", $"Backup complete. {files.Count() - missingFiles.Count()} file(s) copied.", PopupButtons.OK).ShowDialog();
                mainWindow_Load();
            }
            else
            {
                new PopupWindow("Backup", $"Backup complete. {files.Count()} file(s) copied.", PopupButtons.OK).ShowDialog();
            }
        }
        private void deleteFiles()
        {
            List<string> missingFiles = new List<string>();
            bool useBackups = SelectedBackups.Any();
            IEnumerable<IFileEntry> files = useBackups ? SelectedBackups : SelectedFiles;
            string location = useBackups ? backupPath : filesLocation;
            if (folderCheck(location))
            {
                if (useBackups)
                {
                    Backups.Clear();
                }
                else
                {
                    FileDatas.Clear();
                }
                return;
            }
            if (new PopupWindow("Delete", $"Do you want to delete {files.Count()} file(s)?", PopupButtons.YesNo).ShowDialog() == true)
            {
                foreach (var file in files)
                {
                    string path = Path.Combine(location, file.Name + file.Extension);
                    if (!File.Exists(path))
                    {
                        missingFiles.Add(file.Name);
                        continue;
                    }
                    else
                    {
                        File.Delete(path);
                    }
                }
                if(missingFiles.Any())    
                {
                    new PopupWindow("Delete", $"Files not found {missingFiles.Count()}", PopupButtons.OK).ShowDialog();
                }
                new PopupWindow("Delete", "Files removed.", PopupButtons.OK).ShowDialog();
            }
            unselect();
            mainWindow_Load();
        }

        private void filesLocationChange()
        {
            if(!Directory.Exists(filesLocation))
            {
                filesLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
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
                if (!Directory.Exists(filesLocation))
                {
                    filesLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
            }
            backupPath = Path.Combine(filesLocation, "backup");
        }
        private void unselect()
        {
            SelectedFileData = null;
            SelectedBackup = null;
        }

        private bool folderCheck(string path)
        {
            if (!Directory.Exists(path))
            {
                new PopupWindow("Error", "Source folder not found.", PopupButtons.OK).ShowDialog();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
