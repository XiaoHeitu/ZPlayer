﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Forms;

namespace XiaoHeitu.ZPlayer.WinForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args.FirstOrDefault()));
            //Application.Run(new blankTest.Form1());
            //Application.Run(new OsdForm());
        }
    }
}
