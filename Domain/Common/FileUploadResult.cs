using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
   public class FileUploadResult
    {
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
    }
}
