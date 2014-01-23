using System;
using System.Drawing;
using System.Windows.Forms;
using MyBucks.Properties;
using System.IO;

namespace MyBucks
{
  public partial class DialogSettings : Form
  {
    public DialogSettings()
    {
      InitializeComponent();
    }
    
    private void DialogSettings_Load(object sender, EventArgs e)
    {
      this.Text = UI.findLangResString("Settings");

      //load settings data
      comboBoxPickLang.Items.AddRange(UI.langRes[0]);
      comboBoxPickIcon.Items.AddRange(UI.getIconsetNames());
      comboBoxPickIcon.Items.Insert(0, UI.findLangResString("[default]"));
      
      //full list of font will be updated during dropping down
      if (Settings.Default.customFontFamily == "[default]")
        comboBoxPickFont.Items.Add(UI.findLangResString("[default]"));
      else
        comboBoxPickFont.Items.Add(Settings.Default.customFontFamily);

      //load user settings values from application settings
      checkBox1.Checked = Settings.Default.useLongPathInTitlebar;
      checkBox2.Checked = Settings.Default.useFileExtInExp;
      comboBoxPickLang.SelectedIndex = Settings.Default.languageIndexSettings;
      comboBoxPickIcon.SelectedIndex = Settings.Default.iconIndexSettings;
      comboBoxPickFont.SelectedIndex = 0;

      //apply translation
      label1.Text = UI.findLangResString("Language");
      label2.Text = UI.findLangResString("Font");
      tabControl1.TabPages[0].Text = UI.findLangResString("Display");
      checkBox1.Text = UI.findLangResString("show_full_path_titlebar");
      checkBox2.Text = UI.findLangResString("show_file_ext");

      tabControl1.TabPages[1].Text = UI.findLangResString("Path");
      button1.Text = UI.findLangResString("Clear Temporary Files");
      button2.Text = UI.findLangResString("Open Temporary Folder");

    }

    private void comboBoxPickFont_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBoxPickFont.Items[comboBoxPickFont.SelectedIndex].ToString() == UI.findLangResString("[default]"))
      {
        UI.getDefaultFont();
        Settings.Default.customFontFamily = "[default]";
      }
      else
      {
        try
        {
          string familyName = comboBoxPickFont.Items[comboBoxPickFont.SelectedIndex].ToString();
          Font testIfValid = new Font(new FontFamily(familyName, new System.Drawing.Text.InstalledFontCollection()), 9.0F);
          Settings.Default.customFontFamily = familyName;
        }
        catch { MessageBox.Show(UI.findLangResString("font_not_supported")); }
      }
    }

    private void comboBoxPickFont_DropDown(object sender, EventArgs e)
    {
      comboBoxPickFont.Items.Clear();
      System.Drawing.Text.InstalledFontCollection sysCollection = new System.Drawing.Text.InstalledFontCollection();
      foreach (FontFamily family in sysCollection.Families)
      {
        comboBoxPickFont.Items.Add(family.Name);
      }

      comboBoxPickFont.Items.Add(UI.findLangResString("[default]"));
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      Settings.Default.useLongPathInTitlebar = checkBox1.Checked;
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
      Settings.Default.useFileExtInExp = checkBox2.Checked;
    }

    private void DialogSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.iconIndexSettings = comboBoxPickIcon.SelectedIndex;
      UI.switchLang(comboBoxPickLang.SelectedIndex, "frmExplorer");
      //switchLang invokes style change in all opened frmExplorer
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DirectoryInfo tempfolderInfo = new DirectoryInfo(Application.LocalUserAppDataPath);
      foreach (DirectoryInfo subTempfolderInfo in tempfolderInfo.GetDirectories("mbstemp~*"))
      {
        subTempfolderInfo.Delete(true);
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start(Application.LocalUserAppDataPath);
    }

  }
}