namespace Backup_maker
{
    partial class Backup_Maker
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fLabel = new Label();
            bLabel = new Label();
            backupButton = new Button();
            deleteButton = new Button();
            filesList = new ListBox();
            backupsList = new ListBox();
            filesLocationButton = new Button();
            filesLocationLabel = new Label();
            restoreButton = new Button();
            SuspendLayout();
            // 
            // fLabel
            // 
            fLabel.AutoSize = true;
            fLabel.Location = new Point(210, 126);
            fLabel.Margin = new Padding(4, 0, 4, 0);
            fLabel.Name = "fLabel";
            fLabel.Size = new Size(41, 21);
            fLabel.TabIndex = 0;
            fLabel.Text = "Files";
            // 
            // bLabel
            // 
            bLabel.AutoSize = true;
            bLabel.Location = new Point(649, 126);
            bLabel.Margin = new Padding(4, 0, 4, 0);
            bLabel.Name = "bLabel";
            bLabel.Size = new Size(67, 21);
            bLabel.TabIndex = 1;
            bLabel.Text = "Backups";
            // 
            // backupButton
            // 
            backupButton.Location = new Point(761, 74);
            backupButton.Margin = new Padding(4);
            backupButton.Name = "backupButton";
            backupButton.Size = new Size(96, 32);
            backupButton.TabIndex = 2;
            backupButton.Text = "Backup";
            backupButton.UseVisualStyleBackColor = true;
            backupButton.Click += backupButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(584, 74);
            deleteButton.Margin = new Padding(4);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(168, 32);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "Delete selected files";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // filesList
            // 
            filesList.FormattingEnabled = true;
            filesList.Location = new Point(15, 151);
            filesList.Margin = new Padding(4);
            filesList.Name = "filesList";
            filesList.SelectionMode = SelectionMode.MultiExtended;
            filesList.Size = new Size(377, 550);
            filesList.TabIndex = 4;
            // 
            // backupsList
            // 
            backupsList.FormattingEnabled = true;
            backupsList.Location = new Point(480, 151);
            backupsList.Margin = new Padding(4);
            backupsList.Name = "backupsList";
            backupsList.SelectionMode = SelectionMode.MultiExtended;
            backupsList.Size = new Size(377, 550);
            backupsList.TabIndex = 5;
            // 
            // filesLocationButton
            // 
            filesLocationButton.Location = new Point(584, 17);
            filesLocationButton.Margin = new Padding(4);
            filesLocationButton.Name = "filesLocationButton";
            filesLocationButton.Size = new Size(168, 32);
            filesLocationButton.TabIndex = 6;
            filesLocationButton.Text = "Change files source";
            filesLocationButton.UseVisualStyleBackColor = true;
            filesLocationButton.Click += fileLocationButton_Click;
            // 
            // filesLocationLabel
            // 
            filesLocationLabel.AutoSize = true;
            filesLocationLabel.Location = new Point(27, 22);
            filesLocationLabel.Margin = new Padding(4, 0, 4, 0);
            filesLocationLabel.Name = "filesLocationLabel";
            filesLocationLabel.Size = new Size(104, 21);
            filesLocationLabel.TabIndex = 7;
            filesLocationLabel.Text = "Files Location";
            // 
            // restoreButton
            // 
            restoreButton.Location = new Point(761, 17);
            restoreButton.Margin = new Padding(4);
            restoreButton.Name = "restoreButton";
            restoreButton.Size = new Size(96, 32);
            restoreButton.TabIndex = 8;
            restoreButton.Text = "Restore";
            restoreButton.UseVisualStyleBackColor = true;
            // 
            // Backup_Maker
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 706);
            Controls.Add(restoreButton);
            Controls.Add(filesLocationLabel);
            Controls.Add(filesLocationButton);
            Controls.Add(backupsList);
            Controls.Add(filesList);
            Controls.Add(deleteButton);
            Controls.Add(backupButton);
            Controls.Add(bLabel);
            Controls.Add(fLabel);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "Backup_Maker";
            Text = "Backup Maker";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected Label fLabel;
        protected Label bLabel;
        private Button backupButton;
        private Button deleteButton;
        private ListBox filesList;
        private ListBox backupsList;
        private Button filesLocationButton;
        private Label filesLocationLabel;
        private Button restoreButton;
    }
}
