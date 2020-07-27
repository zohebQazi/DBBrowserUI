using Itecx.Util.Chooser;
using Itecx.Util.DBBrowser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Itecx.Gui
{
    class DbBrowserPanel : Form
    {
        private TextBox sqlTextBox;
        private Label label4;
        private DataGridView dataGridView1;
        private Button executeButton;
        private Button clearAllButton;
        private ListBox tableListBox;
        private string tableName;
        private TableView view;
        private SplitContainer splitContainer3;
        private Panel panel1;
        private SplitContainer splitContainer4;
        private Panel panel2;
        private Panel panel3;
        private TextBox searchTableTextBox;
        private DBBrowserController controller = DBBrowserController.GetInstance();
        private ComboBox databaseComboBox;
        private SplitContainer splitContainer1;
        private Panel panel4;
        private Label label1;
        private Label label2;
        private DataGridView operationGridView;
        private DataGridViewCheckBoxColumn Executed;
        private DataGridViewTextBoxColumn Operation;
        private List<string> tabelList;
        private Color updateColor = Color.Orange;
        private Color insertColor = Color.LightGreen;
        private Color deleteColor = Color.PaleVioletRed;
        private DateTimePicker dtp = new DateTimePicker();
        private Rectangle rectangle;

        public DbBrowserPanel()
        {
            InitializeComponent();
            this.splitContainer3.SplitterDistance = splitContainer3.Width * 25 / 100;
            InitDomain();

            dataGridView1.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            dtp.TextChanged += new EventHandler(dtp_TextChange);
        }

        private void InitDomain()
        {
            List<string> dbList = controller.GetDataBaseList();
            foreach (string db in dbList)
            {
                databaseComboBox.Items.Add(db);
            }
            databaseComboBox.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbBrowserPanel));
            this.sqlTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.executeButton = new System.Windows.Forms.Button();
            this.clearAllButton = new System.Windows.Forms.Button();
            this.tableListBox = new System.Windows.Forms.ListBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.databaseComboBox = new System.Windows.Forms.ComboBox();
            this.searchTableTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.operationGridView = new System.Windows.Forms.DataGridView();
            this.Executed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operationGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // sqlTextBox
            // 
            this.sqlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sqlTextBox.Location = new System.Drawing.Point(3, 33);
            this.sqlTextBox.Multiline = true;
            this.sqlTextBox.Name = "sqlTextBox";
            this.sqlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sqlTextBox.Size = new System.Drawing.Size(732, 297);
            this.sqlTextBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Sql";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(733, 304);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            this.dataGridView1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView1_Scroll);
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            // 
            // executeButton
            // 
            this.executeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.executeButton.Location = new System.Drawing.Point(578, 6);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(75, 23);
            this.executeButton.TabIndex = 11;
            this.executeButton.Text = "Execute";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // clearAllButton
            // 
            this.clearAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearAllButton.Location = new System.Drawing.Point(659, 6);
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(75, 23);
            this.clearAllButton.TabIndex = 12;
            this.clearAllButton.Text = "Clear All";
            this.clearAllButton.UseVisualStyleBackColor = true;
            this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
            // 
            // tableListBox
            // 
            this.tableListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableListBox.FormattingEnabled = true;
            this.tableListBox.Location = new System.Drawing.Point(1, 85);
            this.tableListBox.Name = "tableListBox";
            this.tableListBox.Size = new System.Drawing.Size(167, 563);
            this.tableListBox.Sorted = true;
            this.tableListBox.TabIndex = 13;
            this.tableListBox.SelectedIndexChanged += new System.EventHandler(this.tableListBox_SelectedIndexChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(996, 664);
            this.splitContainer3.SplitterDistance = 172;
            this.splitContainer3.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.databaseComboBox);
            this.panel1.Controls.Add(this.searchTableTextBox);
            this.panel1.Controls.Add(this.tableListBox);
            this.panel1.Location = new System.Drawing.Point(3, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 656);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Data Source";
            // 
            // databaseComboBox
            // 
            this.databaseComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseComboBox.FormattingEnabled = true;
            this.databaseComboBox.Location = new System.Drawing.Point(1, 33);
            this.databaseComboBox.Name = "databaseComboBox";
            this.databaseComboBox.Size = new System.Drawing.Size(167, 21);
            this.databaseComboBox.TabIndex = 15;
            this.databaseComboBox.SelectedIndexChanged += new System.EventHandler(this.databaseComboBox_SelectedIndexChanged);
            // 
            // searchTableTextBox
            // 
            this.searchTableTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTableTextBox.Location = new System.Drawing.Point(1, 60);
            this.searchTableTextBox.Name = "searchTableTextBox";
            this.searchTableTextBox.Size = new System.Drawing.Size(167, 20);
            this.searchTableTextBox.TabIndex = 14;
            this.searchTableTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.Location = new System.Drawing.Point(3, 2);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label2);
            this.splitContainer4.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.panel3);
            this.splitContainer4.Size = new System.Drawing.Size(811, 659);
            this.splitContainer4.SplitterDistance = 339;
            this.splitContainer4.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(755, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "User Action";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.clearAllButton);
            this.panel2.Controls.Add(this.sqlTextBox);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.executeButton);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(738, 333);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Location = new System.Drawing.Point(3, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(741, 317);
            this.panel3.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(1141, 664);
            this.splitContainer1.SplitterDistance = 922;
            this.splitContainer1.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.operationGridView);
            this.panel4.Location = new System.Drawing.Point(3, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(209, 656);
            this.panel4.TabIndex = 1;
            // 
            // operationGridView
            // 
            this.operationGridView.AllowUserToAddRows = false;
            this.operationGridView.AllowUserToDeleteRows = false;
            this.operationGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.operationGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.operationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.operationGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Executed,
            this.Operation});
            this.operationGridView.Location = new System.Drawing.Point(0, 3);
            this.operationGridView.Name = "operationGridView";
            this.operationGridView.ReadOnly = true;
            this.operationGridView.RowHeadersVisible = false;
            this.operationGridView.RowHeadersWidth = 51;
            this.operationGridView.RowTemplate.Height = 24;
            this.operationGridView.Size = new System.Drawing.Size(206, 640);
            this.operationGridView.TabIndex = 1;
            this.operationGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.operationGridView_CellContentClick);
            // 
            // Executed
            // 
            this.Executed.Frozen = true;
            this.Executed.HeaderText = "";
            this.Executed.MinimumWidth = 6;
            this.Executed.Name = "Executed";
            this.Executed.ReadOnly = true;
            this.Executed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Executed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Executed.Width = 25;
            // 
            // Operation
            // 
            this.Operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Operation.HeaderText = "Operation";
            this.Operation.MinimumWidth = 6;
            this.Operation.Name = "Operation";
            this.Operation.ReadOnly = true;
            // 
            // DbBrowserPanel
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1142, 664);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DbBrowserPanel";
            this.Text = "Itec Static Maintenance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DbBrowserPanel_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.operationGridView)).EndInit();
            this.ResumeLayout(false);

        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = sqlTextBox.SelectedText;
                if (string.IsNullOrWhiteSpace(sql))
                {
                    sql = sqlTextBox.Text;
                }
                if (string.IsNullOrWhiteSpace(sql))
                {
                    MessageBox.Show("SQL is blank", "SQL is blank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (sql.ToUpper().StartsWith("SELECT"))
                {
                    view = controller.ExecuteSelectSQL(tableName, sql);
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    foreach (string col in view.Columns.Keys)
                    {
                        DataChooser dataChooser = null;
                        foreach (DataChooser chooser in view.DataChoosers)
                        {
                            if (col.Equals(chooser.Column))
                            {
                                dataChooser = chooser;
                            }
                        }

                        DataGridViewColumn gridColumn = null;
                        if (dataChooser == null)
                        {
                            if (view.Columns[col] == typeof(DateTime))
                            {
                                gridColumn = getDataGridViewTextBoxColumn(col, true);
                            }
                            else
                            {
                                gridColumn = getDataGridViewTextBoxColumn(col, false);
                            }
                        }
                        else
                        {
                            gridColumn = getDataGridViewColumn(dataChooser);
                        }
                        dataGridView1.Columns.Add(gridColumn);

                        if (DBBrowserConstant.DBBROWSER_ACTION.Equals(col))
                        {
                            gridColumn.Visible = false;
                        }
                    }
                    foreach (List<object> row in view.Data)
                    {
                        dataGridView1.Rows.Add(row.ToArray());
                    }
                }
                else
                {
                    string[] sqlArray = controller.ExecuteSQL(view, sql);
                    foreach (string sqlStr in sqlArray)
                    {
                        foreach (DataGridViewRow row in operationGridView.Rows)
                        {
                            object sqlObj = row.Cells[Operation.Index].Value;
                            if (sqlObj != null && sqlStr.Equals(sqlObj.ToString()))
                            {
                                row.Cells[Executed.Index].Value = true;
                            }
                        }
                    }
                }
                MessageBox.Show("Sql execution successful", "Sql execution successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.splitContainer4.SplitterDistance = this.splitContainer4.Height * 40 / 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while Executing sql\r\n" + ex.Message, "Error while Executing sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tableListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string table = tableListBox.GetItemText(tableListBox.SelectedItem);
            sqlTextBox.Text = "SELECT * FROM " + table;
            tableName = table;
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Dictionary<string, object> colObjectDict = new Dictionary<string, object>();
                foreach (DataGridViewCell cell in e.Row.Cells)
                {
                    DataGridViewColumn col = dataGridView1.Columns[cell.ColumnIndex];
                    colObjectDict.Add(col.Name, cell.Value);
                }
                string deleteKey = controller.UpdateDeleteSql(colObjectDict, view);
                updateDBActionCell(e.Row, deleteKey);
                updateSql(view);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting row\r\n" + ex.Message, "Error while deleting row", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string dbAction = getDBActionCell(dataGridView1.Rows[e.RowIndex]);
            string action = "";

            Dictionary<string, object> colObjectDict = new Dictionary<string, object>();
            Dictionary<string, bool> colChangeDict = new Dictionary<string, bool>();
            foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
            {
                DataGridViewColumn col = dataGridView1.Columns[cell.ColumnIndex];
                colObjectDict.Add(col.Name, cell.Value);
                if(cell.Style.BackColor == updateColor)
                {
                    colChangeDict.Add(col.Name,true);
                }
                else
                {
                    colChangeDict.Add(col.Name, false);
                }
            }
            if (string.IsNullOrWhiteSpace(dbAction) || dbAction.StartsWith(DBBrowserConstant.INSERT))
            {
                action = controller.UpdateInsertSql(colObjectDict, view);
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = insertColor;
            }
            if (DBBrowserConstant.FROM_TABLE.Equals(dbAction) || dbAction.StartsWith(DBBrowserConstant.UPDATE))
            {
                action = controller.UpdateUpdateSql(colObjectDict, colChangeDict, view);
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = updateColor;
            }
            updateDBActionCell(dataGridView1.Rows[e.RowIndex], action);
            updateSql(view);
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            DataGridViewColumn col = dataGridView1.Columns[colIndex];
            foreach (DataChooser chooser in view.DataChoosers)
            {
                if (chooser.IsChooser() && col.Name.Equals(chooser.Column))
                {
                    string className = DBBrowserConstant.DATA_CHOOSER_PACKAGE + "." + chooser.Chooser + "ChooserForm";
                    Type chooserType = Type.GetType(className);
                    if (chooserType == null)
                    {
                        MessageBox.Show("Chooser " + className + " is not implemented", "Chooser " + className + " is not implemented", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    object chooserObj = Activator.CreateInstance(chooserType);
                    if (chooserObj is ChooserForm)
                    {
                        using (ChooserForm chooserForm = (ChooserForm)chooserObj)
                        {
                            chooserForm.ShowDialog(this);
                            dataGridView1.Rows[e.RowIndex].Cells[colIndex].Value = chooserForm.SelectedValue;
                            return;
                        }
                    }
                }
            }
        }

        private void updateDBActionCell(DataGridViewRow row, string value)
        {
            DataGridViewColumnCollection cols = dataGridView1.Columns;
            foreach (DataGridViewColumn col in cols)
            {
                if ("DBBROWSER_ACTION".Equals(col.Name))
                {
                    row.Cells[dataGridView1.Columns.IndexOf(col)].Value = value;
                }
            }
        }
        private string getDBActionCell(DataGridViewRow row)
        {
            DataGridViewColumnCollection cols = dataGridView1.Columns;
            foreach (DataGridViewColumn col in cols)
            {
                if ("DBBROWSER_ACTION".Equals(col.Name))
                {
                    object value = row.Cells[dataGridView1.Columns.IndexOf(col)].Value;
                    if (value != null)
                    {
                        return value.ToString();
                    }
                }
            }
            return "";
        }


        private void updateSql(TableView view)
        {
            operationGridView.Rows.Clear();
            foreach (string key in view.ActionPerformed.Keys)
            {
                string sql = view.ActionPerformed[key];
                int rowIndex = operationGridView.Rows.Add(false, sql);
                if (sql.ToUpper().StartsWith("INSERT"))
                {
                    operationGridView.Rows[rowIndex].DefaultCellStyle.BackColor = insertColor;
                }
                else if (sql.ToUpper().StartsWith("DELETE"))
                {
                    operationGridView.Rows[rowIndex].DefaultCellStyle.BackColor = deleteColor;
                }
                else if (sql.ToUpper().StartsWith("UPDATE"))
                {
                    operationGridView.Rows[rowIndex].DefaultCellStyle.BackColor = updateColor;
                }
            }
        }
        //private void sqlTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    int splitDistance = this.splitContainer4.Height * 70 / 100;
        //    if (this.splitContainer4.SplitterDistance == splitDistance)
        //    {
        //        splitDistance = this.splitContainer4.Height * 40 / 100;
        //    }
        //    this.splitContainer4.SplitterDistance = splitDistance;
        //}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tabelList == null)
            {
                return;
            }
            string filter = searchTableTextBox.Text;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
            }
            tableListBox.Items.Clear();

            foreach (string table in tabelList)
            {
                if (string.IsNullOrWhiteSpace(filter) || table.Contains(filter))
                {
                    tableListBox.Items.Add(table);
                }
            }
        }

        private void DbBrowserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void databaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.SetSqlConnection(controller.ServerName, databaseComboBox.Text, controller.User, controller.Password);
            tabelList = controller.GetTableList();
            tableListBox.Items.Clear();
            foreach (string table in tabelList)
            {
                tableListBox.Items.Add(table);
            }

            sqlTextBox.ResetText();
            operationGridView.Rows.Clear();
        }

        private void actionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";
            foreach (string selectedSQL in operationGridView.SelectedRows)
            {
                sql = sql + selectedSQL + "\r\n\r\n";
            }
            sqlTextBox.Text = sql;
        }

        private void clearAllButton_Click(object sender, EventArgs e)
        {
            sqlTextBox.Clear();
            operationGridView.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
        }

        private DataGridViewTextBoxColumn getDataGridViewTextBoxColumn(string col, bool readOnly)
        {
            DataGridViewTextBoxColumn gridColumn = new DataGridViewTextBoxColumn();
            gridColumn.Name = col;
            gridColumn.HeaderText = col;
            gridColumn.ReadOnly = readOnly;
            gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            return gridColumn;
        }

        private DataGridViewComboBoxColumn getDataGridViewComboBoxColumn(string col, List<string> values)
        {
            DataGridViewComboBoxColumn gridColumn = new DataGridViewComboBoxColumn();
            gridColumn.Name = col;
            gridColumn.HeaderText = col;
            gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            if (values != null)
            {
                gridColumn.Items.AddRange(values.ToArray());
            }
            return gridColumn;
        }

        private DataGridViewColumn getDataGridViewColumn(DataChooser chooser)
        {
            string col = chooser.Column;
            if (chooser.IsSQL())
            {
                if (string.IsNullOrWhiteSpace(chooser.Sql))
                {
                    throw new Exception("Data Chooser config for colum " + chooser.Column + " is set as SQL but the SQL is kept as blank");
                }
                List<List<object>> result = controller.ExecuteSelectSQL(chooser.Sql, false);
                List<string> values = new List<string>();
                foreach (List<object> row in result)
                {
                    values.Add(row[0].ToString());
                }
                return getDataGridViewComboBoxColumn(chooser.Column, values);
            }
            else if (chooser.IsChooser())
            {
                DataGridViewTextBoxColumn column = getDataGridViewTextBoxColumn(chooser.Column, false);
                return column;
            }
            else if (chooser.IsTable())
            {
                if (string.IsNullOrWhiteSpace(chooser.SourceColumn))
                {
                    throw new Exception("Data Chooser config for colum " + chooser.Column + " is set as Table but the SourceColumn is kept as blank");
                }
                if (string.IsNullOrWhiteSpace(chooser.SourceTable))
                {
                    throw new Exception("Data Chooser config for colum " + chooser.Column + " is set as Table but the SourceTable is kept as blank");
                }
                string sql = "SELECT DISTINCT(" + chooser.SourceColumn + ") FROM " + chooser.SourceTable;

                List<List<object>> result = controller.ExecuteSelectSQL(sql, false);
                List<string> values = new List<string>();
                foreach (List<object> row in result)
                {
                    values.Add(row[0].ToString());
                }
                return getDataGridViewComboBoxColumn(chooser.Column, values);
            }
            else
            {
                throw new Exception("Data Chooser config for colum " + chooser.Column + " is set with a invalid Source " + chooser.ChooserType);
            }
        }

        private void operationGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex != Operation.Index)
            {
                return;
            }
            sqlTextBox.Clear();
            foreach (DataGridViewCell cell in operationGridView.SelectedCells)
            {
                object sqlObj = operationGridView.Rows[cell.RowIndex].Cells[Operation.Index].Value;
                if (sqlObj != null)
                {
                    if (string.IsNullOrWhiteSpace(sqlTextBox.Text))
                    {
                        sqlTextBox.Text = sqlObj.ToString();
                    }
                    else
                    {
                        sqlTextBox.Text = sqlTextBox.Text  + "\r\n\r\n" + sqlObj.ToString();
                    }
                }
            }
        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = dtp.Text.ToString();
        }
        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;

        }
        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            string dbAction = getDBActionCell(dataGridView1.Rows[e.RowIndex]);
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            Type dataType = view.Columns[colName];
            if(dataType == typeof(DateTime))
            {
                rectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                dtp.Size = new Size(rectangle.Width, rectangle.Height);
                dtp.Location = new Point(rectangle.X, rectangle.Y);
                dtp.Visible = true;
            }
        }
    }
}
