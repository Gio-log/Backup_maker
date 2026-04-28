using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Backup_Maker
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dataFileExists();
            mainWindow_Load();
        }
        private void mainWindow_Load()
        {
            string file = filesLocationTextBlock.Text;
            listBox_Load(filesList, file);
            if (Directory.Exists(filesLocationTextBlock.Text + "\\backup"))
            {
                listBox_Load(backupsList, filesLocationTextBlock.Text + "\\backup");
            }
        }
        
        private void listBox_Load(ListBox list, string path)
        {
            var files = Directory.GetFiles(path).Select(f => System.IO.Path.GetFileName(f)).ToList();
            list.ItemsSource = files;
        }

        private void filesLocationButton_Click(object sender, RoutedEventArgs e)
        {
            string appDataPath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Backup_maker_data.ini");
            OpenFolderDialog dialog = new OpenFolderDialog
            {
                Title = "Select Folder",
                InitialDirectory = filesLocationTextBlock.Text
            };
            {
                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllText(appDataPath, dialog.FolderName);
                    filesLocationTextBlock.Text = dialog.FolderName;
                }
            }
            mainWindow_Load();
        }

        private void restoreButton_Click(object sender, RoutedEventArgs e)
        {
            copyFiles(backupsList, filesLocationTextBlock.Text + "\\backup", filesLocationTextBlock.Text);
            listBox_Load(filesList, filesLocationTextBlock.Text);
            backupsList.SelectedItem = null;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            deleteFiles(filesList, filesLocationTextBlock.Text);
            deleteFiles(backupsList, filesLocationTextBlock.Text + "\\backup");
            unselect();
            mainWindow_Load();
        }

        private void backupButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(filesLocationTextBlock.Text + "\\backup"))
            {
                Directory.CreateDirectory(filesLocationTextBlock.Text + "\\backup");
            }
            copyFiles(filesList, filesLocationTextBlock.Text, filesLocationTextBlock.Text + "\\backup");
            listBox_Load(backupsList, filesLocationTextBlock.Text + "\\backup");
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
                    File.Delete(path + "\\" + selectedFiles[i]);
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
            string appDataPath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Backup_maker_data.ini");
            if (File.Exists(appDataPath) == false)
            {
                string startingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                filesLocationTextBlock.Text = startingFolder;
                File.WriteAllText(appDataPath, startingFolder);
            }
            else
            {
                using (StreamReader reader = new StreamReader(appDataPath))
                {
                    filesLocationTextBlock.Text = reader.ReadLine();
                }
            }
        }
    }
}