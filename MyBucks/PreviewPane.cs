using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyBucks
{
  public partial class PreviewPane : UserControl
  {
    private string _filename;

    public string filename
    {
      get { return _filename; }
      set { _filename = value; }
    }

    public PreviewPane()
    {
      InitializeComponent();
    }

    public void displayContent()
    {
      try
      {
        FileInfo info = new FileInfo(_filename);
        if (Properties.Settings.Default.previewPicFormats.Contains(info.Extension.ToLower().Substring(1)))//mostly txt
        {
          picPane.LoadAsync(info.FullName);
          picPane.BringToFront();
        }
        else if (Properties.Settings.Default.previewWebFormats.Contains(info.Extension.ToLower().Substring(1)))//mostly txt
        {
          webPane.Navigate(info.FullName);
          webPane.BringToFront();
        }
        else if (info.Length < 65536)//mostly txt
        {
          using (StreamReader reader = new StreamReader(_filename))
          {
            txtPane.Text = "\r\n" + reader.ReadToEnd();
          }
          txtPane.BringToFront();
        }
        else
        {
          label1.BringToFront();

          // label No Preview will be shown
        }
      }
      catch
      {
        label1.BringToFront();
      }
    }

    private void X_Click(object sender, System.EventArgs e)
    {
      this.Visible = false;
    }

    private void X_MouseDown(object sender, MouseEventArgs e)
    {
      X.BackColor = Color.FromArgb(120, Color.DimGray);
    }

    private void X_MouseUp(object sender, MouseEventArgs e)
    {
      X.BackColor = Color.FromArgb(50, Color.White);
    }

    private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
    {
    }

    private int lastVal = 5;

    private void hScrollBar1_ValueChanged(object sender, System.EventArgs e)
    {
      this.Width = 450 - hScrollBar1.Value * 50;
      this.Left += (hScrollBar1.Value - lastVal) * 50;

      //hScrollBar1.Left = this.Width - hScrollBar1.Width;
      lastVal = hScrollBar1.Value;
    }
  }
}