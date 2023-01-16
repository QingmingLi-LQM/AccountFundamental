using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 华泽_会计基础模拟考试
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmStart());
         //   Application.Run(new frmMain());
        //   Application.Run(new 功能测试());
           // Application.Run(new errorReport());
          // Application.Run(new scoreDiagram());
          //  Application.Run(new Exercise());
          // Application.Run(new 计算填空());
          //   Application.Run(new 计算分录());
        }
    }
}
