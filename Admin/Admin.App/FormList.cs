using Admin.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Admin.App
{
    public partial class FormList : Form
    {
        public static FormList? instance; // implement singleton pattern

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TemplateId { get; set; } = string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TemplateOutputDir { get; set; } = string.Empty;
        public List<PlaceHolderInfo> List = new List<PlaceHolderInfo>();

        public FormList()
        {
            InitializeComponent();
        }

        public static FormList GetSingleton()
        {
            if (instance == null)
            {
                instance = new FormList();
            }
            return instance;
        }


        private void FormList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = List;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();
        }

        public void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = List;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();
            TopMost = true;
            txtTemplateId.Text = TemplateId;
            txtTemplateOutputDir.Text = TemplateOutputDir;
            Show();
        }

        private void FormList_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }
    }
}
