using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public enum MaterialType:byte
    {
        [EnumDescription("Chọn loại vật liệu")]
        All = 0,
        [EnumDescription("Sàn gỗ")]
        Woodfloor = 1,
        [EnumDescription("Sàn nhựa")]
        Plasticfloor = 2,
        [EnumDescription("Xốp")]
        Styrofoam = 3,
        [EnumDescription("Nẹp")]
        Splint = 4,
        [EnumDescription("Ốp tường")]
        Walling = 5,
        [EnumDescription("Keo dán")]
        Glue = 6
    }
}
