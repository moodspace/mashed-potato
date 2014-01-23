using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MyBucks.Properties;

namespace MyBucks
{
  public partial class DlgExtract : Form
  {
    private List<string> filelist;
    private string packpath;
    private string destpath;

    public DlgExtract(List<string> filenamesSelected, string archive_filename, string dest)
    {
      InitializeComponent();
      filelist = filenamesSelected;
      packpath = archive_filename;
      destpath = dest;
    }

    private void extractAll(object objArrinObj)
    {
      Utility.OvrDialogResult globalOvrOption = Utility.OvrDialogResult.empty;

      //used to select correct file option;
      //only accept allYes, allNo and empty(initial)!

      //declare two delegates here
      updateProg myhandleProg = new updateProg(progPlusPlus);
      updateFilename myhandleProgText = new updateFilename(progShowFilename);

      object[] objArr = (object[])objArrinObj;
      string archivefile = (string)objArr[1];
      List<string> filenames = (List<string>)objArr[2];
      string dest = (string)objArr[3];

      string mode = (string)objArr[0];

      foreach (string filename_relative in filenames)
      {
        //display next filename
        this.Invoke(myhandleProgText, new object[1] { filename_relative });

        string combinedDestPath = Path.Combine(dest, filename_relative.Replace("/", "\\"));

        //confirmation of overwriting

        if (File.Exists(combinedDestPath) || Directory.Exists(combinedDestPath))
        {
          //if globalOvrOption is allYes, then skip this part (and process all duplicates)
          if (globalOvrOption == Utility.OvrDialogResult.allYes)
          {
          }

          //allNo, skip all !duplicates!
          else if (globalOvrOption == Utility.OvrDialogResult.allNo)
            continue;
          else
          {//empty, (yes, no : not accepted)
            DlgOvrOption option = new DlgOvrOption(filename_relative, combinedDestPath);
            option.ShowDialog();

            //if option.realDialogresult is yes, then skip this part (and process this duplicate)
            if (option.realDialogresult == Utility.OvrDialogResult.no)
              continue;

            //skip current single file, will prompt again in face of duplicate
            else if (option.realDialogresult == Utility.OvrDialogResult.abort)
              break;

            //no more files will be extracted and no more file exist confirmation prompted
            else if (option.realDialogresult == Utility.OvrDialogResult.allYes)
              globalOvrOption = Utility.OvrDialogResult.allYes;

            //in the following loopno more files will be extracted and no more file exist confirmation prompted
            else if (option.realDialogresult == Utility.OvrDialogResult.allNo)
            {
              globalOvrOption = Utility.OvrDialogResult.allNo; continue;
            }
          }
        }

        switch (mode)
        {
          case "ZIP":
            Archive.UnZipSingle(filename_relative, archivefile, dest);
            break;

          case "7Z":
            Archive.Un7zSingle(filename_relative, archivefile, dest);
            break;

          case "RAR":
            Archive.UnRarSingle(filename_relative, archivefile, dest);
            break;

          case "MBA":
            Archive.UnMbaSingle(filename_relative, archivefile, dest);
            break;
        }
        this.Invoke(myhandleProg);

        //progress plus plus after a file's done
      }
    }

    private delegate void updateProg();

    private delegate void updateFilename(string filename);

    private void progPlusPlus()
    {
      this.progressBar1.Value++;
    }

    private void progShowFilename(string filename)
    {
      this.Text = UI.findLangResString("Extracting") + " " + filename;
    }

    private Thread extractThread;

    internal object[] extractLoader(string mode)
    {
      object[] objParams = { mode, packpath, filelist, destpath };
      return objParams;
    }

    private void timerDetectFinish_Tick(object sender, EventArgs e)
    {
      if (extractThread == null)
      {
        timerDetectFinish.Enabled = false;
        this.Close();
      }
      else
      {
        if (extractThread.ThreadState == System.Threading.ThreadState.Stopped)
        {
          progressBar1.Value = progressBar1.Maximum;
          this.Text = UI.findLangResString("Operation complete");
          timerDetectFinish.Enabled = false;
          this.Close();
        }
      }
    }

    private void buttonAbort_Click(object sender, EventArgs e)
    {
      progressBar1.Value = progressBar1.Maximum;
      this.Text = UI.findLangResString("Operation aborted");
      extractThread.Abort();
    }

    private void DlgExtract_Load(object sender, EventArgs e)
    {
      Font currentFont;
      if (Settings.Default.customFontFamily == "[default]")
      {
        currentFont = Settings.Default.customFont;
      }
      else
      {
        currentFont = new Font(new FontFamily(Settings.Default.customFontFamily),
          Settings.Default.customFontSize, FontStyle.Regular);
      }

      this.Font = currentFont;

      buttonAbort.Text = UI.findLangResString("Abort");

      Directory.CreateDirectory(destpath);

      object[] startParam = null;
      if (Archive.isZipSupported(packpath))
        startParam = extractLoader("ZIP");
      else if (Archive.is7zSupported(packpath))
        startParam = extractLoader("7Z");
      else if (Archive.isUnrarSupported(packpath))
        startParam = extractLoader("RAR");
      else if (Archive.isMbaSupported(packpath))
        startParam = extractLoader("MBA");
      if (startParam != null)
      {
        progressBar1.Value = 0;
        progressBar1.Maximum = filelist.Count;
        extractThread = new Thread(new ParameterizedThreadStart(extractAll));
        extractThread.Start(startParam);
      }
      timerDetectFinish.Enabled = true;
    }
  }
}