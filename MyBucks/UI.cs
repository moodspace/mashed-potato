using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MyBucks.Properties;

namespace MyBucks
{
  internal class UI
  {
    public static int getImgKeyForFolder(ImageList imgList, bool isEmpty)
    {
      string typename;
      if (isEmpty)
        typename = "mbs-folderE";
      else
        typename = "mbs-folder";

      if (imgList.Images.ContainsKey(typename))
      {
        return imgList.Images.IndexOfKey(typename);
        //already added icon
      }
      else
      {
        String fullpathFassoc = Path.Combine(Application.StartupPath, "fassoc\\" + typename + ".pnx");
        if (File.Exists(fullpathFassoc))
        {
          imgList.Images.Add(typename, new Bitmap(fullpathFassoc));

          //apply arbitary iconset under program path
          return imgList.Images.IndexOfKey(typename);
        }
      }

      return imgList.Images.IndexOfKey(typename);
    }

    public static int getImgKey(ImageList imgList, String filepath)
    {
      filepath = filepath.ToLower();
      String typename;
      int imgKey;

      if (new List<string>(Environment.GetLogicalDrives()).Contains(filepath.ToUpper()))
      {
        //must be a drive
        DriveInfo info = new DriveInfo(filepath);
        switch (info.DriveType)
        {
          case DriveType.CDRom:
            typename = "mbs-dsc"; break;
          case DriveType.Network:
            typename = "mbs-ntw"; break;
          case DriveType.Removable:
            typename = "mbs-usb"; break;
          case DriveType.Fixed:
            typename = "mbs-hdd"; break;
          default:
            typename = "mbs-folder"; break;
        }
      }
      else
      {
        //the reason I put drive condition out of directory condition:
        //for an hdd drive, directory.exist works; for an EMPTY optical drive, it doesn't.
        //but the optical drive "exists" and it must be displayed with icon, right?
        if (Directory.Exists(filepath))
        {
          try
          {
            //must be a folder
            if (Directory.GetFiles(filepath).Length + Directory.GetDirectories(filepath).Length > 0)
              typename = "mbs-folder";
            else
              typename = "mbs-folderE";
          }
          catch { typename = "mbs-folderE"; }
        }
        else if (File.Exists(filepath))
        {
          //must be a file
          String filename = Path.GetFileName(filepath);
          typename = filename.Substring(filename.LastIndexOf(".") + 1);
          if (filename.LastIndexOf(".") == -1)
          {
            typename = "mbs-file";
          }
        }
        else
        {
          typename = "mbs-file";
        }
      }

      //finish categorizing, now find existing images, else add from system/local hdd
      if (imgList.Images.ContainsKey(typename))
      {
        imgKey = imgList.Images.IndexOfKey(typename);

        //already added icon
      }
      else
      {
        String fullpathFassoc = Path.Combine(Application.StartupPath, "fassoc\\" + typename + ".pnx");
        if (File.Exists(fullpathFassoc))
        {
          imgList.Images.Add(typename, new Bitmap(fullpathFassoc));

          //apply arbitary iconset under program path
        }
        else
        {
          Icon icon = IconHandler.IconHandler.IconFromExtension(typename, IconHandler.IconSize.Large);
          if (icon == null)
          {
            imgList.Images.Add(typename, Properties.Resources.mbs_file);

            //not associated in sys shell32, use FILE icon
          }
          else
          {
            imgList.Images.Add(typename, icon.ToBitmap());

            //find associated icon in sys shell
          }
        }
      }

      return imgList.Images.IndexOfKey(typename);
    }

    /// <summary>
    /// Do not use a folder here, all entries will be regarded as FILE
    /// </summary>
    /// <param name="imgList"></param>
    /// <param name="filenameNotFolder"></param>
    /// <returns></returns>
    public static int getImgKeyFromFilename(ImageList imgList, String filenameNotFolder)
    {
      filenameNotFolder = filenameNotFolder.ToLower();
      String typename;
      int imgKey;

      //must be a file
      typename = filenameNotFolder.Substring(filenameNotFolder.LastIndexOf(".") + 1);
      if (filenameNotFolder.LastIndexOf(".") == -1)
      {
        typename = "mbs-file";
      }
      else if (imgList.Images.ContainsKey(typename))
      {
        imgKey = imgList.Images.IndexOfKey(typename);
      }
      else
      {
        String fullpathFassoc = Path.Combine(Application.StartupPath, "fassoc\\" + typename + ".pnx");
        if (File.Exists(fullpathFassoc))
        {
          imgList.Images.Add(typename, new Bitmap(fullpathFassoc));

          //apply arbitary iconset under program path
        }
        else
        {
          Icon icon = IconHandler.IconHandler.IconFromExtension(typename, IconHandler.IconSize.Large);
          if (icon == null)
          {
            imgList.Images.Add(typename, Properties.Resources.mbs_file);

            //not associated in sys shell32, use FILE icon
          }
          else
          {
            imgList.Images.Add(typename, icon.ToBitmap());

            //find associated icon in sys shell
          }
        }
      }

      return imgList.Images.IndexOfKey(typename);
    }


