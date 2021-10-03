using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShareFile.Utils
{
    public class FileUtil
    {
        public readonly static Hashtable IMG = new Hashtable()
        {
            { ".jpg", true },
            { ".jpeg", true },
            { ".png", true },
            { ".gif", true },
            { ".ico", true },
            { ".bmp", true },
            { ".tif", true },
            { ".webp", true }
        };

        public readonly static Hashtable VIDEO = new Hashtable()
        {
            { ".mp4", true },
            { ".mkv", true },
            { ".webm", true },
            { ".mov", true },
            { ".mpeg", true },
            { ".m4v", true },
            { ".avi", true },
            { ".wmv", true },
            { ".flv", true }
        };


        public readonly static Hashtable VOICE = new Hashtable()
        {
            { ".mp3", true },
            { ".wav", true },
            { ".flac", true },
            { ".ape", true },
            { ".aac", true }
        };


        public readonly static Hashtable TEXT = new Hashtable()
        {
            { ".am", true },
            { ".bat", true },
            { ".c", true },
            { ".cc", true },
            { ".cmake", true },
            { ".cpp", true },
            { ".cs", true },
            { ".css", true },
            { ".diff", true },
            { ".el", true },
            { ".h", true },
            { ".html", true },
            { ".htm", true },
            { ".java", true },
            { ".js", true },
            { ".json", true },
            { ".less", true },
            { ".make", true },
            { ".org", true },
            { ".php", true },
            { ".pl", true },
            { ".properties", true },
            { ".py", true },
            { ".rb", true },
            { ".scala", true },
            { ".script", true },
            { ".sh", true },
            { ".sql", true },
            { ".txt", true },
            { ".text", true },
            { ".tex", true },
            { ".vi", true },
            { ".vim", true },
            { ".xhtml", true },
            { ".xml", true },
            { ".log", true },
            { ".csv", true },
            { ".groovy", true },
            { ".rst", true },
            { ".patch", true },
            { ".go", true },
            { ".yml", true }
        };

        public readonly static string PDF = ".pdf";

        public static string GetFileSuffix(string filename)
        {
            int number = filename.LastIndexOf(".");
            if (number <= 0)
            {
                return "";
            }

            return filename[number..].ToLower();
        }

        public static int GetFileType(string filename)
        {
            string extension = GetFileSuffix(filename);
            if (IMG.Contains(extension))
            {
                return 2;
            }
            if (VIDEO.Contains(extension))
            {
                return 3;
            }
            if (VOICE.Contains(extension))
            {
                return 4;
            }
            if (TEXT.Contains(extension))
            {
                return 5;
            }
            if (extension == PDF)
            {
                return 6;
            }
            return 1;
        }


        public static bool IsUrl(string str)
        {
            try
            {
                string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
                return Regex.IsMatch(str, Url);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
