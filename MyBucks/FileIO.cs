using System;
using System.IO;

namespace MyBucks
{
  public class FileIO
  {
    public class AsyncFileCopier
    {
      public delegate void FileCopyDelegate(string filepath, string destDir, bool move_flag);

      public static void AsynFileCopy(string filepath, string destDir, bool move_flag)
      {
        FileCopyDelegate del = new FileCopyDelegate(FileCopy);
        IAsyncResult result = del.BeginInvoke(filepath, destDir, move_flag, CallBackAfterFileCopied, null);
      }

      public static void FileCopy(string filepath, string destDir, bool move_flag)
      {
        // Code to copy the file
        try
        {
          if (File.Exists(filepath))
          {
            File.Copy(filepath, Path.Combine(destDir, Path.GetFileName(filepath)));
            if (move_flag)
              File.Delete(filepath);
          }
          else if (Directory.Exists(filepath))
          {
            IO.CopyFolderTo(filepath, Path.Combine(destDir, Path.GetFileName(filepath)));
            if (move_flag)
              Directory.Delete(filepath, true);
          }
        }
        catch { }
      }

      public static void CallBackAfterFileCopied(IAsyncResult result)
      {
        // Code to be run after file copy is done
      }
    }
  }
}