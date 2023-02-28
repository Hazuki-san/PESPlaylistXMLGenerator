using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;

namespace PESPlaylistXMLGenerator
{
    public partial class PESPlaylistXMLData : Form
    {
        public PESPlaylistXMLData()
        {
            InitializeComponent();
        }

        private string filePath;
        private DataTable dataTable = new DataTable();

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML Files|*.xml";
            openFileDialog1.Title = "Select a Playlist File";
            openFileDialog1.FileName = "Playlist.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                dataTable.Clear(); // Clear the table before adding new data
                xmlData.Rows.Clear(); // Clear the rows

                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);
                    XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/Playlist/CueList/Label");

                    foreach (XmlNode node in nodeList)
                    {
                        string title = node.Attributes["Title"].Value;
                        string artist = node.Attributes["Artist"].Value;

                        if (!dataTable.Columns.Contains("Title")) // check if Title column exists
                        {
                            dataTable.Columns.Add("Title", typeof(string)); // add Title column if it doesn't exist
                        }

                        if (!dataTable.Columns.Contains("Artist")) // check if Artist column exists
                        {
                            dataTable.Columns.Add("Artist", typeof(string)); // add Artist column if it doesn't exist
                        }

                        dataTable.Rows.Add(title, artist);
                    }

                    // Copy the rows from the dataTable to the xmlData DataGridView
                    foreach (DataRow row in dataTable.Rows)
                    {
                        xmlData.Rows.Add(row.ItemArray);
                    }

                    MessageBox.Show("Data loaded successfully.", "PES Playlist.xml Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private DataTable CreateResultDataTable()
        {
            DataTable result = new DataTable();
            foreach (DataGridViewColumn col in xmlData.Columns)
            {
                result.Columns.Add(col.HeaderText);
            }
            for (int i = 0; i < xmlData.Rows.Count - 1; i++)
            {
                DataGridViewRow row = xmlData.Rows[i];
                DataRow dataRow = result.NewRow();
                for (int j = 0; j < xmlData.Columns.Count; j++)
                {
                    dataRow[j] = row.Cells[j].Value;
                }
                result.Rows.Add(dataRow);
            }
            return result;
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Please open a file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DataTable dataTable = CreateResultDataTable();

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("There is no data to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", null, null);
                xmlDeclaration.Standalone = "yes";
                xmlDoc.AppendChild(xmlDeclaration);

                XmlElement root = xmlDoc.CreateElement("Playlist");
                root.SetAttribute("converter_version", "20230001"); //
                root.SetAttribute("wepes", GetYearFromComboBox());
                root.SetAttribute("target", "CS");
                root.SetAttribute("version", "0");
                root.SetAttribute("create", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture));
                xmlDoc.AppendChild(root);

                XmlElement cueList = xmlDoc.CreateElement("CueList");
                cueList.SetAttribute("Num", dataTable.Rows.Count.ToString());
                root.AppendChild(cueList);

                foreach (DataRow row in dataTable.Rows)
                {
                    // Create a CueName attribute based on the row index
                    string cueName = string.Format("BGM0_LIC_MENU_{0:00}", dataTable.Rows.IndexOf(row) + 1);
                    int songId = dataTable.Rows.IndexOf(row) + 19000;

                    XmlElement label = xmlDoc.CreateElement("Label");
                    label.SetAttribute("CueName", cueName);
                    label.SetAttribute("Id", songId.ToString());
                    label.SetAttribute("Title", row["Title"].ToString());
                    label.SetAttribute("Artist", row["Artist"].ToString());
                    label.SetAttribute("Offset", "0");
                    label.SetAttribute("RemainTimeForFadeoutStart", "0");
                    label.SetAttribute("FadeoutTime", "0");
                    label.SetAttribute("FadeoutWaitTime", "0");

                    // Add the Label element to the CueList element
                    cueList.AppendChild(label);
                }


                XmlElement defaultPlayList = xmlDoc.CreateElement("DefaultPlayList");
                defaultPlayList.SetAttribute("Num", "2");
                root.AppendChild(defaultPlayList);

                XmlElement menuList = xmlDoc.CreateElement("List");
                menuList.SetAttribute("ListName", "Menu");
                menuList.SetAttribute("NumContents", dataTable.Rows.Count.ToString());
                defaultPlayList.AppendChild(menuList);

                foreach (DataRow row in dataTable.Rows)
                {
                    int songId = dataTable.Rows.IndexOf(row) + 19000;
                    XmlElement label = xmlDoc.CreateElement("Label");
                    label.SetAttribute("Id", songId.ToString());
                    menuList.AppendChild(label);
                }

                XmlElement highlightList = xmlDoc.CreateElement("List");
                highlightList.SetAttribute("ListName", "Highlight");
                highlightList.SetAttribute("OffsetStart", "1");
                highlightList.SetAttribute("NumContents", dataTable.Rows.Count.ToString());
                defaultPlayList.AppendChild(highlightList);

                foreach (DataRow row in dataTable.Rows)
                {
                    int songId = dataTable.Rows.IndexOf(row) + 19000;
                    XmlElement label = xmlDoc.CreateElement("Label");
                    label.SetAttribute("Id", songId.ToString());
                    highlightList.AppendChild(label);
                }

                XmlElement regionTopBgm = xmlDoc.CreateElement("RegionTopBgm");
                regionTopBgm.SetAttribute("Num", "1");
                root.AppendChild(regionTopBgm);

                XmlElement regionTopBgmLabel = xmlDoc.CreateElement("Label");
                regionTopBgmLabel.SetAttribute("Region", "def");
                regionTopBgmLabel.SetAttribute("Id", "-1");
                regionTopBgm.AppendChild(regionTopBgmLabel);

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.FileName = "Playlist.xml";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;

                    // Create XmlWriterSettings with indent set to true
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.IndentChars = "\t";

                    // Use XmlWriter to write the XML to the file with the specified settings
                    using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                    {
                        xmlDoc.Save(writer);
                    }

                    MessageBox.Show("Data saved successfully.", "PES Playlist.xml Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (xmlData.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Loop through the selected rows and delete them
                foreach (DataGridViewRow row in xmlData.SelectedRows)
                {
                    // Skip the new row
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    xmlData.Rows.RemoveAt(row.Index);
                }
            }
        }

        private string GetYearFromComboBox()
        {
            string selectedValue = pesVersions.SelectedItem.ToString();
            string year = selectedValue.Substring(selectedValue.Length - 2);
            return year;
        }


    }
}
