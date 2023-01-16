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
    public partial class scoreDiagram : Form
    {
        OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=题库.mdb;User ID=admin;Jet OLEDB:Database Password='chaohuganjiabao'");
        OleDbDataAdapter oledbda;     //创建适配器
        DataSet ds = new DataSet();   //创建记录集
        Label[] labScore = new Label[10];
        double[] userCj = new double[10];
        double averageScore=0;
        double maxScore = 0;
        double minScore = 0;
        int i=0;
        int j=0;
        int k = 0;
        public scoreDiagram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void scoreDiagram_Load(object sender, EventArgs e)
        {
            labScore[0] = labScore0;
            labScore[1] = labScore1;
            labScore[2] = labScore2;
            labScore[3] = labScore3;
            labScore[4] = labScore4;
            labScore[5] = labScore5;
            labScore[6] = labScore6;
            labScore[7] = labScore7;
            labScore[8] = labScore8;
            labScore[9] = labScore9;

            oledbda = new OleDbDataAdapter("select * from recordScore", myConnection);   //利用适配器选择库中的具体表
            oledbda.Fill(ds, "recordScore");                                     //对记录集进行填充
            for (int tempi = 0; tempi < 10; tempi++)
                userCj[tempi] = 0;
            i = ds.Tables["recordScore"].Rows.Count;
            j = 0;
            k = 0;
            double tempk = 0;
            if (i >= 10)
            {
                for (j = i - 10; j < i; j++, k++)   //将最后10次成绩读入数组
                    userCj[k] = double.Parse((ds.Tables["recordScore"].Rows[j]["成绩"].ToString()));
                for (int tempi = 0; tempi < 10; tempi++)
                {
                    labScore[tempi].Height = (int)(labScore[tempi].Height / 100) * (int)userCj[tempi];
                    labScore[tempi].Text = userCj[tempi].ToString();
                    labScore[tempi].Top = 550 - labScore[tempi].Height;
                }
                for (int tempi = 0; tempi < 9; tempi++)
                    for (int tempj = tempi + 1; tempj < 10; tempj++)
                        if (userCj[tempi] < userCj[tempj])
                        {
                            tempk = userCj[tempi];
                            userCj[tempi] = userCj[tempj];
                            userCj[tempj] = tempk;
                        }
                maxScore = userCj[0];
                minScore = userCj[9];
                tempk = 0;
                for (int tempi = 0; tempi < 10; tempi++)
                    tempk += userCj[tempi];
                averageScore = tempk / 10;
                averageScore = Math.Round(averageScore, 1);
                labfx.Text = "最近10次的成绩为：最高分" + maxScore.ToString() + "；最低分" + minScore.ToString() + "；平均分" + averageScore.ToString(); 
            }
            else
            {
                for (int tempi = 0; tempi < i; tempi++)
                {
                    userCj[tempi] = double.Parse((ds.Tables["recordScore"].Rows[tempi]["成绩"].ToString()));
                    labScore[tempi].Height = (int)(labScore[tempi].Height / 100) * (int)userCj[tempi];
                    labScore[tempi].Text = userCj[tempi].ToString();
                    labScore[tempi].Top = 550 - labScore[tempi].Height;
                }
                for (int tempi = i ; tempi < 10; tempi++)

                    labScore[tempi].Visible = false;

                for (int tempi = 0; tempi < i - 1; tempi++)
                    for (int tempj = tempi + 1; tempj < i; tempj++)
                        if (userCj[tempi] < userCj[tempj])
                        {
                            tempk = userCj[tempi];
                            userCj[tempi] = userCj[tempj];
                            userCj[tempj] = tempk;
                        }
                maxScore = userCj[0];
                minScore = userCj[i-1];
                tempk = 0;
                for (int tempi = 0; tempi < i; tempi++)
                    tempk += userCj[tempi];
                averageScore = tempk / i;
                averageScore = Math.Round(averageScore, 1);
                labfx.Text = "最近" + i.ToString() + "次的成绩为：最高分" + maxScore.ToString() + "；最低分" + minScore.ToString() + "；平均分" + averageScore.ToString();
            }
        }
    }
}
