using System;
using System.Collections.Generic;
using System.IO;

namespace MyBucks
{
  internal class IO
  {
    private const int FilenameLen = 250;

    public static void appendHeader(String path, int fileCount)
    {
      try
      {
        using (FileStream output = new FileStream(path, FileMode.Append))
        {
          using (BinaryWriter binWriter = new BinaryWriter(output))
          {
            binWriter.Write(fileCount);
          }
        }

        //store file count
        //binWriter.Close();
        //output.Close();
      }
      catch { }
    }

    public static int readHeader(String path)
    {
      try
      {
        using (FileStream input = new FileStream(path, FileMode.Open))
        {
          using (BinaryReader binReader = new BinaryReader(input))
          {
            int fileCount = binReader.ReadInt32();

            //binReader.Close(); input.Close();
            return fileCount;
          }
        }
      }
      catch
      {
        return int.MinValue;
      }
    }

    public static String appendFilename(String path, String filename)
    {
      try
      {
        using (FileStream output = new FileStream(path, FileMode.Append))
        {
          using (BinaryWriter binWriter = new BinaryWriter(output))
          {
            binWriter.Write(filename.PadLeft(FilenameLen).ToCharArray());
          }
        }

        //binWriter.Close();
        //output.Close();
      }
      catch (Exception ex) { return ex.Message; }
      return "";
    }

    public static String appendData(String path, Byte[] data)
    {
      if (data == null)
        return "";

      try
      {
        using (FileStream output = new FileStream(path, FileMode.Append))
        {
          using (BinaryWriter binWriter = new BinaryWriter(output))
          {
            binWriter.Write(data.Length);

            //store byte counts
            binWriter.Write(data);

            //store data
          }
        }

        //binWriter.Close();
        //output.Close();
      }
      catch (Exception ex) { return ex.Message; }
      return "";
    }

    public static String readFilename(String path, long startPosition)
    {
      try
      {
        using (FileStream input = new FileStream(path, FileMode.Open))
        {
          using (BinaryReader binReader = new BinaryReader(input))
          {
            input.Position = startPosition;
            String filename = new String(binReader.ReadChars(FilenameLen));

            //binReader.Close(); input.Close();
            return filename;
          }
        }
      }

      catch (Exception ex) { return "EX:" + ex.Message; }
    }

    public static String readFilenameWithEnd(String path, long startPosition)
    {
      try
      {
        using (FileStream input = new FileStream(path, FileMode.Open))
        {
          using (BinaryReader binReader = new BinaryReader(input))
          {
            input.Position = startPosition;
            String filename = new String(binReader.ReadChars(FilenameLen));
            long end = input.Position;

            //binReader.Close(); input.Close();
            return filename + "|" + end.ToString();
          }
        }
      }
      catch (Exception ex) { return "EX:" + ex.Message; }
    }

    public static long readFilenameEnd(long startPosition)
    {
      return startPosition + FilenameLen;
    }

    public static Byte[] readData(String path)
    {
      try
      {
        using (FileStream input = new FileStream(path, FileMode.Open))
        {
          using (BinaryReader binReader = new BinaryReader(input))
          {
            Byte[] inputBytes = binReader.ReadBytes((int)input.Length);

            //max file size is around 16777216 kB
            binReader.Close(); input.Close();
            return inputBytes;
          }
        }
      }

      catch { return null; }
    }

    public static long readDataEnd(String path, long startPosition)
    {
      try
      {
        using (FileStream input = new FileStream(path, FileMode.Open))
        {
          using (BinaryReader binReader = new BinaryReader(input))
          {
            input.Position = startPosition;
            int data_length = binReader.ReadInt32();
            input.Position += data_length;

            long endposit = input.Position;
            binReader.Close(); input.Close();
            return endposit;
          }
        }

        //cannot access input after close, every return value has to first be stored in local var
      }

      catch
      {
        return long.MinValue; //mostly occur when reaching end
      }
    }

    public static byte[] readDataWithEnd(String path, long startPosition)
    {
      try
      {
        using (FileStream input = new FileStream(path, FileMode.Open))
        {
          using (BinaryReader binReader = new BinaryReader(input))
          {
            input.Position = startPosition;
            int data_length = binReader.ReadInt32();
            byte[] data = binReader.ReadBytes(data_length);
            long end = input.Position;

            byte[] longPositByteArr = longToByte(end);
            List<byte> tmpComb = new List<byte>(data);
            tmpComb.AddRange(longPositByteArr);

            //binReader.Close(); input.Close();
            return tmpComb.ToArray();
          }
        }
      }

      catch { return null; }
    }

    public static byte[] longToByte(long number)
    {
      return BitConverter.GetBytes(number);
    }

    public static long byteToLong(byte[] number)
    {
      return BitConverter.ToInt64(number, 0);
    }

    public static bool isFileInUse(string filename)
    {
      bool inUse = true;

      FileStream fs = null;
      try
      {
        fs = new FileStream(filename, FileMode.Open, FileAccess.Read,
        FileShare.None);
        inUse = false;
      }
      catch { }
      finally
      {
        if (fs != null)
          fs.Close();
      }
      return inUse;//true means in use
    }

    public static Exception ErrorReadingPackHeader = new Exception("Error reading package header.");

    public static Exception UnexpectedEndOfFile = new Exception("Unexpected end of file. Package is corrupt.");

    //public static Exception ErrorReadingPackHeader = new Exception("Error reading package header.");

    //public static Exception ErrorReadingPackHeader = new Exception("Error reading package header.");

    /// <summary>
    /// destDir, the dir to PUT !!files/folders!! of srcDir
    /// </summary>
    /// <param name="srcDir"></param>
    /// <param name="destDir"></param>
    internal static void CopyFolderTo(string srcDir, string destDir)
    {
      try
      {
        //检查是否存在目的目录
        if (!Directory.Exists(destDir))
        {
          Directory.CreateDirectory(destDir);
        }
        if (!Directory.Exists(Path.Combine(destDir, Path.GetFileName(srcDir))))
        {
          Directory.CreateDirectory(Path.Combine(destDir, Path.GetFileName(srcDir)));
        }

        //先来复制文件
        DirectoryInfo directoryInfo = new DirectoryInfo(srcDir);
        FileInfo[] files = directoryInfo.GetFiles();

        //复制所有文件
        foreach (FileInfo file in files)
        {
          string newDirToPutFile = Path.Combine(destDir, Path.GetFileName(srcDir));
          if (!Directory.Exists(newDirToPutFile))
          {
            Directory.CreateDirectory(newDirToPutFile);
          }
          FileIO.AsyncFileCopier.AsynFileCopy(file.FullName, Path.Combine(newDirToPutFile, file.Name), false);
        }

        //最后复制目录
        DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
        foreach (DirectoryInfo dir in directoryInfoArray)
        {
          CopyFolderTo(Path.Combine(srcDir, dir.Name), Path.Combine(destDir, Path.GetFileName(srcDir)));
        }
      }
      catch { }
    }
  }
}