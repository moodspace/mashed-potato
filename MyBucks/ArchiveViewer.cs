using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MyBucks.Properties;

namespace MyBucks
{
  public partial class ArchiveViewer : Form
  {
    private String archive_filename;

    private List<String> packFileNameOut;

    public ArchiveViewer(String path)
    {
      archive_filename = path;
      InitializeComponent();
    }

    #region codes to deal with virtual filesystem (of archive)

    private bool DirectoryExists(string dir)
    {
      if (dir.Length > 0)
        return dir.EndsWith("/");
      else
        return true;
    }

    //has some similar copies of classes under System.IO,
    //however here we deal with filesystem representing that of an archive
    private List<string> getAllFileSystemEntries(List<string> filelistsorted, string pathname)
    {
      //startI = -1 if at root, plus one in for loop to list all entries
      int startI = filelistsorted.IndexOf(pathname);
      List<string> files = new List<string>();
      for (int i = startI + 1; i < filelistsorted.Count; i++)
      {
        //make sure all children are of the same family
        if (filelistsorted[i].StartsWith(pathname))
          files.Add(filelistsorted[i]);
        else
          break;
      }
      return files;
    }

    /// <summary>
    /// Here, directory name is the full path without filename or directory name
    /// (parent dir of provided path)
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string GetDirectoryName(string path)
    {
      //case 1: ""
      //case 2: "abx/"
      //case 3: "abc.jpg"
      if (path == "")
        return path;

      path = "/" + path;

      //case 1: N/A
      //case 2: "/abx/"
      //case 3: "/abc.jpg"
      if (path.EndsWith("/"))
      {
        path = path.Substring(0, path.Length - 1);

        //case 1: N/A
        //case 2: "/abx"
        //case 3: "/abc(.jpg"
      }
      if (path.Remove(path.LastIndexOf("/")).StartsWith("/"))
      {
        return path.Remove(path.LastIndexOf("/")).Substring(1);
      }
      else
      {
        return path.Remove(path.LastIndexOf("/"));
      }
    }

    //used to display dir
    private string GetFileName(string path)
    {
      if (path.EndsWith("/"))
      {
        path = path.Substring(0, path.Length - 1);
      }
      return path.Substring(path.LastIndexOf("/") + 1);
    }

    //used to display dir
    private string GetFileNameWithoutExt(string path)
    {
      if (!path.EndsWith("/") && path.IndexOf(".") != -1)
        return GetFileName(path).Remove(GetFileName(path).LastIndexOf("."));
      else
        return GetFileName(path);
    }

    private List<string> getNextLvFileSystemEntries(List<string> filelistsorted, string pathname)
    {
      //startI = -1 if at root, plus one in for loop to list all entries
      int startI = filelistsorted.IndexOf(pathname);
      List<string> files = new List<string>();
      for (int i = startI + 1; i < filelistsorted.Count; i++)
      {
        //make sure all children are of the same family
        if (filelistsorted[i].StartsWith(pathname))
        {
          //cannot replace "" with "", need special care here
          string path_relative = filelistsorted[i];
          if (pathname != "")
            path_relative = path_relative.Replace(pathname, "");

          //a subfolder (which has only one separator) or a file (has no separator, therefore both index = -1)
          if (path_relative.IndexOf("/") == -1)
          {
            //must be a file, with no separator
            files.Add(filelistsorted[i]);
          }
          else if (path_relative.IndexOf("/") == path_relative.Length - 1)
          {
            //must be a subfolder, with one separator
            files.Add(filelistsorted[i]);
          }
        }
        else
        {
          break;
        }
      }
      return files;
    }

    #endregion codes to deal with virtual filesystem (of archive)

    #region goToDir

    /// <summary>
    /// goToDir has everything you need to navigate into a new directory,
    /// has error processing
    /// </summary>
    /// <param name="target directory"></param>
    private void goToDir(string dir)
    {
      //try
      {
        toolStripNav_DoNavigate(dir);

        displayCurrentDir(dir);

        //clean up ui
        listViewExp.Focus();

        //end clean up ui
      }

      //catch { MessageBox.Show(UI.findLangResString("Directory not found")); }
    }

    #endregion goToDir

    #region fx and behavior of navToolstrip

    private ToolStripItem clickedItem_fx;

