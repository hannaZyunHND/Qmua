using Dapper;
using Microsoft.Extensions.Configuration;
using PhamGiaLandingPage.Web.ExecuteCommand;
using PhamGiaLandingPage.Web.Services.FlashSale.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.FlashSale.Repository
{
    public interface IFlashSaleRepository
    {
        List<FlashSaleViewModel> GetFlashSaleByTime(DateTime time);
        List<ProductInFlashSale> GetProductInFlashSale(int fSaleId, int locationId, string lanng_code, int pageIndex, int pageSize, out int total);
        void AutoUpdateFlashSale();
    }
    public class FlashSaleRepository : IFlashSaleRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;

        public FlashSaleRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;
        }

        public void AutoUpdateFlashSale()
        {
            var time = DateTime.Now;
            //usp_Web_AutoUpdateFlashSale
            var commandText = "usp_Web_AutoUpdateFlashSale";
            _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, null, commandType: System.Data.CommandType.StoredProcedure));
        }

        public List<FlashSaleViewModel> GetFlashSaleByTime(DateTime time)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetFlashSaleByTime";
            p.Add("@time", time);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<FlashSaleViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public List<ProductInFlashSale> GetProductInFlashSale(int fSaleId, int locationId, string lanng_code, int pageIndex, int pageSize, out int total)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductInFlashSale";
            p.Add("@fSaleId", fSaleId);
            p.Add("@locationId", locationId);
            p.Add("@lang_code", lanng_code);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductInFlashSale>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return result;
        }
    }
}