    private static PrivateFontCollection myFontCollection = new PrivateFontCollection();

    public static void getDefaultFont()
    {
      byte[] fontBinArray; string fontfilename;
      fontBinArray = Properties.Resources.SourceSansPro_Regular; fontfilename = "ssans.otf";

      try
      {
        using (FileStream output = new FileStream(Path.Combine(Application.LocalUserAppDataPath, fontfilename), FileMode.OpenOrCreate))
        {
          using (BinaryWriter binWriter = new BinaryWriter(output))
          {
            binWriter.Write(fontBinArray);
            binWriter.Close();
          }
        }
      }
      catch { }

      try
      {
        myFontCollection.AddFontFile(Path.Combine(Application.LocalUserAppDataPath, fontfilename));
        Settings.Default.customFont = new Font(new FontFamily(myFontCollection.Families[0].Name, myFontCollection),
          Settings.Default.customFontSize, FontStyle.Regular);
      }
      catch { }
    }

    public static List<string[]> langRes = new List<string[]>();

    internal static string findLangResString(string key)
    {
      foreach (string[] strReg in langResKeysReg)
      {
        if (strReg.GetValue(1).ToString().EndsWith(key) &&
          strReg.GetValue(1).ToString().StartsWith("(str)"))
        {
          return langRes[int.Parse(strReg.GetValue(0).ToString()) + 1].
            GetValue(Settings.Default.languageIndexSettings + 1).ToString();
        }
      }
      return key;
    }

    internal static void switchLang(int langI, string target)
    {
      foreach (Form form in Application.OpenForms)
      {
        if (form.Name == target)
        {
          //update settings in App Settings
          Settings.Default.languageIndexSettings = langI;

          //change text for controls
          form.Text = "       ";

          //now continue this process in form.TextChanged event
        }
      }
    }

    /// <summary>
    /// compared to switchLang, can be more concise while invoked in a form's own methods
    /// easier to pinpoint a particular control
    /// </summary>
    /// <param name="container"></param>
    internal static void switchLangInObjs(object obj, string type, int oldLangIndex, int newLangIndex)
    {
      foreach (string[] entryLangStrArr in langRes)
      {
        if (entryLangStrArr.Length == langRes[0].Length + 1)
        {
          //example: 01, 02, 03
          int entryKey = int.Parse((string)entryLangStrArr.GetValue(0));

          //example: (string)Computer, navCaption
          string objName = (string)langResKeysReg[entryKey].GetValue(1);

          if (type == "Control")
          {
            Control[] matchResult = ((Control)obj).Controls.Find(objName, true);
            if (matchResult.Length > 0)
              matchResult[0].Text = (string)entryLangStrArr.GetValue(newLangIndex + 1);
          }
          else if (type == "Menu")
          {
            foreach (ToolStripMenuItem item in (ToolStripItemCollection)obj)
            {
              foreach (ToolStripMenuItem subitem in item.DropDownItems)
              {
                if (subitem.Name == objName)
                {
                  subitem.Text = (string)entryLangStrArr.GetValue(newLangIndex + 1);
                }
              }
              if (item.Name == objName)
              {
                item.Text = (string)entryLangStrArr.GetValue(newLangIndex + 1);
              }
            }
          }
          else if (type == "List")
          {
            foreach (ListViewItem item in ((ListView)obj).Items)
            {
              if (item.Text == (string)entryLangStrArr.GetValue(oldLangIndex + 1))
              {
                item.Text = (string)entryLangStrArr.GetValue(newLangIndex + 1);
              }
            }
          }
          else if (type == "Cntx")
          {
            foreach (ToolStripItem item in ((ContextMenuStrip)obj).Items)
            {
              if (item.Name == objName)
              {
                item.Text = (string)entryLangStrArr.GetValue(newLangIndex + 1); break;
              }
            }
          }
          else if (type == "Tool")
          {
            foreach (ToolStripItem item in ((ToolStrip)obj).Items)
            {
              if (item.Name == objName)
              {
                item.Text = (string)entryLangStrArr.GetValue(newLangIndex + 1);
              }
              if (item is ToolStripDropDownButton)
              {
                foreach (ToolStripItem subitem in ((ToolStripDropDownButton)item).DropDownItems)
                {
                  if (subitem.Name == objName)
                    subitem.Text = (string)entryLangStrArr.GetValue(newLangIndex + 1);
                }
              }
            }
          }
          else if (type == "Tree")
          {
            foreach (TreeNode item in ((TreeView)obj).Nodes)
            {
              if (item.Text == (string)entryLangStrArr.GetValue(oldLangIndex + 1))
              {
                item.Text = (string)entryLangStrArr.GetValue(newLangIndex + 1);
              }
            }
          }
        }
      }
    }

