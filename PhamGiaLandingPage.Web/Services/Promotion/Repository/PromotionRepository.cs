using Dapper;
using Microsoft.Extensions.Configuration;
using PhamGiaLandingPage.Web.ExecuteCommand;
using PhamGiaLandingPage.Web.Services.Promotion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Promotion.Repository
{
    public interface IPromotionRepository
    {
        List<PromotionViewModel> GetAllPromotions(string lang_code);
    }
    public class PromotionRepository : IPromotionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;

        public PromotionRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;
        }

        public List<PromotionViewModel> GetAllPromotions(string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetAllPromotions";

            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<PromotionViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
    }
}
