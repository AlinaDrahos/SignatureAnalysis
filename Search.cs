using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace SignatureAnalysis
{
    class Search
    {
        public static List<FileMetaData> RetrieveAllFilesinDirectory(string directory)
        {
            List<FileMetaData> allFiles = new List<FileMetaData>();
            string[] allmyFiles = Directory.GetFiles(directory);
            foreach (string aF in allmyFiles)
            {
                FileMetaData myfile = new FileMetaData();
                myfile.FilePath = aF;
                myfile.FileType = CalculateFileType(aF);
                myfile.MD5Hash = ExtractMD5Hash(aF);
                if (!string.IsNullOrEmpty(myfile.FileType))
                {
                    allFiles.Add(myfile);
                }
            }
            return allFiles;
        }

        public static List<FileMetaData> RetrieveAllFilesinDirectoryandSubdirectories(string directory)
        {
            List<FileMetaData> allFiles =RetrieveAllFilesinDirectory(directory);
            string[] allsubdirectories = Directory.GetDirectories(directory);
            foreach (string subdirectory in allsubdirectories)
            {
                List<FileMetaData> myfiles =RetrieveAllFilesinDirectoryandSubdirectories(subdirectory);
                foreach (FileMetaData file in myfiles)
                {
                    allFiles.Add(file);
                }
            }
            return allFiles;
        }


        private static string CalculateFileType(string path)
        {
            byte[] doc = File.ReadAllBytes(path);
            if (doc.Length > 2)
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