    private void timerClickFxNavi_Tick(object sender, EventArgs e)
    {
      //wait for the fx to complete before do actual navigation
      clickedItem_fx.ForeColor = Color.Black;

      //navigate
      //clicked item is root
      if (clickedItem_fx.Text == Path.GetFileName(archive_filename))
      {
        goToDir("");
        timerClickFxNavi.Enabled = false;
        return;
      }

      //clicked item is not root, must ignore root while building path
      string path = "";

      foreach (ToolStripItem item in toolStripNav.Items)
      {
        if (item is ToolStripButton && item.Text != Path.GetFileName(archive_filename))
        {
          path += (item.Text + "/");
          if (item.Text == clickedItem_fx.Text)
            break;
        }
      }

      goToDir(path);
      timerClickFxNavi.Enabled = false;
    }

    private void toolStripNav_DoNavigate(string path)
    {
      toolStripNav.SuspendLayout();
      toolStripNav.Items.Clear();
      while (path != "")
      {
        toolStripNav.Items.Insert(0, new ToolStripButton(GetFileName(path)));
        toolStripNav.Items.Insert(0, new ToolStripLabel(">"));
        path = GetDirectoryName(path);
      }
      toolStripNav.Items.Insert(0, new ToolStripButton(Path.GetFileName(archive_filename)));
      foreach (ToolStripItem item in toolStripNav.Items)
        item.ForeColor = Color.Black;//set forecolor to avoid compatibility issue in winxp

      toolStripNav.Items[toolStripNav.Items.Count - 1].ForeColor = Color.DeepSkyBlue;
      toolStripNav.ResumeLayout();
    }

    private void toolStripNav_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      if (e.ClickedItem is ToolStripButton)
      {
        e.ClickedItem.ForeColor = Color.DeepSkyBlue;
        clickedItem_fx = e.ClickedItem;
        timerClickFxNavi.Enabled = true;
      }
    }

    #endregion fx and behavior of navToolstrip

    #region displayCurrentDir

    /// <summary>
    ///
    /// </summary>
    /// <param name="Current directory to display"></param>
    private void displayCurrentDir(string currentDir)
    {
      listViewExp.Clear(); //refreshButtonState();

      //statusBar.Text = UI.showFilestatuslabel(this.explorerView);
      foreach (string path in getNextLvFileSystemEntries(packFileNameOut, currentDir))
      {
        string displayedFilename;
        if (Settings.Default.useFileExtInExp)
          displayedFilename = GetFileName(path);
        else
          displayedFilename = GetFileNameWithoutExt(path);

        if (DirectoryExists(path))
        {
          listViewExp.Items.Add(path, displayedFilename, UI.getImgKeyForFolder(listViewExp.LargeImageList, false));

          //a folder (ended with separator) (currrently no difference between empty or full
          UI.getImgKeyForFolder(listViewExp.SmallImageList, false);
        }
        else
        {
          listViewExp.Items.Add(path, displayedFilename, UI.getImgKeyFromFilename(listViewExp.LargeImageList, path));

          //a file
          UI.getImgKeyFromFilename(listViewExp.SmallImageList, path);
        }
      }

      //change titlebar
      string dirName = GetFileName(currentDir);
      if (dirName == "")
      {
        this.Text = UI.findLangResString("MyBucks Archive Viewer") + " - " + archive_filename;
      }
      else
      {
        if (Settings.Default.useLongPathInTitlebar)
        {
          this.Text = UI.findLangResString("MyBucks Archive Viewer") + " - " + currentDir;
        }
        else
        {
          this.Text = UI.findLangResString("MyBucks Archive Viewer") + " - " + dirName;
        }
      }
    }

    #endregion displayCurrentDir

    private void buttonExtract_ButtonClick(object sender, EventArgs e)
    {
      if (folderBrowserDialogExtract.ShowDialog() != DialogResult.OK)
      { return; }

      String dest = Path.Combine(folderBrowserDialogExtract.SelectedPath,
          Path.GetFileNameWithoutExtension(archive_filename));

      DlgExtract extractDlg = new DlgExtract(getFilesToExtract(), archive_filename, dest);
      extractDlg.ShowDialog();

      frmExplorer explorerExtracted = new frmExplorer(dest);
      explorerExtracted.Show();
    }

    private void buttonExtractCurr_Click(object sender, EventArgs e)
    {
      String dest = Path.Combine(Path.GetDirectoryName(archive_filename),
        Path.GetFileNameWithoutExtension(archive_filename));

      List<string> filesToOpen = getFilesToExtract();

      DlgExtract extractDlg = new DlgExtract(filesToOpen, archive_filename, dest);
      extractDlg.ShowDialog();

      frmExplorer explorer = new frmExplorer(dest);
      explorer.Show();
    }

