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
                CSVBuilder.CreateCsvFile(myfiles, outputFile);
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
                CSVBuilder.CreateCsvFile(allfiles, outputFile);
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
    }
}





