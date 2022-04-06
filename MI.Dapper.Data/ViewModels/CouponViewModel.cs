using System;
using System.Collections.Generic;
using MI.Dapper.Data.Models;

namespace MI.Dapper.Data.ViewModels
{
    public class CouponViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DiscountOption { get; set; }
        public int NumberOfUsed { get; set; }
        public int QuantityDiscount { get; set; }
        public bool Locked { get; set; }
        public string ValueDiscount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsCategory { get; set; }
        public List<int> Category { get; set; }
        public List<int> ListProduct { get; set; }

        public CouponViewModel()
        {
            this.Id = 0;
            this.Name = string.Empty;
            this.Code = string.Empty;
            this.DiscountOption = 0;
            this.NumberOfUsed = 0;
            this.QuantityDiscount = 0;
            this.Locked = false;
            this.ValueDiscount = string.Empty;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.IsCategory = false;
            this.Category =new List<int>();
            this.ListProduct = new List<int>();
        }
    }
}