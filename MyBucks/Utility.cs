using System;
using System.IO;

namespace MyBucks
{
  internal class Utility
  {
    internal static String correctPath(String path)
    {
      if (path == null || path.Length < 3 || !char.IsLetter(path, 0) || path.Substring(2, 1) != "\\")
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //this is to prevent inputing ".", which is judged valid by Directory.Exists
        //also prevents null and empty, and wierd input like "#:\Windows"
      }

      if (!Directory.Exists(path))
      {
        //MessageBox.Show(new DirectoryNotFoundException().Message);
        return correctPath(Path.GetDirectoryName(path));
        //try returning parents, returning default path only when all parents are illegal
      }
      else
      {
        while (path.Contains("\\\\"))
          path = path.Replace("\\\\", "\\");

        //format C:\\ABC\\\\\\\ into C:\ABC\

        if (path.EndsWith("\\") && path.Length > 3)
          path = path.Remove(path.Length - 1);
        /*
         * avoid processing C:\ABC\CBD\ as illegal filename
         * more seriously, family tree will be like this without removing "\\"
         * C:\
         * C:\ABC\
         * C:\ABC\CBD\
         * C:\ABC\CBD\\
         */
        try
        { Directory.GetFiles(path); }
        catch
        {
          //MessageBox.Show(new UnauthorizedAccessException().Message);
          return correctPath(Path.GetDirectoryName(path));
          //try returning parents, returning default path only when all parents are illegal
        }

        return path;
      }
    }

    internal static string getReadableFileLength(string filename)
    {
      if (!File.Exists(filename))
      {
        return "unknown";
      }

      string[] sizes = { UI.findLangResString("bytes"), "KB", "MB", "GB" };
      double len = new FileInfo(filename).Length;
      int order = 0;
      while (len >= 1024 && order + 1 < sizes.Length)
      {
        order++;
        len = len / 1024;
      }

      // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
      // show a single decimal place, and no space.
      return String.Format("{0:0.##} {1}", len, sizes[order]);
    }

    internal enum OvrDialogResult
    { yes, allYes, no, allNo, abort, empty };
  }
}