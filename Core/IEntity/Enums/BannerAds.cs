using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public class BannerAds
    {
        public enum BannerAdsType : byte
        {
            [EnumDescription("Chọn khu vực xuất hiện")]
            All = 0,
            [EnumDescription("Slide trang chủ")]
            SlideHome = 1,
            [EnumDescription("Bài viết")]
            Article = 2,
        }
    }
}
