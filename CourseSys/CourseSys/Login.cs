using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.pictureBox1.Controls.Add(this.label1);
            this.pictureBox1.Controls.Add(this.label2);
            this.pictureBox1.Controls.Add(this.label3);
            this.pictureBox1.Controls.Add(this.label4);
            this.pictureBox1.Controls.Add(this.label5);
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
        }
        static UserClass user = new UserClass();
        public static UserClass returnUser() {
            return user;
        }
        public static string checkCode = "";
        /// <summary>
        /// 产生验证码随机数
        /// </summary>
        /// <param name="CodeCount"></param>
        /// <returns></returns>
        private string GetRandomCode(int CodeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,M,N,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(33);

                while (temp == t)
                {
                    t = rand.Next(33);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }

            return RandomCode;
        }
        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 14);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 29);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial ", 10);//, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(Color.Black);
            Brush r = new System.Drawing.SolidBrush(Color.FromArgb(166, 8, 8));

            g.Clear(System.Drawing.ColorTranslator.FromHtml("#99C1CB"));//背景色

            char[] ch = checkCode.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] >= '0' && ch[i] <= '9')
                {
                    //数字用红色显示
                    g.DrawString(ch[i].ToString(), f, r, 3 + (i * 12), 3);
                }
                else
                {   //字母用黑色显示
                    g.DrawString(ch[i].ToString(), f, b, 3 + (i * 12), 3);
                }
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //history back 不重复 
            pictureBox3.Image = image;
        }
        //登录按钮事件
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //if (txtSNO.Text == "12310720127" && txtPWD.Text == "123456")
            //{
            //    Sindex si = new Sindex();
            //    si.Show();
            //    this.Hide();
            //}
            //else if(txtSNO.Text=="001"&&txtPWD.Text=="123456")
            //{
            //    Tindex ti = new Tindex();
            //    ti.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    label4.Visible = true;
            //    txtPWD.Text = "";
            //}
            string username = this.txtSNO.Text;
            string password = this.txtPWD.Text;
            if (username == "")
            {
                MessageBox.Show("用户名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (password == "")
            {
                MessageBox.Show("密码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            else if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("验证码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                if (checkCode.ToLower() != this.textBox1.Text.Trim().ToLower())
                {
                    MessageBox.Show("验证码输入错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox1.Text = "";
                    checkCode = GetRandomCode(4);
                    CreateImage(checkCode);
                }
                else if (password == DataQuery.VerificationLogin(username))
                    {
                        user.UserAccount = username;
                        user.UserPassword = password;
                        user.UserName = DataQuery.GetUserName(username);
                        user.UserType = DataQuery.GetUserType(username);
                        if (user.UserType == 0)
                        {
                            Sindex sin = new Sindex();
                            sin.Show();
                            this.Hide();
                        }
                        else
                        {
                            Tindex tin = new Tindex();
                            tin.Show();
                            this.Hide();
                        }
                    }
                else
                {
                    label4.Visible = true;
                    txtPWD.Text = "";
                    textBox1.Text = "";
                    checkCode = GetRandomCode(4);
                    CreateImage(checkCode);
                }
            }
        }
        //页面加载事件
        private void Login_Load(object sender, EventArgs e)
        {
            checkCode = GetRandomCode(4);
            CreateImage(checkCode);
            this.Opacity = 0;
            timer1.Start();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            checkCode = GetRandomCode(4);
            CreateImage(checkCode);
        }

        double dou = 0.03;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += dou;
            if (this.Opacity == 1)
            {
                timer1.Stop();
                dou = -0.05;

            }
            else if (this.Opacity == 0)
            {
                timer1.Stop();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }
    }
}
