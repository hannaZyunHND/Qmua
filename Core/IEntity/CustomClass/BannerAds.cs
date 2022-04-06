using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class BannerAds
    {
        public BannerAds()
        {
            this.Code = string.Empty;
            this.Id = string.Empty;
            this.LanguageCode = "vi-VN";
            this.MetaData = JsonConvert.SerializeObject(new DetailBanerAds());
            this.Type = 1;
        }

        public BannerAds(string id)
        {
       
            this.Code = string.Empty;
            this.Id = string.Empty;
            this.LanguageCode = "vi-VN";
            this.MetaData = JsonConvert.SerializeObject(new DetailBanerAds());
            this.Type = 1;
        }


        public string CheckJson()
        {
            try
            {
                if (this.Type == 1)
                {
                    return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<DetailBanerAds>(this.MetaData));
                }
                else
                {
                    return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<DetailBanerAds>>(this.MetaData));
                }
            }
            catch
            {
                if (this.Type == 1)
                {
                    return JsonConvert.SerializeObject(new DetailBanerAds());
                }
                else
                {
                    return JsonConvert.SerializeObject(new List<DetailBanerAds>());
                }
            }
        }
    }

    public class DetailBanerAds
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        public bool Show { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        //1 là banner , 2 side
        public DetailBanerAds()
        {
            this.Title = string.Empty;
            this.Url = string.Empty;
            this.Description = string.Empty;
            this.Image = string.Empty;
            this.Order = 0;
            this.Show = true;
        }
    }
}
