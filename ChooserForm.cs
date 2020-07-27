using Itecx.Util.DBBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Itecx.Util.Chooser
{
    class ChooserForm : Form
    {
        private DataGridView dataGridView1;
        private Panel panel2;
        private Button cancelButton;
        private Button selectButton;
        private Panel panel1;
        private DBBrowserController controller;
        private int identifierIndex;
        public string Table { get; set; }
        public string Identifier { get; set; }
        public object SelectedValue { get; set; }
        //For Complex Chooser
        public ChooserForm()
        {

        }
        public ChooserForm(string table, string identifier)
        {
            Table = table;
            Identifier = identifier;
            InitializeComponent();
            InitDomain();
        }
        protected void InitDomain()
        {
            if (string.IsNullOrEmpty(Table))
            {
                MessageBox.Show("Table is not populated for chooser", "Table is not populated for chooser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            controller = DBBrowserController.GetInstance();
            string sql = "SELECT * FROM " + Table;
            List<List<object>> result = controller.ExecuteSelectSQL(sql,true);
            if(result!= null && result.Count > 0)
            {
                List<object> columns = result[0];
                foreach(object obj in columns)
                {
                    DataGridViewColumn column = getDataGridViewTextBoxColumn((string)obj);
                    dataGridView1.Columns.Add(column);
                    if (obj.Equals(Identifier))
                    {
                        identifierIndex = columns.IndexOf(obj);
                    }
                }

                for(int i=1; i< result.Count; i++)
                {
                    List<object> row = result[i];
                    dataGridView1.Rows.Add(row.ToArray());
                }
            }
        }
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(5, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 411);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(694, 405);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Controls.Add(this.selectButton);
            this.panel2.Location = new System.Drawing.Point(7, 429);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(704, 30);
            this.panel2.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(624, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectButton.Location = new System.Drawing.Point(5, 3);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 0;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // ChooserForm
            // 
            this.ClientSize = new System.Drawing.Size(718, 467);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ChooserForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            SelectedValue = row.Cells[identifierIndex].Value;
            Close();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            if(rows.Count > 0)
            {
                SelectedValue = rows[0].Cells[identifierIndex].Value;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a row", "Please select a row", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SelectedValue = "";
            Close();
        }

        private DataGridViewTextBoxColumn getDataGridViewTextBoxColumn(string col)
        {
            DataGridViewTextBoxColumn gridColumn = new DataGridViewTextBoxColumn();
            gridColumn.DataPropertyName = col;
            gridColumn.Name = col;
            gridColumn.HeaderText = col;
            gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            return gridColumn;
        }

    }
}
