using System;
using System.Windows.Forms;

namespace MyBucks
{
  internal static class Program
  {
    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>

    [STAThread]
    private static void Main(String[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new frmBoot(args));
    }

    internal static void LaunchNew()
    {
      frmExplorer newInstance = new frmExplorer();
      newInstance.Show();
    }

    internal static void LaunchNew(String startup_path)
    {
      frmExplorer newInstance = new frmExplorer(startup_path);
      newInstance.Show();
    }
  }
}