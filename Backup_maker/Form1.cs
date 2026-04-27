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
            ListBox_Load(filesList, filesLocationLabel.Text);
            if(Directory.Exists(filesLocationLabel.Text + "\\backup"))
            {
                ListBox_Load(backupsList, filesLocationLabel.Text + "\\backup");
            }
            
        }

        private void backupButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

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
            
        }

        private void ListBox_Load(ListBox list, string path)
        {
            list.Items.Clear();
            int lengthF = path.Length + 1;
            string[] files = Directory.GetFiles(path).Select(f => f.Remove(0, lengthF)).ToArray();
            list.Items.AddRange(files);
        }
    }
}
