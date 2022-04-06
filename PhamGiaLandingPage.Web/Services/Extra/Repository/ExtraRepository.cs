using Dapper;
using Mi.MemoryCache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PhamGiaLandingPage.Web.ExecuteCommand;
using PhamGiaLandingPage.Web.Services.Extra.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Extra.Repository
{
    public interface IExtraRepository
    {
        List<PropertyDetail> GetPropertyDetails(string lang_code);
        List<CommentDetail> GetCommentPublisedByObjectId(int id, int type, int pageIndex = 1, int pageSize = 10);
        List<ManufactureViewModel> GetManufactures(string lang_code);
        List<ColorViewModel> GetColors(string lang_code);
        TagViewModel GetTagTarget(string tag);

        int CreateRating(int objectId, int objectType, int rate);
        int CalculateDepartment();
        decimal GetRatingByObjectId(int objectId, int objectType);
        int CreateComment(int objectId, int objectType, string name, string phoneOrEmail, string avatar, string content, string type, int rate, string lang_code, int parentId = 0);
        int CreateContact(ServiceTicket ticket);
        int CreateContactwBookingTime(ServiceTicket ticket);
        int CreateViewCount(int objectId, string type);
        List<SiteMapViewModel> GetSiteMapUrl();
        List<ManufactureViewModel> GetManufacturesByZoneId(string lang_code, int zone_id);
        void AutoBackupDatabase();
        void TestJob();
    }
    public class ExtraRepository : IExtraRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        private ICacheController _memory;

        public ExtraRepository(IConfiguration configuration, IExecuters executers, ICacheController memory)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;
            _memory = memory;
        }

        public List<PropertyDetail> GetPropertyDetails(string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetPropertiesByLanguage";
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<PropertyDetail>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
        //usp_Web_CreateRating
        public int CreateRating(int objectId, int objectType, int rate)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_CreateRating";
            p.Add("@objectId", objectId);
            p.Add("@objectType", objectType);
            p.Add("@rate", rate);
            p.Add("@insertedId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            var insertedId = p.Get<int>("@insertedId");
            return insertedId;
        }

        public int CreateComment(int objectId, int objectType, string name, string phoneOrEmail, string avatar, string content, string type, int rate, string lang_code, int parentId = 0)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_CreateCommentInWebsite";
            p.Add("@objectId", objectId);
            p.Add("@objectType", objectType);
            p.Add("@name", name);
            p.Add("@phoneOrMail", phoneOrEmail);
            p.Add("@avatar", avatar);
            p.Add("@content", content);
            p.Add("@type", type);
            p.Add("@rate", rate);
            p.Add("@parentId", parentId);
            p.Add("@lang_code", lang_code);
            p.Add("@insertedId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            var insertedId = p.Get<int>("@insertedId");
            return insertedId;
        }
        public int CreateContact(ServiceTicket ticket)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_CreateContact";
            p.Add("@Name", ticket.Name);
            p.Add("@Phone", ticket.Phone);
            p.Add("@Address", ticket.Address);
            p.Add("@Title", ticket.Title);
            p.Add("@Content", ticket.Content);
            p.Add("@Type", ticket.Type);
            p.Add("@Source", ticket.Source);
            p.Add("@Inserted", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            var insertedId = p.Get<int>("@Inserted");
            return insertedId;
        }



        public List<CommentDetail> GetCommentPublisedByObjectId(int id, int type, int pageIndex = 1, int pageSize = 10)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetCommentPublisedByObjectId";
            p.Add("@id", id);
            p.Add("@type", type);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<CommentDetail>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public decimal GetRatingByObjectId(int objectId, int objectType)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetRatingByObjectId";
            p.Add("@objectId", objectId);
            p.Add("@objectType", objectType);
            p.Add("@rateAvg", dbType: System.Data.DbType.Decimal, direction: System.Data.ParameterDirection.Output);

            _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            var result = p.Get<decimal>("@rateAvg");
            return result;
        }

        public List<ManufactureViewModel> GetManufactures(string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetManufactures";
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ManufactureViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public List<ColorViewModel> GetColors(string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetColors";
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ColorViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public TagViewModel GetTagTarget(string tag)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetTagByAlias";
            p.Add("@tag", tag);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<TagViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result;
        }

        public int CreateViewCount(int objectId, string type)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_AddViewCount";
            p.Add("@objectId", objectId);
            p.Add("@type", type);

            p.Add("@insertedId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            try
            {
                _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
                var insertedId = p.Get<int>("@insertedId");
                return insertedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }


        }

        public int CalculateDepartment()
        {

            var keyCache = string.Format("Department_{0}", "CalculateDepartment");
            var result = 0;


            //get in cache
            var result_after_cache = _memory.Get(keyCache);
            if (result_after_cache != null)
                result = JsonConvert.DeserializeObject<int>(result_after_cache);
            if (result_after_cache == null)
            {
                var query = "select count(1) from Department";
                result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<int>(query));
                //Add cache
                var add_to_cache = JsonConvert.SerializeObject(result);
                result_after_cache = add_to_cache;

                _memory.Create(keyCache, result_after_cache);
            }
            return result;
        }

        public int CreateContactwBookingTime(ServiceTicket ticket)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_CreateContact_w_BookingTime";
            p.Add("@Name", ticket.Name);
            p.Add("@Phone", ticket.Phone);
            p.Add("@Address", ticket.Address);
            p.Add("@Title", ticket.Title);
            p.Add("@Content", ticket.Content);
            p.Add("@Type", ticket.Type);
            p.Add("@Source", ticket.Source);
            p.Add("@BookingTime", ticket.BookingTime);
            p.Add("@Inserted", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            var insertedId = p.Get<int>("@Inserted");
            return insertedId;
        }

        public List<SiteMapViewModel> GetSiteMapUrl()
        {
            var p = new DynamicParameters();
            var commandText = "usp_Tools_GenerateSiteMapUrl";
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<SiteMapViewModel>(commandText, null, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
        public List<ManufactureViewModel> GetManufacturesByZoneId(string lang_code, int zone_id)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetManufactures_By_ZoneId";
            p.Add("@lang_code", lang_code);
            p.Add("@zone_id", zone_id);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ManufactureViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
        public void AutoBackupDatabase()
        {
            var p = new DynamicParameters();
            var commandText = "usp_Backupdatabase";
            try
            {
                _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void TestJob()
        {
            Console.WriteLine("Job active");
        }
    }
}
