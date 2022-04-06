using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public enum GroupProp : byte
    {
        [EnumDescription("Sản phẩm")]
        Product = 1,
        [EnumDescription("Bài viết")]
        Article = 2,
    }
    public enum PositionProp : byte
    {
        [EnumDescription("Phía trên")]
        top = 1,
        [EnumDescription("Phía dưới")]
        bottom = 2,
        [EnumDescription("Căn giữa")]
        middle = 3,
    }
}
