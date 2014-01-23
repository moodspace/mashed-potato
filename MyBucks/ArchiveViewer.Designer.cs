namespace MyBucks
{
  partial class ArchiveViewer
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveViewer));
      this.listViewExp = new System.Windows.Forms.ListView();
      this.contextMenuExp = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.invertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.viewModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.largeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.smallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fileTypeImgList = new System.Windows.Forms.ImageList(this.components);
      this.fileTypeSImgList = new System.Windows.Forms.ImageList(this.components);
      this.folderBrowserDialogExtract = new System.Windows.Forms.FolderBrowserDialog();
      this.timerListFilenames = new System.Windows.Forms.Timer(this.components);
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolStripNav = new System.Windows.Forms.ToolStrip();
      this.timerClickFxNavi = new System.Windows.Forms.Timer(this.components);
      this.buttonUp = new System.Windows.Forms.Label();
      this.panelTop = new System.Windows.Forms.Panel();
      this.toolStripMain = new System.Windows.Forms.ToolStrip();
      this.buttonExtract = new System.Windows.Forms.ToolStripSplitButton();
      this.buttonExtractCurr = new System.Windows.Forms.ToolStripMenuItem();
      this.buttonExtractTmp = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.buttonView = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.findIcon = new System.Windows.Forms.ToolStripLabel();
      this.findBox = new System.Windows.Forms.ToolStripTextBox();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openArchiveDialog = new System.Windows.Forms.OpenFileDialog();
      this.contextMenuExp.SuspendLayout();
      this.panelTop.SuspendLayout();
      this.toolStripMain.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // listViewExp
      // 
      this.listViewExp.Activation = System.Windows.Forms.ItemActivation.OneClick;
      this.listViewExp.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
      this.listViewExp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.listViewExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
      this.listViewExp.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.listViewExp.ContextMenuStrip = this.contextMenuExp;
      this.listViewExp.ForeColor = System.Drawing.Color.Black;
      this.listViewExp.LargeImageList = this.fileTypeImgList;
      this.listViewExp.Location = new System.Drawing.Point(0, 99);
      this.listViewExp.Name = "listViewExp";
      this.listViewExp.Size = new System.Drawing.Size(760, 397);
      this.listViewExp.SmallImageList = this.fileTypeSImgList;
      this.listViewExp.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.listViewExp.TabIndex = 0;
      this.listViewExp.UseCompatibleStateImageBehavior = false;
      this.listViewExp.DoubleClick += new System.EventHandler(this.listViewPackExp_DoubleClick);
      this.listViewExp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewPackExp_KeyDown);
      // 
      // contextMenuExp
      // 
      this.contextMenuExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
      this.contextMenuExp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.invertSelectionToolStripMenuItem,
            this.toolStripSeparator2,
            this.viewModeToolStripMenuItem,
            this.largeIconsToolStripMenuItem,
            this.smallIconsToolStripMenuItem,
            this.listToolStripMenuItem,
            this.tileToolStripMenuItem});
      this.contextMenuExp.Name = "contextMenuExp";
      this.contextMenuExp.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.contextMenuExp.Size = new System.Drawing.Size(207, 164);
      // 
      // selectAllToolStripMenuItem
      // 
      this.selectAllToolStripMenuItem.Image = global::MyBucks.Properties.Resources.selAll;
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
      this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
      this.selectAllToolStripMenuItem.Text = "Select All";
      this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
      // 
      // invertSelectionToolStripMenuItem
      // 
      this.invertSelectionToolStripMenuItem.Image = global::MyBucks.Properties.Resources.selI;
      this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
      this.invertSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
      this.invertSelectionToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
      this.invertSelectionToolStripMenuItem.Text = "Invert Selection";
      this.invertSelectionToolStripMenuItem.Click += new System.EventHandler(this.invertSelectionToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
      // 
      // viewModeToolStripMenuItem
      // 
      this.viewModeToolStripMenuItem.Enabled = false;
      this.viewModeToolStripMenuItem.Name = "viewModeToolStripMenuItem";
      this.viewModeToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
      this.viewModeToolStripMenuItem.Text = "View Mode";
      // 
      // largeIconsToolStripMenuItem
      // 
      this.largeIconsToolStripMenuItem.Name = "largeIconsToolStripMenuItem";
      this.largeIconsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
      this.largeIconsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
      this.largeIconsToolStripMenuItem.Text = "Large Icons";
      this.largeIconsToolStripMenuItem.Click += new System.EventHandler(this.largeIconsToolStripMenuItem_Click);
      // 
      // smallIconsToolStripMenuItem
      // 
      this.smallIconsToolStripMenuItem.Name = "smallIconsToolStripMenuItem";
      this.smallIconsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
      this.smallIconsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
      this.smallIconsToolStripMenuItem.Text = "Small Icons";
      this.smallIconsToolStripMenuItem.Click += new System.EventHandler(this.smallIconsToolStripMenuItem_Click);
      // 
      // listToolStripMenuItem
      // 
      this.listToolStripMenuItem.Name = "listToolStripMenuItem";
      this.listToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
      this.listToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
      this.listToolStripMenuItem.Text = "List";
      this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
      // 
      // tileToolStripMenuItem
      // 
      this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
      this.tileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
      this.tileToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
      this.tileToolStripMenuItem.Text = "Tile";
      this.tileToolStripMenuItem.Click += new System.EventHandler(this.tileToolStripMenuItem_Click);
      // 
      // fileTypeImgList
      // 
      this.fileTypeImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("fileTypeImgList.ImageStream")));
      this.fileTypeImgList.TransparentColor = System.Drawing.Color.Empty;
      this.fileTypeImgList.Images.SetKeyName(0, "7z.png");
      this.fileTypeImgList.Images.SetKeyName(1, "bat.png");
      this.fileTypeImgList.Images.SetKeyName(2, "bmp.png");
      this.fileTypeImgList.Images.SetKeyName(3, "bz2.png");
      this.fileTypeImgList.Images.SetKeyName(4, "c.png");
      this.fileTypeImgList.Images.SetKeyName(5, "c++.png");
      this.fileTypeImgList.Images.SetKeyName(6, "cab.png");
      this.fileTypeImgList.Images.SetKeyName(7, "class.png");
      this.fileTypeImgList.Images.SetKeyName(8, "cmd.png");
      this.fileTypeImgList.Images.SetKeyName(9, "com.png");
      this.fileTypeImgList.Images.SetKeyName(10, "cpp.png");
      this.fileTypeImgList.Images.SetKeyName(11, "cs.png");
      this.fileTypeImgList.Images.SetKeyName(12, "css.png");
      this.fileTypeImgList.Images.SetKeyName(13, "dll.png");
      this.fileTypeImgList.Images.SetKeyName(14, "doc.png");
      this.fileTypeImgList.Images.SetKeyName(15, "docx.png");
      this.fileTypeImgList.Images.SetKeyName(16, "exe.png");
      this.fileTypeImgList.Images.SetKeyName(17, "file.png");
      this.fileTypeImgList.Images.SetKeyName(18, "flv.png");
      this.fileTypeImgList.Images.SetKeyName(19, "folder.png");
      this.fileTypeImgList.Images.SetKeyName(20, "folder2.png");
      this.fileTypeImgList.Images.SetKeyName(21, "gif.png");
      this.fileTypeImgList.Images.SetKeyName(22, "gz.png");
      this.fileTypeImgList.Images.SetKeyName(23, "h.png");
      this.fileTypeImgList.Images.SetKeyName(24, "htm.png");
      this.fileTypeImgList.Images.SetKeyName(25, "html.png");
      this.fileTypeImgList.Images.SetKeyName(26, "inf.png");
      this.fileTypeImgList.Images.SetKeyName(27, "ini.png");
      this.fileTypeImgList.Images.SetKeyName(28, "iso.png");
      this.fileTypeImgList.Images.SetKeyName(29, "jar.png");
      this.fileTypeImgList.Images.SetKeyName(30, "java.png");
      this.fileTypeImgList.Images.SetKeyName(31, "jpeg.png");
      this.fileTypeImgList.Images.SetKeyName(32, "jpg.png");
      this.fileTypeImgList.Images.SetKeyName(33, "lnk.png");
      this.fileTypeImgList.Images.SetKeyName(34, "m3u.png");
      this.fileTypeImgList.Images.SetKeyName(35, "mba.png");
      this.fileTypeImgList.Images.SetKeyName(36, "mdb.png");
      this.fileTypeImgList.Images.SetKeyName(37, "mid.png");
      this.fileTypeImgList.Images.SetKeyName(38, "mkv.png");
      this.fileTypeImgList.Images.SetKeyName(39, "mmm.png");
      this.fileTypeImgList.Images.SetKeyName(40, "mov.png");
      this.fileTypeImgList.Images.SetKeyName(41, "mp3.png");
      this.fileTypeImgList.Images.SetKeyName(42, "mp4.png");
      this.fileTypeImgList.Images.SetKeyName(43, "mpeg.png");
      this.fileTypeImgList.Images.SetKeyName(44, "mpg.png");
      this.fileTypeImgList.Images.SetKeyName(45, "msi.png");
      this.fileTypeImgList.Images.SetKeyName(46, "pdf.png");
      this.fileTypeImgList.Images.SetKeyName(47, "png.png");
      this.fileTypeImgList.Images.SetKeyName(48, "ppt.png");
      this.fileTypeImgList.Images.SetKeyName(49, "pptx.png");
      this.fileTypeImgList.Images.SetKeyName(50, "psd.png");
      this.fileTypeImgList.Images.SetKeyName(51, "py.png");
      this.fileTypeImgList.Images.SetKeyName(52, "rar.png");
      this.fileTypeImgList.Images.SetKeyName(53, "rtf.png");
      this.fileTypeImgList.Images.SetKeyName(54, "swf.png");
      this.fileTypeImgList.Images.SetKeyName(55, "tar.png");
      this.fileTypeImgList.Images.SetKeyName(56, "tif.png");
      this.fileTypeImgList.Images.SetKeyName(57, "tiff.png");
      this.fileTypeImgList.Images.SetKeyName(58, "txt.png");
      this.fileTypeImgList.Images.SetKeyName(59, "wav.png");
      this.fileTypeImgList.Images.SetKeyName(60, "wma.png");
      this.fileTypeImgList.Images.SetKeyName(61, "wmv.png");
      this.fileTypeImgList.Images.SetKeyName(62, "xls.png");
      this.fileTypeImgList.Images.SetKeyName(63, "xlsx.png");
      this.fileTypeImgList.Images.SetKeyName(64, "xml.png");
      this.fileTypeImgList.Images.SetKeyName(65, "zip.png");
      // 
      // fileTypeSImgList
      // 
      this.fileTypeSImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("fileTypeSImgList.ImageStream")));
      this.fileTypeSImgList.TransparentColor = System.Drawing.Color.Empty;
      this.fileTypeSImgList.Images.SetKeyName(0, "7z.png");
      this.fileTypeSImgList.Images.SetKeyName(1, "bat.png");
      this.fileTypeSImgList.Images.SetKeyName(2, "bmp.png");
      this.fileTypeSImgList.Images.SetKeyName(3, "bz2.png");
      this.fileTypeSImgList.Images.SetKeyName(4, "c.png");
      this.fileTypeSImgList.Images.SetKeyName(5, "c++.png");
      this.fileTypeSImgList.Images.SetKeyName(6, "cab.png");
      this.fileTypeSImgList.Images.SetKeyName(7, "class.png");
      this.fileTypeSImgList.Images.SetKeyName(8, "cmd.png");
      this.fileTypeSImgList.Images.SetKeyName(9, "com.png");
      this.fileTypeSImgList.Images.SetKeyName(10, "cpp.png");
      this.fileTypeSImgList.Images.SetKeyName(11, "cs.png");
      this.fileTypeSImgList.Images.SetKeyName(12, "css.png");
      this.fileTypeSImgList.Images.SetKeyName(13, "dll.png");
      this.fileTypeSImgList.Images.SetKeyName(14, "doc.png");
      this.fileTypeSImgList.Images.SetKeyName(15, "docx.png");
      this.fileTypeSImgList.Images.SetKeyName(16, "exe.png");
      this.fileTypeSImgList.Images.SetKeyName(17, "file.png");
      this.fileTypeSImgList.Images.SetKeyName(18, "flv.png");
      this.fileTypeSImgList.Images.SetKeyName(19, "folder.png");
      this.fileTypeSImgList.Images.SetKeyName(20, "folder2.png");
      this.fileTypeSImgList.Images.SetKeyName(21, "gif.png");
      this.fileTypeSImgList.Images.SetKeyName(22, "gz.png");
      this.fileTypeSImgList.Images.SetKeyName(23, "h.png");
      this.fileTypeSImgList.Images.SetKeyName(24, "htm.png");
      this.fileTypeSImgList.Images.SetKeyName(25, "html.png");
      this.fileTypeSImgList.Images.SetKeyName(26, "inf.png");
      this.fileTypeSImgList.Images.SetKeyName(27, "ini.png");
      this.fileTypeSImgList.Images.SetKeyName(28, "iso.png");
      this.fileTypeSImgList.Images.SetKeyName(29, "jar.png");
      this.fileTypeSImgList.Images.SetKeyName(30, "java.png");
      this.fileTypeSImgList.Images.SetKeyName(31, "jpeg.png");
      this.fileTypeSImgList.Images.SetKeyName(32, "jpg.png");
      this.fileTypeSImgList.Images.SetKeyName(33, "lnk.png");
      this.fileTypeSImgList.Images.SetKeyName(34, "m3u.png");
      this.fileTypeSImgList.Images.SetKeyName(35, "mba.png");
      this.fileTypeSImgList.Images.SetKeyName(36, "mdb.png");
      this.fileTypeSImgList.Images.SetKeyName(37, "mid.png");
      this.fileTypeSImgList.Images.SetKeyName(38, "mkv.png");
      this.fileTypeSImgList.Images.SetKeyName(39, "mmm.png");
      this.fileTypeSImgList.Images.SetKeyName(40, "mov.png");
      this.fileTypeSImgList.Images.SetKeyName(41, "mp3.png");
      this.fileTypeSImgList.Images.SetKeyName(42, "mp4.png");
      this.fileTypeSImgList.Images.SetKeyName(43, "mpeg.png");
      this.fileTypeSImgList.Images.SetKeyName(44, "mpg.png");
      this.fileTypeSImgList.Images.SetKeyName(45, "msi.png");
      this.fileTypeSImgList.Images.SetKeyName(46, "pdf.png");
      this.fileTypeSImgList.Images.SetKeyName(47, "png.png");
      this.fileTypeSImgList.Images.SetKeyName(48, "ppt.png");
      this.fileTypeSImgList.Images.SetKeyName(49, "pptx.png");
      this.fileTypeSImgList.Images.SetKeyName(50, "psd.png");
      this.fileTypeSImgList.Images.SetKeyName(51, "py.png");
      this.fileTypeSImgList.Images.SetKeyName(52, "rar.png");
      this.fileTypeSImgList.Images.SetKeyName(53, "rtf.png");
      this.fileTypeSImgList.Images.SetKeyName(54, "swf.png");
      this.fileTypeSImgList.Images.SetKeyName(55, "tar.png");
      this.fileTypeSImgList.Images.SetKeyName(56, "tif.png");
      this.fileTypeSImgList.Images.SetKeyName(57, "tiff.png");
      this.fileTypeSImgList.Images.SetKeyName(58, "txt.png");
      this.fileTypeSImgList.Images.SetKeyName(59, "wav.png");
      this.fileTypeSImgList.Images.SetKeyName(60, "wma.png");
      this.fileTypeSImgList.Images.SetKeyName(61, "wmv.png");
      this.fileTypeSImgList.Images.SetKeyName(62, "xls.png");
      this.fileTypeSImgList.Images.SetKeyName(63, "xlsx.png");
      this.fileTypeSImgList.Images.SetKeyName(64, "xml.png");
      this.fileTypeSImgList.Images.SetKeyName(65, "zip.png");
      // 
      // timerListFilenames
      // 
      this.timerListFilenames.Tick += new System.EventHandler(this.timerListFilenames_Tick);
      // 
      // toolStripNav
      // 
      this.toolStripNav.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.toolStripNav.AutoSize = false;
      this.toolStripNav.BackColor = System.Drawing.Color.Transparent;
      this.toolStripNav.BackgroundImage = global::MyBucks.Properties.Resources.plbg;
      this.toolStripNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.toolStripNav.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStripNav.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripNav.Location = new System.Drawing.Point(-1, 58);
      this.toolStripNav.Name = "toolStripNav";
      this.toolStripNav.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStripNav.Size = new System.Drawing.Size(762, 25);
      this.toolStripNav.TabIndex = 18;
      this.toolStripNav.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripNav_ItemClicked);
      // 
      // timerClickFxNavi
      // 
      this.timerClickFxNavi.Tick += new System.EventHandler(this.timerClickFxNavi_Tick);
      // 
      // buttonUp
      // 
      this.buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonUp.BackColor = System.Drawing.Color.LightSteelBlue;
      this.buttonUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.buttonUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.buttonUp.Location = new System.Drawing.Point(-1, 80);
      this.buttonUp.Name = "buttonUp";
      this.buttonUp.Size = new System.Drawing.Size(762, 20);
      this.buttonUp.TabIndex = 20;
      this.buttonUp.Text = "┗┅Up";
      this.buttonUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.buttonUp.DoubleClick += new System.EventHandler(this.buttonUp_DoubleClick);
      this.buttonUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonUp_MouseDown);
      this.buttonUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonUp_MouseUp);
      // 
      // panelTop
      // 
      this.panelTop.BackgroundImage = global::MyBucks.Properties.Resources.plbg;
      this.panelTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.panelTop.Controls.Add(this.toolStripMain);
      this.panelTop.Controls.Add(this.menuStrip);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTop.Location = new System.Drawing.Point(0, 0);
      this.panelTop.Name = "panelTop";
      this.panelTop.Size = new System.Drawing.Size(760, 60);
      this.panelTop.TabIndex = 19;
      // 
      // toolStripMain
      // 
      this.toolStripMain.BackColor = System.Drawing.Color.Transparent;
      this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripMain.ImageScalingSize = new System.Drawing.Size(30, 30);
      this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonExtract,
            this.toolStripSeparator6,
            this.buttonView,
            this.toolStripSeparator9,
            this.findIcon,
            this.findBox});
      this.toolStripMain.Location = new System.Drawing.Point(0, 23);
      this.toolStripMain.Name = "toolStripMain";
      this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStripMain.Size = new System.Drawing.Size(760, 37);
      this.toolStripMain.TabIndex = 4;
      // 
      // buttonExtract
      // 
      this.buttonExtract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.buttonExtract.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonExtractCurr,
            this.buttonExtractTmp});
      this.buttonExtract.Image = global::MyBucks.Properties.Resources.unpack;
      this.buttonExtract.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.buttonExtract.Name = "buttonExtract";
      this.buttonExtract.Size = new System.Drawing.Size(46, 34);
      this.buttonExtract.Text = "Extract to...";
      this.buttonExtract.ButtonClick += new System.EventHandler(this.buttonExtract_ButtonClick);
      // 
      // buttonExtractCurr
      // 
      this.buttonExtractCurr.Name = "buttonExtractCurr";
      this.buttonExtractCurr.Size = new System.Drawing.Size(175, 22);
      this.buttonExtractCurr.Text = "To current folder";
      this.buttonExtractCurr.Click += new System.EventHandler(this.buttonExtractCurr_Click);
      // 
      // buttonExtractTmp
      // 
      this.buttonExtractTmp.Name = "buttonExtractTmp";
      this.buttonExtractTmp.Size = new System.Drawing.Size(175, 22);
      this.buttonExtractTmp.Text = "To temp folder";
      this.buttonExtractTmp.Click += new System.EventHandler(this.buttonExtractTmp_Click);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.ForeColor = System.Drawing.Color.Black;
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 37);
      // 
      // buttonView
      // 
      this.buttonView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.buttonView.Image = global::MyBucks.Properties.Resources.view;
      this.buttonView.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.buttonView.Name = "buttonView";
      this.buttonView.Size = new System.Drawing.Size(34, 34);
      this.buttonView.Text = "View mode";
      this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 37);
      // 
      // findIcon
      // 
      this.findIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.findIcon.Image = global::MyBucks.Properties.Resources.find;
      this.findIcon.Name = "findIcon";
      this.findIcon.Size = new System.Drawing.Size(30, 34);
      this.findIcon.Text = "Find";
      // 
      // findBox
      // 
      this.findBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.findBox.Name = "findBox";
      this.findBox.Size = new System.Drawing.Size(100, 37);
      this.findBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.findBox.MouseLeave += new System.EventHandler(this.findBox_MouseLeave);
      this.findBox.TextChanged += new System.EventHandler(this.findBox_TextChanged);
      // 
      // menuStrip
      // 
      this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(30)))));
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.menuStrip.Size = new System.Drawing.Size(760, 25);
      this.menuStrip.TabIndex = 5;
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openArchiveToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // openArchiveToolStripMenuItem
      // 
      this.openArchiveToolStripMenuItem.Name = "openArchiveToolStripMenuItem";
      this.openArchiveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.openArchiveToolStripMenuItem.Text = "Open archive";
      this.openArchiveToolStripMenuItem.Click += new System.EventHandler(this.openArchiveToolStripMenuItem_Click);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // openArchiveDialog
      // 
      this.openArchiveDialog.Filter = "Zip Archive(*.zip)|*.zip|MyBucks Archive(*.mbA)|*.mbA|7-zip Archive(*.7z)|*.7z|ta" +
          "r Archive(*.tar)|*.tar|gzip Archive(*.gz)|*.gz|bzip2 Archive(*.bz2)|*.bz2";
      // 
      // ArchiveViewer
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(760, 496);
      this.Controls.Add(this.buttonUp);
      this.Controls.Add(this.toolStripNav);
      this.Controls.Add(this.panelTop);
      this.Controls.Add(this.listViewExp);
      this.Font = new System.Drawing.Font("Source Sans Pro", 9F);
      this.ForeColor = System.Drawing.Color.Black;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip;
      this.MinimumSize = new System.Drawing.Size(640, 360);
      this.Name = "ArchiveViewer";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MyBucks Archive Viewer";
      this.Load += new System.EventHandler(this.DialogExtract_Load);
      this.contextMenuExp.ResumeLayout(false);
      this.panelTop.ResumeLayout(false);
      this.panelTop.PerformLayout();
      this.toolStripMain.ResumeLayout(false);
      this.toolStripMain.PerformLayout();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView listViewExp;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogExtract;
    private System.Windows.Forms.ImageList fileTypeImgList;
    private System.Windows.Forms.Timer timerListFilenames;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ToolStrip toolStripNav;
    private System.Windows.Forms.Timer timerClickFxNavi;
    private System.Windows.Forms.Label buttonUp;
    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.ToolStrip toolStripMain;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripButton buttonView;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripLabel findIcon;
    private System.Windows.Forms.ToolStripTextBox findBox;
    private System.Windows.Forms.ToolStripSplitButton buttonExtract;
    private System.Windows.Forms.ToolStripMenuItem buttonExtractCurr;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openArchiveToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openArchiveDialog;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip contextMenuExp;
    private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem invertSelectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem viewModeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem largeIconsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem smallIconsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tileToolStripMenuItem;
    private System.Windows.Forms.ImageList fileTypeSImgList;
    private System.Windows.Forms.ToolStripMenuItem buttonExtractTmp;
  }
}