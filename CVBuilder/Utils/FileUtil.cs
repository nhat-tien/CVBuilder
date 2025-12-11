using CoreHtmlToImage;
using System;

namespace CVBuilder.Utils
{
    public class FileUtil
    {
        public static void SaveFileText(string text)
        {
            File.WriteAllText("D:\\assets\\data\\test.html", text);
        }
        public static string SaveImageToAssets(byte[] imageBytes)
        {
            return SaveImageToPathByBytes(imageBytes, "D:\\assets\\data");
        }
        public static string SaveImageToPathByBytes(byte[] imageBytes, string folderPath)
        {
            Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid() + ".jpg";
            string fullPath = Path.Combine(folderPath, fileName);

            File.WriteAllBytes(fullPath, imageBytes);

            return fullPath;
        }
    }
}
