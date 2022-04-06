using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public class RegionPage
    {

        public static Dictionary<string, string> RegionPages
        {
            get
            {
                var Regions = new Dictionary<string, string>();
                Regions.Add("", "Chọn vùng");
                Regions.Add("flash-sale", "Flash Sale");
                Regions.Add("san-go-noi-bat", "Sàn gỗ nổi bật");
                Regions.Add("san-go-giam-gia", "Sàn gỗ giảm giá");
                Regions.Add("san-go-moi", "Sàn gõ mới về");
                return Regions;
            }
        }

    }
}
