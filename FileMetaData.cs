using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignatureAnalysis
{
    public class FileMetaData
    {
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string MD5Hash { get; set; }

        public FileMetaData(string filePath)
        {
            FilePath = filePath;
            FileType = CalculateFileType(filePath);
            MD5Hash = ExtractMD5Hash(filePath);
        }
        private string CalculateFileType(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                byte[] doc = new byte[4];
                int numberofBytes = stream.Read(doc, 0, 4);
                if (numberofBytes > 2)
                {
                    byte doc1 = doc[0];
                    byte doc2 = doc[1];
                    byte doc3 = doc[2];
                    byte doc4 = doc[3];
                    //JPG file
                    if (Convert.ToByte("FF", 16) == doc1 && Convert.ToByte("D8", 16) == doc2)
                    {
                        return "JPG";
                    }
                    //PDF file
                    else if (Convert.ToByte("25", 16) == doc1 && Convert.ToByte("50", 16) == doc2 &&
                             Convert.ToByte("44", 16) == doc3 && Convert.ToByte("46", 16) == doc4)
                    {
                        return "PDF";
                    }
                }

            }
            return null;
        }




        public static string ExtractMD5Hash(string file)
        {
            byte[] doc = File.ReadAllBytes(file);
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(doc);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

}

