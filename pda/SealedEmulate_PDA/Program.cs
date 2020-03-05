using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SealedEmulate_PDA._2020; 

namespace SealedEmulate_PDA
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(frmLogin.GetInstance());
            //Application.Run(new CGMK_CGSH());
        }
    }
}