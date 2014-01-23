using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MyBucks
{
  internal class Archive
  {
    internal static readonly String path_tmp = Path.Combine(Application.StartupPath, "reliables");
    internal static readonly String path_7za = Path.Combine(path_tmp, "7za.exe");
    internal static readonly String path_unrar = Path.Combine(path_tmp, "unrar.exe");

    internal static bool isArchiveSupported(String filename)
    {
      return (isMbaSupported(filename) || is7zSupported(filename) || isUnrarSupported(filename) || isZipSupported(filename));
    }

    internal static bool is7zSupported(String filename)
    {
      filename = filename.ToLower();
      return (filename.EndsWith(".7z") ||
            filename.EndsWith(".tar") ||
            filename.EndsWith(".gz") ||
            filename.EndsWith(".bz2"));
    }

    internal static bool isMbaSupported(String filename)
    {
      return (filename.ToLower().EndsWith(".mba"));
    }

    internal static bool isUnrarSupported(String filename)
    {
      return (filename.ToLower().EndsWith(".rar"));
    }

    internal static bool isZipSupported(String filename)
    {
      return (filename.ToLower().EndsWith(".zip"));
    }

    private static void release7za()
    {
      try
      {
        if (!File.Exists(path_7za))
        {
          Directory.CreateDirectory(path_tmp);
          FileStream fStream = new FileStream(path_7za, FileMode.Create);
          BinaryWriter binWriter = new BinaryWriter(fStream);
          binWriter.Write(Properties.Resources._7za);
          binWriter.Close(); fStream.Close();
        }
      }
      catch { MessageBox.Show("Cannot find supporting files, try relaunching with higher execution level."); Application.Exit(); }
    }

    private static void releaseUNRAR()
    {
      try
      {
        if (!File.Exists(path_unrar))
        {
          Directory.CreateDirectory(path_tmp);
          FileStream fStream = new FileStream(path_unrar, FileMode.Create);
          BinaryWriter binWriter = new BinaryWriter(fStream);
          binWriter.Write(Properties.Resources.UnRAR);
          binWriter.Close(); fStream.Close();
        }
      }
      catch { MessageBox.Show("Cannot find supporting files, try relaunching with higher execution level."); Application.Exit(); }
    }

    internal static void DoMbs(string[] filelist, string destFullPath)
    {
      if (MessageBox.Show("MyBucks Archive only supports file adding; all folder entries will be ignored!",
        UI.findLangResString("MyBucks"), MessageBoxButtons.OKCancel) != DialogResult.OK)
        return;

      System.IO.File.Create(destFullPath).Close();
      IO.appendHeader(destFullPath, filelist.Length);

      foreach (string fileObj in filelist)
      {
        try
        {
          byte[] inputSingleFile = IO.readData(fileObj);
          IO.appendFilename(destFullPath, Path.GetFileName(fileObj));
          IO.appendData(destFullPath, inputSingleFile);
        }
        catch { }
      }
    }

    internal static string Do7z(string[] filelist, string destFullPath, bool mergeInsteadofOvrWrite)
    {
      if (!mergeInsteadofOvrWrite)
      {
        File.Delete(destFullPath);
      }

      release7za();

      String results = "";

      try
      {
        //string tmpDir = Path.Combine(Path.GetDirectoryName(workingDir),
        //"~" + Convert.ToString(DateTime.Now.TimeOfDay.ToString().GetHashCode(), 16));
        //Directory.CreateDirectory(tmpDir);
        foreach (string filestr in filelist)
        {
          string argument;

          //use relative path
          //must ensure all files are in the same dir
          if (Directory.Exists(filestr))
          {
            argument = filestr + "\\*";

            //if occurs to be a dir, then add * to include all files & subfolders within
            // C:\Users --> C:\Users\*
          }
          else if (File.Exists(filestr))
          {
            argument = filestr;

            // C:\boot.ini --> C:\boot.ini
          }

          Process process = new Process();
          process.StartInfo = new ProcessStartInfo(path_7za, "a \"" + destFullPath + "\" \"" + filestr + "\"");
          process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

          //process.StartInfo.RedirectStandardOutput = true;
          process.StartInfo.UseShellExecute = false;

          process.Start();

          //StreamReader sReader = process.StandardOutput;
          process.WaitForExit();

          //results += "\r\n" + sReader.ReadToEnd();
        }
        return results;
      }
      catch { return "Error while adding into 7z archive."; }
    }

    internal static void DoZip(string[] filelist, string workingDir, string destFullPath, bool mergeInsteadofOvrWrite)
    {
      if (!mergeInsteadofOvrWrite)
      {
        try
        {
          File.Delete(destFullPath);
        }
        catch { MessageBox.Show("Failed to delete old archive."); return; }
      }

      Ionic.Zip.ZipFile archive = new Ionic.Zip.ZipFile(destFullPath);
      foreach (string filepath in filelist)
      {
        if (Directory.Exists(filepath))
          archive.AddDirectory(filepath, Path.GetDirectoryName(filepath.Replace(workingDir, "")));//Not Done with files in dir
        else if (File.Exists(filepath))
          archive.AddFile(filepath, Path.GetDirectoryName(filepath.Replace(workingDir, "")));
      }
      archive.Save();
    }

    internal static String Un7z(string archiveFile, string destDirectory)
    {
      try
      {
        release7za();
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo(path_7za, "x \"" + archiveFile + "\" -o\"" + destDirectory + "\" -y");
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        StreamReader sReader = process.StandardOutput;

        while (!process.HasExited)
        { }
        String results = "\r\n" + sReader.ReadToEnd();

        return results;
      }
      catch { return "Error while extracting RAR archive."; }
    }

    internal static void Un7zSingle(string pathInArchive, string archiveFile, string destDirectory)
    {
      try
      {
        release7za();
        Process process = new Process();

        //-ir!DIR1\*.cpp
        process.StartInfo = new ProcessStartInfo(path_7za, "x \"" + archiveFile + "\" -o\"" + destDirectory + "\" -i!" + pathInArchive.Replace("/", "\\") + " -y");
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();
        process.WaitForExit();
        while (!process.HasExited)
        { }
      }
      catch { }
    }

    internal static String UnRAR(string archiveFile, string destDirectory)
    {
      try
      {
        releaseUNRAR();
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo(path_unrar, "x \"" + archiveFile + "\" \"" + destDirectory + "\\\" -y");
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        StreamReader sReader = process.StandardOutput;

        while (!process.HasExited)
        { }
        String results = "\r\n" + sReader.ReadToEnd();

        return results;
      }
      catch { return "Error while extracting RAR archive."; }
    }

    internal static void UnRarSingle(string pathInArchive, string archiveFile, string destDirectory)
    {
      try
      {
        releaseUNRAR();
        Process process = new Process();

        //unrar x "C:\jvm.rar" "tools" "C:\Users\Abc\Desktop\newfolder"
        //tools is a directory (must not add '\')

        process.StartInfo = new ProcessStartInfo(path_unrar,
          "x \"" + archiveFile + "\" \"" + pathInArchive.Replace("/", "\\") + "\" \"" + destDirectory + "\" -y");
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();
        process.WaitForExit();
        while (!process.HasExited)
        { }
      }
      catch { }
    }

    internal static void UnZipSingle(string pathInArchive, string archiveFile, string destDirectory)
    {
      Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile(archiveFile, System.Text.Encoding.GetEncoding("GBK"));

      Ionic.Zip.ZipEntry entry = zipfile[pathInArchive];

      entry.Extract(destDirectory, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
    }

    internal static String UnZipAll(string archiveFile, string destDirectory)
    {
      StringWriter errwriter = new StringWriter();

      Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile(archiveFile, errwriter);
      if (MessageBox.Show(UI.findLangResString("Overwrite ALL existing files?"),
        UI.findLangResString("MyBucks"), MessageBoxButtons.YesNo) == DialogResult.Yes)
        zipfile.ExtractAll(destDirectory, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
      else
        zipfile.ExtractAll(destDirectory, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);

      errwriter.WriteLine("Extraction Complete!");

      return errwriter.ToString();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="fileindices must be only selected files in package"></param>
    /// <param name="filenames must be all files within package"></param>
    /// <param name="destDir"></param>
    internal static String UnMba(List<int> fileindices, List<String> filenames, String srcPath, String destDir)
    {
      List<String> errMessages = new List<string>();

      //List<String> packFileNameOut;
      List<byte[]> packFileDataOut = new List<byte[]>();

      int fileCount = IO.readHeader(srcPath);
      if (fileCount < 1)
        errMessages.Add("\r\nError reading archive header: Archive may be empty or corrupt.");

      //return if file is corrupt

      //rules of error processing, codes in a same block can be put together in try-catch
      //separate code like readHeader, always consider returning a error code and process separately

      try
      {
        long lastFilenamePosit = 4;
        long lastDataPosit;

        for (int i = 0; i < fileCount; i++)
        {
          lastDataPosit = IO.readFilenameEnd(lastFilenamePosit);

          //lastDataPosit is the beginning of the current file's data, fetched from readFilenameEnd
          if (fileindices.Contains(i))
          {
            //case with data to read
            byte[] singleFileByteArrRAW = IO.readDataWithEnd(srcPath, lastDataPosit);
            byte[] singleFileByteArr = new byte[singleFileByteArrRAW.Length - 8];
            Array.Copy(singleFileByteArrRAW, singleFileByteArr, singleFileByteArr.Length);
            packFileDataOut.Add(singleFileByteArr);
            byte[] singleFileByteArrRAWEnd = new byte[8];
            for (int endPositByteIndex = 0; endPositByteIndex < 8; endPositByteIndex++)
              singleFileByteArrRAWEnd.SetValue(singleFileByteArrRAW.GetValue(singleFileByteArrRAW.Length - 8 + endPositByteIndex), endPositByteIndex);

            lastFilenamePosit = IO.byteToLong(singleFileByteArrRAWEnd);
          }
          else
          {
            //case without data to read
            lastFilenamePosit = IO.readDataEnd(srcPath, lastDataPosit);
          }
        }
      }
      catch (Exception ex) { errMessages.Add("\r\nError reading archive: " + ex.Message); }

      try
      {
        Directory.CreateDirectory(destDir);

        for (int i = 0; i < fileindices.Count; i++)
        {
          String singleFileName = filenames[fileindices[i]];
          byte[] singleFileData = packFileDataOut[i];
          String singleFilePath = Path.Combine(destDir, Path.GetFileName(singleFileName));

          using (FileStream output = new FileStream(singleFilePath, FileMode.Create))
          {
            using (BinaryWriter binWriter = new BinaryWriter(output))
            {
              binWriter.Write(singleFileData);
            }
          }
        }
      }
      catch (Exception ex) { errMessages.Add("\r\nError extracting archive files: " + ex.Message); }

      String result = "";
      foreach (String str in errMessages.ToArray())
        result += (str + "\r\n");
      return result;
    }

    internal static string UnZip(List<string> filenamesSelected, string archive_filename, string dest)
    {
      StringWriter errwriter = new StringWriter();

      Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile(archive_filename, errwriter, System.Text.Encoding.GetEncoding("GBK"));
      if (MessageBox.Show(UI.findLangResString("Overwrite ALL existing files?"), UI.findLangResString("MyBucks"), MessageBoxButtons.YesNo)
        == DialogResult.Yes)
      {
        foreach (Ionic.Zip.ZipEntry entry in zipfile)
          if (filenamesSelected.Contains(entry.FileName))
            entry.Extract(dest, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
      }
      else
      {
        foreach (Ionic.Zip.ZipEntry entry in zipfile)
          if (filenamesSelected.Contains(entry.FileName))
            entry.Extract(dest, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);
      }

      errwriter.WriteLine("Extraction Complete!");

      return ("\r\n" + errwriter.ToString());
    }

    internal static List<string> list7zFilenames(string archive_filename)
    {
      List<string> filelistOfArchive = new List<string>();

      String results = "";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo(Archive.path_7za, "l \"" + archive_filename + "\"");
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.UseShellExecute = false;

      process.Start();

      while (!process.HasExited)
      { }
      results = process.StandardOutput.ReadToEnd();

      string[] resultsInLines = Regex.Split(results, "\r\n");
      int filenameStartI = 0;
      int filenameStartLine = 0;
      for (int i = 0; i < resultsInLines.Length; i++)
      {
        if (resultsInLines[i].Contains("Compressed") && resultsInLines[i].Contains("Name") && !resultsInLines[i].EndsWith(".7z"))
        {
          filenameStartI = resultsInLines[i].IndexOf("Name");
          filenameStartLine = i + 2;
          break;
        }
      }

      for (int i = filenameStartLine; i < resultsInLines.Length; i++)
      {
        if (!resultsInLines[i].StartsWith("-------------------"))
        {
          string pathWithoutSlashFolder = resultsInLines[i].Substring(filenameStartI).Replace("\\", "/");
          if (resultsInLines[i].Remove(filenameStartI).Contains("D...."))
          {
            //is a dir
            filelistOfArchive.Add(pathWithoutSlashFolder + "/");
          }
          else if (resultsInLines[i].Remove(filenameStartI).Contains("....A") || resultsInLines[i].Remove(filenameStartI).Contains("....."))
          {
            //is a file
            filelistOfArchive.Add(pathWithoutSlashFolder);
          }
        }
        else
        {
          break; //avoid reading last line
        }
      }

      return filelistOfArchive;
    }

    internal static List<string> listRarFilenames(string archive_filename)
    {
      String resultsFull = "";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo(Archive.path_unrar, "v \"" + archive_filename + "\"");
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.UseShellExecute = false;

      process.Start();
      while (!process.HasExited)
      { }
      resultsFull = process.StandardOutput.ReadToEnd();
      string[] resultsInLinesFull = Regex.Split(resultsFull, "\r\n");

      String resultsBare = "";
      process.StartInfo = new ProcessStartInfo(Archive.path_unrar, "vb \"" + archive_filename + "\"");
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      while (!process.HasExited)
      { }
      resultsBare = process.StandardOutput.ReadToEnd();
      string[] resultsInLinesBare = Regex.Split(resultsBare, "\r\n");

      //sort to ensure functionality of archive file listing
      Array.Sort(resultsInLinesBare);

      //cannot sort resultsInLinesFull to speed up,
      //each file's properties (dir or file) are stored in the line next to where its filename is

      List<string> finalFilelist = new List<string>(resultsInLinesBare);
      finalFilelist.Remove("");

      for (int i = 0; i < finalFilelist.Count; i++)
      {
        finalFilelist[i] = finalFilelist[i].Replace("\\", "/");
        for (int j = 0; j < resultsInLinesFull.Length; j++)
        {
          //find matching file/dir entry
          //in full info, filenames have a 1 char padding " ", don't know why anyway
          if (((string)resultsInLinesFull.GetValue(j)).TrimStart() == finalFilelist[i])
          {
            //can know it is a directory from full info, then add slash
            if (((string)resultsInLinesFull.GetValue(j + 1)).Contains(".D....."))
            {
              finalFilelist[i] += "/";
            }
          }
        }
      }

      return finalFilelist;
    }

    internal static string UnMbaSingle(string filename_relative, string archivefile, string dest)
    {
      List<String> errMessages = new List<string>();

      int fileCount = IO.readHeader(archivefile);
      if (fileCount < 1)
        errMessages.Add("\r\nError reading archive header: Archive may be empty or corrupt.");

      //return if file is corrupt

      //rules of error processing, codes in a same block can be put together in try-catch
      //separate code like readHeader, always consider returning a error code and process separately

      byte[] singleFileByteArr = new byte[0];

      try
      {
        long lastFilenamePosit = 4;
        long lastDataPosit;

        for (int i = 0; i < fileCount; i++)
        {
          string filenamWE = IO.readFilenameWithEnd(archivefile, lastFilenamePosit);
          lastDataPosit = long.Parse(filenamWE.Substring(filenamWE.IndexOf("|") + 1));
          string filename = filenamWE.Remove(filenamWE.IndexOf("|")).TrimStart();

          //lastDataPosit is the beginning of the current file's data, fetched from readFilenameEnd
          if (filename == filename_relative)
          {
            //case with data to read
            byte[] singleFileByteArrRAW = IO.readDataWithEnd(archivefile, lastDataPosit);
            singleFileByteArr = new byte[singleFileByteArrRAW.Length - 8];
            Array.Copy(singleFileByteArrRAW, singleFileByteArr, singleFileByteArr.Length);
            break;
          }
          else
          {
            //case without data to read
            lastFilenamePosit = IO.readDataEnd(archivefile, lastDataPosit);
          }
        }
      }
      catch (Exception ex) { errMessages.Add("\r\nError reading archive: " + ex.Message); }

      try
      {
        Directory.CreateDirectory(dest);

        using (FileStream output = new FileStream(Path.Combine(dest, filename_relative), FileMode.Create))
        {
          using (BinaryWriter binWriter = new BinaryWriter(output))
          {
            binWriter.Write(singleFileByteArr);
          }
        }
      }
      catch (Exception ex) { errMessages.Add("\r\nError extracting archive files: " + ex.Message); }

      String result = "";
      foreach (String str in errMessages.ToArray())
        result += (str + "\r\n");
      return result;
    }
  }
}