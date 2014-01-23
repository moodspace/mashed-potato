using System;
using System.IO;
using System.Windows.Forms;

namespace MyBucks
{
  public partial class DialogZip : Form
  {
    private string[] _filelist;
    private string _workingDir;

    public DialogZip(string[] filelist, string workingDir)
    {
      InitializeComponent();
      _filelist = filelist;
      _workingDir = workingDir;//to help set dest automatically, doesn't have to be app dir
    }

    private void button1_Click(object sender, EventArgs e)
    {
      saveFileDialogPack.FileName =
        Path.GetFileNameWithoutExtension(textBox2.Text);

      if (saveFileDialogPack.ShowDialog() == DialogResult.OK)
      {
        textBox2.Text = saveFileDialogPack.FileName;
      }
    }

    private void buttonPack_Click(object sender, EventArgs e)
    {
      bool mergeInsteadofOvrWrite = radioBtnMerge.Checked;

      string message = "";
      if (comboBox1.Text == "ZIP")
      {
        Archive.DoZip(_filelist, _workingDir, textBox2.Text, mergeInsteadofOvrWrite);
      }
      else if (comboBox1.Text == "mbA")
      {
        Archive.DoMbs(_filelist, textBox2.Text);
      }
      else if (comboBox1.Text == "7z")
      {
        message = Archive.Do7z(_filelist, textBox2.Text, mergeInsteadofOvrWrite);
      }

      MessageBox.Show("Tasks done!\r\n\r\n" + message.Replace("\r\n\r\n", "\r\n"));
      this.Close();
    }

    private void DialogZip_Load(object sender, EventArgs e)
    {
      textBox2.Text = Path.Combine(_workingDir, Path.ChangeExtension(_filelist[0], "zip"));
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
      buttonPack.Enabled = false;
      if (Archive.isZipSupported(textBox2.Text))
      {
        comboBox1.SelectedIndex = 0;
        buttonPack.Enabled = true;
      }
      if (Archive.isMbaSupported(textBox2.Text))
      {
        comboBox1.SelectedIndex = 1;
        buttonPack.Enabled = true;
      }
      if (textBox2.Text.ToLower().EndsWith(".7z"))
      {
        comboBox1.SelectedIndex = 2;
        buttonPack.Enabled = true;
      }

      //for 7z, cannot us is7zSupported since 7z supports a wide range of formats
    }
  }
}