using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

using System.Runtime.InteropServices;
//using System.Management;
//using System.Threading;


namespace 华泽_会计基础模拟考试
{
    public partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {  
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor ;
            frmMain myfrm = new frmMain();
            myfrm.Show();
            MessageBox.Show("点击确定后，计时开始！时间60分钟！");
            myfrm.timer1.Interval = 1000;
            myfrm.timer1.Start();
            Cursor.Current = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Exercise myfrm = new Exercise();
            myfrm.Show();
            myfrm.timlianxi.Interval = 1000;
            myfrm.timlianxi.Start();
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
          //  checkCDROM();
        }

        private void frmStart_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("软件开发：上海建桥学院 甘家宝工作室");
        }

        private void checkCDROM()
        {
            string s = "";
            StringBuilder volumeName = new StringBuilder(256);
            int srNum = new int();
            int comLen = new int();
            string sysName = "";
            int sysFlags = new int();
            int result;
            string[] logDrives = System.IO.Directory.GetLogicalDrives();
            int i = 0;
            for (i = 0; i < logDrives.Length; i++)
            {
                if (api.GetDriveType(logDrives[i]) == 5)
                {
                    result = api.GetVolumeInformation(logDrives[i].ToString(), volumeName, 256, srNum, comLen, sysFlags, sysName, 256);
                    if (result == 0)
                    {
                        MessageBox.Show("光驱中无光盘，请插入正确的光盘，重新启动应用程序！","重要提示");
                        this.Close();
                    }
                    else
                    {
                        s = volumeName.ToString();
                        if(s=="华泽-会计基础")
                            return;
                        else
                        {
                            MessageBox.Show("光盘不符，请更换正确光盘，重新启动应用程序!", "重要提示");
                            this.Close();
                        }
                    }
                }
            }
            if (i >= logDrives.Length)
            {
                MessageBox.Show("你的计算机没有光驱，不能正常运行此程序！");
                this.Close();
            }
        }
  }
class api
 {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
        public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        [DllImport("kernel32.dll", EntryPoint = "GetVolumeInformationA")]
        public static extern int GetVolumeInformation(string lpRootPathName, StringBuilder lpVolumeNameBuffer, int nVolumeNameSize, int lpVolumeSerialNumber, int lpMaximumComponentLength, int lpFileSystemFlags, string lpFileSystemNameBuffer, int nFileSystemNameSize);
        [DllImport("kernel32.dll", EntryPoint = "GetDriveTypeA")]
        public static extern int GetDriveType(string nDrive);
  }
}

