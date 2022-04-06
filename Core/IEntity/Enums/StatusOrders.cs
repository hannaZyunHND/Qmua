using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public class StatusOrders
    {
        public static string GetStatusName(string statusCode)
        {
            if (ListStatusOrders.ContainsKey(statusCode))
            {
                return ListStatusOrders[statusCode];
            }
            return "Mới";
        }
        public static Dictionary<string, string> ListStatusOrders = new Dictionary<string, string>()
        {
            {"TAO_MOI","Mới" },
            {"DANG_XAC_NHAN","Đang xác nhận" },
            {"DA_XAC_NHAN","Đã xác nhận" },
            {"THANH_CONG","Thành công" },
            {"KHACH_HUY","Khách hủy" },
        };
    }
}
