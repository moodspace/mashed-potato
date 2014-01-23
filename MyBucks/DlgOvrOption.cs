using System;
using System.IO;
using System.Windows.Forms;

namespace MyBucks
{
  public partial class DlgOvrOption : Form
  {
    /// <summary>
    /// infoA: the file to overwite
    /// infoB: the file to be overwritten
    /// </summary>
    /// <param name="infoA"></param>
    /// <param name="infoB"></param>
    public DlgOvrOption(FileInfo infoA, FileInfo infoB)
    {
      InitializeComponent();
      label1.Text = label1.Text.Replace("A", infoA.FullName);
      label2.Text = label2.Text.Replace("B", infoB.FullName);
      label2.Text += "\r\nSize: " + Utility.getReadableFileLength(infoB.FullName);
    }

    /// <summary>
    /// fileA: the file to overwite
    /// fileB: the file to be overwritten
    /// </summary>
    /// <param name="infoA"></param>
    /// <param name="infoB"></param>
    public DlgOvrOption(string fileA, string fileB)
    {
      InitializeComponent();
      label1.Text = label1.Text.Replace("A", fileA);
      label2.Text = label2.Text.Replace("B", fileB);
      label2.Text += "\r\nSize: " + Utility.getReadableFileLength(fileB);
    }

    private Utility.OvrDialogResult dialogresult = Utility.OvrDialogResult.abort;

    internal Utility.OvrDialogResult realDialogresult
    {
      get { return dialogresult; }
    }

    private void yes_Click(object sender, EventArgs e)
    {
      dialogresult = Utility.OvrDialogResult.yes;
      this.Close();
    }

    private void no_Click(object sender, EventArgs e)
    {
      dialogresult = Utility.OvrDialogResult.no;
      this.Close();
    }

    private void allYes_Click(object sender, EventArgs e)
    {
      dialogresult = Utility.OvrDialogResult.allYes;
      this.Close();
    }

    private void allNo_Click(object sender, EventArgs e)
    {
      dialogresult = Utility.OvrDialogResult.allNo;
      this.Close();
    }

    private void abort_Click(object sender, EventArgs e)
    {
      dialogresult = Utility.OvrDialogResult.abort;
      this.Close();
    }

    private void DlgOvrOption_Load(object sender, EventArgs e)
    {
    }
  }
}