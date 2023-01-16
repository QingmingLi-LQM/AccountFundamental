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
    public partial class frmMain : Form
    {
#region 定义变量
        Label[] JieXi = new Label[80];           //声明一个动态控件数组，用以交卷后显示解析。

        string[] bhDanxuanti = new string[20];         //单选题编号
        string[] tiGanDanxuanti = new string[20];      //单选题题干
        string[] daanADanxuanti = new string[20];      //单选题答案A
        string[] daanBDanxuanti = new string[20];      //单选题答案B
        string[] daanCDanxuanti = new string[20];      //单选题答案C
        string[] daanDDanxuanti = new string[20];      //单选题答案D
        string[] daanDanxuanti = new string[20];       //单选题答案
        string[] jiexiDanxuanti = new string[20];      //单选题解析
        string[] userAnswerDanxuan = new string[20];   //记录用户答单选题时选择的答案
   

        string[] bhDuoxuanti = new string[20];         //多选题编号
        string[] tiGanDuoxuanti = new string[20];      //多选题题干
        string[] daanADuoxuanti = new string[20];      //多选题答案A
        string[] daanBDuoxuanti = new string[20];      //多选题答案B
        string[] daanCDuoxuanti = new string[20];      //多选题答案C
        string[] daanDDuoxuanti = new string[20];      //多选题答案D
        string[] daanDuoxuanti = new string[20];       //多选题答案
        string[] jiexiDuoxuanti = new string[20];      //多选题解析
        string[] userAnswerDuoxuan = new string[20];   //记录用户答多选题时选择的答案
       

        string[] bhPanduanti = new string[20];         //判断题编号
        string[] tiGanPanduanti = new string[20];      //判断题题干
        string[] daanPanduanti = new string[20];       //判断题答案
        string[] jiexiPanduanti = new string[20];      //判断题解析
        string[] userAnswerPanduan = new string[20];   //记录用户答判断题时选择的答案

        int titleNumber;        //记录计算填空题的序号
        string picName1;        //记录计算填空题的第1张图片的路径名和文件名
        string picName2;        //记录计算填空题的第2张图片的路径名和文件名
        string jiexiName1;      //记录计算填空题的解析之第1张图片的路径名和文件名
        string jiexiName2;      //记录计算填空题的解析之第2张图片的路径名和文件名
        string[] answer = new string[7];   //记录计算填空题的答案

        Label[] labDanxuan = new Label[20];           //声明一个动态控件数组，将20个单选题的lab转赋过来,用以显示单选题。
        Label[] labDuoxuan = new Label[20];           //声明一个动态控件数组，将20个多选题的lab转赋过来,用以显示多选题。
        Label[] labPanduan = new Label[20];           //声明一个动态控件数组，将20个判断题的lab转赋过来,用以显示判断题。

        RadioButton[] radDanxuanA = new RadioButton[20];  //20道单选题，每道4个选项。
        RadioButton[] radDanxuanB = new RadioButton[20];
        RadioButton[] radDanxuanC = new RadioButton[20];
        RadioButton[] radDanxuanD = new RadioButton[20];

        RadioButton[] radPanduanA = new RadioButton[20];  //20道判断题，每道分“对”和“错”两个选项
        RadioButton[] radPanduanB = new RadioButton[20];

        CheckBox[] chkDuoxuanA = new CheckBox[20];       //20道多选题，每道题4个选项
        CheckBox[] chkDuoxuanB = new CheckBox[20];
        CheckBox[] chkDuoxuanC = new CheckBox[20];
        CheckBox[] chkDuoxuanD = new CheckBox[20];

        int cmbNumber1 = 0;       //统计分录题5个小题的组合框的数目，这是由用户动态添加的
        int cmbNumber2 = 0;
        int cmbNumber3 = 0;
        int cmbNumber4 = 0;
        int cmbNumber5 = 0;

        ComboBox[,] cmbUser借贷 = new ComboBox[5, 15];         //测验过程中动态添加借或贷的下拉框
        ComboBox[,] cmbUser借贷科目 = new ComboBox[5, 15];     //测验过程中动态添加借贷科目的下拉框
        TextBox[,] txtUserAnswer = new TextBox[5, 15];        //测验过程中动态添加填写具体答数的文本框

        int fenluTitleNumber = 0;   //抽取分录题的题号
        string fenluPicName1;       //分录题题干的第1幅图片
        string fenluPicName2;       //分录题题干的第2幅图片
        string fenluJiexiName1;     //分录题解析的第1幅图片
        string fenluJiexiName2;     //分录题解析的第2幅图片
        string[] fenluAnswer = new string[5];    //记录5个分录答案
        int[] answers = new int[5];        //记录5个分录答案后面的数字，表明该答案共有几条

        OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=题库.mdb;User ID=admin;Jet OLEDB:Database Password='chaohuganjiabao'");
        OleDbDataAdapter oledbda;     //创建适配器
        OleDbCommand mycmd = new OleDbCommand();
        DataSet ds = new DataSet();   //创建记录集

        int i,j,k=0;
        int second= 0;     //倒计时秒的显示
        string ss="00";
        int minute = 60;  //倒计时time的时间为90分钟
        string mm="60";
        bool timeout = false;  //是否超时
        bool flag;
        double userCj = 0;    //记录用户的成绩
        Random myran = new Random();
        string mydate = System.DateTime.Now.ToShortDateString();
        DialogResult result;
        string sql = "";

#endregion
        
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
#region 界面初始化
            this.Width = 1360;
            panelJisuanTiankong.Width = 888;
            panelJisuanTiankong.Height = 630;
            panelFenlu.Width = 1170;
            panelFenlu.Height = 630;
            panelDanxuan.Height = panelDuoxuan.Height = panelPanduan.Height = 580;
            panelDanxuan.Width = panelDuoxuan.Width = panelPanduan.Width = 1060;
            panelJisuanTiankong.Left = panelDanxuan.Left = panelDuoxuan.Left = panelPanduan.Left = panelFenlu.Left=180;
            panelJisuanTiankong.Top = panelDanxuan.Top = panelDuoxuan.Top = panelPanduan.Top=panelFenlu.Top  = 110;
            btnDanxuan_Click(null,null);
#endregion
        #region 将60道解析转赋给控件数组
        JieXi[0] = labdxjx1;   JieXi[1] = labdxjx2;   JieXi[2] = labdxjx3;   JieXi[3] = labdxjx4;   JieXi[4] = labdxjx5;
        JieXi[5] = labdxjx6;   JieXi[6] = labdxjx7;   JieXi[7] = labdxjx8;   JieXi[8] = labdxjx9;   JieXi[9] = labdxjx10;
        JieXi[10] =labdxjx11;  JieXi[11] = labdxjx12; JieXi[12] = labdxjx13; JieXi[13] = labdxjx14; JieXi[14] = labdxjx15;
        JieXi[15] = labdxjx16; JieXi[16] = labdxjx17; JieXi[17] = labdxjx18; JieXi[18] = labdxjx19; JieXi[19] = labdxjx20;

        JieXi[20]=labduojx1;  JieXi[21]=labduojx2;   JieXi[22]=labduojx3;  JieXi[23]=labduojx4;     JieXi[24]=labduojx5;
        JieXi[25]=labduojx6;  JieXi[26]=labduojx7;   JieXi[27]=labduojx8;  JieXi[28]=labduojx9;     JieXi[29]=labduojx10;
        JieXi[30]=labduojx11; JieXi[31]=labduojx12;  JieXi[32]=labduojx13; JieXi[33]=labduojx14;    JieXi[34]=labduojx15;
        JieXi[35]=labduojx16; JieXi[36]=labduojx17;  JieXi[37]=labduojx18; JieXi[38]=labduojx19;    JieXi[39]=labduojx20;

        JieXi[40]=labpdjx1;   JieXi[41]=labpdjx2;   JieXi[42]=labpdjx3;   JieXi[43]=labpdjx4;   JieXi[44]=labpdjx5;
        JieXi[45]=labpdjx6;   JieXi[46]=labpdjx7;   JieXi[47]=labpdjx8;   JieXi[48]=labpdjx9;   JieXi[49]=labpdjx10;
        JieXi[50]=labpdjx11;  JieXi[51]=labpdjx12;  JieXi[52]=labpdjx13;  JieXi[53]=labpdjx14;  JieXi[54]=labpdjx15;
        JieXi[55]=labpdjx16;  JieXi[56]=labpdjx17;  JieXi[57]=labpdjx18;  JieXi[58]=labpdjx19;  JieXi[59]=labpdjx20;
            
        #endregion
        #region  单选题、多选题和判断题三种题型的题目所对应的标签赋值给相应的标签数组
        labDanxuan[0] = labDanxuan1;      labDanxuan[1] = labDanxuan2;      labDanxuan[2] = labDanxuan3;      labDanxuan[3] = labDanxuan4;
        labDanxuan[4] = labDanxuan5;      labDanxuan[5] = labDanxuan6;      labDanxuan[6] = labDanxuan7;      labDanxuan[7] = labDanxuan8;
        labDanxuan[8] = labDanxuan9;      labDanxuan[9] = labDanxuan10;     labDanxuan[10] = labDanxuan11;    labDanxuan[11] = labDanxuan12;
        labDanxuan[12] = labDanxuan13;    labDanxuan[13] = labDanxuan14;    labDanxuan[14] = labDanxuan15;    labDanxuan[15] = labDanxuan16;
        labDanxuan[16] = labDanxuan17;    labDanxuan[17] = labDanxuan18;    labDanxuan[18] = labDanxuan19;    labDanxuan[19] = labDanxuan20;

        labDuoxuan[0] = labduoxuan1;      labDuoxuan[1] = labduoxuan2;      labDuoxuan[2] = labduoxuan3;      labDuoxuan[3] = labduoxuan4;
        labDuoxuan[4] = labduoxuan5;      labDuoxuan[5] = labduoxuan6;      labDuoxuan[6] = labduoxuan7;      labDuoxuan[7] = labduoxuan8;
        labDuoxuan[8] = labduoxuan9;      labDuoxuan[9] = labduoxuan10;     labDuoxuan[10] = labduoxuan11;    labDuoxuan[11] = labduoxuan12;
        labDuoxuan[12] = labduoxuan13;    labDuoxuan[13] = labduoxuan14;    labDuoxuan[14] = labduoxuan15;    labDuoxuan[15] = labduoxuan16;
        labDuoxuan[16] = labduoxuan17;    labDuoxuan[17] = labduoxuan18;    labDuoxuan[18] = labduoxuan19;    labDuoxuan[19] = labduoxuan20;

        labPanduan[0] = labpanduan1;      labPanduan[1] = labpanduan2;      labPanduan[2] = labpanduan3;      labPanduan[3] = labpanduan4;
        labPanduan[4] = labpanduan5;      labPanduan[5] = labpanduan6;      labPanduan[6] = labpanduan7;      labPanduan[7] = labpanduan8;
        labPanduan[8] = labpanduan9;      labPanduan[9] = labpanduan10;     labPanduan[10] = labpanduan11;    labPanduan[11] = labpanduan12;
        labPanduan[12] = labpanduan13;    labPanduan[13] = labpanduan14;    labPanduan[14] = labpanduan15;    labPanduan[15] = labpanduan16;
        labPanduan[16] = labpanduan17;    labPanduan[17] = labpanduan18;    labPanduan[18] = labpanduan19;    labPanduan[19] = labpanduan20;

        #endregion
        #region 将20道单选题各选项对应的控件赋值给相应的控件数组
        radDanxuanA[0] = radDanxuan1a;      radDanxuanA[1] = radDanxuan2a;      radDanxuanA[2] = radDanxuan3a;      radDanxuanA[3] = radDanxuan4a;
        radDanxuanA[4] = radDanxuan5a;      radDanxuanA[5] = radDanxuan6a;      radDanxuanA[6] = radDanxuan7a;      radDanxuanA[7] = radDanxuan8a;
        radDanxuanA[8] = radDanxuan9a;      radDanxuanA[9] = radDanxuan10a;     radDanxuanA[10] = radDanxuan11a;    radDanxuanA[11] = radDanxuan12a;
        radDanxuanA[12] = radDanxuan13a;    radDanxuanA[13] = radDanxuan14a;    radDanxuanA[14] = radDanxuan15a;    radDanxuanA[15] = radDanxuan16a;
        radDanxuanA[16] = radDanxuan17a;    radDanxuanA[17] = radDanxuan18a;    radDanxuanA[18] = radDanxuan19a;    radDanxuanA[19] = radDanxuan20a;

        radDanxuanB[0] = radDanxuan1b;      radDanxuanB[1] = radDanxuan2b;      radDanxuanB[2] = radDanxuan3b;      radDanxuanB[3] = radDanxuan4b;
        radDanxuanB[4] = radDanxuan5b;      radDanxuanB[5] = radDanxuan6b;      radDanxuanB[6] = radDanxuan7b;      radDanxuanB[7] = radDanxuan8b;
        radDanxuanB[8] = radDanxuan9b;      radDanxuanB[9] = radDanxuan10b;     radDanxuanB[10] = radDanxuan11b;    radDanxuanB[11] = radDanxuan12b;
        radDanxuanB[12] = radDanxuan13b;    radDanxuanB[13] = radDanxuan14b;    radDanxuanB[14] = radDanxuan15b;    radDanxuanB[15] = radDanxuan16b;
        radDanxuanB[16] = radDanxuan17b;    radDanxuanB[17] = radDanxuan18b;    radDanxuanB[18] = radDanxuan19b;    radDanxuanB[19] = radDanxuan20b;

        radDanxuanC[0] = radDanxuan1c;      radDanxuanC[1] = radDanxuan2c;      radDanxuanC[2] = radDanxuan3c;      radDanxuanC[3] = radDanxuan4c;
        radDanxuanC[4] = radDanxuan5c;      radDanxuanC[5] = radDanxuan6c;      radDanxuanC[6] = radDanxuan7c;      radDanxuanC[7] = radDanxuan8c;
        radDanxuanC[8] = radDanxuan9c;      radDanxuanC[9] = radDanxuan10c;     radDanxuanC[10] = radDanxuan11c;    radDanxuanC[11] = radDanxuan12c;
        radDanxuanC[12] = radDanxuan13c;    radDanxuanC[13] = radDanxuan14c;    radDanxuanC[14] = radDanxuan15c;    radDanxuanC[15] = radDanxuan16c;
        radDanxuanC[16] = radDanxuan17c;    radDanxuanC[17] = radDanxuan18c;    radDanxuanC[18] = radDanxuan19c;    radDanxuanC[19] = radDanxuan20c;

        radDanxuanD[0] = radDanxuan1d;      radDanxuanD[1] = radDanxuan2d;      radDanxuanD[2] = radDanxuan3d;      radDanxuanD[3] = radDanxuan4d;
        radDanxuanD[4] = radDanxuan5d;      radDanxuanD[5] = radDanxuan6d;      radDanxuanD[6] = radDanxuan7d;      radDanxuanD[7] = radDanxuan8d;
        radDanxuanD[8] = radDanxuan9d;      radDanxuanD[9] = radDanxuan10d;     radDanxuanD[10] = radDanxuan11d;    radDanxuanD[11] = radDanxuan12d;
        radDanxuanD[12] = radDanxuan13d;    radDanxuanD[13] = radDanxuan14d;    radDanxuanD[14] = radDanxuan15d;    radDanxuanD[15] = radDanxuan16d;
        radDanxuanD[16] = radDanxuan17d;    radDanxuanD[17] = radDanxuan18d;    radDanxuanD[18] = radDanxuan19d;    radDanxuanD[19] = radDanxuan20d;
        #endregion
        #region 将20道多选题各选项对应的控件赋值给相应的控件数组
        chkDuoxuanA[0] = chkduoxuan1a;      chkDuoxuanA[1] = chkduoxuan2a;      chkDuoxuanA[2] = chkduoxuan3a;      chkDuoxuanA[3] = chkduoxuan4a;
        chkDuoxuanA[4] = chkduoxuan5a;      chkDuoxuanA[5] = chkduoxuan6a;      chkDuoxuanA[6] = chkduoxuan7a;      chkDuoxuanA[7] = chkduoxuan8a;
        chkDuoxuanA[8] = chkduoxuan9a;      chkDuoxuanA[9] = chkduoxuan10a;     chkDuoxuanA[10] = chkduoxuan11a;    chkDuoxuanA[11] = chkduoxuan12a;
        chkDuoxuanA[12] = chkduoxuan13a;    chkDuoxuanA[13] = chkduoxuan14a;    chkDuoxuanA[14] = chkduoxuan15a;    chkDuoxuanA[15] = chkduoxuan16a;
        chkDuoxuanA[16] = chkduoxuan17a;    chkDuoxuanA[17] = chkduoxuan18a;    chkDuoxuanA[18] = chkduoxuan19a;    chkDuoxuanA[19] = chkduoxuan20a;

        chkDuoxuanB[0] = chkduoxuan1b;      chkDuoxuanB[1] = chkduoxuan2b;      chkDuoxuanB[2] = chkduoxuan3b;      chkDuoxuanB[3] = chkduoxuan4b;
        chkDuoxuanB[4] = chkduoxuan5b;      chkDuoxuanB[5] = chkduoxuan6b;      chkDuoxuanB[6] = chkduoxuan7b;      chkDuoxuanB[7] = chkduoxuan8b;
        chkDuoxuanB[8] = chkduoxuan9b;      chkDuoxuanB[9] = chkduoxuan10b;     chkDuoxuanB[10] = chkduoxuan11b;    chkDuoxuanB[11] = chkduoxuan12b;
        chkDuoxuanB[12] = chkduoxuan13b;    chkDuoxuanB[13] = chkduoxuan14b;    chkDuoxuanB[14] = chkduoxuan15b;    chkDuoxuanB[15] = chkduoxuan16b;
        chkDuoxuanB[16] = chkduoxuan17b;    chkDuoxuanB[17] = chkduoxuan18b;    chkDuoxuanB[18] = chkduoxuan19b;    chkDuoxuanB[19] = chkduoxuan20b;

        chkDuoxuanC[0] = chkduoxuan1c;      chkDuoxuanC[1] = chkduoxuan2c;      chkDuoxuanC[2] = chkduoxuan3c;      chkDuoxuanC[3] = chkduoxuan4c;
        chkDuoxuanC[4] = chkduoxuan5c;      chkDuoxuanC[5] = chkduoxuan6c;      chkDuoxuanC[6] = chkduoxuan7c;      chkDuoxuanC[7] = chkduoxuan8c;
        chkDuoxuanC[8] = chkduoxuan9c;      chkDuoxuanC[9] = chkduoxuan10c;     chkDuoxuanC[10] = chkduoxuan11c;    chkDuoxuanC[11] = chkduoxuan12c;
        chkDuoxuanC[12] = chkduoxuan13c;    chkDuoxuanC[13] = chkduoxuan14c;    chkDuoxuanC[14] = chkduoxuan15c;    chkDuoxuanC[15] = chkduoxuan16c;
        chkDuoxuanC[16] = chkduoxuan17c;    chkDuoxuanC[17] = chkduoxuan18c;    chkDuoxuanC[18] = chkduoxuan19c;    chkDuoxuanC[19] = chkduoxuan20c;

        chkDuoxuanD[0] = chkduoxuan1d;      chkDuoxuanD[1] = chkduoxuan2d;      chkDuoxuanD[2] = chkduoxuan3d;      chkDuoxuanD[3] = chkduoxuan4d;
        chkDuoxuanD[4] = chkduoxuan5d;      chkDuoxuanD[5] = chkduoxuan6d;      chkDuoxuanD[6] = chkduoxuan7d;      chkDuoxuanD[7] = chkduoxuan8d;
        chkDuoxuanD[8] = chkduoxuan9d;      chkDuoxuanD[9] = chkduoxuan10d;     chkDuoxuanD[10] = chkduoxuan11d;    chkDuoxuanD[11] = chkduoxuan12d;
        chkDuoxuanD[12] = chkduoxuan13d;    chkDuoxuanD[13] = chkduoxuan14d;    chkDuoxuanD[14] = chkduoxuan15d;    chkDuoxuanD[15] = chkduoxuan16d;
        chkDuoxuanD[16] = chkduoxuan17d;    chkDuoxuanD[17] = chkduoxuan18d;    chkDuoxuanD[18] = chkduoxuan19d;    chkDuoxuanD[19] = chkduoxuan20d;
        #endregion
        #region 将20道判断题每一道题的单选按钮赋值给相应的控件数组
        radPanduanA[0] = radpanduan1a;      radPanduanA[1] = radpanduan2a;      radPanduanA[2] = radpanduan3a;      radPanduanA[3] = radpanduan4a;
        radPanduanA[4] = radpanduan5a;      radPanduanA[5] = radpanduan6a;      radPanduanA[6] = radpanduan7a;      radPanduanA[7] = radpanduan8a;
        radPanduanA[8] = radpanduan9a;      radPanduanA[9] = radpanduan10a;     radPanduanA[10] = radpanduan11a;    radPanduanA[11] = radpanduan12a;
        radPanduanA[12] = radpanduan13a;    radPanduanA[13] = radpanduan14a;    radPanduanA[14] = radpanduan15a;    radPanduanA[15] = radpanduan16a;
        radPanduanA[16] = radpanduan17a;    radPanduanA[17] = radpanduan18a;    radPanduanA[18] = radpanduan19a;    radPanduanA[19] = radpanduan20a;

        radPanduanB[0] = radpanduan1b;      radPanduanB[1] = radpanduan2b;      radPanduanB[2] = radpanduan3b;      radPanduanB[3] = radpanduan4b;
        radPanduanB[4] = radpanduan5b;      radPanduanB[5] = radpanduan6b;      radPanduanB[6] = radpanduan7b;      radPanduanB[7] = radpanduan8b;
        radPanduanB[8] = radpanduan9b;      radPanduanB[9] = radpanduan10b;     radPanduanB[10] = radpanduan11b;    radPanduanB[11] = radpanduan12b;
        radPanduanB[12] = radpanduan13b;    radPanduanB[13] = radpanduan14b;    radPanduanB[14] = radpanduan15b;    radPanduanB[15] = radpanduan16b;
        radPanduanB[16] = radpanduan17b;    radPanduanB[17] = radpanduan18b;    radPanduanB[18] = radpanduan19b;    radPanduanB[19] = radpanduan20b;
        #endregion

            choudanxuanti();     //抽取20道单选题
            chouduoxuanti();     //抽取20道多选题
            choupanduanti();     //抽取20道判断题
            displayExamTitle();  //显示题目
            jisuanTiankong();    //抽取计算填空题并显示；计算分析类的题分为两大类，一类为计算填空，别一类为计算分录
            fenluTiankong();     //抽取分录题并显示

        }
        private void choiceTixing(string tixing)    //根据选择的不同题型控制四个题型的按钮的弹起或按下，并显示相应的Panel
        {
            switch (tixing)
            {
                case "btnDanxuan":
                    panelDanxuan.Visible = true;
                    panelJisuanTiankong.Visible = panelDuoxuan.Visible = panelPanduan.Visible =panelFenlu.Visible = false;
                    this.Width = panelDanxuan.Width+panelDaohang.Width+15;
                    btnDanxuan.Enabled = false;
                    btnDuoxuan.Enabled = btnFenluTiankong.Enabled = btnJisuanTiankong.Enabled = btnPanduan.Enabled = true;
                    break;
                case "btnDuoxuan":
                    panelDuoxuan.Visible = true;
                    this.Width = panelDuoxuan.Width+panelDaohang.Width+15;
                    panelJisuanTiankong.Visible = panelDanxuan.Visible = panelPanduan.Visible = panelFenlu.Visible = false;
                    btnDuoxuan.Enabled = false;
                    btnDanxuan.Enabled = btnFenluTiankong.Enabled = btnJisuanTiankong.Enabled = btnPanduan.Enabled = true;
                    break;
                case "btnPanduan":
                    panelPanduan.Visible = true;
                    this.Width = panelPanduan.Width+panelDaohang.Width+15;
                    panelJisuanTiankong.Visible = panelDuoxuan.Visible = panelDanxuan.Visible = panelFenlu.Visible = false;
                    btnPanduan.Enabled = false;
                    btnDanxuan.Enabled = btnFenluTiankong.Enabled = btnJisuanTiankong.Enabled = btnDuoxuan.Enabled = true;
                    break;
                case "btnJisuanTiankong":
                    panelJisuanTiankong.Visible = true;
                    this.Width = panelJisuanTiankong.Width+panelDaohang.Width+25;
                    panelPanduan.Visible = panelDuoxuan.Visible = panelDanxuan.Visible = panelFenlu.Visible = false;
                    btnJisuanTiankong.Enabled = false;
                    btnDanxuan.Enabled = btnFenluTiankong.Enabled = btnPanduan.Enabled = btnDuoxuan.Enabled = true;
                    break;
                case "btnFenluTiankong":
                    panelFenlu.Visible = true;
                    this.Width = panelFenlu.Width+panelDaohang.Width+25;
                    panelJisuanTiankong.Visible = panelDuoxuan.Visible = panelDanxuan.Visible = panelPanduan.Visible = false;
                    btnFenluTiankong.Enabled = false;
                    btnDanxuan.Enabled = btnJisuanTiankong.Enabled = btnPanduan.Enabled = btnDuoxuan.Enabled = true;
                    break;
            }
        }
        private void danxuantifuzhi(int xiabiao, int suijijilu)   //根据下标和抽取的随机数为单选题数组赋值
        {
            bhDanxuanti[xiabiao] = ds.Tables["danxuanti"].Rows[suijijilu]["编号"].ToString();        //单选题编号
            tiGanDanxuanti[xiabiao] = ds.Tables["danxuanti"].Rows[suijijilu]["题干"].ToString();     //单选题题干
            daanADanxuanti[xiabiao] = ds.Tables["danxuanti"].Rows[suijijilu]["选项A"].ToString();    //单选题答案A
            daanBDanxuanti[xiabiao] = ds.Tables["danxuanti"].Rows[suijijilu]["选项B"].ToString();    //单选题答案B
            daanCDanxuanti[xiabiao] = ds.Tables["danxuanti"].Rows[suijijilu]["选项C"].ToString();    //单选题答案C
            daanDDanxuanti[xiabiao] = ds.Tables["danxuanti"].Rows[suijijilu]["选项D"].ToString();    //单选题答案D
            daanDanxuanti[xiabiao] = ds.Tables["danxuanti"].Rows[suijijilu]["答案"].ToString();      //单选题答案
            jiexiDanxuanti[xiabiao] = ds.Tables["danxuanti"].Rows[suijijilu]["解析"].ToString();     //单选题解析
            userAnswerDanxuan[xiabiao] = "";   //初始化用户答单选题时选择的答案
        }
        private void choudanxuanti()      //随机抽取20道单选题,其中第1章1道,第2章2道,第3章2道,第4章5道,第5章2道,第6章3道,第7章1道,第8章2道,第9章1道,第10章1道
        {
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=1", myConnection);   //利用适配器从单选题表中抽出第1章题目
            oledbda.Fill(ds, "danxuanti");                                             //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);  //产生一个随机数
            danxuantifuzhi(0, j);   //第1道单选题赋值，来自第1章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=2", myConnection);   //利用适配器从单选题表中抽出第2章题目
            oledbda.Fill(ds, "danxuanti");                                             //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);  //产生一个随机数
            danxuantifuzhi(1, j);   //第2道单选题赋值，来自第2章
            do
            {
                j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            } while (bhDanxuanti[1].ToString() == ds.Tables["danxuanti"].Rows[j]["编号"].ToString());
             danxuantifuzhi(2, j);   //第3道单选题赋值，来自第2章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=3", myConnection);   //利用适配器从单选题表中抽出第3章题目
            oledbda.Fill(ds, "danxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            danxuantifuzhi(3, j);   //第4道单选题赋值，来自第3章
                do
                {
                    j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
                } while (bhDanxuanti[3].ToString() == ds.Tables["danxuanti"].Rows[j]["编号"].ToString());
                danxuantifuzhi(4, j);   //第5道单选题赋值，来自第3章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=4", myConnection);   //利用适配器从单选题表中抽出第4章题目
            oledbda.Fill(ds, "danxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            danxuantifuzhi(5, j);   //第6道单选题赋值，来自第4章
            for (k = 6; k <= 9; k++)
            {
                do
                {
                    flag = false;
                    j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
                    for (i = 5; i < k; i++)
                    {
                        if (bhDanxuanti[i].ToString() == ds.Tables["danxuanti"].Rows[j]["编号"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                } while (flag == true);
                danxuantifuzhi(k, j);   //第7至10道单选题赋值，来自第4章
            }

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=5", myConnection);   //利用适配器从单选题表中抽出第5章题目
            oledbda.Fill(ds, "danxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            danxuantifuzhi(10, j);   //第11道单选题赋值，来自第5章
            do
            {
                j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            } while (bhDanxuanti[10].ToString() == ds.Tables["danxuanti"].Rows[j]["编号"].ToString());
            danxuantifuzhi(11, j);   //第12道单选题赋值，来自第5章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=6", myConnection);   //利用适配器从单选题表中抽出第6章题目
            oledbda.Fill(ds, "danxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            danxuantifuzhi(12, j);   //第13道单选题赋值，来自第6章
            for (k = 13; k <= 14; k++)
            {
                do
                {
                    flag = false;
                    j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
                    for (i = 12; i < k; i++)
                    {
                        if (bhDanxuanti[i].ToString() == ds.Tables["danxuanti"].Rows[j]["编号"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                } while (flag == true);
                danxuantifuzhi(k, j);   //第14、15道单选题赋值，来自第6章
            }

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=7", myConnection);   //利用适配器从单选题表中抽出第7章题目表
            oledbda.Fill(ds, "danxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            danxuantifuzhi(15, j);   //第16道单选题赋值，来自第7章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=8", myConnection);   //利用适配器从单选题表中抽出第8章题目
            oledbda.Fill(ds, "danxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            danxuantifuzhi(16, j);   //第17道单选题赋值，来自第8章
            do
            {
                j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            } while (bhDanxuanti[16].ToString() == ds.Tables["danxuanti"].Rows[j]["编号"].ToString());
            danxuantifuzhi(17, j);   //第18道单选题赋值，来自第8章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=9", myConnection);   //利用适配器从单选题表中抽出第9章题目
            oledbda.Fill(ds, "danxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            danxuantifuzhi(18, j);   //第19道单选题赋值，来自第9章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from danxuanti where 所属章节=10", myConnection);   //利用适配器从单选题表中抽出第10章题目
            oledbda.Fill(ds, "danxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["danxuanti"].Rows.Count);
            danxuantifuzhi(19, j);   //第20道单选题赋值，来自第10章
            ds.Clear();
        }
        private void duoxuantifuzhi(int xiabiao,int suijijilu)  //根据下标和抽取的随机数为多选题数组赋值
        {
            bhDuoxuanti[xiabiao] = ds.Tables["duoxuanti"].Rows[suijijilu]["编号"].ToString();         //多选题编号
            tiGanDuoxuanti[xiabiao] = ds.Tables["duoxuanti"].Rows[suijijilu]["题干"].ToString();      //多选题题干
            daanADuoxuanti[xiabiao] = ds.Tables["duoxuanti"].Rows[suijijilu]["选项A"].ToString();     //多选题答案A
            daanBDuoxuanti[xiabiao] = ds.Tables["duoxuanti"].Rows[suijijilu]["选项B"].ToString();     //多选题答案B
            daanCDuoxuanti[xiabiao] = ds.Tables["duoxuanti"].Rows[suijijilu]["选项C"].ToString();     //多选题答案C
            daanDDuoxuanti[xiabiao] = ds.Tables["duoxuanti"].Rows[suijijilu]["选项D"].ToString();     //多选题答案D
            daanDuoxuanti[xiabiao] = ds.Tables["duoxuanti"].Rows[suijijilu]["答案"].ToString();       //多选题答案
            jiexiDuoxuanti[xiabiao] = ds.Tables["duoxuanti"].Rows[suijijilu]["解析"].ToString();      //多选题解析
            userAnswerDuoxuan[xiabiao] = "";   //初始化用户答多选题时选择的答案
        }
        private void chouduoxuanti()      //随机抽取20道多选题,其中第1章1道,第2章2道,第3章2道,第4章5道,第5章2道,第6章2道,第7章1道,第8章2道,第9章2道,第10章1道
        {
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=1", myConnection);   //利用适配器从多选题表中抽出第1章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(0, j);  //第1道多选题，来自第1章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=2", myConnection);   //利用适配器从多选题表中抽出第2章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(1, j);  //第2道多选题，来自第2章
                do
                {
                    j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
                } while (bhDuoxuanti[1] == ds.Tables["duoxuanti"].Rows[j]["编号"].ToString());
            duoxuantifuzhi(2, j);  //第3道多选题，来自第2章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=3", myConnection);   //利用适配器从多选题表中抽出第3章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(3, j);  //第4道多选题，来自第3章
            do
            {
              j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            } while (bhDuoxuanti[3] == ds.Tables["duoxuanti"].Rows[j]["编号"].ToString());
            duoxuantifuzhi(4, j);  //第5道多选题，来自第3章
                        
            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=4", myConnection);   //利用适配器从多选题表中抽出第4章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(5, j);   //第6道多选题，来自第4章
            for (k = 6; k <= 9; k++)
            {
                do
                {
                    flag = false;
                    j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
                    for (i = 5; i < k; i++)
                    {
                        if (bhDuoxuanti[i] == ds.Tables["duoxuanti"].Rows[j]["编号"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                } while (flag == true);
                duoxuantifuzhi(k, j);  //第7至第10道多选题，来自第4章
            }

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=5", myConnection);   //利用适配器从多选题表中抽出第5章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(10, j);  //第11道多选题，来自第5章
                do
                {
                    j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
                } while (bhDuoxuanti[10] == ds.Tables["duoxuanti"].Rows[j]["编号"].ToString());
                duoxuantifuzhi(11, j);  //第12道多选题，来自第5章

                ds.Clear();
                oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=6", myConnection);   //利用适配器从多选题表中抽出第6章题目
                oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
                j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
                duoxuantifuzhi(12, j);  //第13道多选题，来自第6章
                do
                {
                    j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
                } while (bhDuoxuanti[12] == ds.Tables["duoxuanti"].Rows[j]["编号"].ToString());
                duoxuantifuzhi(13, j);  //第14道多选题，来自第6章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=7", myConnection);   //利用适配器从多选题表中抽出第7章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(14, j);  //第15道多选题，来自第7章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=8", myConnection);   //利用适配器从多选题表中抽出第8章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(15, j);  //第16道多选题，来自第8章
            do
            {
                j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            } while (bhDuoxuanti[15] == ds.Tables["duoxuanti"].Rows[j]["编号"].ToString());
            duoxuantifuzhi(16, j);  //第17道多选题，来自第8章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=9", myConnection);   //利用适配器从多选题表中抽出第9章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(17, j);  //第18道多选题，来自第9章
            do
            {
                j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            } while (bhDuoxuanti[17] == ds.Tables["duoxuanti"].Rows[j]["编号"].ToString());
            duoxuantifuzhi(18, j);  //第19道多选题，来自第9章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from duoxuanti where 所属章节=10", myConnection);   //利用适配器从多选题表中抽出第10章题目
            oledbda.Fill(ds, "duoxuanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["duoxuanti"].Rows.Count);
            duoxuantifuzhi(19, j);  //第20道多选题，来自第10章
            ds.Clear();

        }
        private void panduantifuzhi(int xiabiao, int suijijilu)   //根据下标和抽取的随机数为判断题数组赋值
        {
            bhPanduanti[xiabiao] = ds.Tables["panduanti"].Rows[suijijilu]["编号"].ToString();         //判断题编号
            tiGanPanduanti[xiabiao] = ds.Tables["panduanti"].Rows[suijijilu]["题干"].ToString();      //判断题题干
            daanPanduanti[xiabiao] = ds.Tables["panduanti"].Rows[suijijilu]["答案"].ToString();       //判断题答案
            jiexiPanduanti[xiabiao] = ds.Tables["panduanti"].Rows[suijijilu]["解析"].ToString();      //判断题解析
            userAnswerPanduan[xiabiao] = "";   //初始化用户答判断题时选择的答案
        }
        private void choupanduanti()      //随机抽取20道判断题,其中第1章2道,第2章2道,第3章2道,第4章4道,第5章2道,第6章2道,第7章1道,第8章2道,第9章2道,第10章1道
        {
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=1", myConnection);   //利用适配器从判断题表中抽出第1章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(0, j);  //第1道判断题赋值，来自第1章
                do
                {
                    j = myran.Next(ds.Tables["panduanti"].Rows.Count);
                } while (bhPanduanti[0] == ds.Tables["panduanti"].Rows[j]["编号"].ToString());
                panduantifuzhi(1, j);  //第2道判断题赋值，来自第1章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=2", myConnection);   //利用适配器从判断题表中抽出第2章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(2, j);  //第3道判断题赋值，来自第2章
            do
            {
                j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            } while (bhPanduanti[2] == ds.Tables["panduanti"].Rows[j]["编号"].ToString());
            panduantifuzhi(3, j);  //第4道判断题赋值，来自第2章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=3", myConnection);   //利用适配器从判断题表中抽出第3章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(4, j);  //第5道判断题赋值，来自第3章
            do
            {
                j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            } while (bhPanduanti[4] == ds.Tables["panduanti"].Rows[j]["编号"].ToString());
            panduantifuzhi(5, j);  //第6道判断题赋值，来自第3章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=4", myConnection);   //利用适配器从判断题表中抽出第4章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(6, j);  //第7道判断题赋值，来自第4章
            for (k = 7; k <= 9; k++)
            {
                do
                {
                    flag = false;
                    j = myran.Next(ds.Tables["panduanti"].Rows.Count);
                    for (i = 6; i < k; i++)
                    {
                        if (bhPanduanti[i] == ds.Tables["panduanti"].Rows[j]["编号"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                } while (flag == true);
                panduantifuzhi(k, j);  //第8至10道判断题赋值，来自第4章
            }

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=5", myConnection);   //利用适配器从判断题表中抽出第5章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(10, j);  //第11道判断题赋值，来自第5章
            do
            {
                j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            } while (bhPanduanti[10] == ds.Tables["panduanti"].Rows[j]["编号"].ToString());
            panduantifuzhi(11, j);  //第12道判断题赋值，来自第5章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=6", myConnection);   //利用适配器从判断题表中抽出第6章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(12, j);  //第13道判断题赋值，来自第6章
            do
            {
                j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            } while (bhPanduanti[12] == ds.Tables["panduanti"].Rows[j]["编号"].ToString());
            panduantifuzhi(13, j);  //第14道判断题赋值，来自第6章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=7", myConnection);   //利用适配器从判断题表中抽出第7章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(14, j);  //第15道判断题赋值，来自第7章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=8", myConnection);   //利用适配器从判断题表中抽出第8章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(15, j);  //第16道判断题赋值，来自第8章
            do
            {
              j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            } while (bhPanduanti[15] == ds.Tables["panduanti"].Rows[j]["编号"].ToString());
            panduantifuzhi(16, j);  //第17道判断题赋值，来自第8章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=9", myConnection);   //利用适配器从判断题表中抽出第9章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(17, j);  //第18道判断题赋值，来自第9章
            do
            {
                j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            } while (bhPanduanti[17] == ds.Tables["panduanti"].Rows[j]["编号"].ToString());
            panduantifuzhi(18, j);  //第19道判断题赋值，来自第9章

            ds.Clear();
            oledbda = new OleDbDataAdapter("select * from panduanti where 所属章节=10", myConnection);   //利用适配器从判断题表中抽出第10章题目
            oledbda.Fill(ds, "panduanti");                                     //对记录集进行填充
            j = myran.Next(ds.Tables["panduanti"].Rows.Count);
            panduantifuzhi(19, j);  //第20道判断题赋值，来自第10章

            ds.Clear();
        }
     private void displayExamTitle()   //显示试题:单选题20道，多选题20道，判断题20道，案例分析题2*5道
        {
            for (int temp = 0; temp < 20; temp++)  //显示单选题20道
            {
                labDanxuan[temp].Text = (temp + 1).ToString() + ". " + tiGanDanxuanti[temp];
                radDanxuanA[temp].Text = daanADanxuanti[temp];
                radDanxuanB[temp].Text = daanBDanxuanti[temp];
                radDanxuanC[temp].Text = daanCDanxuanti[temp];
                radDanxuanD[temp].Text = daanDDanxuanti[temp];
            }
            for (int temp = 0; temp < 20; temp++)  //显示多选题20道
            {
                labDuoxuan[temp].Text = (temp + 1).ToString() + ". " + tiGanDuoxuanti[temp];
                chkDuoxuanA[temp].Text = daanADuoxuanti[temp];
                chkDuoxuanB[temp].Text = daanBDuoxuanti[temp];
                chkDuoxuanC[temp].Text = daanCDuoxuanti[temp];
                chkDuoxuanD[temp].Text = daanDDuoxuanti[temp];
            }
            for (int temp = 0; temp < 20; temp++)  //显示判断题20道
            {
                labPanduan[temp].Text = (temp + 1).ToString() + ". " + tiGanPanduanti[temp];
                radPanduanA[temp].Text = "对";
                radPanduanB[temp].Text = "错";
            }
            }
     public void deleteErrorTable()       //删除错误记录表中原有内容
        {
            string[] tableName = new string[4] { "errDanxuan", "errDuoxuan", "errPanduan", "errAnlifenxi" };
            foreach (string mytab in tableName)
            {
                mycmd = new OleDbCommand("delete * from " + mytab , myConnection);
                myConnection.Close();
                myConnection.Open();
                mycmd.ExecuteNonQuery();
            }
        }
     public void writeErrorToTable()      //写入错题
     {
         for (int temp = 0; temp < 20; temp++)  //单选题
         {
             if (daanDanxuanti[temp] != userAnswerDanxuan[temp])
             {
                 sql = "insert into errDanxuan (题干,选项A,选项B,选项C,选项D,答案,你的选择,解析,错误日期) values ('"
                     + tiGanDanxuanti[temp] + "','" + daanADanxuanti[temp] + "','" + daanBDanxuanti[temp] + "','" + daanCDanxuanti[temp] + "','"
                     + daanDDanxuanti[temp] + "','" + daanDanxuanti[temp] + "','" + userAnswerDanxuan[temp] + "','" + jiexiDanxuanti[temp] + "','" + mydate + "')";
                 mycmd = new OleDbCommand(sql, myConnection);
                 mycmd.ExecuteNonQuery();
             }
         }
         for (int temp = 0; temp < 20; temp++)     //多选题
         {
             if (daanDuoxuanti[temp] != userAnswerDuoxuan[temp])  
             {
                 sql = "insert into errDuoxuan (题干,选项A,选项B,选项C,选项D,答案,你的选择,解析,错误日期) values ('"
                     + tiGanDuoxuanti[temp] + "','" + daanADuoxuanti[temp] + "','" + daanBDuoxuanti[temp] + "','" + daanCDuoxuanti[temp] + "','"
                     + daanDDuoxuanti[temp] + "','" + daanDuoxuanti[temp] + "','" + userAnswerDuoxuan[temp] + "','" + jiexiDuoxuanti[temp] + "','" + mydate + "')";
                 mycmd = new OleDbCommand(sql, myConnection);
                 mycmd.ExecuteNonQuery();
             }
         }
         for (int temp = 0; temp < 20; temp++)   //判断题
         {
             if (daanPanduanti[temp] != userAnswerPanduan[temp])
             {
                 sql = "insert into errPanduan (题干,答案,你的选择,解析,错误日期) values ('"
                     + tiGanPanduanti[temp] + "','" + daanPanduanti[temp] + "','" + userAnswerPanduan[temp] + "','" + jiexiPanduanti[temp] + "','" + mydate + "')";
                 mycmd = new OleDbCommand(sql, myConnection);
                 mycmd.ExecuteNonQuery();
             }
         }
     }
     public void changeErrorToRed()       //将错题的相关信息改为红色
     {
         for (int temp = 0; temp < 20; temp++)  //单选题       
            if (daanDanxuanti[temp] != userAnswerDanxuan[temp])
             {
                 labDanxuan[temp].ForeColor = Color.Red;
                 if (daanDanxuanti[temp] == "A") radDanxuanA[temp].ForeColor = Color.Red;
                 if (daanDanxuanti[temp] == "B") radDanxuanB[temp].ForeColor = Color.Red;
                 if (daanDanxuanti[temp] == "C") radDanxuanC[temp].ForeColor = Color.Red;
                 if (daanDanxuanti[temp] == "D") radDanxuanD[temp].ForeColor = Color.Red;
                 JieXi[temp].Text = "解析：" + jiexiDanxuanti[temp];
                 JieXi[temp].ForeColor = Color.Red;
             }
         for (int temp = 0; temp < 20; temp++)    //多选题
            if (daanDuoxuanti[temp] != userAnswerDuoxuan[temp])
             {
                 labDuoxuan[temp].ForeColor = Color.Red;
                 if (daanDuoxuanti[temp].IndexOf("A") >= 0) chkDuoxuanA[temp].ForeColor = Color.Red;
                 if (daanDuoxuanti[temp].IndexOf("B") >= 0) chkDuoxuanB[temp].ForeColor = Color.Red;
                 if (daanDuoxuanti[temp].IndexOf("C") >= 0) chkDuoxuanC[temp].ForeColor = Color.Red;
                 if (daanDuoxuanti[temp].IndexOf("D") >= 0) chkDuoxuanD[temp].ForeColor = Color.Red;
                 JieXi[temp + 20].Text = "解析：" + jiexiDuoxuanti[temp];
                 JieXi[temp + 20].ForeColor = Color.Red;
             }
         for (int temp = 0; temp < 20; temp++)   //判断题
             if (daanPanduanti[temp] != userAnswerPanduan[temp])
             {
                 labPanduan[temp].ForeColor = Color.Red;
                 if (daanPanduanti[temp] == "对") radPanduanA[temp].ForeColor = Color.Red;
                 if (daanPanduanti[temp] == "错") radPanduanB[temp].ForeColor = Color.Red;
                 JieXi[temp + 40].Text = "解析：" + jiexiPanduanti[temp];
                 JieXi[temp + 40].ForeColor = Color.Red;
             }
     }
     public void recordUserAnswer()       //记录用户答题情况
     {
         for (int temp = 0; temp < 20; temp++)    //记录用户单选题的答案
         {
             if (radDanxuanA[temp].Checked == true) userAnswerDanxuan[temp] = "A";
             if (radDanxuanB[temp].Checked == true) userAnswerDanxuan[temp] = "B";
             if (radDanxuanC[temp].Checked == true) userAnswerDanxuan[temp] = "C";
             if (radDanxuanD[temp].Checked == true) userAnswerDanxuan[temp] = "D";
         }
         for (int temp = 0; temp < 20; temp++)  //记录用户多选题的答案
         {
             if (chkDuoxuanA[temp].Checked == true) userAnswerDuoxuan[temp] += "A";
             if (chkDuoxuanB[temp].Checked == true) userAnswerDuoxuan[temp] += "B";
             if (chkDuoxuanC[temp].Checked == true) userAnswerDuoxuan[temp] += "C";
             if (chkDuoxuanD[temp].Checked == true) userAnswerDuoxuan[temp] += "D";
         }
         for (int temp = 0; temp < 20; temp++)  //记录用户判断题的答案
         {
             if (radPanduanA[temp].Checked == true) userAnswerPanduan[temp] = "对";
             if (radPanduanB[temp].Checked == true) userAnswerPanduan[temp] = "错";
         }
     }
     public void conuntRecord()           //计算用户的答题成绩并写入到成绩登记表
     {
         for (int temp = 0; temp < 20; temp++)
         {
             if (daanDanxuanti[temp] == userAnswerDanxuan[temp])    //单选题20道，每题1分，计20分
                 userCj += 1;
             if (daanDuoxuanti[temp] == userAnswerDuoxuan[temp])//多选题20道，每题2分，计40分
                 userCj += 2;
             if (daanPanduanti[temp] == userAnswerPanduan[temp])//判断题20道，每题1分，计20分
                 userCj += 1;
         }
         //登记成绩
         ds.Clear();

         oledbda = new OleDbDataAdapter("select * from recordScore", myConnection);   //利用适配器从成绩记录表中列出已有记录数
         oledbda.Fill(ds, "recordScore");                                               //对记录集进行填充
         i = ds.Tables["recordScore"].Rows.Count;
         sql = "insert into recordScore (序号,日期,成绩) values ('" + (i + 1).ToString() + "','" + mydate + "','" + userCj + "')";
         myConnection.Open();
         mycmd = new OleDbCommand(sql, myConnection);
         mycmd.ExecuteNonQuery();

     }
     private void picsIni()    //图片控件初始化
     {
         pic1.Height = 0;
         pic2.Height = 0;
         picJisuanJiexi1.Height = 0;
         picJisuanJiexi2.Height = 0;
         pic2.Top = pic1.Bottom + 3;
         picJisuanJiexi1.Top = pic2.Bottom + 3;
         picJisuanJiexi2.Top = picJisuanJiexi1.Bottom + 3;

         picFenlu1.Height = 0;
         picFenlu2.Height = 0;
         picFenluJiexi1.Height = 0;
         picFenluJiexi2.Height = 0;
         picFenlu2.Top = picFenlu1.Bottom + 3;
         picFenluJiexi1.Top = picFenlu2.Bottom + 3;
         picFenluJiexi2.Top = picJisuanJiexi1.Bottom + 3;
     }
     private void fenluJiexi()     //显示分录题的解析
     {
         picsIni();  //图片控件初始化
         if (fenluJiexiName1 != "无")
             picFenluJiexi1.Image = Image.FromFile("pics\\" + fenluJiexiName1);
         else
             picFenluJiexi1.Image = null;
         picFenluJiexi1.Top = picFenlu2.Bottom;
         if (fenluJiexiName2 != "无")
             picFenluJiexi2.Image = Image.FromFile("pics\\" + fenluJiexiName2);
         else
             picFenlu2.Image = null;
         picFenluJiexi2.Top = picFenluJiexi1.Bottom;
     }
     private void jisuanJiexi()     //显示计算题的解析
     {
         picsIni();  //图片控件初始化
         if (jiexiName1 != "无")
             picJisuanJiexi1.Image = Image.FromFile("pics\\" + jiexiName1);
         else
             picJisuanJiexi1.Image = null;
         picJisuanJiexi1.Top = pic2.Bottom;
         if (jiexiName2 != "无")
             picJisuanJiexi2.Image = Image.FromFile("pics\\" + jiexiName2);
         else
             picJisuanJiexi2.Image = null;
         picJisuanJiexi2.Top = picJisuanJiexi1.Bottom;
     }
  public void jiaojuan()
    {
            recordUserAnswer();         //记录用户答题的情况

            int countDanxuan = 0;  //记录未答的单选题数
            int countDuoxuan = 0;  //记录未答的多选题数
            int countPanduan = 0;  //记录未答的判断题数
            
            string countDanS = "";
            string countDuoS = "";
            string countPanS = "";
            string countJisuanS = "";
            string countFenluS = "";

            for (i = 0; i < 20; i++)
            {
                if (userAnswerDanxuan[i] == "")   //检查几道单选题未答
                    countDanxuan++;
                if (userAnswerDuoxuan[i] == "")   //检查几道多选题未答
                    countDuoxuan++;
                if (userAnswerPanduan[i] == "")   //检查几道判断题未答
                    countPanduan++;
            }

            if (countDanxuan > 0)
                countDanS = countDanxuan.ToString() + "道单选题没做,";
            if (countDuoxuan > 0)
                countDuoS = countDuoxuan.ToString() + "道多选题没做,";
            if (countPanduan > 0)
                countPanS = countPanduan.ToString() + "道判断题没做，";
            if (txtUserAnswer1.Text.Length * txtUserAnswer2.Text.Length * txtUserAnswer3.Text.Length * txtUserAnswer4.Text.Length * txtUserAnswer5.Text.Length == 0)
                countJisuanS = "至少还有一道计算填空题未做,";
            if(txtAnswer.Text.Length * txtAnswer1.Text.Length * txtAnswer2.Text.Length * txtAnswer3.Text.Length * txtAnswer4.Text.Length ==0)
                countFenluS = "至少还有一道分录填空题未做,";

            if ((countDanxuan + countDuoxuan + countPanduan > 0) && (timeout == false))
                result = MessageBox.Show(this, "还有" + countDanS + countDuoS + countPanS + countJisuanS + countFenluS  + "您确认交卷吗?", "重要提示", MessageBoxButtons.YesNo);
            else
                result = DialogResult.Yes;
      if (result == DialogResult.Yes)    //计算成绩
     {
         unEnabledControls();        //交卷后使各控件不可再选
         conuntRecord();             //计算用户的答题成绩并写入到成绩登记表
         deleteErrorTable();       //删除错误记录表中原有内容
         writeErrorToTable();        //写入错题到相应表中
         jisuanTiankongSubmit();    //计算“计算填空”题成绩
         FenluSubmit();           //计算分录填空题成绩   
         result = MessageBox.Show(this, "您本次测验的得分为:" + userCj.ToString() + "\n" + "是否查看答卷", "重要提示", MessageBoxButtons.YesNo);
         if (result == DialogResult.Yes)
         {
            MessageBox.Show("做错的题目将以红色显示！");
            changeErrorToRed();    //将错题的相关信息改为红色
            fenluJiexi();          //显示分录题的解析
            jisuanJiexi();         //显示计算题的解析
          }
      btnJiaojuan.Enabled = false;
    }
  }
public void unEnabledControls()   //交卷后使各控件不可再选
{
    //  改变选项AutoCheck属性，使其交卷后不能再选
    for(i=0;i<=19;i++)
        {
            radDanxuanA[i].AutoCheck = false;
            radDanxuanB[i].AutoCheck = false;
            radDanxuanC[i].AutoCheck = false;
            radDanxuanD[i].AutoCheck = false;
        }
    for(i=20;i<=39;i++)
        {
            chkDuoxuanA[i - 20].AutoCheck = false;
            chkDuoxuanB[i - 20].AutoCheck = false;
            chkDuoxuanC[i - 20].AutoCheck = false;
            chkDuoxuanD[i - 20].AutoCheck = false;
        }
    for(i=40;i<=59;i++)
        {
            radPanduanA[i - 40].AutoCheck = false;
            radPanduanB[i - 40].AutoCheck = false;
        }
}
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "会计基础模拟考试";
            labTime.Text = mm+"分钟"+":"+ss+"秒";
            if ( second==0 ) 
            { second = 59; ss = second.ToString();
              minute--;
              if (minute < 10)
                  mm = "0" + minute.ToString();
              else
                  mm = minute.ToString();
            }
            else 
            {   second--;
                if (second < 10)
                ss = "0" + second.ToString();
                else 
                ss=second.ToString();
            }
            if (minute == -1 && second == 59) { timeout = true; timer1.Stop(); btnJiaojuan.Enabled = false; jiaojuan(); }
         if (timeout == true)  //时间到了，提醒要交卷了
         {
            MessageBox.Show("时间到了！请交卷！","重要提示",MessageBoxButtons.OK); 
         }
       }
        #region   导航
        private void 一_1_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan1.Top + 8;
        }
        private void 一_2_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan2.Top + 8;
        }
        private void 一_3_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan3.Top + 8;
        }
        private void 一_4_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan4.Top + 8;
        }
        private void 一_5_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan5.Top + 8;
        }
        private void 一_6_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan6.Top + 8;
        }
        private void 一_7_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan7.Top + 8;
        }
        private void 一_8_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan8.Top + 8;
        }
        private void 一_9_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan9.Top + 8;
        }
        private void 一_10_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan10.Top + 8;
        }
        private void 一_11_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan11.Top + 8;
        }
        private void 一_12_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan12.Top + 8;
        }
        private void 一_13_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan13.Top + 8;
        }
        private void 一_14_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan14.Top + 8;
        }
        private void 一_15_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan15.Top + 8;
        }
        private void 一_16_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan16.Top + 8;
        }
        private void 一_17_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan17.Top + 8;
        }
        private void 一_18_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan18.Top + 8;
        }
        private void 一_19_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan19.Top + 8;
        }
        private void 一_20_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
            grpDanxuan.Top = -grpdanxuan20.Top + 8;
        }
        private void 二_1_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan1.Top + 8;
        }
        private void 二_2_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan2.Top + 8;
        }
        private void 二_3_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan3.Top + 8;
        }
        private void 二_4_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan4.Top + 8;
        }
        private void 二_5_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan5.Top + 8;
        }
        private void 二_6_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan6.Top + 8;
        }
        private void 二_7_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan7.Top + 8;
        }
        private void 二_8_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan8.Top + 8;
        }

        private void 二_9_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan9.Top + 8;
        }

        private void 二_10_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan10.Top + 8;
        }

        private void 二_11_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan11.Top + 8;
        }

        private void 二_12_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan12.Top + 8;
        }

        private void 二_13_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan13.Top + 8;
        }

        private void 二_14_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan14.Top + 8;
        }

        private void 二_15_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan15.Top + 8;
        }

        private void 二_16_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan16.Top + 8;
        }

        private void 二_17_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan17.Top + 8;
        }

        private void 二_18_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan18.Top + 8;
        }

        private void 二_19_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan19.Top + 8;
        }

        private void 二_20_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
            grpDuoxuan.Top = -grpduoxuan20.Top + 8;
        }

        private void 三_1_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan1.Top + 8;
        }

        private void 三_2_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan2.Top + 8;
        }

        private void 三_3_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan3.Top + 8;
        }

        private void 三_4_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan4.Top + 8;
        }

        private void 三_5_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan5.Top + 8;
        }

        private void 三_6_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan6.Top + 8;
        }

        private void 三_7_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan7.Top + 8;
        }

        private void 三_8_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan8.Top + 8;
        }

        private void 三_9_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan9.Top + 8;
        }

        private void 三_10_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan10.Top + 8;
        }

        private void 三_11_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan11.Top + 8;
        }

        private void 三_12_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan12.Top + 8;
        }

        private void 三_13_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan13.Top + 8;
        }

        private void 三_14_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan14.Top + 8;
        }

        private void 三_15_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan15.Top + 8;
        }

        private void 三_16_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan16.Top + 8;
        }

        private void 三_17_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan17.Top + 8;
        }

        private void 三_18_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan18.Top + 8;
        }
        private void 三_19_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan19.Top + 8;
        }
        private void 三_20_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
            grpPanduan.Top = -grppanduan20.Top + 8;
        }

        private void jisuanTiankong()    //计算题分为两大类，一类为计算型，另一类为分录型，分别在两个Panel中实现
        {                                //此外为计算类填空
            Random myran = new Random();
            titleNumber = myran.Next(7);       //此类题目共计7道题，每次随机产生一道
            this.Text = titleNumber.ToString();
            oledbda = new OleDbDataAdapter("select * from jisuantiankong", myConnection);  //从数据库中读取与题目相关的信息，如题图位置，答案等
            oledbda.Fill(ds, "jisuantikong");
            picName1 = ds.Tables["jisuantikong"].Rows[titleNumber]["题图1"].ToString();
            picName2 = ds.Tables["jisuantikong"].Rows[titleNumber]["题图2"].ToString();
            jiexiName1 = ds.Tables["jisuantikong"].Rows[titleNumber]["解析图1"].ToString();
            jiexiName2 = ds.Tables["jisuantikong"].Rows[titleNumber]["解析图2"].ToString();

            answer[0] = ds.Tables["jisuantikong"].Rows[titleNumber]["空1"].ToString();
            answer[1] = ds.Tables["jisuantikong"].Rows[titleNumber]["空2"].ToString();
            answer[2] = ds.Tables["jisuantikong"].Rows[titleNumber]["空3"].ToString();
            answer[3] = ds.Tables["jisuantikong"].Rows[titleNumber]["空4"].ToString();
            answer[4] = ds.Tables["jisuantikong"].Rows[titleNumber]["空5"].ToString();
            picsIni();  //图片控件初始化
            if (picName1 != "无")
                pic1.Image = Image.FromFile("pics\\" + picName1);
            else
            {
                pic1.Image = null;
                pic1.Height = 0;
            }
            if (picName2 != "无")
                pic2.Image = Image.FromFile("pics\\" + picName2);
            else
            {
                pic2.Image = null;
                pic2.Height = 0;
            }
            pic2.Top = pic1.Bottom;
        }
        private void 四_1_Click(object sender, EventArgs e)
        {
            btnJisuanTiankong_Click(null, null);
        }
       #endregion
        #region 导航-改变单选题颜色
        private void grpdanxuan1_Enter(object sender, EventArgs e)
        {
            一_1.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan2_Enter(object sender, EventArgs e)
        {
            一_2.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan3_Enter(object sender, EventArgs e)
        {
            一_3.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan4_Enter(object sender, EventArgs e)
        {
            一_4.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan5_Enter(object sender, EventArgs e)
        {
            一_5.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan6_Enter(object sender, EventArgs e)
        {
            一_6.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan7_Enter(object sender, EventArgs e)
        {
            一_7.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan8_Enter(object sender, EventArgs e)
        {
            一_8.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan9_Enter(object sender, EventArgs e)
        {
            一_9.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan10_Enter(object sender, EventArgs e)
        {
            一_10.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan11_Enter(object sender, EventArgs e)
        {
            一_11.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan12_Enter(object sender, EventArgs e)
        {
            一_12.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan13_Enter(object sender, EventArgs e)
        {
            一_13.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan14_Enter(object sender, EventArgs e)
        {
            一_14.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan15_Enter(object sender, EventArgs e)
        {
            一_15.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan16_Enter(object sender, EventArgs e)
        {
            一_16.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan17_Enter(object sender, EventArgs e)
        {
            一_17.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan18_Enter(object sender, EventArgs e)
        {
            一_18.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan19_Enter(object sender, EventArgs e)
        {
            一_19.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void grpdanxuan20_Enter(object sender, EventArgs e)
        {
            一_20.BackColor = Color.FromArgb(255, 192, 255);
        }

#endregion
        private void changeCheckBoxColor(CheckBox A, CheckBox B, CheckBox C, CheckBox D, Label E)  //如果多选题的任一个选项被选中，则表明些题已做，修改其背景色
        {
            if(A.Checked ==true || B.Checked ==true || C.Checked == true || D.Checked ==true)
                E.BackColor = label8.BackColor;
            else
                E.BackColor =label6.BackColor;
        }
        #region 导航-改变多选题颜色
        private void chkduoxuan1a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan1a, chkduoxuan1b, chkduoxuan1c, chkduoxuan1d,二_1);
        }
        private void chkduoxuan1b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan1a, chkduoxuan1b, chkduoxuan1c, chkduoxuan1d, 二_1);
        }
        private void chkduoxuan1c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan1a, chkduoxuan1b, chkduoxuan1c, chkduoxuan1d, 二_1);
        }
        private void chkduoxuan1d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan1a, chkduoxuan1b, chkduoxuan1c, chkduoxuan1d, 二_1);
        }

        private void chkduoxuan2a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan2a, chkduoxuan2b, chkduoxuan2c, chkduoxuan2d, 二_2);
        }
        private void chkduoxuan2b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan2a, chkduoxuan2b, chkduoxuan2c, chkduoxuan2d, 二_2);
        }
        private void chkduoxuan2c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan2a, chkduoxuan2b, chkduoxuan2c, chkduoxuan2d, 二_2);
        }
        private void chkduoxuan2d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan2a, chkduoxuan2b, chkduoxuan2c, chkduoxuan2d, 二_2);
        }
 
        private void chkduoxuan3a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan3a, chkduoxuan3b, chkduoxuan3c, chkduoxuan3d, 二_3);
        }
        private void chkduoxuan3b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan3a, chkduoxuan3b, chkduoxuan3c, chkduoxuan3d, 二_3);
        }
        private void chkduoxuan3c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan3a, chkduoxuan3b, chkduoxuan3c, chkduoxuan3d, 二_3);
        }
        private void chkduoxuan3d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan3a, chkduoxuan3b, chkduoxuan3c, chkduoxuan3d, 二_3);
        }

        private void chkduoxuan4a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan4a, chkduoxuan4b, chkduoxuan4c, chkduoxuan4d, 二_4);
        }
        private void chkduoxuan4b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan4a, chkduoxuan4b, chkduoxuan4c, chkduoxuan4d, 二_4);
        }
        private void chkduoxuan4c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan4a, chkduoxuan4b, chkduoxuan4c, chkduoxuan4d, 二_4);
        }
        private void chkduoxuan4d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan4a, chkduoxuan4b, chkduoxuan4c, chkduoxuan4d, 二_4);
        }

        private void chkduoxuan5a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan5a, chkduoxuan5b, chkduoxuan5c, chkduoxuan5d, 二_5);
        }
        private void chkduoxuan5b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan5a, chkduoxuan5b, chkduoxuan5c, chkduoxuan5d, 二_5);
        }
        private void chkduoxuan5c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan5a, chkduoxuan5b, chkduoxuan5c, chkduoxuan5d, 二_5);
        }
        private void chkduoxuan5d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan5a, chkduoxuan5b, chkduoxuan5c, chkduoxuan5d, 二_5);
        }

        private void chkduoxuan6a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan6a, chkduoxuan6b, chkduoxuan6c, chkduoxuan6d, 二_6);
        }
        private void chkduoxuan6b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan6a, chkduoxuan6b, chkduoxuan6c, chkduoxuan6d, 二_6);
        }
        private void chkduoxuan6c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan6a, chkduoxuan6b, chkduoxuan6c, chkduoxuan6d, 二_6);
        }
        private void chkduoxuan6d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan6a, chkduoxuan6b, chkduoxuan6c, chkduoxuan6d, 二_6);
        }

        private void chkduoxuan7a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan7a, chkduoxuan7b, chkduoxuan7c, chkduoxuan7d, 二_7);
        }
        private void chkduoxuan7b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan7a, chkduoxuan7b, chkduoxuan7c, chkduoxuan7d, 二_7);
        }
        private void chkduoxuan7c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan7a, chkduoxuan7b, chkduoxuan7c, chkduoxuan7d, 二_7);
        }
        private void chkduoxuan7d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan7a, chkduoxuan7b, chkduoxuan7c, chkduoxuan7d, 二_7);
        }

        private void chkduoxuan8a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan8a, chkduoxuan8b, chkduoxuan8c, chkduoxuan8d, 二_8);
        }
        private void chkduoxuan8b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan8a, chkduoxuan8b, chkduoxuan8c, chkduoxuan8d, 二_8);
        }
        private void chkduoxuan8c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan8a, chkduoxuan8b, chkduoxuan8c, chkduoxuan8d, 二_8);
        }
        private void chkduoxuan8d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan8a, chkduoxuan8b, chkduoxuan8c, chkduoxuan8d, 二_8);
        }

        private void chkduoxuan9a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan9a, chkduoxuan9b, chkduoxuan9c, chkduoxuan9d, 二_9);
        }
        private void chkduoxuan9b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan9a, chkduoxuan9b, chkduoxuan9c, chkduoxuan9d, 二_9);
        }
        private void chkduoxuan9c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan9a, chkduoxuan9b, chkduoxuan9c, chkduoxuan9d, 二_9);
        }
        private void chkduoxuan9d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan9a, chkduoxuan9b, chkduoxuan9c, chkduoxuan9d, 二_9);
        }

        private void chkduoxuan10a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan10a, chkduoxuan10b, chkduoxuan10c, chkduoxuan10d, 二_10);
        }
        private void chkduoxuan10b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan10a, chkduoxuan10b, chkduoxuan10c, chkduoxuan10d, 二_10);
        }
        private void chkduoxuan10c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan10a, chkduoxuan10b, chkduoxuan10c, chkduoxuan10d, 二_10);
        }
        private void chkduoxuan10d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan10a, chkduoxuan10b, chkduoxuan10c, chkduoxuan10d, 二_10);
        }

        private void chkduoxuan11a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan11a, chkduoxuan11b, chkduoxuan11c, chkduoxuan11d, 二_11);
        }
        private void chkduoxuan11b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan11a, chkduoxuan11b, chkduoxuan11c, chkduoxuan11d, 二_11);
        }
        private void chkduoxuan11c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan11a, chkduoxuan11b, chkduoxuan11c, chkduoxuan11d, 二_11);
        }
        private void chkduoxuan11d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan11a, chkduoxuan11b, chkduoxuan11c, chkduoxuan11d, 二_11);
        }
        private void chkduoxuan12a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan12a, chkduoxuan12b, chkduoxuan12c, chkduoxuan12d, 二_12);
        }
        private void chkduoxuan12b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan12a, chkduoxuan12b, chkduoxuan12c, chkduoxuan12d, 二_12);
        }
        private void chkduoxuan12c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan12a, chkduoxuan12b, chkduoxuan12c, chkduoxuan12d, 二_12);
        }
        private void chkduoxuan12d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan12a, chkduoxuan12b, chkduoxuan12c, chkduoxuan12d, 二_12);
        }

        private void chkduoxuan13a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan13a, chkduoxuan13b, chkduoxuan13c, chkduoxuan13d, 二_13);
        }
        private void chkduoxuan13b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan13a, chkduoxuan13b, chkduoxuan13c, chkduoxuan13d, 二_13);
        }
        private void chkduoxuan13c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan13a, chkduoxuan13b, chkduoxuan13c, chkduoxuan13d, 二_13);
        }
        private void chkduoxuan13d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan13a, chkduoxuan13b, chkduoxuan13c, chkduoxuan13d, 二_13);
        }

        private void chkduoxuan14a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan14a, chkduoxuan14b, chkduoxuan14c, chkduoxuan14d, 二_14);
        }
        private void chkduoxuan14b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan14a, chkduoxuan14b, chkduoxuan14c, chkduoxuan14d, 二_14);
        }
        private void chkduoxuan14c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan14a, chkduoxuan14b, chkduoxuan14c, chkduoxuan14d, 二_14);
        }
        private void chkduoxuan14d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan14a, chkduoxuan14b, chkduoxuan14c, chkduoxuan14d, 二_14);
        }

        private void chkduoxuan15a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan15a, chkduoxuan15b, chkduoxuan15c, chkduoxuan15d, 二_15);
        }
        private void chkduoxuan15b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan15a, chkduoxuan15b, chkduoxuan15c, chkduoxuan15d, 二_15);
        }
        private void chkduoxuan15c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan15a, chkduoxuan15b, chkduoxuan15c, chkduoxuan15d, 二_15);
        }
        private void chkduoxuan15d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan15a, chkduoxuan15b, chkduoxuan15c, chkduoxuan15d, 二_15);
        }

        private void chkduoxuan16a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan16a, chkduoxuan16b, chkduoxuan16c, chkduoxuan16d, 二_16);
        }
        private void chkduoxuan16b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan16a, chkduoxuan16b, chkduoxuan16c, chkduoxuan16d, 二_16);
        }
        private void chkduoxuan16c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan16a, chkduoxuan16b, chkduoxuan16c, chkduoxuan16d, 二_16);
        }
        private void chkduoxuan16d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan16a, chkduoxuan16b, chkduoxuan16c, chkduoxuan16d, 二_16);
        }

        private void chkduoxuan17a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan17a, chkduoxuan17b, chkduoxuan17c, chkduoxuan17d, 二_17);
        }
        private void chkduoxuan17b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan17a, chkduoxuan17b, chkduoxuan17c, chkduoxuan17d, 二_17);
        }
        private void chkduoxuan17c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan17a, chkduoxuan17b, chkduoxuan17c, chkduoxuan17d, 二_17);
        }
        private void chkduoxuan17d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan17a, chkduoxuan17b, chkduoxuan17c, chkduoxuan17d, 二_17);
        }

        private void chkduoxuan18a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan18a, chkduoxuan18b, chkduoxuan18c, chkduoxuan18d, 二_18);
        }
        private void chkduoxuan18b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan18a, chkduoxuan18b, chkduoxuan18c, chkduoxuan18d, 二_18);
        }
        private void chkduoxuan18c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan18a, chkduoxuan18b, chkduoxuan18c, chkduoxuan18d, 二_18);
        }
        private void chkduoxuan18d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan18a, chkduoxuan18b, chkduoxuan18c, chkduoxuan18d, 二_18);
        }

        private void chkduoxuan19a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan19a, chkduoxuan19b, chkduoxuan19c, chkduoxuan19d, 二_19);
        }
        private void chkduoxuan19b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan19a, chkduoxuan19b, chkduoxuan19c, chkduoxuan19d, 二_19);
        }
        private void chkduoxuan19c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan19a, chkduoxuan19b, chkduoxuan19c, chkduoxuan19d, 二_19);
        }
        private void chkduoxuan19d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan19a, chkduoxuan19b, chkduoxuan19c, chkduoxuan19d, 二_19);
        }

        private void chkduoxuan20a_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan20a, chkduoxuan20b, chkduoxuan20c, chkduoxuan20d, 二_20);
        }
        private void chkduoxuan20b_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan20a, chkduoxuan20b, chkduoxuan20c, chkduoxuan20d, 二_20);
        }
        private void chkduoxuan20c_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan20a, chkduoxuan20b, chkduoxuan20c, chkduoxuan20d, 二_20);
        }
        private void chkduoxuan20d_CheckedChanged(object sender, EventArgs e)
        {
            changeCheckBoxColor(chkduoxuan20a, chkduoxuan20b, chkduoxuan20c, chkduoxuan20d, 二_20);
        }
        #endregion
        #region 导航-改变判断题颜色
        private void grppanduan1_Enter(object sender, EventArgs e)
        {
            三_1.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan2_Enter(object sender, EventArgs e)
        {
            三_2.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan3_Enter(object sender, EventArgs e)
        {
            三_3.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan4_Enter(object sender, EventArgs e)
        {
            三_4.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan5_Enter(object sender, EventArgs e)
        {
            三_5.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan6_Enter(object sender, EventArgs e)
        {
            三_6.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan7_Enter(object sender, EventArgs e)
        {
            三_7.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan8_Enter(object sender, EventArgs e)
        {
            三_8.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan9_Enter(object sender, EventArgs e)
        {
            三_9.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan10_Enter(object sender, EventArgs e)
        {
            三_10.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan11_Enter(object sender, EventArgs e)
        {
            三_11.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan12_Enter(object sender, EventArgs e)
        {
            三_12.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan13_Enter(object sender, EventArgs e)
        {
            三_13.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan14_Enter(object sender, EventArgs e)
        {
            三_14.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan15_Enter(object sender, EventArgs e)
        {
            三_15.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan16_Enter(object sender, EventArgs e)
        {
            三_16.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan17_Enter(object sender, EventArgs e)
        {
            三_17.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan18_Enter(object sender, EventArgs e)
        {
            三_18.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan19_Enter(object sender, EventArgs e)
        {
            三_19.BackColor = Color.FromArgb(255, 192, 255);
        }
        private void grppanduan20_Enter(object sender, EventArgs e)
        {
            三_20.BackColor = Color.FromArgb(255, 192, 255);
        }
        #endregion
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            grpDanxuan.Top = -vScrollBar1.Value;
        }

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            grpDuoxuan.Top = -vScrollBar2.Value;
        }

        private void vScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            grpPanduan.Top = -vScrollBar3.Value;
        }

        private void btnDanxuan_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDanxuan");
        }
        private void btnDuoxuan_Click(object sender, EventArgs e)
        {
            choiceTixing("btnDuoxuan");
        }
        private void btnPanduan_Click(object sender, EventArgs e)
        {
            choiceTixing("btnPanduan");
        }
        private void btnJisuanTiankong_Click(object sender, EventArgs e)
        {
            choiceTixing("btnJisuanTiankong");
        }

        private void label3_Click(object sender, EventArgs e)  //导航栏的展开和折叠
        {
            if (label3.Text == "<")
            {
                panelDanxuan.Left = panelDuoxuan.Left = panelPanduan.Left =panelJisuanTiankong.Left =panelFenlu.Left = 50;
                panelDaohang.Visible = false;
                label3.Left = 0;
                label3.Text = ">";
            }
            else
            {
                panelDaohang.Visible = true;
                panelDanxuan.Left = panelDuoxuan.Left = panelPanduan.Left = panelJisuanTiankong.Left =panelFenlu.Left = 180;
                label3.Left = 138;
                label3.Text = "<";
            }
        }

        private void btnJiaojuan_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            jiaojuan();  
        }

        private void btnViewError_Click(object sender, EventArgs e)
        {
            Form errform = new Form();
            System.Windows.Forms.RichTextBox TextBox1;
            errform.ClientSize = new System.Drawing.Size(1020, 735);
            errform.MaximizeBox = false;
            errform.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            errform.Text = "错误记录";
            TextBox1 = new System.Windows.Forms.RichTextBox();
            TextBox1.Location = new System.Drawing.Point(0, 0);
            TextBox1.Name = "rT1";
            TextBox1.Size = new System.Drawing.Size(1020, this.Height);
            TextBox1.TabIndex = 0;
            TextBox1.Text = "";
            TextBox1.ReadOnly = true;
            errform.Controls.Add(TextBox1);
            string[] bianhao = new string[80];
            string[] tigan = new string[80];
            string[] xuanxiangA = new string[80];
            string[] xuanxiangB = new string[80];
            string[] xuanxiangC = new string[80];
            string[] xuanxiangD = new string[80];
            string[] useranswer = new string[80];
            string[] correctanswer = new string[80];
            string[] fenxi = new string[80];
            #region 读错题
            string myDate = DateTime.Today.ToShortDateString();
            // 单选题
            oledbda = new OleDbDataAdapter("select * from errDanxuan where 错误日期 = '" + myDate + "'", myConnection);   //利用适配器选择库中的具体表
            oledbda.Fill(ds, "errDanxuan");
            TextBox1.Text += "一.单选题" + "\n";
            for (k = 0, j = 0; ; k++)
            {
                try
                {
                    if (ds.Tables["errDanxuan"].Rows[j].ToString() == null)
                    {
                    }
                }
                catch
                {
                    break;
                }
                bianhao[k] = (j + 1).ToString() + ".";//单选题编号
                TextBox1.Text += bianhao[k];
                tigan[k] = ds.Tables["errDanxuan"].Rows[j]["题干"].ToString();  //单选题题干
                TextBox1.Text += tigan[k] + "\n";
                xuanxiangA[k] = ds.Tables["errDanxuan"].Rows[j]["选项A"].ToString();     //单选题答案A
                TextBox1.Text += "A" + xuanxiangA[k] + "\n";
                xuanxiangB[k] = ds.Tables["errDanxuan"].Rows[j]["选项B"].ToString();     //单选题答案B
                TextBox1.Text += "B" + xuanxiangB[k] + "\n";
                xuanxiangC[k] = ds.Tables["errDanxuan"].Rows[j]["选项C"].ToString();     //单选题答案C
                TextBox1.Text += "C" + xuanxiangC[k] + "\n";
                xuanxiangD[k] = ds.Tables["errDanxuan"].Rows[j]["选项D"].ToString();     //单选题答案D
                TextBox1.Text += "D" + xuanxiangD[k] + "\n";
                useranswer[k] = ds.Tables["errDanxuan"].Rows[j]["你的选择"].ToString();       //单选题答案
                TextBox1.Text += "您选择的答案：" + useranswer[k] + "\n";
                correctanswer[k] = ds.Tables["errDanxuan"].Rows[j]["答案"].ToString();
                TextBox1.Text += "正确答案：" + correctanswer[k] + "\n";
                fenxi[k] = ds.Tables["errDanxuan"].Rows[j]["解析"].ToString();   //单选题解析
                TextBox1.Text += "解析：" + fenxi[k] + "\n";
                TextBox1.Text += "\n";
                j++;
            }
            #endregion
            #region 多选题
            oledbda = new OleDbDataAdapter("select * from errDuoxuan where 错误日期 = '" + myDate + "'", myConnection);   //利用适配器选择库中的具体表
            oledbda.Fill(ds, "errDuoxuan");
            TextBox1.Text += "二.多选题" + "\n";
            for (k = 0, j = 0; ; k++)
            {
                try { if (ds.Tables["errDuoxuan"].Rows[j].ToString() == null) { } }
                catch { break; }
                //else
                // {
                bianhao[k] = (j + 1).ToString() + ".";//多选题编号
                TextBox1.Text += bianhao[k];
                tigan[k] = ds.Tables["errDuoxuan"].Rows[j]["题干"].ToString();  //多选题题干
                TextBox1.Text += tigan[k] + "\n";
                xuanxiangA[k] = ds.Tables["errDuoxuan"].Rows[j]["选项A"].ToString();     //多选题答案A
                TextBox1.Text += "A" + xuanxiangA[k] + "\n";
                xuanxiangB[k] = ds.Tables["errDuoxuan"].Rows[j]["选项B"].ToString();     //多选题答案B
                TextBox1.Text += "B" + xuanxiangB[k] + "\n";
                xuanxiangC[k] = ds.Tables["errDuoxuan"].Rows[j]["选项C"].ToString();     //多选题答案C
                TextBox1.Text += "C" + xuanxiangC[k] + "\n";
                xuanxiangD[k] = ds.Tables["errDuoxuan"].Rows[j]["选项D"].ToString();     //多选题答案D
                TextBox1.Text += "D" + xuanxiangD[k] + "\n";
                useranswer[k] = ds.Tables["errDuoxuan"].Rows[j]["你的选择"].ToString();       //多选题答案
                TextBox1.Text += "您选择的答案：" + useranswer[k] + "\n";

                correctanswer[k] = ds.Tables["errDuoxuan"].Rows[j]["答案"].ToString();
                TextBox1.Text += "正确答案：" + correctanswer[k] + "\n";
                fenxi[k] = ds.Tables["errDuoxuan"].Rows[j]["解析"].ToString();   //多选题解析
                TextBox1.Text += "解析：" + fenxi[k] + "\n";
                TextBox1.Text += "\n";
                j++;
                //  }

            }
            #endregion
            #region 判断题
            oledbda = new OleDbDataAdapter("select * from errPanduan where 错误日期 = '" + myDate + "'", myConnection);   //利用适配器选择库中的具体表
            oledbda.Fill(ds, "errPanduan");
            TextBox1.Text += "三.判断题" + "\n";
            for (k = 0, j = 0; ; k++)
            {
                try { if (ds.Tables["errPanduan"].Rows[j].ToString() == null) { } }
                catch { break; }
                //else
                // {
                bianhao[k] = (j + 1).ToString() + ".";//判断题编号
                TextBox1.Text += bianhao[k];
                tigan[k] = ds.Tables["errPanduan"].Rows[j]["题干"].ToString();  //判断题题干
                TextBox1.Text += tigan[k] + "\n";

                useranswer[k] = ds.Tables["errPanduan"].Rows[j]["你的选择"].ToString();       //判断题答案
                TextBox1.Text += "您选择的答案：" + useranswer[k] + "\n";

                correctanswer[k] = ds.Tables["errPanduan"].Rows[j]["答案"].ToString();
                TextBox1.Text += "正确答案：" + correctanswer[k] + "\n";
                fenxi[k] = ds.Tables["errPanduan"].Rows[j]["解析"].ToString();   //判断题解析
                TextBox1.Text += "解析：" + fenxi[k] + "\n";
                TextBox1.Text += "\n";
                j++;
                //  }

            }
            #endregion
            errform.Show();   

        }

        private void btnViewRecord_Click(object sender, EventArgs e)
        {
            scoreDiagram myform = new scoreDiagram();
            myform.Show();
        }
        private void jisuanTiankongSubmit()
        {
            if (txtUserAnswer1.Text == answer[0])
                userCj += 2;
            else
                txtUserAnswer1.BackColor = Color.Red;
            if (txtUserAnswer2.Text == answer[1])
                userCj += 2;
            else
                txtUserAnswer2.BackColor = Color.Red;
            if (txtUserAnswer3.Text == answer[2])
                userCj += 2;
            else
                txtUserAnswer3.BackColor = Color.Red;
            if (txtUserAnswer4.Text == answer[3])
                userCj += 2;
            else
                txtUserAnswer4.BackColor = Color.Red;
            if (txtUserAnswer5.Text == answer[4])
                userCj += 2;
            else
                txtUserAnswer5.BackColor = Color.Red;
            txtUserAnswer1.Enabled = false;
            txtUserAnswer2.Enabled = false;
            txtUserAnswer3.Enabled = false;
            txtUserAnswer4.Enabled = false;
            txtUserAnswer5.Enabled = false;
        }
        private void FenluSubmit()    //分录题评分
        {
            //判断第1小题回答是否正确
            string userAnswer = "";
            int rightCount = 0;
            if (cmb借贷.Text.Trim().Length + cmb借贷科目.Text.Trim().Length + txtAnswer.Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                userAnswer = "空字符串";
            else
                userAnswer = cmb借贷.Text + cmb借贷科目.Text + txtAnswer.Text;
            if (fenluAnswer[0].IndexOf(userAnswer) >= 0) rightCount++;
            if (cmbNumber1 >= 0)      //再将用户添加的控件内容与答案中的内容进行对比。
            {
                for (int i = 0; i <= cmbNumber1 - 1; i++)
                {
                    if (cmbUser借贷[0, i].Text.Trim().Length + cmbUser借贷科目[0, i].Text.Trim().Length + txtUserAnswer[0, i].Text.Trim().Length == 0)
                        userAnswer = "空字符串";
                    else
                        userAnswer = cmbUser借贷[0, i].Text + cmbUser借贷科目[0, i].Text + txtUserAnswer[0, i].Text;
                    if (fenluAnswer[0].IndexOf(userAnswer) >= 0) rightCount++;
                }
            }
            if (rightCount.ToString() == answers[0].ToString())
                userCj += 2;
            else
                panelFenluAnswer.BackColor = Color.Red;

            //判断第2小题回答是否正确
            rightCount = 0;
            if (cmb借贷1.Text.Trim().Length + cmb借贷科目1.Text.Trim().Length + txtAnswer1.Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                userAnswer = "空字符串";
            else
                userAnswer = cmb借贷1.Text + cmb借贷科目1.Text + txtAnswer1.Text;
            if (fenluAnswer[1].IndexOf(userAnswer) >= 0) rightCount++;
            if (cmbNumber2 >= 0)      //再将用户添加的控件内容与答案中的内容进行对比。
            {
                for (int i = 0; i <= cmbNumber2 - 1; i++)
                {
                    if (cmbUser借贷[1, i].Text.Trim().Length + cmbUser借贷科目[1, i].Text.Trim().Length + txtUserAnswer[1, i].Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                        userAnswer = "空字符串";
                    else
                        userAnswer = cmbUser借贷[1, i].Text + cmbUser借贷科目[1, i].Text + txtUserAnswer[1, i].Text;
                    if (fenluAnswer[1].IndexOf(userAnswer) >= 0) rightCount++;
                }
            }
            if (rightCount.ToString() == answers[1].ToString())
                userCj += 2;
            else
                panelFenluAnswer1.BackColor = Color.Red;

            //判断第3小题回答是否正确
            rightCount = 0;
            if (cmb借贷2.Text.Trim().Length + cmb借贷科目2.Text.Trim().Length + txtAnswer2.Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                userAnswer = "空字符串";
            else
                userAnswer = cmb借贷2.Text + cmb借贷科目2.Text + txtAnswer2.Text;
            if (fenluAnswer[2].IndexOf(userAnswer) >= 0) rightCount++;
            if (cmbNumber3 >= 0)      //再将用户添加的控件内容与答案中的内容进行对比。
            {
                for (int i = 0; i <= cmbNumber3 - 1; i++)
                {
                    if (cmbUser借贷[2, i].Text.Trim().Length + cmbUser借贷科目[2, i].Text.Trim().Length + txtUserAnswer[2, i].Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                        userAnswer = "空字符串";
                    else
                        userAnswer = cmbUser借贷[2, i].Text + cmbUser借贷科目[2, i].Text + txtUserAnswer[2, i].Text;
                    if (fenluAnswer[2].IndexOf(userAnswer) >= 0) rightCount++;
                }
            }
            if (rightCount.ToString() == answers[2].ToString())
                userCj += 2;
            else
                panelFenluAnswer2.BackColor = Color.Red;

            //判断第4小题回答是否正确
            rightCount = 0;
            if (cmb借贷3.Text.Trim().Length + cmb借贷科目3.Text.Trim().Length + txtAnswer3.Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                userAnswer = "空字符串";
            else
                userAnswer = cmb借贷3.Text + cmb借贷科目3.Text + txtAnswer3.Text;
            if (fenluAnswer[3].IndexOf(userAnswer) >= 0) rightCount++;
            if (cmbNumber4 >= 0)      //再将用户添加的控件内容与答案中的内容进行对比。
            {
                for (int i = 0; i <= cmbNumber4 - 1; i++)
                {
                    if (cmbUser借贷[3, i].Text.Trim().Length + cmbUser借贷科目[3, i].Text.Trim().Length + txtUserAnswer[3, i].Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                        userAnswer = "空字符串";
                    else
                        userAnswer = cmbUser借贷[3, i].Text + cmbUser借贷科目[3, i].Text + txtUserAnswer[3, i].Text;
                    if (fenluAnswer[3].IndexOf(userAnswer) >= 0) rightCount++;
                }
            }
            if (rightCount.ToString() == answers[3].ToString())
                userCj += 2;
            else
                panelFenluAnswer3.BackColor = Color.Red;

            //判断第5小题回答是否正确
            rightCount = 0;
            if (cmb借贷4.Text.Trim().Length + cmb借贷科目4.Text.Trim().Length + txtAnswer4.Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                userAnswer = "空字符串";
            else
                userAnswer = cmb借贷4.Text + cmb借贷科目4.Text + txtAnswer4.Text;
            if (fenluAnswer[4].IndexOf(userAnswer) >= 0) rightCount++;
            if (cmbNumber5 >= 0)      //再将用户添加的控件内容与答案中的内容进行对比。
            {
                for (int i = 0; i <= cmbNumber5 - 1; i++)
                {
                    if (cmbUser借贷[4, i].Text.Trim().Length + cmbUser借贷科目[4, i].Text.Trim().Length + txtUserAnswer[4, i].Text.Trim().Length == 0)    //防止出现空串遇到数字答案时出现的不答题却得分的怪事
                        userAnswer = "空字符串";
                    else
                        userAnswer = cmbUser借贷[4, i].Text + cmbUser借贷科目[4, i].Text + txtUserAnswer[4, i].Text;
                    if (fenluAnswer[4].IndexOf(userAnswer) >= 0) rightCount++;
                }
            }
            if (rightCount.ToString() == answers[4].ToString())
                userCj += 2;
            else
                panelFenluAnswer4.BackColor = Color.Red;

        }

        private void fenluTiankong()
        {
            additems(cmb借贷);
            addItemsKemu(cmb借贷科目);
            txtAnswer.Text = "";

            additems(cmb借贷1);
            addItemsKemu(cmb借贷科目1);
            txtAnswer1.Text = "";

            additems(cmb借贷2);
            addItemsKemu(cmb借贷科目2);
            txtAnswer2.Text = "";

            additems(cmb借贷3);
            addItemsKemu(cmb借贷科目3);
            txtAnswer3.Text = "";

            additems(cmb借贷4);
            addItemsKemu(cmb借贷科目4);
            txtAnswer4.Text = "";

            Random myran = new Random();
            fenluTitleNumber = myran.Next(13);       //此类题目共计7道题，每次随机产生一道
            this.Text = fenluTitleNumber.ToString();
            oledbda = new OleDbDataAdapter("select * from jisuanfenlu", myConnection);  //从数据库中读取与题目相关的信息，如题图位置，答案等
            oledbda.Fill(ds, "jisuanfenlu");
            fenluPicName1 = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["题图1"].ToString();
            fenluPicName2 = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["题图2"].ToString();
            fenluJiexiName1 = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["解析图1"].ToString();
            fenluJiexiName2 = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["解析图2"].ToString();

            fenluAnswer[0] = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["分录1"].ToString();
            fenluAnswer[1] = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["分录2"].ToString();
            fenluAnswer[2] = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["分录3"].ToString();
            fenluAnswer[3] = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["分录4"].ToString();
            fenluAnswer[4] = ds.Tables["jisuanfenlu"].Rows[fenluTitleNumber]["分录5"].ToString();

            int temp1;
            int temp2;
            for (int i = 0; i < 5; i++)
            {
                temp1 = fenluAnswer[i].IndexOf("@");
                temp2 = fenluAnswer[i].Length - temp1 - 1;
                answers[i] = int.Parse(fenluAnswer[i].Substring(temp1 + 1, temp2));
            }
            this.Text = answers[0].ToString() + "," + answers[1].ToString() + "," + answers[2].ToString() + "," + answers[3].ToString() + "," + answers[4].ToString();
            picsIni();  //图片控件初始化

            if (fenluPicName1 != "无")
                picFenlu1.Image = Image.FromFile("pics\\" + fenluPicName1);
            else
            {
                picFenlu1.Image = null;
                picFenlu1.Height = 0;
            }
            if (fenluPicName2 != "无")
                picFenlu2.Image = Image.FromFile("pics\\" + fenluPicName2);
            else
            {
                picFenlu2.Image = null;
                picFenlu2.Height = 0;
            }
            picFenlu2.Top = picFenlu1.Bottom+3;
            myConnection.Close();

        }
        private void addItemsKemu(ComboBox mycmb)    //向组合框添加科目名称
        {
            mycmb.Items.Add("现金");
            mycmb.Items.Add("银行存款");
            mycmb.Items.Add("其他货币资金");
            mycmb.Items.Add("交易性金融资产");
            mycmb.Items.Add("交易性金融资产-成本");
            mycmb.Items.Add("交易性金融资产-公允价值变动");

            mycmb.Items.Add("短期投资");
            mycmb.Items.Add("短期投资跌价准备");
            mycmb.Items.Add("应收票据");
            mycmb.Items.Add("应收股利");
            mycmb.Items.Add("应收账款");
            mycmb.Items.Add("其他应收款");
            mycmb.Items.Add("坏账准备");
            mycmb.Items.Add("在途物资");
            mycmb.Items.Add("原材料");
            mycmb.Items.Add("材料采购");
            mycmb.Items.Add("材料成本差异");
            mycmb.Items.Add("低值易耗品");
            mycmb.Items.Add("库存商品");
            mycmb.Items.Add("商品进销差价");
            mycmb.Items.Add("委托加工物资");
            mycmb.Items.Add("委托代销商品");
            mycmb.Items.Add("存货跌价准备");
            mycmb.Items.Add("待摊费用");
            mycmb.Items.Add("待处理财产损溢");
            mycmb.Items.Add("长期股权投资");
            mycmb.Items.Add("长期债权投资");
            mycmb.Items.Add("固定资产");
            mycmb.Items.Add("累计折旧");
            mycmb.Items.Add("工程物资");
            mycmb.Items.Add("在建工程");
            mycmb.Items.Add("固定资产清理");
            mycmb.Items.Add("无形资产");
            mycmb.Items.Add("长期待摊费用");
            mycmb.Items.Add("短期借款");
            mycmb.Items.Add("应付票据");
            mycmb.Items.Add("应付账款");
            mycmb.Items.Add("应付工资");
            mycmb.Items.Add("应付福利费");
            mycmb.Items.Add("应付利润");
            mycmb.Items.Add("应交税金");
            mycmb.Items.Add("应交税金-应交增值税");
            mycmb.Items.Add("应交税金-进项税额");
            mycmb.Items.Add("应交税金-已交税金");
            mycmb.Items.Add("应交税金-减免税款");
            mycmb.Items.Add("应交税金-出口抵减内销产品应纳税额");
            mycmb.Items.Add("应交税金-转出未交增值税");
            mycmb.Items.Add("应交税金-销项税额");
            mycmb.Items.Add("应交税金-出口退税");
            mycmb.Items.Add("应交税金-进项税额转出");
            mycmb.Items.Add("应交税金-转出多交增值税");
            mycmb.Items.Add("应交税金-未交增值税");
            mycmb.Items.Add("应交税金-应交营业税");
            mycmb.Items.Add("应交税金-应交消费税");
            mycmb.Items.Add("应交税金-应交资源税");
            mycmb.Items.Add("应交税金-应交所得税");
            mycmb.Items.Add("应交税金-应交土地增值税");
            mycmb.Items.Add("应交税金-应交城市维护建设税");
            mycmb.Items.Add("应交税金-应交房产税");
            mycmb.Items.Add("应交税金-应交土地使用税");
            mycmb.Items.Add("应交税金-应交车船使用税");
            mycmb.Items.Add("应交税金-应交个人所得税");

            mycmb.Items.Add("其他应交款");
            mycmb.Items.Add("其他应付款");
            mycmb.Items.Add("预提费用");
            mycmb.Items.Add("待转资产价值");
            mycmb.Items.Add("长期借款");
            mycmb.Items.Add("长期应付款");
            mycmb.Items.Add("实收资本");
            mycmb.Items.Add("资本公积");
            mycmb.Items.Add("盈余公积");
            mycmb.Items.Add("本年利润");

            mycmb.Items.Add("利润分配");
            mycmb.Items.Add("利润分配-其他转入");
            mycmb.Items.Add("利润分配-提取法定盈余公积");
            mycmb.Items.Add("利润分配-提取法定公益金");
            mycmb.Items.Add("利润分配-提取任意盈余公积");
            mycmb.Items.Add("利润分配-应付利润");
            mycmb.Items.Add("利润分配-转作资本的利润");
            mycmb.Items.Add("利润分配-分配利润");

            mycmb.Items.Add("生产成本");
            mycmb.Items.Add("劳务成本");
            mycmb.Items.Add("制造费用");
            mycmb.Items.Add("主营业务收入");
            mycmb.Items.Add("其他业务收入");
            mycmb.Items.Add("公允价值变动损益");
            mycmb.Items.Add("投资收益");
            mycmb.Items.Add("资产减值损失");
            mycmb.Items.Add("营业外收入");
            mycmb.Items.Add("主营业务成本");
            mycmb.Items.Add("营业税金及附加");
            mycmb.Items.Add("其他业务支出");
            mycmb.Items.Add("营业费用");
            mycmb.Items.Add("管理费用");
            mycmb.Items.Add("财务费用");
            mycmb.Items.Add("营业外支出");
            mycmb.Items.Add("所得税");
            mycmb.Text = "";

        }

        private void additems(ComboBox mycmb)
        {
            mycmb.Items.Add("借:");
            mycmb.Items.Add("贷:");
            mycmb.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cmbUser借贷[0, cmbNumber1] = new ComboBox();
            cmbUser借贷[0, cmbNumber1].Font = cmb借贷.Font;
            cmbUser借贷[0, cmbNumber1].Left = cmb借贷.Left;
            cmbUser借贷[0, cmbNumber1].Size = cmb借贷.Size;

            cmbUser借贷科目[0, cmbNumber1] = new ComboBox();
            cmbUser借贷科目[0, cmbNumber1].Font = cmb借贷科目.Font;
            cmbUser借贷科目[0, cmbNumber1].Left = cmb借贷科目.Left;
            cmbUser借贷科目[0, cmbNumber1].Size = cmb借贷科目.Size;

            txtUserAnswer[0, cmbNumber1] = new TextBox();
            txtUserAnswer[0, cmbNumber1].Font = txtAnswer.Font;
            txtUserAnswer[0, cmbNumber1].Left = txtAnswer.Left;
            txtUserAnswer[0, cmbNumber1].Size = txtAnswer.Size;

            if (cmbNumber1 == 0)
            {
                cmbUser借贷[0, cmbNumber1].Top = cmb借贷.Bottom + 5;
                cmbUser借贷科目[0, cmbNumber1].Top = cmb借贷科目.Bottom + 5;
                txtUserAnswer[0, cmbNumber1].Top = txtAnswer.Bottom + 4;
            }
            else
            {
                cmbUser借贷[0, cmbNumber1].Top = cmbUser借贷[0, cmbNumber1 - 1].Bottom + 5;
                cmbUser借贷科目[0, cmbNumber1].Top = cmbUser借贷科目[0, cmbNumber1 - 1].Bottom + 5;
                txtUserAnswer[0, cmbNumber1].Top = txtUserAnswer[0, cmbNumber1 - 1].Bottom + 4;
            }
            panelFenluAnswer.Controls.Add(cmbUser借贷[0, cmbNumber1]);
            panelFenluAnswer.Controls.Add(cmbUser借贷科目[0, cmbNumber1]);
            panelFenluAnswer.Controls.Add(txtUserAnswer[0, cmbNumber1]);
            additems(cmbUser借贷[0, cmbNumber1]);
            addItemsKemu(cmbUser借贷科目[0, cmbNumber1]);
            btnAdd.Top = btnRemove.Top = txtUserAnswer[0, cmbNumber1].Top;
            cmbNumber1++;
        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            cmbUser借贷[1, cmbNumber2] = new ComboBox();
            cmbUser借贷[1, cmbNumber2].Font = cmb借贷1.Font;
            cmbUser借贷[1, cmbNumber2].Left = cmb借贷1.Left;
            cmbUser借贷[1, cmbNumber2].Size = cmb借贷1.Size;
            cmbUser借贷[1, cmbNumber2].BackColor = cmb借贷1.BackColor;

            cmbUser借贷科目[1, cmbNumber2] = new ComboBox();
            cmbUser借贷科目[1, cmbNumber2].Font = cmb借贷科目1.Font;
            cmbUser借贷科目[1, cmbNumber2].Left = cmb借贷科目1.Left;
            cmbUser借贷科目[1, cmbNumber2].Size = cmb借贷科目1.Size;
            cmbUser借贷科目[1, cmbNumber2].BackColor = cmb借贷科目1.BackColor;

            txtUserAnswer[1, cmbNumber2] = new TextBox();
            txtUserAnswer[1, cmbNumber2].Font = txtAnswer1.Font;
            txtUserAnswer[1, cmbNumber2].Left = txtAnswer1.Left;
            txtUserAnswer[1, cmbNumber2].Size = txtAnswer1.Size;
            txtUserAnswer[1, cmbNumber2].BackColor = txtAnswer1.BackColor;

            if (cmbNumber2 == 0)
            {
                cmbUser借贷[1, cmbNumber2].Top = cmb借贷1.Bottom + 5;
                cmbUser借贷科目[1, cmbNumber2].Top = cmb借贷科目1.Bottom + 5;
                txtUserAnswer[1, cmbNumber2].Top = txtAnswer1.Bottom + 4;
            }
            else
            {
                cmbUser借贷[1, cmbNumber2].Top = cmbUser借贷[1, cmbNumber2 - 1].Bottom + 5;
                cmbUser借贷科目[1, cmbNumber2].Top = cmbUser借贷科目[1, cmbNumber2 - 1].Bottom + 5;
                txtUserAnswer[1, cmbNumber2].Top = txtUserAnswer[1, cmbNumber2 - 1].Bottom + 4;
            }
            panelFenluAnswer1.Controls.Add(cmbUser借贷[1, cmbNumber2]);
            panelFenluAnswer1.Controls.Add(cmbUser借贷科目[1, cmbNumber2]);
            panelFenluAnswer1.Controls.Add(txtUserAnswer[1, cmbNumber2]);
            additems(cmbUser借贷[1, cmbNumber2]);
            addItemsKemu(cmbUser借贷科目[1, cmbNumber2]);
            btnAdd1.Top = btnRemove1.Top = txtUserAnswer[1, cmbNumber2].Top;
            cmbNumber2++;
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            cmbUser借贷[2, cmbNumber3] = new ComboBox();
            cmbUser借贷[2, cmbNumber3].Font = cmb借贷2.Font;
            cmbUser借贷[2, cmbNumber3].Left = cmb借贷2.Left;
            cmbUser借贷[2, cmbNumber3].Size = cmb借贷2.Size;

            cmbUser借贷科目[2, cmbNumber3] = new ComboBox();
            cmbUser借贷科目[2, cmbNumber3].Font = cmb借贷科目2.Font;
            cmbUser借贷科目[2, cmbNumber3].Left = cmb借贷科目2.Left;
            cmbUser借贷科目[2, cmbNumber3].Size = cmb借贷科目2.Size;

            txtUserAnswer[2, cmbNumber3] = new TextBox();
            txtUserAnswer[2, cmbNumber3].Font = txtAnswer2.Font;
            txtUserAnswer[2, cmbNumber3].Left = txtAnswer2.Left;
            txtUserAnswer[2, cmbNumber3].Size = txtAnswer2.Size;

            if (cmbNumber3 == 0)
            {
                cmbUser借贷[2, cmbNumber3].Top = cmb借贷2.Bottom + 5;
                cmbUser借贷科目[2, cmbNumber3].Top = cmb借贷科目2.Bottom + 5;
                txtUserAnswer[2, cmbNumber3].Top = txtAnswer2.Bottom + 4;
            }
            else
            {
                cmbUser借贷[2, cmbNumber3].Top = cmbUser借贷[2, cmbNumber3 - 1].Bottom + 5;
                cmbUser借贷科目[2, cmbNumber3].Top = cmbUser借贷科目[2, cmbNumber3 - 1].Bottom + 5;
                txtUserAnswer[2, cmbNumber3].Top = txtUserAnswer[2, cmbNumber3 - 1].Bottom + 4;
            }
            panelFenluAnswer2.Controls.Add(cmbUser借贷[2, cmbNumber3]);
            panelFenluAnswer2.Controls.Add(cmbUser借贷科目[2, cmbNumber3]);
            panelFenluAnswer2.Controls.Add(txtUserAnswer[2, cmbNumber3]);
            additems(cmbUser借贷[2, cmbNumber3]);
            addItemsKemu(cmbUser借贷科目[2, cmbNumber3]);
            btnAdd2.Top = btnRemove2.Top = txtUserAnswer[2, cmbNumber3].Top;
            cmbNumber3++;
        }

        private void btnAdd3_Click(object sender, EventArgs e)
        {
            cmbUser借贷[3, cmbNumber4] = new ComboBox();
            cmbUser借贷[3, cmbNumber4].Font = cmb借贷3.Font;
            cmbUser借贷[3, cmbNumber4].Left = cmb借贷3.Left;
            cmbUser借贷[3, cmbNumber4].Size = cmb借贷3.Size;
            cmbUser借贷[3, cmbNumber4].BackColor = cmb借贷3.BackColor;

            cmbUser借贷科目[3, cmbNumber4] = new ComboBox();
            cmbUser借贷科目[3, cmbNumber4].Font = cmb借贷科目3.Font;
            cmbUser借贷科目[3, cmbNumber4].Left = cmb借贷科目3.Left;
            cmbUser借贷科目[3, cmbNumber4].Size = cmb借贷科目3.Size;
            cmbUser借贷科目[3, cmbNumber4].BackColor = cmb借贷科目3.BackColor;

            txtUserAnswer[3, cmbNumber4] = new TextBox();
            txtUserAnswer[3, cmbNumber4].Font = txtAnswer3.Font;
            txtUserAnswer[3, cmbNumber4].Left = txtAnswer3.Left;
            txtUserAnswer[3, cmbNumber4].Size = txtAnswer3.Size;
            txtUserAnswer[3, cmbNumber4].BackColor = txtAnswer3.BackColor;

            if (cmbNumber4 == 0)
            {
                cmbUser借贷[3, cmbNumber4].Top = cmb借贷3.Bottom + 5;
                cmbUser借贷科目[3, cmbNumber4].Top = cmb借贷科目3.Bottom + 5;
                txtUserAnswer[3, cmbNumber4].Top = txtAnswer3.Bottom + 4;
            }
            else
            {
                cmbUser借贷[3, cmbNumber4].Top = cmbUser借贷[3, cmbNumber4 - 1].Bottom + 5;
                cmbUser借贷科目[3, cmbNumber4].Top = cmbUser借贷科目[3, cmbNumber4 - 1].Bottom + 5;
                txtUserAnswer[3, cmbNumber4].Top = txtUserAnswer[3, cmbNumber4 - 1].Bottom + 4;
            }
            panelFenluAnswer3.Controls.Add(cmbUser借贷[3, cmbNumber4]);
            panelFenluAnswer3.Controls.Add(cmbUser借贷科目[3, cmbNumber4]);
            panelFenluAnswer3.Controls.Add(txtUserAnswer[3, cmbNumber4]);
            additems(cmbUser借贷[3, cmbNumber4]);
            addItemsKemu(cmbUser借贷科目[3, cmbNumber4]);
            btnAdd3.Top = btnRemove3.Top = txtUserAnswer[3, cmbNumber4].Top;
            cmbNumber4++;

        }

        private void btnAdd4_Click(object sender, EventArgs e)
        {
            cmbUser借贷[4, cmbNumber5] = new ComboBox();
            cmbUser借贷[4, cmbNumber5].Font = cmb借贷4.Font;
            cmbUser借贷[4, cmbNumber5].Left = cmb借贷4.Left;
            cmbUser借贷[4, cmbNumber5].Size = cmb借贷4.Size;

            cmbUser借贷科目[4, cmbNumber5] = new ComboBox();
            cmbUser借贷科目[4, cmbNumber5].Font = cmb借贷科目4.Font;
            cmbUser借贷科目[4, cmbNumber5].Left = cmb借贷科目4.Left;
            cmbUser借贷科目[4, cmbNumber5].Size = cmb借贷科目4.Size;

            txtUserAnswer[4, cmbNumber5] = new TextBox();
            txtUserAnswer[4, cmbNumber5].Font = txtAnswer4.Font;
            txtUserAnswer[4, cmbNumber5].Left = txtAnswer4.Left;
            txtUserAnswer[4, cmbNumber5].Size = txtAnswer4.Size;

            if (cmbNumber5 == 0)
            {
                cmbUser借贷[4, cmbNumber5].Top = cmb借贷4.Bottom + 5;
                cmbUser借贷科目[4, cmbNumber5].Top = cmb借贷科目4.Bottom + 5;
                txtUserAnswer[4, cmbNumber5].Top = txtAnswer4.Bottom + 4;
            }
            else
            {
                cmbUser借贷[4, cmbNumber5].Top = cmbUser借贷[4, cmbNumber5 - 1].Bottom + 5;
                cmbUser借贷科目[4, cmbNumber5].Top = cmbUser借贷科目[4, cmbNumber5 - 1].Bottom + 5;
                txtUserAnswer[4, cmbNumber5].Top = txtUserAnswer[4, cmbNumber5 - 1].Bottom + 4;
            }
            panelFenluAnswer4.Controls.Add(cmbUser借贷[4, cmbNumber5]);
            panelFenluAnswer4.Controls.Add(cmbUser借贷科目[4, cmbNumber5]);
            panelFenluAnswer4.Controls.Add(txtUserAnswer[4, cmbNumber5]);
            additems(cmbUser借贷[4, cmbNumber5]);
            addItemsKemu(cmbUser借贷科目[4, cmbNumber5]);
            btnAdd4.Top = btnRemove4.Top = txtUserAnswer[4, cmbNumber5].Top;
            cmbNumber5++;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (cmbNumber1 > 0)
            {
                cmbNumber1--;
                panelFenluAnswer.Controls.Remove(cmbUser借贷[0, cmbNumber1]);
                panelFenluAnswer.Controls.Remove(cmbUser借贷科目[0, cmbNumber1]);
                panelFenluAnswer.Controls.Remove(txtUserAnswer[0, cmbNumber1]);
                btnAdd.Top = btnRemove.Top = txtUserAnswer[0, cmbNumber1].Top - 25;
            }
        }

        private void btnRemove1_Click(object sender, EventArgs e)
        {
            if (cmbNumber2 > 0)
            {
                cmbNumber2--;
                panelFenluAnswer1.Controls.Remove(cmbUser借贷[1, cmbNumber2]);
                panelFenluAnswer1.Controls.Remove(cmbUser借贷科目[1, cmbNumber2]);
                panelFenluAnswer1.Controls.Remove(txtUserAnswer[1, cmbNumber2]);
                btnAdd1.Top = btnRemove1.Top = txtUserAnswer[1, cmbNumber2].Top - 25;
            }
        }

        private void btnRemove2_Click(object sender, EventArgs e)
        {
            if (cmbNumber3 > 0)
            {
                cmbNumber3--;
                panelFenluAnswer2.Controls.Remove(cmbUser借贷[2, cmbNumber3]);
                panelFenluAnswer2.Controls.Remove(cmbUser借贷科目[2, cmbNumber3]);
                panelFenluAnswer2.Controls.Remove(txtUserAnswer[2, cmbNumber3]);
                btnAdd2.Top = btnRemove2.Top = txtUserAnswer[2, cmbNumber3].Top - 25;
            }
        }

        private void btnRemove3_Click(object sender, EventArgs e)
        {
            if (cmbNumber4 > 0)
            {
                cmbNumber4--;
                panelFenluAnswer3.Controls.Remove(cmbUser借贷[3, cmbNumber4]);
                panelFenluAnswer3.Controls.Remove(cmbUser借贷科目[3, cmbNumber4]);
                panelFenluAnswer3.Controls.Remove(txtUserAnswer[3, cmbNumber4]);
                btnAdd3.Top = btnRemove3.Top = txtUserAnswer[3, cmbNumber4].Top - 25;
            }
        }

        private void btnRemove4_Click(object sender, EventArgs e)
        {
            if (cmbNumber5 > 0)
            {
                cmbNumber5--;
                panelFenluAnswer4.Controls.Remove(cmbUser借贷[4, cmbNumber5]);
                panelFenluAnswer4.Controls.Remove(cmbUser借贷科目[4, cmbNumber5]);
                panelFenluAnswer4.Controls.Remove(txtUserAnswer[4, cmbNumber5]);
                btnAdd4.Top = btnRemove4.Top = txtUserAnswer[4, cmbNumber5].Top - 25;
            }
        }

        private void btnFenluTiankong_Click(object sender, EventArgs e)
        {
            choiceTixing("btnFenluTiankong");
        }
        private void 四_2_Click(object sender, EventArgs e)
        {
            btnFenluTiankong_Click(null,null);
        } 
    }
}
