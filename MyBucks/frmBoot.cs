using System;
using System.IO;
using System.Windows.Forms;

namespace MyBucks
{
  public partial class frmBoot : Form
  {
    public frmBoot(String[] args)
    {
      InitializeComponent();

      UI.getDefaultFont();

      UI.readLangRes();

      UI.langResKeysReg.Clear();
      foreach (string langEntryKey in UI.langResKeysReg_txt)
      {
        UI.langResKeysReg.Add(langEntryKey.Split("\t".ToCharArray()));
      }

      if (args.Length != 0)
      {
        if (Directory.Exists(args[0]))
        {
          Program.LaunchNew(args[0]);
        }
        else if (Archive.isArchiveSupported(args[0]))
        {
          ArchiveViewer exDlg = new ArchiveViewer(args[0]);
          exDlg.Show();
        }
        else
        {
          Program.LaunchNew();
        }
      }
      else
      {
        Program.LaunchNew();
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.Visible = false;
      if (Application.OpenForms.Count < 2)
      {
        Properties.Settings.Default.Save(); timer1.Enabled = false; Environment.Exit(0);
      }
    }
  }
}