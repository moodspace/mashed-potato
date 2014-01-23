using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MyBucks.Properties;

namespace MyBucks
{
  public partial class frmExplorer : Form
  {
    public frmExplorer()
    {
      //Program.realFormCount += 1;
      InitializeComponent();
      _startupPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    public frmExplorer(string startupPath)
    {
      //Program.realFormCount += 1;
      InitializeComponent();
      _startupPath = startupPath;
    }

    private string _startupPath;
    private string currentDir;
    private string currentRootDir;

    private List<string> historyBack = new List<string>();
    private List<string> historyForward = new List<string>();

    private bool loadDefaultDir = true;

    #region goToDir

    /// <summary>
    /// goToDir has everything you need to navigate into a new directory,
    /// has error processing
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="invokedByBackAndFore, false in most cases"></param>
    private void goToDir(string dir)
    {
      try
      {
        if (File.Exists(dir))
        {
          System.Diagnostics.Process.Start(dir);
          return;
        }

        if (dir == "mbs-mpc")
        {
          goToMyComputer();
          return;
        }

        if (navigatingSignalRunning)
        { navigatingSignalRunning = false; return; }
        else
          navigatingSignalRunning = true;

        if (dir == currentDir)//code proceeded during refresh
        {

        }
        else//other code can be found in displayCurrentDir
        {
          foreach (ToolStripItem filter in dropdownButtonFilter.DropDownItems)
          {
            ((ToolStripMenuItem)filter).Checked = false;
          }
        }
        //prep

        //avoid flashing
        treeExplorer.BeginUpdate();

        //correct path
        dir = Utility.correctPath(dir);

        //startup path will be stored twice, one at initialization, another here
        if (currentDir != dir)
        {
          if (historyBack[historyBack.Count - 1] != dir)
          {
            historyBack.Add(dir);
            historyForward.Clear();

            //if navigating to a path different from Forward list (storing steps reversed)
            //clear Forward list
          }
        }

        //end prep

        currentDir = dir;
        currentRootDir = Path.GetPathRoot(currentDir);

        //illustrate how to locate C:\Users\admin

        //find C:\
        TreeNode currentLevelNode = (TreeNode)treeExplorer.Nodes[0].Nodes.Find(currentRootDir, false).GetValue(0);
        TreeNodeCollection currentLevelSubNodes = expandFSNode(currentLevelNode);
        int level = 1;

        while (currentLevelNode.Name != currentDir)// first loop: "C:\" != "C:\Users\admin"
        {
          //level increases as node selected and expanded,
          //this top-down routine makes full use of treeview_afterselect()

          //find node to select (and afterwards, expand)
          if (currentLevelSubNodes != null)
          {
            currentLevelNode = (TreeNode)currentLevelSubNodes.Find(getParentFolderAtSpecificLevel(level, currentDir), false).GetValue(0);
            currentLevelSubNodes = expandFSNode(currentLevelNode);
            level++;
          }
          else
          {
            break;

            //occurs on dirs(drives) unable to access (and therefore causing a redirect).
            //in this case, currentDir will be reset while currentLevelNode hasn't been reset yet
          }
        }

        toolStripNav_DoNavigate(currentDir);

        displayCurrentDir(currentDir);

        //clean up ui
        treeExplorer.EndUpdate();
        comboNavi.Text = currentDir;
        explorerView.Focus();

        navigatingSignalRunning = false;

        //end clean up ui
      }
      catch { MessageBox.Show(UI.findLangResString("Directory not found")); }
    }

    #endregion goToDir

    #region displayCurrentDir

    /// <summary>
    /// Can display a physical folder into explorerView, including icon updating
    /// for special folders, use displayMyComputer
    /// </summary>
    /// <param name="currentDir"></param>
    private void displayCurrentDir(string currentDir)
    {
      try
      {
        fileSystemWatcher1.Path = currentDir;
      }
      catch { }

      explorerView.Items.Clear(); refreshButtonState();
      //DO NOT use explorerView.Clear(), which also wipes out columns

      foreach (ColumnHeader header in explorerView.Columns)
        header.Tag = "mbs-list-sortnone";

      statusBar.Text = UI.showFilestatuslabel(explorerView);
      statusBar.Image = Resources.sort_none;//initially displayed in the order 'files->folders'

      foreach (string path in Directory.GetFiles(currentDir))
      {
        string displayedFilename;
        if (Settings.Default.useFileExtInExp)
          displayedFilename = Path.GetFileName(path);
        else
          displayedFilename = Path.GetFileNameWithoutExtension(path);

        string[] properties = new string[4];

        FileInfo info = new FileInfo(path);
        string filelength, creationTime, lastAccessTime, lastModifiedTime;
        filelength = string.Format("{0:N} {1}", info.Length, UI.findLangResString("bytes")).PadLeft(30);
        creationTime = info.CreationTime.ToString();
        lastAccessTime = info.LastAccessTime.ToShortDateString().ToString();
        lastModifiedTime = info.LastWriteTime.ToShortDateString().ToString();
        properties = new string[4] { filelength, creationTime, lastAccessTime, lastModifiedTime };

        ListViewItem item = new ListViewItem(displayedFilename, UI.getImgKey(explorerView.LargeImageList, path));
        item.Name = path;
        item.SubItems.AddRange(properties);
        UI.getImgKey(explorerView.SmallImageList, path);

        explorerView.Items.Add(item);
      }

      foreach (string path in Directory.GetDirectories(currentDir))
      {
        string[] properties = new string[4];

        DirectoryInfo info = new DirectoryInfo(path);
        string creationTime, lastAccessTime, lastModifiedTime;
        creationTime = info.CreationTime.ToShortDateString().ToString();
        lastAccessTime = info.LastAccessTime.ToShortDateString().ToString();
        lastModifiedTime = info.LastWriteTime.ToShortDateString().ToString();
        properties = new string[4] { "", creationTime, lastAccessTime, lastModifiedTime };
        
        ListViewItem item = new ListViewItem(Path.GetFileName(path), UI.getImgKey(explorerView.LargeImageList, path));
        item.Name = path;
        item.SubItems.AddRange(properties);
        UI.getImgKey(explorerView.SmallImageList, path);

        explorerView.Items.Add(item);
      }

      //change titlebar
      string dirName = Path.GetFileName(currentDir);
      if (dirName == "")
      {
        //usually C:\ G:\
        this.Text = UI.findLangResString("MyBucks") + " - " + currentDir;
      }
      else
      {
        if (Settings.Default.useLongPathInTitlebar)
        {
          this.Text = UI.findLangResString("MyBucks") + " - " + currentDir;
        }
        else
        {
          this.Text = UI.findLangResString("MyBucks") + " - " + dirName;
        }
      }
    }

    #endregion displayCurrentDir

    #region GoToMyComputer and DisplayMyComputer (Computer is a special dir)

    private void goToMyComputer()
    {
      comboNavi.Text = UI.findLangResString("Computer");
      toolStripNav.Items.Clear();
      toolStripNav.Items.Add(new ToolStripButton(UI.findLangResString("Computer")));
      toolStripNav.Items[0].ForeColor = Color.Black;//set forecolor to avoid compatibility issue in winxp

      currentDir = "mbs-mpc";
      loadDrivesToTree();
      displayMyComputer();
    }

    private void displayMyComputer()
    {
      fileSystemWatcher1.Path = ".\\";

      explorerView.Items.Clear(); refreshButtonState();

      foreach (ColumnHeader header in explorerView.Columns)
        header.Tag = "mbs-list-sortnone";

      statusBar.Text = UI.showFilestatuslabel(explorerView);
      statusBar.Image = Resources.sort_none;//initially displayed in the order 'files->folders'

      foreach (string path in Environment.GetLogicalDrives())
      {
        explorerView.Items.Add(path, path, UI.getImgKey(explorerView.LargeImageList, path));
        UI.getImgKey(explorerView.SmallImageList, path);
      }

      //change titlebar
      string dirName = UI.findLangResString("Computer");
      this.Text = UI.findLangResString("MyBucks") + " - " + dirName;
    }

    #endregion GoToMyComputer and DisplayMyComputer (Computer is a special dir)

    #region getParentFolderAtSpecificLevel helps to position a folder's parent/grand...

    /// <summary>
    /// Allows you to get a folder's specific parent/grand... in top-to-bottom sequence
    /// </summary>
    /// <param name="levelIndexFromRoot"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private string getParentFolderAtSpecificLevel(int levelIndexFromRoot, string path)
    {
      string path_returned = "";
      String[] parsedDirs = path.Split("\\".ToCharArray());
      for (int i = 0; i <= levelIndexFromRoot; i++)
        path_returned += (parsedDirs[i] + "\\");

      //must remove \\ and return
      return path_returned.Substring(0, path_returned.Length - 1);
    }

    #endregion getParentFolderAtSpecificLevel helps to position a folder's parent/grand...

    private void buttonPack_Click(object sender, EventArgs e)
    {
      string[] packFilesIn = new string[explorerView.SelectedIndices.Count];
      for (int i = 0; i < explorerView.SelectedIndices.Count; i++)
        packFilesIn[i] = explorerView.SelectedItems[i].Name;

      DialogZip dlgzip = new DialogZip(packFilesIn, currentDir);
      dlgzip.ShowDialog();
    }

    private void FrmExplorer_Load(object sender, EventArgs e)
    {
      setFont();
    }

    internal void setFont()
    {
      try
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

        toolStripNav.Font =
          toolStripMain.Font =
          menuStrip.Font =
          treeExplorer.Font =
          explorerView.Font =
          listViewFav.Font =
          this.Font = currentFont;
      }
      catch { }
    }

