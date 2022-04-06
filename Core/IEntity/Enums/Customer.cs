using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public enum SourceCustomer : byte
    {
        [EnumDescription("Chọn nguồn")]
        All = 0,
        [EnumDescription("Đơn hàng")]
        Order = 1,
        [EnumDescription("Admin nhập")]
        Admin = 2,
        [EnumDescription("Vòng quay")]
        RotationLuck = 3,
    }
    public enum TypeCustomer : byte
    {
        [EnumDescription("Chọn loại")]
        All = 0,
        [EnumDescription("Mới")]
        New = 1,
        [EnumDescription("Tiềm năng")]
        Invalid = 2,
        [EnumDescription("Danh sách đen")]
        BlackList = 3,
    }

}
