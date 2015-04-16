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
    public partial class Sindex : Form
    {
        static UserClass user = new UserClass();
        public Sindex()
        {
            InitializeComponent();
            user = Login.returnUser();
            label3.Text = user.UserName;
            this.pictureBox1.Controls.Add(this.label1);
            this.pictureBox1.Controls.Add(this.label2);
            this.pictureBox1.Controls.Add(this.label3); 
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            
        }
        public static UserClass returnUser()
        {
            return user;
        }

        /*private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(ListViewItem lst in listView1.SelectedItems)
            {
                if(1>0)//判断该课程是否满员，如未满员则选课成功
                {
                    SC sc1 = new SC(lst.SubItems[1].Text, lst.SubItems[3].Text, lst.SubItems[4].Text, lst.SubItems[5].Text);
                    ScList.Add(sc1);
                    MessageBox.Show("你已成功选修："+lst.SubItems[1].Text+" 授课教师："+lst.SubItems[3].Text);
                }
            }
        }*/

        
        private void button2_Click(object sender, EventArgs e)
        {
            Sstatus st = new Sstatus();
            st.Show();
            this.Hide();
        }//查看选课情况

        private void button1_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }//回到登陆界面

        private void Sindex_Load(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            //DataTable dt = new DataTable();
            //object courses = DataQuery.GetACourse();
            //dataGridView1.DataSource = courses;
            //dt.Columns.Add(new DataColumn("课程号", typeof(string)));
            //dt.Columns.Add(new DataColumn("课程名称", typeof(string)));
            //dt.Columns.Add(new DataColumn("授课教师", typeof(string)));
            //dt.Columns.Add(new DataColumn("授课时间", typeof(string)));
            //dt.Columns.Add(new DataColumn("授课地点", typeof(string)));
            //dt.Columns.Add(new DataColumn("已选", typeof(string)));
            //dt.Columns.Add(new DataColumn("限选", typeof(string)));
            //for(int i=0;i<coursenum;i++)
            //{
            //    DataRow dr = dt.NewRow();
            //}
            
            SetCols(dataGridView1);
            FillCols(dataGridView1);
        }//窗体加载事件

        private void FillCols(DataGridView dataGridView1)
        {
            int coursenum;
            coursenum = DataQuery.GetCourseNum();
            object[] course = DataQuery.coursetbsource(coursenum);
            foreach (string[] a in course)
            {
                dataGridView1.Rows.Add(a);
            }
            int rn = dataGridView1.RowCount;
            for (int n = 0; n < rn; n++)
            {
                if (dataGridView1.Rows[n].Cells[5].Value.ToString()==""||int.Parse(dataGridView1.Rows[n].Cells[5].Value.ToString()) >= int.Parse(dataGridView1.Rows[n].Cells[6].Value.ToString()))
                {
                    dataGridView1.Rows[n].ReadOnly = true;
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView1.Rows[n].DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
            AddCheckCol(dataGridView1);
        }//填充表格内容

        private void AddCheckCol(DataGridView dataGridView1)
        {
 	
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            {
                column.HeaderText = "选中";
                column.Width = 50;
                column.Name = "isSelected";
                column.FalseValue = "0";
                column.TrueValue = "1";
                column.FlatStyle = FlatStyle.Standard;
                column.ThreeState = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                column.ReadOnly = false;
                column.CellTemplate = new DataGridViewCheckBoxCell();
            }
            dataGridView1.Columns.Insert(0, column);
            int cnum = dataGridView1.RowCount;
            for (int n = 0; n < cnum; n++)
            {
                dataGridView1.Rows[n].Cells[0].Value = false;
            }
        }//添加勾选栏

        private void SetCols(DataGridView dataGridView1)
        {
            dataGridView1.ColumnCount = 7;
            dataGridView1.ColumnHeadersVisible = true;
            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            dataGridView1.Columns[0].Name = "课程号";
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Name = "课程名称";
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Name = "授课教师";
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Name = "授课时间";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Name = "授课地点";
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Name = "已选";
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[6].Name = "限选";
            dataGridView1.Columns[6].Width = 50;
        }//设置表格格式

        private void button3_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < dataGridView1.RowCount;i++ )
            //    if (int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()) == 1)
            //    {
            //        SC select = new SC(dataGridView1.Rows[i].Cells[1].Value.ToString(), user.UserAccount);
            //        SCList.Add(select);
            //    }
            int rownum=4;
            for (int row = 0; row < rownum; row++)
            {
                //bool selected=true;
                //object sld = selected;
                if ((Boolean)dataGridView1.Rows[row].Cells[0].Value == true)
                {
                    string cid = dataGridView1.Rows[row].Cells[1].Value.ToString();
                    if (DataQuery.ChkFull(cid))
                    {
                        MessageBox.Show("你已选的课程中有已经选满的课程，请重新选择");
                        Refreshdata();
                        break;
                    }
                    else
                    {
                        string uid = user.UserAccount;
                        DataQuery.QueryExctWithouValue(DataQuery.InsertInfo(uid, cid));//插入选课数据
                        DataQuery.QueryExctWithouValue(DataQuery.Update(cid,int.Parse(DataQuery.Select(uid).ToString())+1));//更改已选人数
                    }
                }
            }
        }//提交选课信息
        
        private void btnRef_Click(object sender, EventArgs e)
        {
            Refreshdata();
        }//刷新按钮功能

        private void Refreshdata()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            SetCols(dataGridView1);
            FillCols(dataGridView1);

        } //刷新函数
    }
}
