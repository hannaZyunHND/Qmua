using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class BankInstallment
    {
        public BankInstallment()
        {

        }
        public string CheckJson()
        {
            try
            {

                return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<BankInstallmentDetail>(this.InfoCard));
            }
            catch
            {
                return JsonConvert.SerializeObject(new BankInstallmentDetail());
            }
        }
    }
    public class BankInstallmentDetail
    {
        public BankInstallmentDetail()
        {
            this.InfoCard = new List<InfoCardData>();
        }
        public List<InfoCardData> InfoCard { get; set; }
    }
    public class InfoCardData
    {
        public InfoCardData()
        {
            this.MonthNumber = string.Empty;
            this.InterestRate = string.Empty;
        }
        public string MonthNumber { get; set; }
        public string InterestRate { get; set; }
    }
}
