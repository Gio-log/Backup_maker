using System.Windows.Controls;

namespace Backup_Maker.Views.UserControls
{
    public partial class BackupControls : UserControl
    {
        public BackupControls()
        {
            InitializeComponent();
        }

        //private void filesLocationButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string appDataPath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Backup_maker_data.ini");
        //    OpenFolderDialog dialog = new OpenFolderDialog
        //    {
        //        Title = "Select Folder",
        //        InitialDirectory = filesLocationTextBlock.Text
        //    };
        //    {
        //        if (dialog.ShowDialog() == true)
        //        {
        //            File.WriteAllText(appDataPath, dialog.FolderName);
        //            filesLocationTextBlock.Text = dialog.FolderName;
        //        }
        //    }
        //    mainWindow_Load();
        //}

        //private void restoreButton_Click(object sender, RoutedEventArgs e)
        //{
        //    copyFiles(backupsList, filesLocationTextBlock.Text + "\\backup", filesLocationTextBlock.Text);
        //    listBox_Load(filesList, filesLocationTextBlock.Text);
        //    backupsList.SelectedItem = null;
        //}

        //private void deleteButton_Click(object sender, RoutedEventArgs e)
        //{
        //    deleteFiles(filesList, filesLocationTextBlock.Text);
        //    deleteFiles(backupsList, filesLocationTextBlock.Text + "\\backup");
        //    unselect();
        //    mainWindow_Load();
        //}

        //private void backupButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!Directory.Exists(filesLocationTextBlock.Text + "\\backup"))
        //    {
        //        Directory.CreateDirectory(filesLocationTextBlock.Text + "\\backup");
        //    }
        //    copyFiles(filesList, filesLocationTextBlock.Text, filesLocationTextBlock.Text + "\\backup");
        //    listBox_Load(backupsList, filesLocationTextBlock.Text + "\\backup");
        //    filesList.SelectedItem = null;
        //}

        //private void unselectButton_Click(object sender, RoutedEventArgs e)
        //{
        //    unselect();
        //}
    }
}
