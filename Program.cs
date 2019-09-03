using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SignatureAnalysis
{

    class Program
    {
        static List<string> alldirectories = new List<string>();
        static List<string> allfiles = new List<string>();
        static string outputFile;
        public static void Main(string[] args)
        {
            //No flag given
            if (args.Length == 2)
            {
                string directory = args[0];
                outputFile = args[1];
                if (Directory.Exists(directory) == false)
                {
                    Console.WriteLine("Please provide a valid directory!!!");
                }
                //Retrieve all files from Main Directory
                string[] allmyFiles = Directory.GetFiles(directory);
                foreach (string aF in allmyFiles)
                {
                    allfiles.Add(aF);
                }
                //Create CSV File
                CreateCsvFile(allfiles);
            }
            //Flag given
            else if (args.Length == 3)
            {
                string directory = args[0];
                outputFile = args[1];
                string flag = args[2];
                if (Directory.Exists(directory) == false)
                {
                    Console.WriteLine("Please provide a valid directory!");
                }
                if (flag != "-f")
                {
                    Console.WriteLine("Please provide a valid flag!");
                }
                //Retrieve all files from all directories
                Program.AddSubdirectoriesToAllDirectoriesList(directory);
                //Create CSV File
                CreateCsvFile(allfiles);
            }
            //General Input Directions
            else
            {
                Console.WriteLine("You have a mistake in your input." +
                "\nPlease provide a valid directory, your output file's path and name " +
                "\nand a flag -f if subdirectories are supposed to be included.");
                Console.WriteLine("Example: SignatureAnalysis.exe c:\\test_files c:\\result\\output.csv -f");
            }
            Console.ReadLine();
        }

        public static void AddSubdirectoriesToAllDirectoriesList(string directory)
        {
            string[] af = Directory.GetFiles(directory);
            foreach (string allmyfiles in af)
            {
                allfiles.Add(allmyfiles);
            }
            string[] allsubdirectories = Directory.GetDirectories(directory);
            foreach (string subdirectory in allsubdirectories)
            {
                alldirectories.Add(subdirectory);
                AddSubdirectoriesToAllDirectoriesList(subdirectory);
            }
        }

        public static void CreateCsvFile(List<string> toomanyfiles)
        {
            int jcount = 0;
            int pcount = 0;
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                sw.Write("FILE PATH, FILE TYPE, MD5 HASH" + Environment.NewLine);
                foreach (string file in toomanyfiles)
                {
                    FileMetaData myData = new FileMetaData();
                    myData.FilePath = file;
                    byte[] doc = File.ReadAllBytes(file);
                    if (doc.Length > 2)
                    {
                        byte doc1 = doc[0];
                        byte doc2 = doc[1];
                        byte doc3 = doc[2];
                        byte doc4 = doc[3];

                        //JPG file
                        if (Convert.ToByte("FF", 16) == doc1 && Convert.ToByte("D8", 16) == doc2)
                        {
                            myData.FileType = "JPG";
                            myData.MD5Hash = ExtractMD5Hash(doc);
                            jcount++;
                        }
                        //PDF file
                        else if (Convert.ToByte("25", 16) == doc1 && Convert.ToByte("50", 16) == doc2 &&
                                 Convert.ToByte("44", 16) == doc3 && Convert.ToByte("46", 16) == doc4)
                        {
                            myData.FileType = "PDF";
                            myData.MD5Hash = ExtractMD5Hash(doc);
                            pcount++;
                        }

                        if (!string.IsNullOrEmpty(myData.FileType))
                        {
                            sw.Write(myData.FilePath + ", ");
                            sw.Write(myData.FileType + ", ");
                            sw.Write(myData.MD5Hash);
                            sw.WriteLine();
                        }

                    }
                }
            }
            Console.WriteLine("Thank you for using my application!" +
                "\nThere are {0} JPEGs and {1} PDFs in your folder(s).", jcount, pcount);
        }

        public static string ExtractMD5Hash(byte[] fcontent)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(fcontent);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}



