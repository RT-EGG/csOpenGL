using System;
using System.Windows.Forms;
using GLDelaunayTriangulation;

namespace GLDelaunayDiagram
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TFormMain());
        }
    }
}
