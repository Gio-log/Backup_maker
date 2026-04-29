using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Backup_Maker
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            dataFileExists();
            mainWindow_Load();
            DataContext = this;
        }

        private string filesLocation;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string FilesLocation
        {
            get { return filesLocation; }
            set
            {
                filesLocation = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void mainWindow_Load()
        {
            listBox_Load(filesList, filesLocation);
            if (Directory.Exists(Path.Combine(filesLocation, "backup")))
            {
                listBox_Load(backupsList, Path.Combine(filesLocation, "backup"));
            }
        }
        
        private void listBox_Load(ListBox list, string path)
        {
            var files = Directory.GetFiles(path).Select(f => System.IO.Path.GetFileName(f)).ToList();
            list.ItemsSource = files;
        }

        private void filesLocationButton_Click(object sender, RoutedEventArgs e)
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Backup_maker_data.ini");
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
                }
            }
            mainWindow_Load();
        }

        private void restoreButton_Click(object sender, RoutedEventArgs e)
        {
            copyFiles(backupsList, Path.Combine(filesLocation, "backup"), filesLocation);
            listBox_Load(filesList, filesLocation);
            backupsList.SelectedItem = null;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                deleteFiles(filesList, filesLocation);
                deleteFiles(backupsList, Path.Combine(filesLocation, "backup"));
                unselect();
                mainWindow_Load();
            }  
        }

        private void backupButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(filesLocation, "backup");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            copyFiles(filesList, filesLocation, path);
            listBox_Load(backupsList, path);
            filesList.SelectedItem = null;
        }

        private void unselectButton_Click(object sender, RoutedEventArgs e)
        {
            unselect();
        }

        private void copyFiles(ListBox list, string pathA, string pathB)
        {
            var selectedFiles = list.SelectedItems;
            //bool replace = overwriteCB.Checked;
            if (list.SelectedIndex != -1)
            {
                for (int i = selectedFiles.Count - 1; i >= 0; i--)
                {
                    File.Copy(Path.Combine(pathA, selectedFiles[i].ToString()), Path.Combine(pathB, selectedFiles[i].ToString()), true);
                }
            }
        }

        private void deleteFiles(ListBox list, string path)
        {
            var selectedFiles = list.SelectedItems;
            if (list.SelectedIndex != -1)
            {
                for (int i = selectedFiles.Count - 1; i >= 0; i--)
                {
                    File.Delete(Path.Combine(path, selectedFiles[i].ToString()));
                }
            }
        }

        private void unselect()
        {
            filesList.SelectedItem = null;
            backupsList.SelectedItem = null;
        }

        private void dataFileExists()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Backup_maker_data.ini");
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
        }
    }
}