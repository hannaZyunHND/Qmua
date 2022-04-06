using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public enum TypeArticle : byte
    {
        [EnumDescription("Loại bài viết")]
        All = 0,
        [EnumDescription("Sản phẩm")]
        Product = 1,
        [EnumDescription("Bài viết Nội dung")]
        Blog = 3,
        [EnumDescription("Bài viết Video")]
        Video = 4,
        [EnumDescription("Bài viết hình ảnh")]
        Image = 5,
        [EnumDescription("Bài viết File Download")]
        Download = 6,
        [EnumDescription("Bài viết tuyển dụng")]
        Recument = 7,
        [EnumDescription("Bài viết bất động sản")]
        Footer = 8,
        [EnumDescription("Bài viết dịch vụ")]
        DichVu = 9,
        [EnumDescription("Bài viết giới thiệu")]
        GioiThieu = 10

    }
}
