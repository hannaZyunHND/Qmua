using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{

    public enum StatusArticle : Byte
    {
        [EnumDescription("Trạng thái bài viết")]
        All = 0,
        [EnumDescription("Chưa xuất bản")]
        NotPublic = 1,
        [EnumDescription("Đã xuất bản")]
        Public = 2,
        [EnumDescription("Đã xóa")]
        Remove = 3,
    }
}
