using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Extra.ViewModel
{
    public class PropertyDetail
    {
        public int Id { get; set; }
        public string Thumb { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string LanguageCode { get; set; }


    }

    public class CommentDetail
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByAdminId { get; set; }
        public string Content { get; set; }
        public int ObjectId { get; set; }
        public int ObjectType { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string PhoneOrMail { get; set; }
        public string Avatar { get; set; }
        public string Type { get; set; }
        public int Rate { get; set; }
    }

    public class RatingAVG
    {
        public int ObjectId { get; set; }
        public int Type { get; set; }
        public decimal RateAVG { get; set; }
    }


    public class ManufactureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Avatar { get; set; }
    }


    public class ColorViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Show { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LanguageCode { get; set; }
    }

    public class ServiceTicket
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public string Source { get; set; }
        public DateTime BookingTime { get; set; } = DateTime.Now;
    }

    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SiteMapViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Create { get; set; }
        public DateTime Modified { get; set; }
        public string Type { get; set; }
    }
}
