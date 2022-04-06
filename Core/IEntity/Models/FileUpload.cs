using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class FileUpload
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public byte Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileDownloadPath { get; set; }
        public string FilePath { get; set; }
        public string FileExt { get; set; }
        public double? FileSize { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public byte? Status { get; set; }
        public string Dimensions { get; set; }
        public int? DimSum { get; set; }
    }
}