    private void buttonExtractTmp_Click(object sender, EventArgs e)
    {
      List<string> filesToOpen = getFilesToExtract();

      String dest = Path.Combine(Application.LocalUserAppDataPath,
        "mbstemp~" + Convert.ToString(DateTime.Now.TimeOfDay.ToString().GetHashCode(), 16));
      DlgExtract extractDlg = new DlgExtract(filesToOpen, archive_filename, dest);
      extractDlg.ShowDialog();

      frmExplorer explorer = new frmExplorer(dest);
      explorer.Show();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private List<string> getFilesToExtract()
    {
      List<string> returnedValue = new List<string>();
      if (listViewExp.SelectedIndices.Count == 0)
      {
        returnedValue.AddRange(packFileNameOut);
      }
      else
      {
        foreach (ListViewItem item in listViewExp.SelectedItems)
        {
          returnedValue.Add(item.Name);
          //add files within a dir
          if (item.Name.EndsWith("/"))
          {
            returnedValue.AddRange(getAllFileSystemEntries(packFileNameOut, item.Name));
          }
        }
      }
      return returnedValue;
    }

    //private void changeEnabledOfAllExButtons(Boolean toEnable)
    //{
    //  if (toEnable)
    //    buttonExTmp.Enabled = buttonExCurr.Enabled = buttonEx.Enabled = buttonEx.Enabled = true;
    //  else
    //    buttonExTmp.Enabled = buttonExCurr.Enabled = buttonEx.Enabled = buttonEx.Enabled = false;
    //}

    #region Load event

    /// <summary>
    /// set font, string resources, invoke methods to load filenames
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DialogExtract_Load(object sender, EventArgs e)
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
      for (int i = 0; i < 5; i++)
      {
        //i may not exist
        try
        {
          UI.switchLangInObjs(this, "Control", i, Properties.Settings.Default.languageIndexSettings);
          UI.switchLangInObjs(contextMenuExp, "Cntx", i, Properties.Settings.Default.languageIndexSettings);
          UI.switchLangInObjs(toolStripMain, "Tool", i, Properties.Settings.Default.languageIndexSettings);
          UI.switchLangInObjs(menuStrip.Items, "Menu", i, Properties.Settings.Default.languageIndexSettings);

          //unify different srcLang (if exist) to display in targetLang
        }
        catch { }
      }

      buttonExtractTmp.Text = UI.findLangResString("buttonExtractTmp");
      buttonExtractCurr.Text = UI.findLangResString("buttonExtractCurr");

      if (Archive.isMbaSupported(archive_filename))
      {
        loadMbaFileNames();
        timerListFilenames.Enabled = true;
      }
      else if (Archive.is7zSupported(archive_filename))
      {
        load7zFilenames();
        timerListFilenames.Enabled = true;
      }
      else if (Archive.isZipSupported(archive_filename))
      {
        loadZipFilenames();
        timerListFilenames.Enabled = true;
      }
      else if (Archive.isUnrarSupported(archive_filename))
      {
        loadRarFilenames();
        timerListFilenames.Enabled = true;
      }
    }

    #endregion Load event

    #region load filenames of an mba archive

    private void loadMbaFileNames()
    {
      packFileNameOut = new List<String>();

      int fileCount = IO.readHeader(archive_filename);
      if (fileCount < 1)
        return;
      //return if file is corrupt

      try
      {
        long lastFilenamePosit = 4;
        long lastDataPosit;

        for (int i = 0; i < fileCount; i++)
        {
          lastDataPosit = lastFilenamePosit;
          String lastFilenameRAW = IO.readFilenameWithEnd(archive_filename, lastFilenamePosit).Trim();
          packFileNameOut.Add(lastFilenameRAW.Substring(0, lastFilenameRAW.LastIndexOf("|")));
          lastDataPosit = long.Parse(lastFilenameRAW.Substring(lastFilenameRAW.LastIndexOf("|") + 1));

          //lastDataPosit is the beginning of the current file's data when the current filename was read
          lastFilenamePosit = IO.readDataEnd(archive_filename, lastDataPosit);
        }
      }
      catch
      {
        return;
      }
    }

    #endregion load filenames of an mba archive

    #region load filenames of other types of archive

    private void load7zFilenames()
    { packFileNameOut = Archive.list7zFilenames(archive_filename); }

    private void loadRarFilenames()
    { packFileNameOut = Archive.listRarFilenames(archive_filename); }

