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
                List<FileMetaData> myfiles = Search.RetrieveAllFilesinDirectory(directory);
                //Create CSV File
                CreateCsvFile(myfiles);
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
                //Program.AddSubdirectoriesToAllDirectoriesList(directory);
                List<FileMetaData> allfiles = Search.RetrieveAllFilesinDirectoryandSubdirectories(directory);
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



        public static void CreateCsvFile(List<FileMetaData> toomanyfiles)
        {
            int jcount = 0;
            int pcount = 0;
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                sw.Write("FILE PATH, FILE TYPE, MD5 HASH" + Environment.NewLine);
                foreach (FileMetaData file in toomanyfiles)
                {
                    if (file.FileType == "JPG")
                    {
                        jcount++;
                    }

                    else
                    {
                        pcount++;
                    }
                    sw.Write(file.FilePath + ", ");
                    sw.Write(file.FileType + ", ");
                    sw.Write(file.MD5Hash);
                    sw.WriteLine();
                }
            }

            Console.WriteLine("Thank you for using my application!" +
                "\nThere are {0} JPEGs and {1} PDFs in your folder(s).", jcount, pcount);
        }
    }
}





