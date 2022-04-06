using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public enum OrderType : byte
    {

        [EnumDescription("Tất cả")]
        All = 0,
        [EnumDescription("Giỏ hàng")]
        Cart = 1,
        [EnumDescription("Dự toán")]
        Estimartes = 2,
        [EnumDescription("Flash sale")]
        FlashSale = 3,
        [EnumDescription("Trả góp")]
        Installment = 4
    }
}
