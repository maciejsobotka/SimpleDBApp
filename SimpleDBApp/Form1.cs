using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleDBApp
{
    public partial class SimpleDBApp : Form
    {
        private DataTable dt;
        private bool newRow = false;
        public SimpleDBApp()
        {
            InitializeComponent();
            InitializeDataGrid();

        }

        //=====================================================================
        // Data Init
        private void InitializeDataGrid()
        {
            dt = new DataTable("dataTable");
            if (File.Exists("dataTable.xml"))
            {
                DataSet ds = new DataSet();
                ds.ReadXml("dataTable.xml");
                dt = ds.Tables[0];
            }
            else 
            {
                dt.Columns.Add("Imię");
                dt.Columns.Add("Nazwisko");
                dt.Columns.Add("Grupa");
                for (int i = 0; i < dt.Columns.Count; ++i )
                {
                    dt.Columns[i].DefaultValue = String.Empty;
                }
            }
            dataGridView1.DataSource = dt;

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                comboBox1.Items.Add(i+1);
            }

            if (comboBox1.Items.Count != 0)
                comboBox1.Text = comboBox1.Items[0].ToString();
            else comboBox1.Text = "No rows!";

            for (int i = 0; i < dt.Columns.Count; ++i )
            {
                comboBox2.Items.Add(dataGridView1.Columns[i].HeaderText);
            }
            comboBox2.Text = comboBox2.Items[0].ToString();
        }

        //=====================================================================
        // Record Hash
        private void button1_Click(object sender, EventArgs e)
        {
            var elem = dt.Rows[(int) comboBox1.SelectedItem - 1][dt.Columns[comboBox2.SelectedItem.ToString()].Ordinal].ToString().ToCharArray();
            for (int i = 0; i < elem.Length; ++i)
            {
                elem[i] = (char)(elem[i] + 3);
            }
            string newElem = new string(elem);
            dt.Rows[(int)comboBox1.SelectedItem - 1][dt.Columns[comboBox2.SelectedItem.ToString()].Ordinal] = newElem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var elem = dt.Rows[(int)comboBox1.SelectedItem - 1][dt.Columns[comboBox2.SelectedItem.ToString()].Ordinal].ToString().ToCharArray();
            for (int i = 0; i < elem.Length; ++i)
            {
                elem[i] = (char)(elem[i] - 3);
            }
            string newElem = new string(elem);
            dt.Rows[(int)comboBox1.SelectedItem - 1][dt.Columns[comboBox2.SelectedItem.ToString()].Ordinal] = newElem;
        }

        //=====================================================================
        // XML Serialization
        private void buttonToXML_Click(object sender, EventArgs e)
        {
            dt.WriteXml("dataTable.xml");
        }

        private void buttonFromXML_Click(object sender, EventArgs e)
        {
            dt = new DataTable("dataTable");
            if (File.Exists("dataTable.xml"))
            {
                DataSet ds = new DataSet();
                ds.ReadXml("dataTable.xml");
                dt = ds.Tables[0];
            }
            dataGridView1.DataSource = dt;
            comboBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                comboBox1.Items.Add(i + 1);
            }
            comboBox1.Text = comboBox1.Items[0].ToString();
        }

        //=====================================================================
        // UI Control
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            newRow = true;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (newRow)
            {
                comboBox1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    comboBox1.Items.Add(i + 1);
                }
                comboBox1.Text = comboBox1.Items[0].ToString();
                newRow = false;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = (e.RowIndex + 1).ToString();
            comboBox2.Text = dt.Columns[e.ColumnIndex].ColumnName;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[(int)comboBox1.SelectedItem - 1].Cells[dt.Columns[comboBox2.SelectedItem.ToString()].Ordinal];
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[(int)comboBox1.SelectedItem - 1].Cells[dt.Columns[comboBox2.SelectedItem.ToString()].Ordinal];
        }
    }
}
