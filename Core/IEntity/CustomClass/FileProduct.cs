using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.CustomClass
{
    public class FileProduct
    {
        public string File360 { get; set; }
        public string FileDowload { get; set; }
        public string FileVideo { get; set; }
        public FileProduct()
        {
            this.FileDowload = string.Empty;
            this.File360 = string.Empty;
            this.FileVideo = string.Empty;
        }
    }
}
