using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseSys
{
    public partial class Sstatus : Form
    {
        static UserClass user = new UserClass();
        public Sstatus()
        {
            InitializeComponent();
            //ArrayList Scl = Sindex.returnList();
            //for (int i = 0; i < Scl.Count; i++)
            //{
            //    SC newsc = (SC)(Scl[i]);
            //    listView1.Items.Add(newsc.cname.ToString());
            //}
            //this.listView1.GridLines = true;
            user = Sindex.returnUser();
            label3.Text = user.UserName;
            this.pictureBox1.Controls.Add(this.label1);
            this.pictureBox1.Controls.Add(this.label2);
            this.pictureBox1.Controls.Add(this.label3);
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            SetCols(dataGridView1);
            FilllCols(dataGridView1);
        }
        private void Sstatus_Load(object sender, EventArgs e)
        {
        }

        private void FilllCols(DataGridView dataGridView1)
        {
            object[] course = DataQuery.selectedcoursesrc(user.UserAccount);
            foreach (string[] sc in course)
            {
                dataGridView1.Rows.Add(sc);
            }
            int rn = dataGridView1.RowCount;
            for (int n = 0; n < rn; n++)
            {
                if (dataGridView1.Rows[n].Cells[0].Value.ToString() == "" )
                {
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView1.Rows[n].DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }//填充表格数据

        private void SetCols(DataGridView dataGridView1)
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.ColumnHeadersVisible = true;
            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            dataGridView1.Columns[0].Name = "课程号";
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Name = "课程名称";
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Name = "授课教师";
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Name = "授课时间";
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Name = "授课地点";
            dataGridView1.Columns[4].Width = 100;
        }//设置表格格式

        private void button1_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }//回到登陆界面




    }
}
