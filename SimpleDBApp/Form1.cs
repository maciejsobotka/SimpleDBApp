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
        public SimpleDBApp()
        {
            InitializeComponent();
            dt = new DataTable("dataTable.xml");
            if (File.Exists("dataTable.xml"))
            {
                DataSet ds = new DataSet();
                ds.ReadXml("dataTable.xml");
                dt = ds.Tables[0];
            }
            else 
            {
                dt.Columns.Add("Column1");
                dt.Columns.Add("Column2");
                dt.Columns.Add("Column3");
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

        private void button1_Click(object sender, EventArgs e)
        {
            dt.WriteXml("dataTable.xml");
        }
    }
}
