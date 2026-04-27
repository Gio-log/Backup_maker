using System.Runtime.InteropServices.Marshalling;

namespace Backup_maker
{
    public partial class Backup_Maker : Form
    {
        private string appDataPath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Backup_maker_data.ini");
        public Backup_Maker()
        {
            InitializeComponent();
            if (File.Exists(appDataPath) == false)
            {
                string startingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                filesLocationLabel.Text = startingFolder;
                File.WriteAllText(appDataPath, startingFolder);

            }
            else
            {
                using (StreamReader reader = new StreamReader(appDataPath))
                {
                    filesLocationLabel.Text = reader.ReadLine();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox_Load(filesList, filesLocationLabel.Text);
            if (Directory.Exists(filesLocationLabel.Text + "\\backup"))
            {
                listBox_Load(backupsList, filesLocationLabel.Text + "\\backup");
            }
        }

        private void backupButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(filesLocationLabel.Text + "\\backup"))
            {
                Directory.CreateDirectory(filesLocationLabel.Text + "\\backup");
            }
            copyFiles(filesList, filesLocationLabel.Text, filesLocationLabel.Text + "\\backup");
            listBox_Load(backupsList, filesLocationLabel.Text + "\\backup");
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteFiles(filesList, filesLocationLabel.Text);
            deleteFiles(backupsList, filesLocationLabel.Text + "\\backup");
            Form1_Load(sender, e);
        }

        private void fileLocationButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.InitialDirectory = filesLocationLabel.Text;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.WriteAllText(appDataPath, folderBrowserDialog.SelectedPath);
                filesLocationLabel.Text = folderBrowserDialog.SelectedPath;
            }
            Form1_Load(sender, e);
        }

        private void listBox_Load(ListBox list, string path)
        {
            list.Items.Clear();
            int lengthF = path.Length + 1;
            string[] files = Directory.GetFiles(path).Select(f => f.Remove(0, lengthF)).ToArray();
            list.Items.AddRange(files);
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            copyFiles(backupsList, filesLocationLabel.Text + "\\backup", filesLocationLabel.Text);
            listBox_Load(filesList, filesLocationLabel.Text);
        }

        private void copyFiles(ListBox list, string pathA, string pathB)
        {
            ListBox.SelectedObjectCollection selectedFiles = new ListBox.SelectedObjectCollection(list);
            selectedFiles = list.SelectedItems;
            bool replace = overwriteCB.Checked;
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
            ListBox.SelectedObjectCollection selectedFiles = new ListBox.SelectedObjectCollection(list);
            selectedFiles = list.SelectedItems;
            bool replace = overwriteCB.Checked;
            if (list.SelectedIndex != -1)
            {
                for (int i = selectedFiles.Count - 1; i >= 0; i--)
                {
                    File.Delete(path + "\\" + selectedFiles[i]);
                }
            }
        }

        private void unselectButton_Click(object sender, EventArgs e)
        {
            filesList.SelectedItem = null;
            backupsList.SelectedItem = null;
        }
    }
}
