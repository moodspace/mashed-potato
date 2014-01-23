using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MyBucks.Properties;

namespace MyBucks
{
  class Shared
  {
    internal static void openArchive(string filename)
    {
      Form archiveViewer = new ArchiveViewer(filename);
      archiveViewer.Show();
      //add to recent list
      if (!Settings.Default.recentArchiveList.Contains(filename))
      {
        if (Settings.Default.recentArchiveList[0] == "")
          Settings.Default.recentArchiveList[0] = filename;
        else
          Settings.Default.recentArchiveList.Insert(0, filename);
      }
    }
  }
}
