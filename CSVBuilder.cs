using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureAnalysis
{
    class CSVBuilder
    {
        public static void CreateCsvFile(List<FileMetaData> toomanyfiles, string outputFile)
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
