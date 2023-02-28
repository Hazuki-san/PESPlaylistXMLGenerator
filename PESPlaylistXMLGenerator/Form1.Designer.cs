using System.Windows.Forms;

namespace PESPlaylistXMLGenerator
{
    partial class PESPlaylistXMLData : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.xmlData = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.pesVersions = new System.Windows.Forms.ComboBox();
            this.someRandoText = new System.Windows.Forms.Label();
            this.deleteRow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xmlData)).BeginInit();
            this.SuspendLayout();
            // 
            // xmlData
            // 
            this.xmlData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.xmlData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Artist});
            this.xmlData.Location = new System.Drawing.Point(12, 12);
            this.xmlData.Name = "xmlData";
            this.xmlData.Size = new System.Drawing.Size(560, 400);
            this.xmlData.TabIndex = 0;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.Width = 275;
            // 
            // Artist
            // 
            this.Artist.HeaderText = "Artist";
            this.Artist.MaxInputLength = 64;
            this.Artist.Name = "Artist";
            this.Artist.Width = 220;
            // 
            // openBtn
            // 
            this.openBtn.Location = new System.Drawing.Point(12, 429);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(75, 23);
            this.openBtn.TabIndex = 1;
            this.openBtn.Text = "Open";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(92, 429);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pesVersions
            // 
            this.pesVersions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.pesVersions.FormattingEnabled = true;
            this.pesVersions.Items.AddRange(new object[] {
            "2017",
            "2018",
            "2019",
            "2020",
            "2021"});
            this.pesVersions.Location = new System.Drawing.Point(496, 430);
            this.pesVersions.Name = "pesVersions";
            this.pesVersions.Size = new System.Drawing.Size(75, 21);
            this.pesVersions.TabIndex = 3;
            this.pesVersions.SelectedIndex = 4;
            // 
            // someRandoText
            // 
            this.someRandoText.AutoSize = true;
            this.someRandoText.Location = new System.Drawing.Point(258, 427);
            this.someRandoText.Name = "someRandoText";
            this.someRandoText.Size = new System.Drawing.Size(235, 26);
            this.someRandoText.TabIndex = 4;
            this.someRandoText.Text = "Select your PES version, if there\'s nothing you\'re\r\nlooking for then just put you" +
    "r PES version in box";
            this.someRandoText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // deleteRow
            // 
            this.deleteRow.Location = new System.Drawing.Point(172, 429);
            this.deleteRow.Name = "deleteRow";
            this.deleteRow.Size = new System.Drawing.Size(75, 23);
            this.deleteRow.TabIndex = 5;
            this.deleteRow.Text = "Delete";
            this.deleteRow.UseVisualStyleBackColor = true;
            this.deleteRow.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // PESPlaylistXMLData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.deleteRow);
            this.Controls.Add(this.someRandoText);
            this.Controls.Add(this.pesVersions);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.xmlData);
            this.MaximizeBox = false;
            this.Name = "PESPlaylistXMLData";
            this.Text = "PES Playlist.xml Generator";
            ((System.ComponentModel.ISupportInitialize)(this.xmlData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DataGridView xmlData;
        private Button openBtn;
        private Button saveBtn;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn Artist;
        private ComboBox pesVersions;
        private Label someRandoText;
        private Button deleteRow;
    }
}
