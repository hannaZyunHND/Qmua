using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Service
{
    partial class ServiceEnterBuy : ServiceBase
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer timer2 = new System.Timers.Timer();        
        public ServiceEnterBuy()
        {
            InitializeComponent();
            //GoogleSheetService.Init();
            //string query = "select x.Ma, x.Name, y.Name Groups, (select COUNT(*) from OrderDetail x inner join Orders y on x.OrderId = y.Id where y.Status = 'THANH_CONG' and x.voucher = Ma) acive, (select isnull(SUM(x.LogPrice),0) from OrderDetail x inner join Orders y on x.OrderId = y.Id where y.Status = 'THANH_CONG' and x.voucher = Ma) revenue, x.CreateDate, x.ExprireDate from CouponChild x inner join Coupon y on x.Parrent = y.Id";
            //DataTable dtResult = Database.GetData(query);
            //GoogleSheetService.AddListRow_v2(dtResult);
        }        
        protected override void OnStart(string[] args)
        {
            log.Info("START SERVICE");
            GoogleSheetService.Init();
            //Viết funtion khởi tạo gì đó ở đây            
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.AutoReset = false;
            //timer.Start();
            //
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer2);
            timer2.AutoReset = false;
            timer2.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            log.Info("START TIMMER");
            int wait = 500;
            int rownum = 10;
            int maxid = 0;
            string queryCustomer = string.Empty;
            string queryOrder = string.Empty;
            string queryContact = string.Empty;
            string queryOrderVoucher = string.Empty;
            try
            {
                wait = int.Parse(Utility.XMLConfig_getValue("TimeWait"));
                rownum = int.Parse(Utility.XMLConfig_getValue("RowNum"));
                //maxid = int.Parse(Utility.XMLConfig_getContent("MaxId"));
                using (StreamReader sr = new StreamReader(System.Windows.Forms.Application.StartupPath + "//MaxId.txt"))
                {
                    string line;

                    // doc va hien thi cac dong trong file cho toi
                    // khi tien toi cuoi file. 
                    while ((line = sr.ReadLine()) != null)
                    {
                        maxid = int.Parse(line);
                    }
                }
                //
                //queryCustomer = "select TOP " + rownum + " Id, CONVERT(varchar, CreatedDate, 105) + ' ' + SUBSTRING(CONVERT(varchar, CreatedDate, 100), 13, 8) DateAndTime, Name, (CASE WHEN Gender = 'male' THEN N'Nam' WHEN Gender = 'female' THEN N'Admin nhập' END) as Gender, PhoneNumber, Email, Address, (CASE WHEN Source = 1 THEN N'Đơn hàng' WHEN Source = 2 THEN N'Admin nhập' WHEN Source = 3 THEN N'Vòng quay' END) as Source, (CASE WHEN Type = 1 THEN N'Mới' WHEN Type = 2 THEN N'Tiềm năng' WHEN Type = 3 THEN N'Danh sách đen' END) as Type, Note from customer where id > " + maxid;
                //queryContact = "select TOP " + rownum + " Id, (CASE WHEN Status = '0' THEN N'Tất cả' WHEN Status = '1' THEN N'<Mới' WHEN Status = '2' THEN N'Đã xử lý' WHEN Status = '3' THEN N'Loại' END) as Status, CONVERT(varchar, CreatedDate, 105) + ' ' + SUBSTRING(CONVERT(varchar, CreatedDate, 100), 13, 8) DateAndTime, Name, (CASE WHEN Gender = 'male' THEN N'Nam' WHEN Gender = 'female' THEN N'Admin nhập' END) as Gender, Phone, Email, Address, Source, (CASE WHEN Type = 1 THEN N'Bảo trì bảo hành' WHEN Type = 2 THEN N'Xem siêu thị có hàng' WHEN Type = 3 THEN N'Liên hệ' END) as Type, Note,Content from contact where id > " + maxid;
                //queryOrder = "select TOP " + rownum + " * from ( select ROW_NUMBER() OVER(ORDER BY a.id ASC) AS STT, a.id, a.OrderCode, a.Status, CASE WHEN a.OrderSourceType = 1 THEN N'Giỏ hàng' WHEN a.OrderSourceType = 2 THEN N'Dự toán' WHEN a.OrderSourceType = 3 THEN N'Flash sale' WHEN a.OrderSourceType = 4 THEN N'Trả góp' END AS OrderSourceType, CONVERT(varchar, a.CreatedDate,105) as CreatedDate, b.Name, b.PhoneNumber, b.Email, c.ProductId, (select Name from Product where id = c.ProductId) ProductName, c.LogPrice, c.Quantity, (select STUFF( (select ', ' +LogName from OrderPromotionDetail where OrderDetailId = a.id  for xml path ('')), 1,2,'')) as promotion ,c.Voucher,a.Note from Orders a left join Customer b on a.CustomerId = b.Id left join OrderDetail c on a.id = c.OrderId) as tbl where id > " + maxid ;
                queryOrderVoucher = "select TOP " + rownum + " * from ( select a.id, a.OrderCode, a.Status, CASE WHEN a.OrderSourceType = 1 THEN N'Giỏ hàng' WHEN a.OrderSourceType = 2 THEN N'Dự toán' WHEN a.OrderSourceType = 3 THEN N'Flash sale' WHEN a.OrderSourceType = 4 THEN N'Trả góp' END AS OrderSourceType, CONVERT(varchar, a.CreatedDate,105) as CreatedDate, b.Name, b.PhoneNumber, b.Email, c.ProductId, (select Name from Product where id = c.ProductId) ProductName, c.LogPrice, c.Quantity, (select STUFF( (select ', ' +LogName from OrderPromotionDetail where OrderDetailId = a.id  for xml path ('')), 1,2,'')) as promotion ,c.Voucher,a.Note from Orders a left join Customer b on a.CustomerId = b.Id left join OrderDetail c on a.id = c.OrderId where c.Voucher is not null and c.Voucher != '' and a.id > " + maxid + ") as tbl";
                log.Info(queryOrderVoucher);
                //
                DataTable dtResult = Database.GetData(queryOrderVoucher);
                if (dtResult.Rows.Count > 0)
                {
                    string getMaxId = dtResult.Rows[dtResult.Rows.Count - 1]["ID"].ToString();
                    if (!string.IsNullOrEmpty(getMaxId))
                    {
                        //Utility.XMLConfig_setValue("MaxId", getMaxId);
                        using (StreamWriter sr = new StreamWriter(System.Windows.Forms.Application.StartupPath + "//MaxId.txt"))
                        {
                            sr.WriteLine(getMaxId);
                        }
                        GoogleSheetService.AddListRow(dtResult);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                timer.Interval = wait;
            }
        }
        private void OnTimer2(object sender, ElapsedEventArgs e)
        {
            log.Info("START TIMMER");
            int wait = 500;
            int rownum = 10;
            int maxid = 0;
            int totalrow = 0;
            string query = string.Empty;
            try
            {
                wait = int.Parse(Utility.XMLConfig_getValue("TimeWait"));
                query += "select x.Ma, x.Name, y.Name Groups, (select COUNT(*) from OrderDetail x inner join Orders y on x.OrderId = y.Id where y.Status = 'THANH_CONG' and x.voucher = Ma) acive, (select isnull(SUM(x.LogPrice),0) from OrderDetail x inner join Orders y on x.OrderId = y.Id where y.Status = 'THANH_CONG' and x.voucher = Ma) revenue, x.CreateDate, x.ExprireDate from CouponChild x inner join Coupon y on x.Parrent = y.Id";
                log.Info(query);
                //
                using (StreamReader sr = new StreamReader(System.Windows.Forms.Application.StartupPath + "//MaxId.txt"))
                {
                    string line;

                    // doc va hien thi cac dong trong file cho toi
                    // khi tien toi cuoi file. 
                    while ((line = sr.ReadLine()) != null)
                    {
                        log.Info(totalrow);
                        totalrow = int.Parse(line);
                        GoogleSheetService.ClearAllData(totalrow);
                    }
                }
                log.Info("xóa xong");
                DataTable dtResult = Database.GetData(query);
                if (dtResult.Rows.Count > 0)
                {
                    string count = dtResult.Rows.Count.ToString();
                    if (!string.IsNullOrEmpty(count))
                    {
                        //Utility.XMLConfig_setValue("MaxId", getMaxId);
                        using (StreamWriter sr = new StreamWriter(System.Windows.Forms.Application.StartupPath + "//MaxId.txt"))
                        {
                            sr.WriteLine(count);
                        }
                        GoogleSheetService.AddListRow_v2(dtResult);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                timer2.Interval = wait;
            }
        }



        protected override void OnStop()
        {
            log.Info("STOP SERVICE");
        }
    }
}
