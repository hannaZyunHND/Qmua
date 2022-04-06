using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TemplateKAO.ExecuteCommand;
using TemplateKAO.Services.Locations.ViewModal;
using TemplateKAO.Services.LuckySpin.ViewModal;
using Utils;

namespace TemplateKAO.Services.LuckySpin.Repository
{
    public interface ILuckySpinRepository
    {
        List<CustomLuckyModel> CustomLucky_GetTop(int pageSize);
        bool CustomLucky_CheckExist(string phone);
        bool CustomLucky_Add(string phone, string luckySpinName);
        List<LuckySpinModel> LuckySpin_GetAll();

    }
    public class LuckySpinRepository : ILuckySpinRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;

        public LuckySpinRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;
        }


        public List<CustomLuckyModel> CustomLucky_GetTop(int pageSize)
        {
            var p = new DynamicParameters();
            var commandText = "CustomLucky_GetTop";
            p.Add("@PageSize", pageSize);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<CustomLuckyModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result.ToList();
        }
        public bool CustomLucky_CheckExist(string phone)
        {
            var p = new DynamicParameters();
            var commandText = "CustomLucky_CheckExist";
            p.Add("@PhoneNumber", phone);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<int>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result.Any();
        }
        public bool CustomLucky_Add(string phone, string luckySpinName)
        {
            var p = new DynamicParameters();
            var commandText = "CustomLucky_Add";
            p.Add("@PhoneNumber", phone);
            p.Add("@LuckySpinName", luckySpinName);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result > 0;
        }

        public List<LuckySpinModel> LuckySpin_GetAll()
        {
            var p = new DynamicParameters();
            var commandText = "LuckySpin_GetAll";
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<LuckySpinModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
    }
}
