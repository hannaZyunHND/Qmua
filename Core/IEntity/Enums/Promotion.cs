using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public class Promotion
    {
        public static string PromotionTypeGetByKey(string key)
        {
            if (Type.ContainsKey(key))
            {
                return Type[key];
            }
            return string.Empty;
        }
        public static Dictionary<string, string> Type = new Dictionary<string, string>()
        {
            {"discount","Giảm giá" },
            {"free-setup","Miễn phí cài đặt" },
            {"discount-percent","Giảm giá phần trăm" }
        };
    }

    public enum StatusPromotion
    {
        [EnumDescription("Tất cả")]
        All = 0,
        [EnumDescription("Xuất bản")]
        Public = 1,
        [EnumDescription("Ẩn")]
        Unpublic = 2,
        [EnumDescription("Xóa")]
        Deleted = 3,
    }
}
