using System;
using System.Collections.Generic;
using System.Text;
//using MI.Dapper.Data.Models;

namespace MI.Entity.CustomClass
{
    public class ArticleRecruitment
    {
        public string Position { get; set; }
        public string Address { get; set; }
        public string Count { get; set; }
        public string Salary { get; set; }
        
        public string ttkh { get; set; }
        public string ttdc { get; set; }
        public string ttlv { get; set; }
        public string ttdv { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AvatarArray { get; set; }
        public List<MoRong> phanMoRong { get; set; } = new List<MoRong>();
    }
    public class MoRong
    {
        public string name { get; set; } = "";
        public List<SubMoRong> sub { get; set; } = new List<SubMoRong>();
    }
    public class SubMoRong
    {
        public string title { get; set; } = "";
        public string avatar { get; set; } = "";
        public string description { get; set; } = "";
        public int sort { get; set; } = 0;
        public string url { get; set; } = "";
    }
}
