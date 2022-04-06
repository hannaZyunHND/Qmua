using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{

    public enum TypeContact : byte
    {
        [EnumDescription("Tất cả")]
        All = 0,
        [EnumDescription("Liên hệ")]
        Contact = 3,
        [EnumDescription("Xem siêu thị có hàng")]
        Looking = 2,
        [EnumDescription("Bảo trì bảo hành")]
        Guarantee = 1,

    }
    public enum StatusContact : byte
    {
        [EnumDescription("Tất cả")]
        All = 0,
        [EnumDescription("Mới")]
        New = 1,
        [EnumDescription("Đã xử lý")]
        Public = 2,
        [EnumDescription("Loại")]
        Unpublic = 3,
    }
}
