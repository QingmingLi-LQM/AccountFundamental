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
    public partial class Exercise : Form
    {
        #region 定义变量
        int totalExams;        //记录总的题量
        string tixingxuanze;   //记录所选择的题型
        int zhangjiexuanze;    //记录用户所选择的章节，1－第1章，2－第2章，3－第3章，4－第4章，5－第5章，11－全部
                               //6－第6章，7－第7章，8－第8章，9－第9章，10－第10章
        int i = 0, j = 0, k = 1;  //i,j临时变量，k作为全程控制变量，表示题的序号
        bool flag;
        double userCj = 0;    //记录用户的成绩
        Random myran = new Random();
        string mydate = System.DateTime.Today.ToShortDateString();
        string userAnswer = ""; //记录用户所选择的答案

        string[] bhDanxuanti = new string[620];         //单选题编号
        string[] tiGanDanxuanti = new string[620];      //单选题题干
        string[] daanADanxuanti = new string[620];      //单选题答案A
        string[] daanBDanxuanti = new string[620];      //单选题答案B
        string[] daanCDanxuanti = new string[620];      //单选题答案C
        string[] daanDDanxuanti = new string[620];      //单选题答案D
        string[] daanDanxuanti = new string[620];       //单选题答案
        string[] jiexiDanxuanti = new string[620];      //单选题解析

        string[] bhDuoxuanti = new string[500];         //多选题编号
        string[] tiGanDuoxuanti = new string[500];      //多选题题干
        string[] daanADuoxuanti = new string[500];      //多选题答案A
        string[] daanBDuoxuanti = new string[500];      //多选题答案B
        string[] daanCDuoxuanti = new string[500];      //多选题答案C
        string[] daanDDuoxuanti = new string[500];      //多选题答案D
        string[] daanDuoxuanti = new string[500];       //多选题答案
        string[] jiexiDuoxuanti = new string[500];      //多选题解析

        string[] bhPanduanti = new string[510];         //判断题编号
        string[] tiGanPanduanti = new string[510];      //判断题题干
        string[] daanPanduanti = new string[510];       //判断题答案
        string[] jiexiPanduanti = new string[510];      //判断题解析

        OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=题库.mdb;User ID=admin;Jet OLEDB:Database Password='chaohuganjiabao'");
        OleDbDataAdapter oledbda;     //创建适配器
        DataSet ds = new DataSet();   //创建记录集
        #endregion

        public Exercise()
        {
            InitializeComponent();
        }
        private void displayExams(string tixing)   //根据题型显示题目
        {
            if (k == totalExams)
                MessageBox.Show("恭喜你本次练习您也做完！");
            else
            {
                k = k + 1;
                switch (tixing)
                {
                    case "danxuanti":
                        panelDanxuan.Visible = true;
                        panelDuoxuan.Visible = false;
                        panelPanduan.Visible = false;
                        grpDanxuanti.Text = "本次练习共有：" + totalExams.ToString() + "道习题";
                        radDanxuanA.Checked = false;
                        radDanxuanB.Checked = false;
                        radDanxuanC.Checked = false;
                        radDanxuanD.Checked = false;
                        labDanxuanti.Text = k.ToString() + ". " + tiGanDanxuanti[k];
                        radDanxuanA.Text = daanADanxuanti[k];
                        radDanxuanB.Text = daanBDanxuanti[k];
                        radDanxuanC.Text = daanCDanxuanti[k];
                        radDanxuanD.Text = daanDDanxuanti[k];

                        break;
                    case "duoxuanti":
                        panelDanxuan.Visible = false;
                        panelDuoxuan.Visible = true;
                        panelPanduan.Visible = false;
                        grpDuoxuanti.Text = "本次练习共有：" + totalExams.ToString() + "道习题";

                        chkDuoxuantiA.Checked = false;
                        chkDuoxuantiB.Checked = false;
                        chkDuoxuantiC.Checked = false;
                        chkDuoxuantiD.Checked = false;
                        labDuoxuanti.Text = k.ToString() + ". "  + tiGanDuoxuanti[k];
                        chkDuoxuantiA.Text =  daanADuoxuanti[k];
                        chkDuoxuantiB.Text =  daanBDuoxuanti[k];
                        chkDuoxuantiC.Text =  daanCDuoxuanti[k];
                        chkDuoxuantiD.Text =  daanDDuoxuanti[k];

                        break;
                    case "panduanti":
                        panelDanxuan.Visible = false;
                        panelDuoxuan.Visible = false;
                        panelPanduan.Visible = true;
                        grpPanduanti.Text = "本次练习共有：" + totalExams.ToString() + "道习题";

                        labPanduanti.Text = k.ToString() + ". "  + tiGanPanduanti[k];
                        radPanduantiF.Checked = false;
                        radPanduantiT.Checked = false;
                        break;
                }
            }
        }
        private void btnPanelDanxuanti_Click(object sender, EventArgs e)
        {
            panelDanxuan.Visible = false;
            panelDuoxuan.Visible = false;
            panelPanduan.Visible = false;
            tixingxuanze = "danxuan";
            btnPanelDanxuanti.FlatStyle = FlatStyle.Flat;
            btnPanelDuoxuanti.FlatStyle = FlatStyle.Standard;
            btnPanelPanduanti.FlatStyle = FlatStyle.Standard;
        }

        private void btnPanelDuoxuanti_Click(object sender, EventArgs e)
        {
            panelDanxuan.Visible = false;
            panelDuoxuan.Visible = false;
            panelPanduan.Visible = false;
            tixingxuanze = "duoxuan";
            btnPanelDuoxuanti.FlatStyle = FlatStyle.Flat;
            btnPanelDanxuanti.FlatStyle = FlatStyle.Standard;
            btnPanelPanduanti.FlatStyle = FlatStyle.Standard;
        }

        private void btnPanelPanduanti_Click(object sender, EventArgs e)
        {
            panelDanxuan.Visible = false;
            panelDuoxuan.Visible = false;
            panelPanduan.Visible = false;
            tixingxuanze = "panduan";
            btnPanelPanduanti.FlatStyle = FlatStyle.Flat;
            btnPanelDanxuanti.FlatStyle = FlatStyle.Standard;
            btnPanelDuoxuanti.FlatStyle = FlatStyle.Standard;
        }
        private void fillExamArray(string tixing)   //将记录集中的记录填到相应的题目数组中
        {
            ds.Clear();
            oledbda.Fill(ds, tixing);                                            //对记录集进行填充
            totalExams = ds.Tables[tixing].Rows.Count;                           //记录集中总的记录数
            j = myran.Next(totalExams);                                               //产生一个随机数

            switch (tixing)
            {
                case "danxuanti":
                    bhDanxuanti[0] = ds.Tables["danxuanti"].Rows[j]["编号"].ToString();       //单选题编号
                    tiGanDanxuanti[0] = ds.Tables["danxuanti"].Rows[j]["题干"].ToString();    //单选题题干
                    daanADanxuanti[0] = ds.Tables["danxuanti"].Rows[j]["选项A"].ToString();   //单选题答案A
                    daanBDanxuanti[0] = ds.Tables["danxuanti"].Rows[j]["选项B"].ToString();   //单选题答案B
                    daanCDanxuanti[0] = ds.Tables["danxuanti"].Rows[j]["选项C"].ToString();   //单选题答案C
                    daanDDanxuanti[0] = ds.Tables["danxuanti"].Rows[j]["选项D"].ToString();   //单选题答案D
                    daanDanxuanti[0] = ds.Tables["danxuanti"].Rows[j]["答案"].ToString();     //单选题答案
                    jiexiDanxuanti[0] = ds.Tables["danxuanti"].Rows[j]["解析"].ToString();    //单选题解析
                    for (k = 1; k < totalExams; k++)
                    {
                        do
                        {
                            flag = false;
                            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
                            for (i = 1; i < k; i++)
                            {
                                if (bhDanxuanti[i] == ds.Tables["danxuanti"].Rows[j]["编号"].ToString())
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        } while (flag == true);
                        bhDanxuanti[k] = ds.Tables["danxuanti"].Rows[j]["编号"].ToString();         //单选题编号
                        tiGanDanxuanti[k] = ds.Tables["danxuanti"].Rows[j]["题干"].ToString();      //单选题题干
                        daanADanxuanti[k] = ds.Tables["danxuanti"].Rows[j]["选项A"].ToString();     //单选题答案A
                        daanBDanxuanti[k] = ds.Tables["danxuanti"].Rows[j]["选项B"].ToString();     //单选题答案B
                        daanCDanxuanti[k] = ds.Tables["danxuanti"].Rows[j]["选项C"].ToString();     //单选题答案C
                        daanDDanxuanti[k] = ds.Tables["danxuanti"].Rows[j]["选项D"].ToString();     //单选题答案D
                        daanDanxuanti[k] = ds.Tables["danxuanti"].Rows[j]["答案"].ToString();       //单选题答案
                        jiexiDanxuanti[k] = ds.Tables["danxuanti"].Rows[j]["解析"].ToString();      //单选题解析
                    }
                    break;
                case "duoxuanti":
                    bhDuoxuanti[0] = ds.Tables["duoxuanti"].Rows[j]["编号"].ToString();         //多选题编号
                    tiGanDuoxuanti[0] = ds.Tables["duoxuanti"].Rows[j]["题干"].ToString();      //多选题题干
                    daanADuoxuanti[0] = ds.Tables["duoxuanti"].Rows[j]["选项A"].ToString();     //多选题答案A
                    daanBDuoxuanti[0] = ds.Tables["duoxuanti"].Rows[j]["选项B"].ToString();     //多选题答案B
                    daanCDuoxuanti[0] = ds.Tables["duoxuanti"].Rows[j]["选项C"].ToString();     //多选题答案C
                    daanDDuoxuanti[0] = ds.Tables["duoxuanti"].Rows[j]["选项D"].ToString();     //多选题答案D
                    daanDuoxuanti[0] = ds.Tables["duoxuanti"].Rows[j]["答案"].ToString();       //多选题答案
                    jiexiDuoxuanti[0] = ds.Tables["duoxuanti"].Rows[j]["解析"].ToString();      //多选题解析
                    for (k = 1; k <= totalExams; k++)
                    {
                        do
                        {
                            flag = false;
                            j = myran.Next(totalExams);
                            for (i = 1; i < k; i++)
                            {
                                if (bhDuoxuanti[i] == ds.Tables["duoxuanti"].Rows[j]["编号"].ToString())
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        } while (flag == true);
                        bhDuoxuanti[k] = ds.Tables["duoxuanti"].Rows[j]["编号"].ToString();          //多选题编号
                        tiGanDuoxuanti[k] = ds.Tables["duoxuanti"].Rows[j]["题干"].ToString();      //多选题题干
                        daanADuoxuanti[k] = ds.Tables["duoxuanti"].Rows[j]["选项A"].ToString();     //多选题答案A
                        daanBDuoxuanti[k] = ds.Tables["duoxuanti"].Rows[j]["选项B"].ToString();     //多选题答案B
                        daanCDuoxuanti[k] = ds.Tables["duoxuanti"].Rows[j]["选项C"].ToString();      //多选题答案C
                        daanDDuoxuanti[k] = ds.Tables["duoxuanti"].Rows[j]["选项D"].ToString();      //多选题答案D
                        daanDuoxuanti[k] = ds.Tables["duoxuanti"].Rows[j]["答案"].ToString();      //多选题答案
                        jiexiDuoxuanti[k] = ds.Tables["duoxuanti"].Rows[j]["解析"].ToString();      //多选题解析
                    }
                    break;
                case "panduanti":
                    bhPanduanti[0] = ds.Tables["panduanti"].Rows[j]["编号"].ToString();         //判断题编号
                    tiGanPanduanti[0] = ds.Tables["panduanti"].Rows[j]["题干"].ToString();      //判断题题干
                    daanPanduanti[0] = ds.Tables["panduanti"].Rows[j]["答案"].ToString();       //判断题答案
                    jiexiPanduanti[0] = ds.Tables["panduanti"].Rows[j]["解析"].ToString();      //判断题解析
                    for (k = 1; k <= totalExams; k++)
                    {
                        do
                        {
                            flag = false;
                            j = myran.Next(totalExams);
                            for (i = 1; i < k; i++)
                            {
                                if (bhPanduanti[i] == ds.Tables["panduanti"].Rows[j]["编号"].ToString())
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        } while (flag == true);
                        bhPanduanti[k] = ds.Tables["panduanti"].Rows[j]["编号"].ToString();          //判断题编号
                        tiGanPanduanti[k] = ds.Tables["panduanti"].Rows[j]["题干"].ToString();      //判断题题干
                        daanPanduanti[k] = ds.Tables["panduanti"].Rows[j]["答案"].ToString();       //判断题答案
                        jiexiPanduanti[k] = ds.Tables["panduanti"].Rows[j]["解析"].ToString();      //判断题解析
                    }
                    break;
            }
            k = 0;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            userCj = 0;
            k = 0;
            labScoreDanxuan.Text = "0";
            labScoreDuoxuan.Text = "0";
            labScorePanduan.Text = "0";
            if (radCharpter1.Checked == true) zhangjiexuanze = 1;
            if (radCharpter2.Checked == true) zhangjiexuanze = 2;
            if (radCharpter3.Checked == true) zhangjiexuanze = 3;
            if (radCharpter4.Checked == true) zhangjiexuanze = 4;
            if (radCharpter5.Checked == true) zhangjiexuanze = 5;
            if (radCharpter6.Checked == true) zhangjiexuanze = 6;
            if (radCharpter7.Checked == true) zhangjiexuanze = 7;
            if (radCharpter8.Checked == true) zhangjiexuanze = 8;
            if (radCharpter9.Checked == true) zhangjiexuanze = 9;
            if (radCharpter10.Checked == true) zhangjiexuanze = 10;
            if (radAll.Checked == true) zhangjiexuanze = 11;
            switch (tixingxuanze)
            {
                case "danxuan":
                    switch (zhangjiexuanze)
                    {
                        case 1:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=1", myConnection);
                            break;
                        case 2:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=2", myConnection);
                            break;
                        case 3:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=3", myConnection);
                            break;
                        case 4:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=4", myConnection);
                            break;
                        case 5:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=5", myConnection);
                            break;
                        case 6:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=6", myConnection);
                            break;
                        case 7:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=7", myConnection);
                            break;
                        case 8:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=8", myConnection);
                            break;
                        case 9:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=9", myConnection);
                            break;
                        case 10:
                            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=10", myConnection);
                            break;
                        case 11:
                            oledbda = new OleDbDataAdapter("select * from danxuanti", myConnection);
                            break;
                    }
                    fillExamArray("danxuanti");      //将抽出的单选题填入数组
                    displayExams("danxuanti");
                    
                    break;
                case "duoxuan":
                    switch (zhangjiexuanze)
                    {
                        case 1:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=1", myConnection);
                            break;
                        case 2:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=2", myConnection);
                            break;
                        case 3:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=3", myConnection);
                            break;
                        case 4:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=4", myConnection);
                            break;
                        case 5:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=5", myConnection);
                            break;
                        case 6:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=6", myConnection);
                            break;
                        case 7:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=7", myConnection);
                            break;
                        case 8:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=8", myConnection);
                            break;
                        case 9:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=9", myConnection);
                            break;
                        case 10:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=10", myConnection);
                            break;
                        case 11:
                            oledbda = new OleDbDataAdapter("select * from duoxuanti", myConnection);
                            break;
                    }
                    fillExamArray("duoxuanti");
                    displayExams("duoxuanti");
                    break;
                case "panduan":
                    switch (zhangjiexuanze)
                    {
                        case 1:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=1", myConnection);
                            break;
                        case 2:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=2", myConnection);
                            break;
                        case 3:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=3", myConnection);
                            break;
                        case 4:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=4", myConnection);
                            break;
                        case 5:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=5", myConnection);
                            break;
                        case 6:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=6", myConnection);
                            break;
                        case 7:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=7", myConnection);
                            break;
                        case 8:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=8", myConnection);
                            break;
                        case 9:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=9", myConnection);
                            break;
                        case 10:
                            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=10", myConnection);
                            break;
                        case 11:
                            oledbda = new OleDbDataAdapter("select * from panduanti", myConnection);
                            break;
                    }
                    fillExamArray("panduanti");
                    displayExams("panduanti");
                    break;
            }
        }

        private void btnNextDanxuanti_Click(object sender, EventArgs e)
        {
            userAnswer = "";
            if (radDanxuanA.Checked == true)
                userAnswer = "A";
            if (radDanxuanB.Checked == true)
                userAnswer = "B";
            if (radDanxuanC.Checked == true)
                userAnswer = "C";
            if (radDanxuanD.Checked == true)
                userAnswer = "D";
            if (userAnswer == "") MessageBox.Show("您还没有选择答案！");
            else
            {
                if (userAnswer == daanDanxuanti[k])
                {
                    userCj++;//记录用户当前的成绩
                }
                else
                {
                    MessageBox.Show(this, jiexiDanxuanti[k], "解析");
                }
                labScoreDanxuan.Text = "正确率：" + ((int)((userCj / k) * 100)).ToString() + "%";
                displayExams("danxuanti");
            }
        }

        private void butNextDuoxuanti_Click(object sender, EventArgs e)
        {
            userAnswer = "";
            if (chkDuoxuantiA.Checked == true)
                userAnswer += "A";
            if (chkDuoxuantiB.Checked == true)
                userAnswer += "B";
            if (chkDuoxuantiC.Checked == true)
                userAnswer += "C";
            if (chkDuoxuantiD.Checked == true)
                userAnswer += "D";
            if (userAnswer == "") MessageBox.Show("您还没有选择答案！");
            else
            {
                if (userAnswer == daanDuoxuanti[k])
                {
                    userCj++;//记录用户当前的成绩
                }
                else
                {
                    MessageBox.Show(this, jiexiDuoxuanti[k], "解析");
                }
                labScoreDuoxuan.Text = "正确率：" + ((int)((userCj / k) * 100)).ToString() + "%";
                displayExams("duoxuanti");
            }
        }

        private void btnNextPanduanti_Click(object sender, EventArgs e)
        {
            userAnswer = "";
            if (radPanduantiT.Checked == true)
                userAnswer += "对";
            if (radPanduantiF.Checked == true)
                userAnswer += "错";
            if (userAnswer == "") MessageBox.Show("您还没有选择答案！");
            else
            {
                if (userAnswer == daanPanduanti[k])
                {
                    userCj++;//记录用户当前的成绩
                }
                else
                {
                    MessageBox.Show(this, jiexiPanduanti[k], "解析");
                }
                labScorePanduan.Text = "正确率：" + ((int)((userCj / k) * 100)).ToString() + "%";
                displayExams("panduanti");

            }
        }

        private void Exercise_Load(object sender, EventArgs e)
        {
            panelDanxuan.Left = panelDuoxuan.Left = panelPanduan.Left = 78;
            panelDanxuan.Top = panelDuoxuan.Top = panelPanduan.Top = 78;
            panelDanxuan.Visible = false;
            panelDuoxuan.Visible = false;
            panelPanduan.Visible = false;
            btnPanelDanxuanti_Click(null,null) ;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