    #region codes of FS_watcher to update entries everywhere after changing (except for Computer)

    private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
    { goToDir(currentDir); }

    private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
    { goToDir(currentDir); }

    private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
    { goToDir(currentDir); }

    private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
    { goToDir(currentDir); }

    #endregion codes of FS_watcher to update entries everywhere after changing (except for Computer)

    private void refreshButtonState()
    {
      if (explorerView.SelectedIndices.Count != 0)
      {
        viewOutToolStripMenuItem.Enabled = buttonViewOut.Enabled = false;

        buttonCopy.Enabled = buttonMove.Enabled =
          buttonDelete.Enabled = buttonPack.Enabled = true;

        copyToolStripMenuItem.Enabled = cutToolStripMenuItem.Enabled =
          deleteToolStripMenuItem.Enabled = true;

        if (explorerView.SelectedIndices.Count == 1)
        {
          buttonViewOut.Enabled = viewOutToolStripMenuItem.Enabled = renameToolStripMenuItem.Enabled = true;

          string filename = explorerView.SelectedItems[0].Name;

          if (previewPane1.Visible)
          {
            previewPane1.filename = explorerView.SelectedItems[0].Name;
            previewPane1.displayContent();
          }
        }
      }
      else
      {
        buttonPack.Enabled = buttonCopy.Enabled = buttonMove.Enabled =
          buttonDelete.Enabled = false;

        viewOutToolStripMenuItem.Enabled = buttonViewOut.Enabled = true;

        copyToolStripMenuItem.Enabled = cutToolStripMenuItem.Enabled =
          deleteToolStripMenuItem.Enabled = renameToolStripMenuItem.Enabled = false;
      }
    }

    private void explorerView_SelectedIndexChanged(object sender, EventArgs e)
    {
      refreshButtonState();
      statusBar.Text = UI.showFilestatuslabel(explorerView);
    }

    

