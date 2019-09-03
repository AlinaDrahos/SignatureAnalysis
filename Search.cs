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
                FileMetaData myfile = new FileMetaData(aF);
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
    }
}
