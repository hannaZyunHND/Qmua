using MI.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utils
{
    public class FilterQuery
    {
        public string keyword { get; set; }
        public string sortDir { get; set; }
        public string sortBy { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string languageCode { get; set; }
        public FilterQuery()
        {
            this.keyword = string.Empty;
            this.sortDir = string.Empty;
            this.sortBy = string.Empty;
            this.pageIndex = 0;
            this.pageSize = 20;
            this.languageCode = Utility.DefaultLang;
        }
    }
    public class FilterPromotion : FilterQuery
    {
        public StatusPromotion status { get; set; }
        public FilterPromotion()
        {
            status = StatusPromotion.All;
        }
    }

    public class FilterCouponChild : FilterQuery
    {
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
        public int Parrent { get; set; }
        public int Status { get; set; }
        public FilterCouponChild()
        {
            this.StartDateFrom = new DateTime(2000, 01, 01);
            this.StartDateTo = new DateTime(3000, 01, 01);
            this.EndDateFrom = new DateTime(2000, 01, 01);
            this.EndDateTo = new DateTime(3000, 01, 01);
            this.Parrent = 0;
            this.Status = 0;
        }
    }
    public class FilterComment : FilterQuery
    {
        public MI.Entity.Enums.CommentType type { get; set; }
        public MI.Entity.Enums.StatusComment status { get; set; }
        public FilterComment()
        {
            type = MI.Entity.Enums.CommentType.All;
            status = MI.Entity.Enums.StatusComment.New;
        }
    }
    public class FilterContact : FilterQuery
    {
        public MI.Entity.Enums.TypeContact type { get; set; }
        public MI.Entity.Enums.StatusContact status { get; set; }
        public FilterContact()
        {
            type = MI.Entity.Enums.TypeContact.All;
            status = MI.Entity.Enums.StatusContact.New;
        }
    }
    public class FilterCustomer : FilterQuery
    {
        public MI.Entity.Enums.TypeCustomer type { get; set; }
        public MI.Entity.Enums.SourceCustomer source { get; set; }
        public FilterCustomer()
        {
            type = MI.Entity.Enums.TypeCustomer.All;
            source = MI.Entity.Enums.SourceCustomer.All;
        }
    }

    public class FilterDepartment : FilterQuery
    {
        public int locationId { get; set; }
        public FilterDepartment()
        {
            this.locationId = 0;
            this.languageCode = string.Empty;
        }
    }
    public class FilterOrders : FilterQuery
    {
        public string orderStatus { get; set; }
        public MI.Entity.Enums.OrderType orderType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public FilterOrders()
        {
            this.orderStatus = string.Empty;
            this.startDate = DateTime.MinValue;
            this.endDate = DateTime.MaxValue;
            this.orderType = OrderType.All;
        }
    }
    public class FilterProduct : FilterQuery
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public MI.Entity.Enums.StatusProduct trangThai { get; set; }
        public string voucher { get; set; }
        public int idZone { get; set; }
        public List<int> idZones { get; set; }
        public bool isInstallment { get; set; }
        public int idPromotion { get; set; }
        public FilterProduct()
        {
            this.from = DateTime.MinValue;
            this.to = DateTime.MaxValue;
            this.trangThai = MI.Entity.Enums.StatusProduct.All;
            this.voucher = string.Empty;
            this.idPromotion = 0;
            this.idZone = 0;
            this.idZones = new List<int>();
            this.isInstallment = false;
        }
    }
    public class FilterProductWithMonth : FilterProduct
    {
        public int month { get; set; }
        public int year { get; set; }
    }
    public class FilterZone : FilterQuery
    {
        public MI.Entity.Enums.TypeZone type { get; set; }
        public FilterZone()
        {
            this.languageCode = string.Empty;
            this.type = 0;
        }
    }
    public class FilterArticle : FilterQuery
    {
        public MI.Entity.Enums.TypeArticle Type { get; set; }
        public MI.Entity.Enums.StatusArticle Status { get; set; }
        public int ZoneId { get; set; }
        public List<int> ZoneIds { get; set; }
        public FilterArticle()
        {
            this.Type = TypeArticle.All;
            this.Status = StatusArticle.All;
            this.ZoneId = 0;
            this.ZoneIds = new List<int>();
        }
    }

    public class FilterArticleWithMonth : FilterArticle
    {
        //public int month { get; set; }
        public int year { get; set; }
    }
}