    private void listViewFav_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (listViewFav.SelectedIndices.Count != 0)
      {
        //goto
        goToDir(listViewFav.SelectedItems[0].Name);

        //clear selection to allow selected items to be reselected
        //necessary since user may go anywhere after navigation
        listViewFav.SelectedItems[0].Selected = false;
      }
    }

    private TreeNodeCollection expandFSNode(TreeNode node)
    {
      try
      {
        node.Nodes.Clear();
        String[] subNodes = Directory.GetDirectories(node.Name);
        foreach (string nodename in subNodes)
          node.Nodes.Add(nodename, Path.GetFileName(nodename), 6);

        node.Expand();

        return node.Nodes;
      }
      catch { return null; }
    }

    #region intelligent selection

    private bool[] comboBox1_MouseDown_PlusDraggingIndicator = { false, false };

    private void comboBox1_MouseDown(object sender, MouseEventArgs e)
    {
      if (comboBox1_MouseDown_PlusDraggingIndicator[0] == false)
      {
        comboBox1_MouseDown_PlusDraggingIndicator[0] = true;
      }
    }

    private void comboBox1_MouseMove(object sender, MouseEventArgs e)
    {
      if (comboBox1_MouseDown_PlusDraggingIndicator[0] == true)
      {
        if (comboBox1_MouseDown_PlusDraggingIndicator[1] == false)
        {
          comboBox1_MouseDown_PlusDraggingIndicator[1] = true;
        }
      }
      else
      {
        comboBox1_MouseDown_PlusDraggingIndicator[1] = false;
      }
    }

    private void comboBox1_MouseUp(object sender, MouseEventArgs e)
    {
      if (comboBox1_MouseDown_PlusDraggingIndicator[1] == false)
      {
        applyIntelligentSelection();
      }
      comboBox1_MouseDown_PlusDraggingIndicator[0] = false;
      comboBox1_MouseDown_PlusDraggingIndicator[1] = true;
    }

    private void applyIntelligentSelection()
    {
      try
      {
        string text = comboNavi.Text;
        int leftContentBegin = text.Substring(0, comboNavi.SelectionStart).LastIndexOf("\\");
        int rightSlashBegin = comboNavi.SelectionStart + text.Substring(comboNavi.SelectionStart).IndexOf("\\");
        if (rightSlashBegin == comboNavi.SelectionStart - 1)
          rightSlashBegin = text.Length;
        //select at the end of path

        comboNavi.Select(leftContentBegin, rightSlashBegin - leftContentBegin);
      }
      catch { }
    }

    #endregion intelligent selection

    private void loadDrivesToTree()
    {
      // This routine adds all computer drives to the root nodes of treeExp control

      treeExplorer.BeginUpdate();
      treeExplorer.Nodes.Clear();
      treeExplorer.Nodes.Add("mbs-mpc", UI.findLangResString("Computer"), 10, 10);

      foreach (string drive in Environment.GetLogicalDrives())
      {
        int imgIndex;
        DriveInfo info = new DriveInfo(drive);
        switch (info.DriveType)
        {
          case DriveType.CDRom:
            imgIndex = 5; break;
          case DriveType.Network:
            imgIndex = 8; break;
          case DriveType.Removable:
            imgIndex = 9; break;
          case DriveType.Fixed:
            imgIndex = 7; break;
          default:
            imgIndex = 6; break;
        }

        try
        {
          if (info.VolumeLabel == "")
            treeExplorer.Nodes[0].Nodes.Add(drive, drive, imgIndex, imgIndex);
          else
            treeExplorer.Nodes[0].Nodes.Add(drive, drive + " [" + info.VolumeLabel + "]", imgIndex, imgIndex);
        }
        catch
        {
          treeExplorer.Nodes[0].Nodes.Add(drive, drive, imgIndex, imgIndex);
        }
      }
      treeExplorer.EndUpdate();
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
      if (explorerView.SelectedIndices.Count == 1)
      {
        if (MessageBox.Show("Are you sure you want to delete '" +
          explorerView.SelectedItems[0].Text + "'?", "Confirmation", MessageBoxButtons.YesNo) 
          != System.Windows.Forms.DialogResult.Yes)
        {
          return;
        }
      }
      else if  (explorerView.SelectedIndices.Count > 1)
      {
        if (MessageBox.Show("Are you sure you want to delete these files/folders?", 
          "Confirmation", MessageBoxButtons.YesNo)
          != System.Windows.Forms.DialogResult.Yes)
        {
          return;
        }
      }

      try
      {
        foreach (int index in explorerView.SelectedIndices)
        {
          string path = explorerView.Items[index].Name;
          if (Directory.Exists(path))
            Directory.Delete(path, true);
          else
          {
            File.Delete(explorerView.Items[index].Name);
          }
        }
      }
      catch (Exception ex) { MessageBox.Show(ex.Message); }
      goToDir(currentDir);
    }

    private void listViewExp_DoubleClick(object sender, EventArgs e)
    {
      if (explorerView.SelectedIndices.Count == 1)
      {
        if (Directory.Exists(explorerView.SelectedItems[0].Name))
        {
          goToDir(explorerView.SelectedItems[0].Name);
        }
        else if (Archive.isArchiveSupported(explorerView.SelectedItems[0].Name))
        {
          Shared.openArchive(explorerView.SelectedItems[0].Name);
        }
        else if (File.Exists(explorerView.SelectedItems[0].Name))
        {
          try
          {
            System.Diagnostics.Process.Start(explorerView.SelectedItems[0].Name);
          }
          catch { }
        }
      }
    }

    private void buttonUp_MouseDown(object sender, MouseEventArgs e)
    { buttonUp.BackColor = Color.SlateGray; }

    private void buttonUp_MouseUp(object sender, MouseEventArgs e)
    { buttonUp.BackColor = Color.LightSteelBlue; }