    private void loadZipFilenames()
    {
      packFileNameOut = new List<String>();

      Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile(archive_filename, System.Text.Encoding.GetEncoding("GBK"));

      foreach (Ionic.Zip.ZipEntry entry in zipfile)
      {
        packFileNameOut.Add(entry.FileName);
      }
    }

    #endregion load filenames of other types of archive

    private void listViewPackExp_KeyDown(object sender, KeyEventArgs e)
    {
      if (!e.Shift && !e.Alt)
      {
        if (e.KeyCode == Keys.Enter)
          listViewPackExp_DoubleClick(this, new EventArgs());
        else if (e.KeyCode == Keys.Back)
          buttonUp_DoubleClick(this, new EventArgs());
      }
    }

    private void openArchiveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (openArchiveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        Shared.openArchive(openArchiveDialog.FileName);
    }

    private void timerListFilenames_Tick(object sender, EventArgs e)
    {
      goToDir("");
      timerListFilenames.Enabled = false;
    }

    #region extract to temp (for a single file) and run

    private void listViewPackExp_DoubleClick(object sender, EventArgs e)
    {
      if (listViewExp.SelectedIndices.Count == 1)
      {
        if (DirectoryExists(listViewExp.SelectedItems[0].Name))
        {
          goToDir(listViewExp.SelectedItems[0].Name);
        }
        else
        {
          List<string> filesToOpen = getFilesToExtract();

          String dest = Path.Combine(Application.LocalUserAppDataPath,
            "mbstemp~" + Convert.ToString(DateTime.Now.TimeOfDay.ToString().GetHashCode(), 16));
          DlgExtract extractDlg = new DlgExtract(filesToOpen, archive_filename, dest);
          extractDlg.ShowDialog();

          System.Diagnostics.Process.Start(Path.Combine(dest, filesToOpen[0].Replace("/", "\\")));
        }
      }
    }

    #endregion extract to temp (for a single file) and run

    #region behavior and fx of findBox

    private void findBox_MouseLeave(object sender, EventArgs e)
    { listViewExp.Focus(); }

    private void findBox_TextChanged(object sender, EventArgs e)
    {
      foreach (ListViewItem item in listViewExp.Items)
      {
        if (findBox.Text != "" && item.Text.ToLower().Contains(findBox.Text.ToLower()))
          item.Selected = true;
        else
          item.Selected = false;
      }
      if (listViewExp.SelectedIndices.Count == 0 && findBox.Text != "")
        findBox.BackColor = Color.LightCoral;
      else
        findBox.BackColor = Color.WhiteSmoke;
    }

    #endregion behavior and fx of findBox

    #region behavior and fx of 'up' button(strip)

    private void buttonUp_DoubleClick(object sender, EventArgs e)
    {
      if (toolStripNav.Items.Count - 3 >= 0)
        toolStripNav.Items[toolStripNav.Items.Count - 3].PerformClick();
    }

    private void buttonUp_MouseDown(object sender, MouseEventArgs e)
    { buttonUp.BackColor = Color.SlateGray; }

    private void buttonUp_MouseUp(object sender, MouseEventArgs e)
    { buttonUp.BackColor = Color.LightSteelBlue; }

    #endregion behavior and fx of 'up' button(strip)

    #region selection

    private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (ListViewItem item in listViewExp.Items)
        item.Selected = !item.Selected;
    }

    private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (listViewExp.SelectedIndices.Count != listViewExp.Items.Count)
      {
        foreach (ListViewItem item in listViewExp.Items)
          item.Selected = true;
      }
    }

    #endregion selection

    #region View Mode

    private void buttonView_Click(object sender, EventArgs e)
    {
      switch (listViewExp.View)
      {
        case View.LargeIcon:
          smallIconsToolStripMenuItem.PerformClick(); break;
        case View.SmallIcon:
          listToolStripMenuItem.PerformClick(); break;
        case View.List:
          tileToolStripMenuItem.PerformClick(); break;
        case View.Tile:
          largeIconsToolStripMenuItem.PerformClick(); break;
      }
    }

    private void largeIconsToolStripMenuItem_Click(object sender, EventArgs e)
    { listViewExp.View = View.LargeIcon; }

    private void listToolStripMenuItem_Click(object sender, EventArgs e)
    { listViewExp.View = View.List; }

    private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
    { listViewExp.View = View.SmallIcon; }

    private void tileToolStripMenuItem_Click(object sender, EventArgs e)
    { listViewExp.View = View.Tile; }

    #endregion View Mode
  }
}