    internal readonly static string[] langResKeysReg_txt = Regex.Split(Properties.Resources.langResKeysReg_txt, "\r\n", RegexOptions.IgnoreCase);
    internal static List<string[]> langResKeysReg = new List<string[]>();

    internal static void readLangRes()
    {
      try
      {
        langRes.Clear();
        using (StreamReader reader = new StreamReader(Path.Combine(Application.StartupPath, "lang.txt")))
        {
          while (!reader.EndOfStream)
          {
            langRes.Add(reader.ReadLine().Split("\t".ToCharArray()));
          }
        }
      }
      catch { }
    }

    internal static string showFilestatuslabel(ListView listview)
    {
      string statuslabel = "";
      if (listview.SelectedIndices.Count == 0)
      {
        //avoid outofrange exception in latter codes
        return statuslabel;
      }
      else if (listview.SelectedIndices.Count == 1)
      {
        string path = listview.SelectedItems[0].Name;
        if (Directory.Exists(path))
        {
          DirectoryInfo info = new DirectoryInfo(path);
          FileAttributes attrs = info.Attributes;
          DateTime creaT = info.CreationTime;
          DateTime lastWT = info.LastWriteTime;

          statuslabel = (UI.findLangResString("Created: ") + creaT.ToString() + UI.findLangResString("last_modified") +
          lastWT.ToString()).PadRight(100) + attrs.ToString();
        }
        else if (File.Exists(path))
        {
          FileInfo info = new FileInfo(path);
          FileAttributes attrs = info.Attributes;
          long len = info.Length;
          double lenKb = len / (double)1024;
          statuslabel = (String.Format("{0:N}", len) + UI.findLangResString("bytes_approximately") +
          String.Format("{0:N}", lenKb) + " KB)").PadRight(100) + attrs.ToString();
        }
      }
      else
      {
        statuslabel = listview.SelectedIndices.Count + UI.findLangResString("Items");
      }

      return statuslabel;
    }

    internal static void applyIconset(string themeName, ToolStrip ts)
    {
      int currentIcnIndex = 0;
      //the index of the first icon not yet applied to button image

      if (themeName == "[default]")
      {
        Bitmap[] tsIcnDefault = 
        { Resources.pack, 
          Resources.copy, 
          Resources.move, 
          Resources.bin, 
          Resources.run, 
          Resources.view, 
          Resources.sidebar, 
          Resources.preview, 
          Resources.find, 
          Resources.filter};

        for (int i = 0; i < ts.Items.Count; i++)
        {
          if (ts.Items[i].Image != null)
          {
            if (currentIcnIndex < tsIcnDefault.Length)
            {
              ts.Items[i].Image = (Bitmap)tsIcnDefault.GetValue(currentIcnIndex);
              currentIcnIndex++;
            }
          }
        }
      }
      else
      { 
        //i.e. C:\ProgramFiles\MyBucks\themeicn\ColorfulTheme
        string[] icons = Directory.GetFiles(Path.Combine(Application.StartupPath, "themeicn\\" + themeName), "*.pnx");
        for (int i = 0; i < ts.Items.Count; i++)
        {
          if (ts.Items[i].Image != null)
          {
            if (currentIcnIndex < icons.Length)
            {
              ts.Items[i].Image = new Bitmap(icons.GetValue(currentIcnIndex).ToString());
              currentIcnIndex++;
            }
          }
        }
      }
    }

    internal static string[] getIconsetNames()
    {
      string[] iconsetNames = Directory.GetDirectories(Path.Combine(Application.StartupPath, "themeicn"));
      for (int i = 0; i < iconsetNames.Length; i++)
      {
        iconsetNames[i] = Path.GetFileName(iconsetNames[i]);
      }
      return iconsetNames;
    }
  }
}