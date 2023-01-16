using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace 华泽_会计基础模拟考试
{
    public partial class 计算填空 : Form
    {
        int titleNumber;
        string picName1;
        string picName2;
        string jiexiName1;
        string jiexiName2;
        string[] answer = new string[7];
        public int usercj;
        OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=题库.mdb;User ID=admin;Jet OLEDB:Database Password='chaohuganjiabao'");
        OleDbDataAdapter myAda = new OleDbDataAdapter();
        DataSet mySet = new DataSet();
        public 计算填空()
        {
            InitializeComponent();
        }
        public int jisuanScore
        {
            get
            {
                return usercj;
            }
            set
            {
                usercj  = value;
            }
        }

        private void 计算填空_Load(object sender, EventArgs e)
        {
            Random myran = new Random();
            titleNumber = myran.Next(7);
            this.Text = titleNumber.ToString();
            myAda = new OleDbDataAdapter ("select * from jisuantiankong",myConnection );
            myAda.Fill(mySet,"jisuantikong");
            picName1 = mySet.Tables["jisuantikong"].Rows[titleNumber]["题图1"].ToString();
            picName2 = mySet.Tables["jisuantikong"].Rows[titleNumber]["题图2"].ToString();
            jiexiName1 = mySet.Tables["jisuantikong"].Rows[titleNumber]["解析图1"].ToString();
            jiexiName2 = mySet.Tables["jisuantikong"].Rows[titleNumber]["解析图2"].ToString();

            answer[0] = mySet.Tables["jisuantikong"].Rows[titleNumber]["空1"].ToString();
            answer[1] = mySet.Tables["jisuantikong"].Rows[titleNumber]["空2"].ToString();
            answer[2] = mySet.Tables["jisuantikong"].Rows[titleNumber]["空3"].ToString();
            answer[3] = mySet.Tables["jisuantikong"].Rows[titleNumber]["空4"].ToString();
            answer[4] = mySet.Tables["jisuantikong"].Rows[titleNumber]["空5"].ToString();
            if (picName1 != "无")
                pic1.Image = Image.FromFile("pics\\" + picName1);
            else
                pic1.Image = null;
            if (picName2 != "无")
                pic2.Image = Image.FromFile("pics\\" + picName2);
            else
                pic2.Image = null;
            pic2.Top = pic1.Bottom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUserAnswer1.Text == answer[0])
                usercj += 2;
            if (txtUserAnswer2.Text == answer[1])
                usercj += 2;
            if (txtUserAnswer3.Text == answer[2])
                usercj += 2;
            if (txtUserAnswer4.Text == answer[3])
                usercj += 2;
            if (txtUserAnswer5.Text == answer[4])
                usercj += 2;
            DialogResult result;
            result =MessageBox.Show(this,"您本题的得分为：" + usercj.ToString() + "。是否查看参考答案？","提示",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (jiexiName1 != "无")
                    pic1.Image = Image.FromFile("pics\\" + jiexiName1);
                else
                    pic1.Image = null;
                if (jiexiName2 != "无")
                    pic2.Image = Image.FromFile("pics\\" + jiexiName2);
                else
                    pic2.Image = null;
                pic2.Top = pic1.Bottom;

            }
            button1.Enabled = false;
            txtUserAnswer1.Enabled = false;
            txtUserAnswer2.Enabled = false;
            txtUserAnswer3.Enabled = false;
            txtUserAnswer4.Enabled = false;
            txtUserAnswer5.Enabled = false;
        }
    }
}
