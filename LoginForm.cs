using Itecx.Util.DBBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;
using System.Data.Sql;
using Itecx.Util;

namespace Itecx.Gui
{
    class LoginForm : Form
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private Label label4;
        private ComboBox authenticationComboBox;
        private Panel panel1;
        private Button connectButton;
        private Button cancelButton;
        private ComboBox serverNameComboBox;
        private PictureBox pictureBox1;
        private DBBrowserController controller;

        public LoginForm()
        {
            try
            {

                controller = DBBrowserController.GetInstance();
                InitializeComponent();
                authenticationComboBox.Text = "Windows Authentication";


                /*RegistryValueDataReader registryValueDataReader = new RegistryValueDataReader();
                string[] instances64Bit = registryValueDataReader.ReadRegistryValueData(Util.RegistryHive.Wow64,
                                                                                        Registry.LocalMachine,
                                                                                        @"SOFTWARE\Microsoft\Microsoft SQL Server",
                                                                                        "InstalledInstances");

                string[] instances32Bit = registryValueDataReader.ReadRegistryValueData(Util.RegistryHive.Wow6432,
                                                                                        Registry.LocalMachine,
                                                                                        @"SOFTWARE\Microsoft\Microsoft SQL Server",
                                                                                        "InstalledInstances");
                IList<string> localInstanceNames = new List<string>(instances64Bit);
                localInstanceNames = localInstanceNames.Union(instances32Bit).ToList();*/

                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                DataTable dataTable = instance.GetDataSources();
                foreach (System.Data.DataRow row in dataTable.Rows)
                {
                    string serverName = "";
                    string instanceName = "";
                    foreach (System.Data.DataColumn col in dataTable.Columns)
                    {
                        if ("ServerName".Equals(col.ColumnName))
                        {
                            serverName = row[col] == null ? null : row[col].ToString();
                        }
                        else if ("InstanceName".Equals(col.ColumnName))
                        {
                            instanceName = row[col] == null ? null : row[col].ToString();
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(instanceName))
                    {
                        serverName = serverName + "\\" + instanceName;
                    }
                    serverNameComboBox.Items.Add(serverName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while Loading Application " + ex.Message, "Error while Loading Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.authenticationComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.serverNameComboBox = new System.Windows.Forms.ComboBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Authentication";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Name";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(115, 79);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(176, 22);
            this.userNameTextBox.TabIndex = 4;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(115, 105);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(176, 22);
            this.passwordTextBox.TabIndex = 5;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // authenticationComboBox
            // 
            this.authenticationComboBox.FormattingEnabled = true;
            this.authenticationComboBox.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.authenticationComboBox.Location = new System.Drawing.Point(115, 52);
            this.authenticationComboBox.Name = "authenticationComboBox";
            this.authenticationComboBox.Size = new System.Drawing.Size(176, 24);
            this.authenticationComboBox.TabIndex = 7;
            this.authenticationComboBox.SelectedIndexChanged += new System.EventHandler(this.authenticationComboBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.serverNameComboBox);
            this.panel1.Controls.Add(this.userNameTextBox);
            this.panel1.Controls.Add(this.authenticationComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.passwordTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 158);
            this.panel1.TabIndex = 8;
            // 
            // serverNameComboBox
            // 
            this.serverNameComboBox.FormattingEnabled = true;
            this.serverNameComboBox.Location = new System.Drawing.Point(115, 26);
            this.serverNameComboBox.Name = "serverNameComboBox";
            this.serverNameComboBox.Size = new System.Drawing.Size(176, 24);
            this.serverNameComboBox.TabIndex = 8;
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.connectButton.Location = new System.Drawing.Point(48, 278);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 9;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(230, 278);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(90, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 91);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(347, 313);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.Text = "Login";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private void authenticationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("SQL Server Authentication".Equals(authenticationComboBox.Text))
            {
                userNameTextBox.Enabled = true;
                passwordTextBox.Enabled = true;
            }
            else
            {
                userNameTextBox.Enabled = false;
                passwordTextBox.Enabled = false;
                userNameTextBox.ResetText();
                passwordTextBox.ResetText();
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string server = serverNameComboBox.Text;
                string authentication = authenticationComboBox.Text;
                string user = userNameTextBox.Text;
                string password = passwordTextBox.Text;

                controller.SetSqlConnection(server, null, user, password);

                DbBrowserPanel dbBrowserPanel = new DbBrowserPanel();
                dbBrowserPanel.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while connecting to DB and fetching table list\r\n" + ex.Message, "Error while connecting to DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
