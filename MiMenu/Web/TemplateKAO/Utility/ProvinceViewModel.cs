using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateKAO.Utility
{
    public class ProvinceViewModel
    {
    }
    public class TinhThanh
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public string name_with_type { get; set; }
        public string code { get; set; }
    }
    public class QuanHuyen
    {
        public string name { get; set; }
        public string type { get; set; }
        public string slug { get; set; }
        public string name_with_type { get; set; }
        public string path { get; set; }
        public string path_with_type { get; set; }
        public string code { get; set; }
        public string parent_code { get; set; }
    }

    public class XaPhuong
    {
        public string name { get; set; }
        public string type { get; set; }
        public string slug { get; set; }
        public string name_with_type { get; set; }
        public string path { get; set; }
        public string path_with_type { get; set; }
        public string code { get; set; }
        public string parent_code { get; set; }
    }



}
