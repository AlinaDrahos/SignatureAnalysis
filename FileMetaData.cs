using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureAnalysis
{
    public class FileMetaData
    {
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string MD5Hash { get; set; }

    }
}
