using System.IO;
using System.Web.Hosting;
using System.Web.ModelBinding;

namespace LYSApp.Domain
{
    public static class FileHelper
    {
        public static void SaveFile(byte[] content, string path, string filename)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }


            using (FileStream str = File.Create(path + "/" + filename + ".png"))
            {
                str.Write(content, 0, content.Length);
            }
        }
    }
}
