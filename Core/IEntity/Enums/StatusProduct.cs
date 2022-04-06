using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public enum StatusProduct : byte
    {
        [EnumDescription("Tất cả")]
        All = 0,
        [EnumDescription("Chưa xuất bản")]
        NotPublic = 2,
        [EnumDescription("Đã xuất bản")]
        Public = 1,
        [EnumDescription("Đã xóa")]
        Deleted = 3,
        [EnumDescription("Thuộc combo")]
        Combo = 4,
    }
    public enum ProductUnit : byte
    {
        [EnumDescription("mm")]
        All = 0,
        [EnumDescription("m2")]
        NotPublic = 1,
        [EnumDescription("m dài")]
        Public = 2,
    }
    public class TypeComboProduct
    {
        public static string combo = "com-bo";
        public static string same = "same";
    }

}
