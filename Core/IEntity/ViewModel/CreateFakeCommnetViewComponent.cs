using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.ViewModel
{
    public class CreateFakeCommnetViewComponent
    {
        public List<ObjectComment> objs { get; set; }
        public List<CommentDescription> comments { get; set; }
    }



    public class ObjectComment
    {
        public int productId { get; set; }
        public string type { get; set; }
    }
    public class CommentDescription
    {
        //Type: "",
        //            Name: "",
        //            Description: "",
        //            Rating:0
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; } = 0;
    }
}
