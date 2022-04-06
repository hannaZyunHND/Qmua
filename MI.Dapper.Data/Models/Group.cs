using System.Collections.Generic;

namespace MI.Dapper.Data.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int? ParentId { get; set; }
        public List<Group> Children { get; set; }
    }
}