using Backup_Maker.Models;
using Backup_Maker.MVVM;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
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

















        //private void filesLocationButton_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFolderDialog dialog = new OpenFolderDialog
        //    {
        //        Title = "Select Folder",
        //        InitialDirectory = filesLocation
        //    };
        //    {
        //        if (dialog.ShowDialog() == true)
        //        {
        //            File.WriteAllText(appDataPath, dialog.FolderName);
        //            filesLocation = dialog.FolderName;
        //        }
        //    }
        //    mainWindow_Load();
        //}

        //private void restoreButton_Click(object sender, RoutedEventArgs e)
        //{
        //    copyFiles(backupsList, Path.Combine(filesLocation, "backup"), filesLocation);
        //    listBox_Load(filesList, filesLocation);
        //    backupsList.SelectedItem = null;
        //}

        //private void deleteButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    {
        //        deleteFiles(filesList, filesLocation);
        //        deleteFiles(backupsList, Path.Combine(filesLocation, "backup"));
        //        unselect();
        //        mainWindow_Load();
        //    }
        //}

        //private void backupButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string path = Path.Combine(filesLocation, "backup");
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    copyFiles(filesList, filesLocation, path);
        //    listBox_Load(backupsList, path);
        //    filesList.SelectedItem = null;
        //}

        //private void unselectButton_Click(object sender, RoutedEventArgs e)
        //{
        //    unselect();
        //}

        //private void copyFiles(ListBox list, string pathA, string pathB)
        //{
        //    var selectedFiles = list.SelectedItems;
        //    //bool replace = overwriteCB.Checked;
        //    if (list.SelectedIndex != -1)
        //    {
        //        for (int i = selectedFiles.Count - 1; i >= 0; i--)
        //        {
        //            File.Copy(Path.Combine(pathA, selectedFiles[i].ToString()), Path.Combine(pathB, selectedFiles[i].ToString()), true);
        //        }
        //    }
        //}

        //private void deleteFiles(ListBox list, string path)
        //{
        //    var selectedFiles = list.SelectedItems;
        //    if (list.SelectedIndex != -1)
        //    {
        //        for (int i = selectedFiles.Count - 1; i >= 0; i--)
        //        {
        //            File.Delete(Path.Combine(path, selectedFiles[i].ToString()));
        //        }
        //    }
        //}

        private void unselect()
        {
            selectedFileData = null;
            selectedBackup = null;
        }
    }
}