    private void label1_DoubleClick(object sender, EventArgs e)
    {
      if (currentDir == "mbs-mpc")
      {
        //already at \\computer
      }
      else if (Path.GetDirectoryName(currentDir) == null)
      {
        goToMyComputer();
      }
      else
      {
        goToDir(Path.GetDirectoryName(currentDir));
      }
    }

    private void buttonView_Click(object sender, EventArgs e)
    {
      switch (explorerView.View)
      {
        case View.LargeIcon:
          smallIconsToolStripMenuItem.PerformClick(); break;
        case View.SmallIcon:
          listToolStripMenuItem.PerformClick(); break;
        case View.List:
          detailsToolStripMenuItem.PerformClick(); break;
        case View.Details:
          tileToolStripMenuItem.PerformClick(); break;
        case View.Tile:
          largeIconsToolStripMenuItem.PerformClick(); break;
      }
    }

    private void label3_MouseDown(object sender, MouseEventArgs e)
    { buttonView.BackColor = Color.SlateGray; }

    private void label3_MouseUp(object sender, MouseEventArgs e)
    { buttonView.BackColor = Color.LightSteelBlue; }

    private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (explorerView.SelectedIndices.Count != explorerView.Items.Count)
      {
        foreach (ListViewItem item in explorerView.Items)
          item.Selected = true;
      }
    }

    private void listViewExp_KeyDown(object sender, KeyEventArgs e)
    {
      if (!e.Control && e.Shift && !e.Alt)
      {
        if (e.KeyCode == Keys.Delete)
          buttonDelete.PerformClick();
      }
      else if (!e.Control && !e.Shift && !e.Alt)
      {
        if (e.KeyCode == Keys.Enter)
          listViewExp_DoubleClick(this, new EventArgs());
        else if (e.KeyCode == Keys.Back)
          label1_DoubleClick(this, new EventArgs());
      }
    }

    private void largeIconsToolStripMenuItem_Click(object sender, EventArgs e)
    { explorerView.View = View.LargeIcon; }

    private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
    { explorerView.View = View.SmallIcon; }

    private void listToolStripMenuItem_Click(object sender, EventArgs e)
    { explorerView.View = View.List; }

    private void tileToolStripMenuItem_Click(object sender, EventArgs e)
    { displayCurrentDir(currentDir); explorerView.View = View.Tile; }
    //must refresh otherwise entry will not auto arrange

    private void comboBox1_DropDown(object sender, EventArgs e)
    {
      comboNavi.Items.Clear();
      comboNavi.Items.Add(currentDir);
      comboNavi.Items.AddRange(Directory.GetDirectories(currentDir));
    }

    private bool navigatingSignalRunning = false;

    private void comboNav_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        if (comboNavi.Text.ToLower() == UI.findLangResString("Computer").ToLower())
          goToMyComputer();
        else
          goToDir(comboNavi.Text);
      }
    }

    private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (ListViewItem item in explorerView.Items)
        item.Selected = !item.Selected;
    }

    private void autoRefreshToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
    {
      if (autoRefreshToolStripMenuItem.Checked)
      {
        goToDir(currentDir);
        fileSystemWatcher1.EnableRaisingEvents = true;
      }
      else
        fileSystemWatcher1.EnableRaisingEvents = false;
    }

    private void launchNewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Program.LaunchNew(currentDir);
    }

    private void buttonViewOut_Click(object sender, EventArgs e)
    {
      viewOutToolStripMenuItem.PerformClick();
    }

    private void label3_DoubleClick(object sender, EventArgs e)
    {
      treeExplorer.Visible = !treeExplorer.Visible;
      treeExplorer.BackColor = listViewFav.BackColor;
      if (treeExplorer.Visible)
      {
        navCaption.ForeColor = Color.SteelBlue;
      }
      else
        navCaption.ForeColor = Color.DimGray;
    }

    private void buttonCopy_Click(object sender, EventArgs e)
    {
      string copyDest;
      if (folderBrowserDialogExtract.ShowDialog() == DialogResult.OK)
        copyDest = folderBrowserDialogExtract.SelectedPath;
      else
        return;

      foreach (int index in explorerView.SelectedIndices)
      {
        FileIO.AsyncFileCopier.AsynFileCopy(explorerView.Items[index].Name, copyDest, false);

        //if (File.Exists(listViewExp.Items[index].Name))
        //{
        //  try
        //  {
        //    File.Copy(listViewExp.Items[index].Name, Path.Combine(copyDest, listViewExp.Items[index].Text));
        //  }
        //  catch { }
        //}
        //else if (Directory.Exists(listViewExp.Items[index].Name))
        //{
        //  IO.CopyFolderTo(listViewExp.Items[index].Name, Path.Combine(copyDest, listViewExp.Items[index].Text));
        //}
      }
    }

    private void buttonMove_Click(object sender, EventArgs e)
    {
      string moveDest;
      if (folderBrowserDialogExtract.ShowDialog() == DialogResult.OK)
        moveDest = folderBrowserDialogExtract.SelectedPath;
      else
        return;

      foreach (int index in explorerView.SelectedIndices)
      {
        FileIO.AsyncFileCopier.AsynFileCopy(explorerView.Items[index].Name, moveDest, true);

        //if (File.Exists(listViewExp.Items[index].Name))
        //{
        //  try
        //  {
        //    File.Move(listViewExp.Items[index].Name, Path.Combine(moveDest, listViewExp.Items[index].Text));
        //  }
        //  catch { }
        //}
        //else if (Directory.Exists(listViewExp.Items[index].Name))
        //{
        //  IO.CopyFolderTo(listViewExp.Items[index].Name, Path.Combine(moveDest, listViewExp.Items[index].Text));

        //  //move = copy & delete old
        //  Directory.Delete(listViewExp.Items[index].Name, true);
        //}
      }
    }

    /// <summary>
    /// Write files to clipboard (from
    /// http://blogs.wdevs.com/idecember/
    ///      archive/2005/10/27/10979.aspx)
    /// </summary>
    /// <param name="cut">True if cut, false if copy</param>
    private void CopyToClipboard(bool cut)
    {
      List<string> files = new List<string>();
      foreach (ListViewItem item in explorerView.SelectedItems)
        files.Add(item.Name);

      if (files.Count > 0)
      {
        IDataObject data = new DataObject(DataFormats.FileDrop, files.ToArray());
        MemoryStream memo = new MemoryStream(4);
        byte[] bytes = new byte[] { (byte)(cut ? 2 : 5), 0, 0, 0 };
        memo.Write(bytes, 0, bytes.Length);
        data.SetData("Preferred DropEffect", memo);
        Clipboard.SetDataObject(data);
      }
    }

    #region Paste Files from System Clipboard

    /// <summary>
    /// Paste context menu option
    /// </summary>
    /// <param name="cut"></param>
    private void PasteFromClipboard()
    {
      //cannot expect any navigation to occur during pasting; use a temp var to freeze currentDir
      string currentDirFrozen = currentDir;
      IDataObject data = Clipboard.GetDataObject();
      if (!data.GetDataPresent(DataFormats.FileDrop))
      {
        MessageBox.Show(UI.findLangResString("Cannot paste.")); return;
      }

      string[] files = (string[])
        data.GetData(DataFormats.FileDrop);
      MemoryStream stream = (MemoryStream)
        data.GetData("Preferred DropEffect", true);
      int flag = stream.ReadByte();
      if (flag != 2 && flag != 5)
        return;
      bool cut = (flag == 2);
      foreach (string file in files)
      {
        string dest = currentDirFrozen + "\\" + Path.GetFileName(file);
        try
        {
          if (File.Exists(file))
          {
            if (cut)
              File.Move(file, dest);
            else
              File.Copy(file, dest, false);
          }
          else if (Directory.Exists(file))
          {
            IO.CopyFolderTo(file, currentDirFrozen);
            if (cut)
              Directory.Delete(file, true);
          }
        }
        catch (IOException ex)
        {
          MessageBox.Show(UI.findLangResString("Operation failed") + ":\r\n" + ex.Message);
        }

        goToDir(currentDirFrozen);
      }
    }

    #endregion Paste Files from System Clipboard

    private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (explorerView.SelectedIndices.Count > 0)
      {
        foreach (ListViewItem item in explorerView.Items)
          item.Selected = false;
      }
    }

    private void switchThemeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Settings.Default.useLightTheme = !Settings.Default.useLightTheme;
      setTheme(Settings.Default.useLightTheme);
    }

    #region Theme Setter

    /// <summary>
    /// set form theme between dark and light
    /// </summary>
    /// <param name="use light"></param>
    private void setTheme(bool p)
    {
      if (p)
      {
        explorerView.BackColor = Settings.Default.listViewBackColor_light;

        contextMenuExp.BackColor = splitContainer.BackColor =
        treeExplorer.BackColor = listViewFav.BackColor =
        this.BackColor = Settings.Default.sidebarBackColor_light;

        panelTop.BackgroundImage = Resources.plbg;
        splitContainer.Panel2.BackgroundImage = Resources.plbg;

        statusBar.BackColor = Settings.Default.statusBackColor_light;

        foreach (ToolStripItem item in contextMenuExp.Items)
          item.ForeColor = Settings.Default.foreColor_light;

        foreach (ToolStripItem item in toolStripMain.Items)
        {
          if (item is ToolStripButton)
          {
            item.ForeColor = Settings.Default.foreColor_light;
          }
        }

        foreach (ToolStripItem item in menuStrip.Items)
          item.ForeColor = Settings.Default.foreColor_light;

        statusBar.ForeColor = panelTop.ForeColor =
        treeExplorer.ForeColor = listViewFav.ForeColor =
        explorerView.ForeColor = this.ForeColor = Settings.Default.foreColor_light;
      }
      else
      {
        explorerView.BackColor = Settings.Default.listViewBackColor_dark;

        contextMenuExp.BackColor = splitContainer.BackColor =
        treeExplorer.BackColor = listViewFav.BackColor =
        this.BackColor = Settings.Default.sidebarBackColor_dark;

        panelTop.BackgroundImage = Resources.plbg_dark;
        splitContainer.Panel2.BackgroundImage = Resources.plbg_dark;

        statusBar.BackColor = Settings.Default.statusBackColor_dark;

        foreach (ToolStripItem item in contextMenuExp.Items)
          item.ForeColor = Settings.Default.foreColor_dark;

        foreach (ToolStripItem item in toolStripMain.Items)
        {
          if (item is ToolStripButton)
          {
            item.ForeColor = Settings.Default.foreColor_dark;
          }
        }

        foreach (ToolStripItem item in menuStrip.Items)
          item.ForeColor = Settings.Default.foreColor_dark;

        statusBar.ForeColor = panelTop.ForeColor =
        treeExplorer.ForeColor = listViewFav.ForeColor =
        explorerView.ForeColor = this.ForeColor = Settings.Default.foreColor_dark;
      }
    }

    #endregion Theme Setter

    #region Form Loader

    private void addDefaultIcons()
    {
      //add icons to favorites iconset
      sidebarImgList.Images.Add("desktop", Resources.fav_desktop);//0
      sidebarImgList.Images.Add("documents", Resources.fav_documents);//1
      sidebarImgList.Images.Add("music", Resources.fav_music);//2
      sidebarImgList.Images.Add("pictures", Resources.fav_pictures);//3
      sidebarImgList.Images.Add("user", Resources.fav_user);//4

      //continue adding tree icons
      //add icons to favorites iconset
      sidebarImgList.Images.Add("dsc", Resources.drv_dsc);//5
      sidebarImgList.Images.Add("fdr", Resources.drv_fdr);//6
      sidebarImgList.Images.Add("hdd", Resources.drv_hdd);//7
      sidebarImgList.Images.Add("ntw", Resources.drv_ntw);//8
      sidebarImgList.Images.Add("usb", Resources.drv_usb);//9

      sidebarImgList.Images.Add("mpc", Resources.drv_mpc);//10

      String fullpathDrvBig = Path.Combine(Application.StartupPath, "drvicn\\");

      fileTypeImgList.Images.Add("mbs-dsc", new Bitmap(fullpathDrvBig + "mbs-dsc.pnx"));
      fileTypeImgList.Images.Add("mbs-hdd", new Bitmap(fullpathDrvBig + "mbs-hdd.pnx"));
      fileTypeImgList.Images.Add("mbs-ntw", new Bitmap(fullpathDrvBig + "mbs-ntw.pnx"));
      fileTypeImgList.Images.Add("mbs-usb", new Bitmap(fullpathDrvBig + "mbs-usb.pnx"));

      fileTypeSImgList.Images.Add("mbs-dsc", fileTypeImgList.Images[fileTypeImgList.Images.IndexOfKey("mbs-dsc")]);
      fileTypeSImgList.Images.Add("mbs-hdd", fileTypeImgList.Images[fileTypeImgList.Images.IndexOfKey("mbs-hdd")]);
      fileTypeSImgList.Images.Add("mbs-ntw", fileTypeImgList.Images[fileTypeImgList.Images.IndexOfKey("mbs-ntw")]);
      fileTypeSImgList.Images.Add("mbs-usb", fileTypeImgList.Images[fileTypeImgList.Images.IndexOfKey("mbs-usb")]);
    }

    private void frmExplorer_Activated(object sender, EventArgs e)
    {  
      if (loadDefaultDir)
      {
        loadDefaultDir = false;

        foreach (ColumnHeader header in explorerView.Columns)
          header.Tag = "mbs-list-sortnone";

        fileSystemWatcher1.EnableRaisingEvents = false;

        addDefaultIcons();

        //add items to favorites
        listViewFav.Items.Add(
        Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)),
        UI.findLangResString("Personal"), 4);

        listViewFav.Items.Add(
        Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), UI.findLangResString("Desktop"), 0);

        listViewFav.Items.Add(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), UI.findLangResString("Documents"), 1);

        listViewFav.Items.Add(
        Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), UI.findLangResString("Music"), 2);

        listViewFav.Items.Add(
        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), UI.findLangResString("Pictures"), 3);

        loadDrivesToTree();
        historyBack.Add(_startupPath);
        goToDir(_startupPath);

        this.Text = "       ";
      }
    }

    #endregion Form Loader

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
      e.Node.Nodes.Clear();
      expandFSNode(e.Node);
      goToDir(treeExplorer.SelectedNode.Name);
    }

    #region button Back(Previous) and Forward code and animation

    private void navBack_Click(object sender, EventArgs e)
    {
      if (historyBack.Count > 1)
      {
        //reaching 1, historyBack[historyBack.Count - 1] = default path, however [historyBack.Count - 2] throws
        //reaching 2, historyBack[historyBack.Count - 1] = default path, too.
        String dirToGo = historyBack[historyBack.Count - 2];
        historyForward.Insert(0, historyBack[historyBack.Count - 1]);
        historyBack.RemoveAt(historyBack.Count - 1);
        navBack.Enabled = false;
        goToDir(dirToGo);
        navBack.Enabled = true;
      }
    }

    private void navForward_Click(object sender, EventArgs e)
    {
      if (historyForward.Count > 0)
      {
        String dirToGo = historyForward[0];
        historyBack.Add(historyForward[0]);
        historyForward.RemoveAt(0);
        navForward.Enabled = false;
        goToDir(dirToGo);
        navForward.Enabled = true;
      }
    }

    private void navBack_MouseFx(object sender, MouseEventArgs e)
    {
      navBack.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
      navBack.Refresh();
    }

    private void navForward_MouseFx(object sender, MouseEventArgs e)
    {
      navForward.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
      navForward.Refresh();
    }

    #endregion button Back(Previous) and Forward code and animation

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DialogSettings setting = new DialogSettings();
      setting.ShowDialog();
    }

    #region set display language

    private void setTranslation()
    {
      for (int i = 0; i < 6; i++)
      {
        //i may not exist
        try
        {
          setTranslation(i, Properties.Settings.Default.languageIndexSettings);//unify
        }
        catch { }
      }
    }

    private void setTranslation(int originalIndex, int newIndex)
    {
      {
        UI.switchLangInObjs(this, "Control", originalIndex, newIndex);
        UI.switchLangInObjs(listViewFav, "List", originalIndex, newIndex);
        UI.switchLangInObjs(menuStrip.Items, "Menu", originalIndex, newIndex);
        UI.switchLangInObjs(treeExplorer, "Tree", originalIndex, newIndex);

        UI.switchLangInObjs(contextMenuExp, "Cntx", originalIndex, newIndex);
        UI.switchLangInObjs(toolStripMain, "Tool", originalIndex, newIndex);
        UI.switchLangInObjs(dropdownButtonFilter.DropDownItems, "Menu", originalIndex, newIndex);

        toolTip.SetToolTip(toolStripNav, UI.findLangResString("dbl_click_enable_edit"));
      }
    }

    #endregion set display language

    private void frmExplorer_TextChanged(object sender, EventArgs e)
    {
      //invoked every time new settings are applied
      if (Text == "       ")
      {
        setTheme(Settings.Default.useLightTheme);
        setTranslation();
        setFont();
        if (Settings.Default.iconIndexSettings == 0)
        {
          UI.applyIconset("[default]", toolStripMain);
        }
        else
        {
          UI.applyIconset(UI.getIconsetNames().GetValue(Settings.Default.iconIndexSettings - 1).ToString(), toolStripMain);
        }
        goToDir(currentDir);//refresh to update caption bar
      }
    }

    private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
      goToDir(currentDir);
    }

    private void explorerView_AfterLabelEdit(object sender, LabelEditEventArgs e)
    {
      if (e.Label == null)
      {
        return;
      }

      string newName = e.Label;

      foreach (char c in Path.GetInvalidFileNameChars())
      {
        if (newName.Contains(Convert.ToString(c)))
        {
          e.CancelEdit = true; MessageBox.Show(UI.findLangResString("Illegal filename!")); return;
        }
      }
      try
      {
        string oldPath = explorerView.Items[e.Item].Name;
        if (Directory.Exists(oldPath))
        {
          Directory.Move(oldPath, Path.Combine(Path.GetDirectoryName(oldPath), newName));
        }
        else if (File.Exists(oldPath))
        {
          string extension = Path.GetExtension(oldPath);
          if (Settings.Default.useFileExtInExp)
            File.Move(oldPath, Path.Combine(Path.GetDirectoryName(oldPath), newName));
          else
            File.Move(oldPath, Path.Combine(Path.GetDirectoryName(oldPath), newName + extension));
        }
      }
      catch { MessageBox.Show("Failed to rename."); }
      refreshToolStripMenuItem.PerformClick();
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CopyToClipboard(false);
    }

    private void cutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CopyToClipboard(true);
      autoRefreshToolStripMenuItem.Checked = true;
    }

    private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PasteFromClipboard();
    }

    private void renameToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (explorerView.SelectedIndices.Count == 1)
      {
        explorerView.SelectedItems[0].BeginEdit();
      }
    }

    private void contextMenuExp_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      pasteToolStripMenuItem.Enabled = Clipboard.ContainsFileDropList();
    }

    private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      buttonDelete.PerformClick();
    }

    private void toolStripNav_DoNavigate(string path)
    {
      toolStripNav.SuspendLayout();
      path = Utility.correctPath(path);
      toolStripNav.Items.Clear();
      while (Path.GetDirectoryName(path) != null || Path.GetFileName(path) != "")
      {
        toolStripNav.Items.Insert(0, new ToolStripButton(Path.GetFileName(path)));
        toolStripNav.Items.Insert(0, new ToolStripLabel(">"));
        path = Path.GetDirectoryName(path);
      }
      toolStripNav.Items.Insert(0, new ToolStripButton(path.Substring(0, 2)));
      foreach (ToolStripItem item in toolStripNav.Items)
        item.ForeColor = Color.Black;//set forecolor to avoid compatibility issue in winxp

      toolStripNav.Items[toolStripNav.Items.Count - 1].ForeColor = Color.DeepSkyBlue;
      toolStripNav.ResumeLayout();
    }

    private void toolStripNav_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      comboNavi.Visible = false;
      if (e.ClickedItem is ToolStripButton)
      {
        e.ClickedItem.ForeColor = Color.DeepSkyBlue;
        clickedItem_fx = e.ClickedItem;
        timerClickFxNavi.Enabled = true;
      }
    }

    private ToolStripItem clickedItem_fx;

    private void timerClickFxNavi_Tick(object sender, EventArgs e)
    {
      //wait for the fx to complete before do actual navigation
      clickedItem_fx.ForeColor = Color.Black;

      //navigate
      string path = "";

      foreach (ToolStripItem item in toolStripNav.Items)
      {
        if (item is ToolStripButton)
        {
          path += (item.Text + "\\");
          if (item.Text == clickedItem_fx.Text)
            break;
        }
      }
      if (path.Substring(0, path.Length - 1).ToLower() == UI.findLangResString("Computer").ToLower())
        goToMyComputer();
      else
        goToDir(path);
      timerClickFxNavi.Enabled = false;
    }

    private void comboNavi_Leave(object sender, EventArgs e)
    {
      comboNavi.Visible = false;
    }

    private void toolStripNav_MouseClick(object sender, MouseEventArgs e)
    {
      comboNavi.Visible = true; comboNavi.Focus();
    }

    private void buttonCollapseView_Click(object sender, EventArgs e)
    {
      splitContainer.Panel1Collapsed = !splitContainer.Panel1Collapsed;
    }

    private void findBox_TextChanged(object sender, EventArgs e)
    {
      foreach (ListViewItem item in explorerView.Items)
      {
        if (findBox.Text != "" && item.Text.ToLower().Contains(findBox.Text.ToLower()))
          item.Selected = true;
        else
          item.Selected = false;
      }
      if (explorerView.SelectedIndices.Count == 0 && findBox.Text != "")
        findBox.BackColor = Color.LightCoral;
      else
        findBox.BackColor = Color.WhiteSmoke;
    }

    private void findBox_MouseLeave(object sender, EventArgs e)
    {
      explorerView.Focus();
    }

    private void buttonPreview_Click(object sender, EventArgs e)
    {
      previewPane1.BringToFront();
      previewPane1.Visible = !previewPane1.Visible;
      if (previewPane1.Visible && explorerView.SelectedIndices.Count == 1)
      {
        previewPane1.filename = explorerView.SelectedItems[0].Name;
        previewPane1.displayContent();
      }
      else
      {
        previewPane1.filename = "NULL";
        previewPane1.displayContent();
      }
    }

    private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      explorerView.View = View.Details;
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DialogAbout about = new DialogAbout();
      about.ShowDialog();
    }

    private void explorerView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      //clicked on the same column (used for sorting)
      if (((string)explorerView.Columns[e.Column].Tag) == "mbs-list-sortascend")
      {
        explorerView.Columns[e.Column].Tag = "mbs-list-sortdescend";
        statusBar.Image = Resources.sort_descending;//set icon indicator on status bar
        swapColumnListView(explorerView, 0, e.Column);//swap clicked column to the first place
        explorerView.Sorting = SortOrder.Descending;//set sort mode
        explorerView.Sort();//sort with reference to the first column
        explorerView.Sorting = SortOrder.None;//reset sort mode
        swapColumnListView(explorerView, 0, e.Column);//swap back the column
      }
      //clicked on the same column (used for sorting)
      else if (((string)explorerView.Columns[e.Column].Tag) == "mbs-list-sortdescend")
      {
        explorerView.Columns[e.Column].Tag = "mbs-list-sortascend";
        statusBar.Image = Resources.sort_ascending;
        swapColumnListView(explorerView, 0, e.Column);
        explorerView.Sorting = SortOrder.Ascending;
        explorerView.Sort();
        explorerView.Sorting = SortOrder.None;
        swapColumnListView(explorerView, 0, e.Column);
      }
      else
      {
        //clicked on a different column
        foreach (ColumnHeader header in explorerView.Columns)
          header.Tag = "mbs-list-sortnone";

        explorerView.Columns[e.Column].Tag = "mbs-list-sortascend";
        statusBar.Image = Resources.sort_ascending;
        //sort column content
        swapColumnListView(explorerView, 0, e.Column);
        explorerView.Sorting = SortOrder.Ascending;
        explorerView.Sort();
        explorerView.Sorting = SortOrder.None;
        swapColumnListView(explorerView, 0, e.Column);
      }
    }

    private void swapColumnListView(ListView listview, int columnA, int columnB)
    {
      //sort column content
      foreach (ListViewItem item in explorerView.Items)
      {
        if (item.SubItems.Count - 1 >= columnA && item.SubItems.Count - 1 >= columnB)
        {
          string subitemA = item.SubItems[columnA].Text;
          item.SubItems[columnA].Text = item.SubItems[columnB].Text;
          item.SubItems[columnB].Text = subitemA;
        }
      }
    }

    private void selectFilesofOneType(string extension)
    {
      extension = extension.ToLower();
      //make sure the explorerview has cleared its selection before this.
      if (!extension.StartsWith("."))
        extension = "." + extension;

      foreach (ListViewItem item in explorerView.Items)
      {
        if (Path.GetExtension(item.Name).ToLower() == extension)
          item.Selected = true;
        //else
        //  item.Selected = false;
        //will not clear any selected items here
      }
    }

    //apply filter settings
    private void applyFilter()
    {
      //will also clear selection
      goToDir(currentDir);

      bool nofilter_flag = true;//true = no filter
      foreach (ToolStripItem filter in dropdownButtonFilter.DropDownItems)
      {
        if (((ToolStripMenuItem)filter).Checked)
          nofilter_flag = false;
      }
      if (nofilter_flag)//if no filter, no need to remove any item
        return;

      #region select files of checked filters
      foreach (ToolStripItem filter in dropdownButtonFilter.DropDownItems)
      {
        if (((ToolStripMenuItem)filter).Checked == true)
        {
          switch (filter.Name)
          {
            case "archiveToolStripMenuItem":
              foreach (string ext in Settings.Default.predefinedArchiveFormats)
                selectFilesofOneType(ext);
              break;
            case "videoToolStripMenuItem":
              foreach (string ext in Settings.Default.predefinedVideoFormats)
                selectFilesofOneType(ext);
              break;
            case "audioToolStripMenuItem":
              foreach (string ext in Settings.Default.predefinedAudioFormats)
                selectFilesofOneType(ext);
              break;
            case "imageToolStripMenuItem":
              foreach (string ext in Settings.Default.predefinedImageFormats)
                selectFilesofOneType(ext);
              break;
            case "documentToolStripMenuItem":
              foreach (string ext in Settings.Default.predefinedDocumentFormats)
                selectFilesofOneType(ext);
              break;
            case "executableToolStripMenuItem":
              selectFilesofOneType("exe");
              break;
          }

        }
      }
      #endregion
      invertSelectionToolStripMenuItem.PerformClick();
      //remove other files
      while (explorerView.SelectedIndices.Count > 0)
      {
        explorerView.Items.RemoveAt(explorerView.SelectedIndices[0]);
      }
      explorerView.Alignment = ListViewAlignment.Top;
      explorerView.Alignment = ListViewAlignment.SnapToGrid;

    }

    private void filterToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
    {
      applyFilter();
    }

    private void viewOutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        string arguments = "/root," + currentDir;
        if (explorerView.SelectedIndices.Count != 0)
          arguments = "/select," + explorerView.SelectedItems[0].Name;

        System.Diagnostics.Process processProp = new System.Diagnostics.Process();
        processProp.StartInfo.FileName = "explorer";
        processProp.StartInfo.Arguments = arguments;
        processProp.Start();
      }
      catch { }
    }

    private void openArchiveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (openArchiveDialog.ShowDialog() == DialogResult.OK)
      {
        Shared.openArchive(openArchiveDialog.FileName);
      }
    }

    private void recentArchivesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      recentArchivesToolStripMenuItem.DropDownItems.Clear();
      if (Settings.Default.recentArchiveList.Count == 0 || Settings.Default.recentArchiveList[0] == "")
      {
        recentArchivesToolStripMenuItem.DropDownItems.Add(UI.findLangResString("Empty"));
        recentArchivesToolStripMenuItem.DropDownItems[0].Enabled = false;
      }
      else
      {
        foreach (string recentArchive in Settings.Default.recentArchiveList)
          recentArchivesToolStripMenuItem.DropDownItems.Add(recentArchive);
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void recentArchivesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      Shared.openArchive(e.ClickedItem.Text);
    }

  }
}