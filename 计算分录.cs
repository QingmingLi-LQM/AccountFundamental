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
    public partial class 计算分录 : Form
    {
        int cmbNumber1 = 0;       //统计5个小题的组合框的数目
        int cmbNumber2 = 0;
        int cmbNumber3 = 0;
        int cmbNumber4 = 0;
        int cmbNumber5 = 0;

        ComboBox[,] cmbUser借贷 = new ComboBox[5, 15];         //测验过程中动态添加借或贷的下拉框
        ComboBox[,] cmbUser借贷科目 = new ComboBox[5, 15];     //测验过程中动态添加借贷科目的下拉框
        TextBox[,] txtUserAnswer = new TextBox[5, 15];        //测验过程中动态添加填写具体答数的文本框

        int titleNumber = 0;
        string picName1;       //题干的第1幅图片
        string picName2;       //题干的第2幅图片
        string jiexiName1;     //解析的第1幅图片
        string jiexiName2;     //解析的第2幅图片
        string[] answer = new string[5];    //记录5个分录答案
        int[] answers = new int [5];        //记录5个分录答案后面的数字，表明该答案共有几条
        public int usercj;     //记录用户的成绩

        OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=题库.mdb;User ID=admin;Jet OLEDB:Database Password='chaohuganjiabao'");
        OleDbDataAdapter oledbda = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        public 计算分录()
        {
            InitializeComponent();
        }
        private void btnFenluSubmit_Click(object sender, EventArgs e)    
        {
        }
        private void btnRemove_Click(object sender, EventArgs e)    //动态减少第1小题控件
        {
        }
        private void btnRemove1_Click(object sender, EventArgs e)
        {
        }

        private void btnRemove2_Click(object sender, EventArgs e)
        {
        }
        private void btnRemove3_Click(object sender, EventArgs e)
        {

        }
        private void btnRemove4_Click(object sender, EventArgs e)
        {

        }

        private void 计算分录_Load(object sender, EventArgs e)
        {
        }
        private void btnAdd_Click(object sender, EventArgs e)    //动态添加第1小题控件
        {
        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
        }

        private void btnAdd3_Click(object sender, EventArgs e)
        {
        }

        private void btnAdd4_Click(object sender, EventArgs e)
        {
        }

        private void panelFenluAnswer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
