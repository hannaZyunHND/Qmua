using Dapper;
using Mi.MemoryCache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using PhamGiaLandingPage.Web.ExecuteCommand;
using PhamGiaLandingPage.Web.Services.Product.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Product.Repository
{
    public interface IProductRepository
    {
        List<ProductMinify> GetProductMinifiesTreeViewByZoneParentId(int parentId, string lang_code, int locationId, int pageNumber, int pageSize, out int total);
        List<ProductMinify> GetAllProductSortBy(int sort_rate, int locationId, string lang_code, int page_index, int page_size, out int total_row);
        List<ProductMinify> GetProductMinifiesTreeViewByZoneParentIdSkipping(int parentId, string lang_code, int locationId, int skip, int size);
        List<ProductMinify> GetProductsInRegionByZoneParentIdSkipping(int parentId, string lang_code, int locationId, int skip, int size);
        List<ProductMinify> FilterProductBySpectifications(FilterProductBySpectification fp, out int total);
        List<ProductMinify> FilterProductBySpectificationsInZone(FilterProductBySpectification fp, out int total);
        ProductDetail GetProductInfomationDetail(int id, string lang_code);
        List<FilterAreaCooked> GetFilterProductByZoneId(int zone_id, string lang_code);
        List<SpectificationEstimatesCooked> GetSpectificationByMaterialType(int materialType, string lang_code);
        List<ProductSpectificationDetail> GetProductSpectificationDetail(int id, string lang_code);
        List<ProductMinify> GetProductInZoneByZoneIdMinify(int zone_id, int locationId, string lang_code, int pageNumber, int pageSize, out int total);
        List<ProductMinify> GetProductInZoneByZoneParentIdMinify(int zone_id, int locationId, string lang_code, int pageNumber, int pageSize, out int total);
        List<ProductMinify> GetProductInRegionByZoneIdMinify(int zone_id, int locationId, string lang_code, int pageNumber, int pageSize, out int total);
        List<ProductPriceInLocationDetail> GetProductPriceInLocationDetail(int product_id, string lang_code);
        List<ProductMinify> GetProductsInProductById(int productId, string type, int locationId, string lang_code, int pageIndex, int pageSize, out int total_row);
        List<PromotionInProduct> GetPromotionInProduct(int productId, string lang_code);
        List<ProductMinify> GetProductInListProductsMinify(string productIds, int location_id, string lang_code, int page_index, int page_size, out int total_row);
        List<ProductMinify> GetProductInListProductsMinify_WithTotalPrice(string productIds, int location_id, string lang_code, int page_index, int page_size, out int total_row, out int total_price);
        int GetTotalPriceInListProductsMinify(string productIds, int location_id, string lang_code);
        List<TagViewModel> GetTagsInProduct(string lang_code);
        List<ProductMinify> GetProductInTagMinify(string tag, int locationId, string lang_code, int pageNumber, int pageSize, out int total);
        //List<ProductMinify> GetProductsByStringFilter(string filter, string lang_code, int location_id, out int total_row);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;

        private ICacheController _memory;
        public ProductRepository(IConfiguration configuration, IExecuters executers, ICacheController memory)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;
            _memory = memory;
        }
        public List<ProductMinify> GetProductMinifiesTreeViewByZoneParentId(int parentId, string lang_code, int locationId, int pageNumber, int pageSize, out int total)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductTreeviewByZoneParentShowLayout";
            p.Add("@parentId", parentId);
            p.Add("@lang_code", lang_code);
            p.Add("@locationId", locationId);
            p.Add("@pageNumber", pageNumber);
            p.Add("@pageSize", pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return result;
        }
        public List<ProductMinify> GetProductMinifiesTreeViewByZoneParentIdSkipping(int parentId, string lang_code, int locationId, int skip, int size)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductTreeviewByZoneParentShowLayout_Skipping";
            p.Add("@parentId", parentId);
            p.Add("@lang_code", lang_code);
            p.Add("@locationId", locationId);
            p.Add("@skip", skip);
            p.Add("@size", size);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public ProductDetail GetProductInfomationDetail(int id, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductInfomationDetail";
            p.Add("@id", id);
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<ProductDetail>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result;
        }

        public List<ProductSpectificationDetail> GetProductSpectificationDetail(int id, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductSpecificationDetail";
            p.Add("@id", id);
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductSpectificationDetail>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public List<ProductMinify> GetProductInZoneByZoneIdMinify(int zone_id, int locationId, string lang_code, int pageNumber, int pageSize, out int total)
        {

            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductInZoneByZoneId_Minify";
            p.Add("@zone_id", zone_id);
            p.Add("@lang_code", lang_code);
            p.Add("@locationId", locationId);
            p.Add("@pageNumber", pageNumber);
            p.Add("@pageSize", pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return result;

        }
        public List<ProductMinify> GetProductInZoneByZoneParentIdMinify(int zone_parent_id, int locationId, string lang_code, int pageNumber, int pageSize, out int total)
        {

            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductInZoneByZoneParentId_Minify";
            p.Add("@zone_parent_id", zone_parent_id);
            p.Add("@lang_code", lang_code);
            p.Add("@locationId", locationId);
            p.Add("@pageNumber", pageNumber);
            p.Add("@pageSize", pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return result;

        }

        public List<ProductPriceInLocationDetail> GetProductPriceInLocationDetail(int product_id, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductPriceInLocations";
            p.Add("@productId", product_id);
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductPriceInLocationDetail>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public List<ProductMinify> GetProductsInProductById(int productId, string type, int locationId, string lang_code, int pageIndex, int pageSize, out int total_row)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductInProductMinify_ComboWithCofigMoney";
            p.Add("@productId", productId);
            p.Add("@locationId", locationId);
            p.Add("@type", type);
            p.Add("@lang_code", lang_code);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            p.Add("@total_row", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total_row = p.Get<int>("@total_row");
            return result;
        }

        public List<PromotionInProduct> GetPromotionInProduct(int productId, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetPromotionsInProduct";
            p.Add("@productId", productId);
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<PromotionInProduct>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public List<ProductMinify> GetProductInListProductsMinify(string productIds, int location_id, string lang_code, int page_index, int page_size, out int total_row)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductInListProductsMinify";
            p.Add("@productIds", productIds);
            p.Add("@locationId", location_id);
            p.Add("@lang_code", lang_code);
            p.Add("@pageIndex", page_index);
            p.Add("@pageSize", page_size);
            p.Add("@total_row", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total_row = p.Get<int>("@total_row");
            return result;
        }

        public int GetTotalPriceInListProductsMinify(string productIds, int location_id, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetTotalPriceProductInListProductsMinify";
            p.Add("@productIds", productIds);
            p.Add("@locationId", location_id);
            p.Add("@lang_code", lang_code);
            p.Add("@total_price", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Execute(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            var total = p.Get<int>("@total_price");
            return total;
        }

        public List<ProductMinify> GetProductInRegionByZoneIdMinify(int zone_id, int locationId, string lang_code, int pageNumber, int pageSize, out int total)
        {
            var a = pageSize;
            var keyCache = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", "productInRegion", lang_code, zone_id, locationId, pageNumber, pageSize);
            var r = new List<ProductMinify>();
            //get in cache
            var result_after_cache = _memory.Get(keyCache);
            if (result_after_cache != null)
            {
                r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductMinify>>(result_after_cache);
            }
            if (result_after_cache == null)
            {
                var p = new DynamicParameters();
                var commandText = "usp_Web_GetProductInRegionByZoneId_Minify";
                p.Add("@zone_id", zone_id);
                p.Add("@lang_code", lang_code);
                p.Add("@locationId", locationId);
                p.Add("@pageNumber", pageNumber);
                p.Add("@pageSize", pageSize);
                p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                r = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                a = p.Get<int>("@total");
                //Add cache
                var add_to_cache = Newtonsoft.Json.JsonConvert.SerializeObject(r);
                result_after_cache = add_to_cache;

                _memory.Create(keyCache, result_after_cache);
            }
            total = a;
            return r;
        }

        public List<ProductMinify> GetProductsInRegionByZoneParentIdSkipping(int parentId, string lang_code, int locationId, int skip, int size)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductsInRegionByZoneParentMinify_Skipping";
            p.Add("@parentId", parentId);
            p.Add("@lang_code", lang_code);
            p.Add("@locationId", locationId);
            p.Add("@skip", skip);
            p.Add("@size", size);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public List<FilterAreaCooked> GetFilterProductByZoneId(int zone_id, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetFilterAreaByZoneId";
            p.Add("@zoneId", zone_id);
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<FilterArea>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();

            //start cooking
            var q = from r in result
                    group r by new
                    {
                        r.SpectificationId,
                        r.Name
                    } into r_after
                    select new FilterAreaCooked()
                    {
                        SpectificationId = r_after.Key.SpectificationId,
                        Name = r_after.Key.Name,
                        Values = r_after.ToList()
                    };
            //var result_cooked = new List<FilterAreaCooked>();
            //foreach(var item in result)
            //{
            //    var r = new FilterAreaCooked();
            //    r.Name = item.Name;
            //    r.Values = new List<string>();
            //    if (result_cooked.Count() == 0 || result_cooked.Where(x => x.Name.Equals(item.Name)).Count()==0 )
            //        result_cooked.Add(r);
            //}
            //foreach(var item in result)
            //{
            //    var f = result_cooked.Where(r => r.Name.Equals(item.Name)).FirstOrDefault();
            //    if (f != null)
            //        f.Values.Add(item.Value);
            //}
            return q.ToList();

        }

        public List<ProductMinify> GetProductInListProductsMinify_WithTotalPrice(string productIds, int location_id, string lang_code, int page_index, int page_size, out int total_row, out int total_price)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductInListProductsMinify_WithTotalPrice";
            p.Add("@productIds", productIds);
            p.Add("@locationId", location_id);
            p.Add("@lang_code", lang_code);
            p.Add("@pageIndex", page_index);
            p.Add("@pageSize", page_size);
            p.Add("@total_row", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            p.Add("@total_price", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total_row = p.Get<int>("@total_row");
            total_price = p.Get<int>("@total_price");
            return result;
        }

        public List<SpectificationEstimatesCooked> GetSpectificationByMaterialType(int materialType, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetSpectificationByMaterialType";
            p.Add("@materialType", materialType);
            p.Add("@lang_code", lang_code);
            var result_query = _executers.ExecuteCommand(_connStr, conn => conn.Query<SpectificationEstimates>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            var q = from r in result_query
                    group r by new
                    {
                        r.SpectificationId,
                        r.Name
                    } into r_after
                    select new SpectificationEstimatesCooked()
                    {
                        SpectificationId = r_after.Key.SpectificationId,
                        Name = r_after.Key.Name,
                        Values = r_after.ToList()
                    };
            return q.ToList();
        }


        //public List<ProductMinify> FilterProductBySpectifications(int parentId, string lang_code, int locationId, int manufacture_id, int min_price, int max_price, int sort_price, int sort_rate, string color_code, List<FilterSpectification> filter,string filter_text, int material_type, int pageNumber, int pageSize, out int total)
        public List<ProductMinify> FilterProductBySpectifications(FilterProductBySpectification fp, out int total)
        {
            DataTable fillter_cooked = new DataTable();
            fillter_cooked.Columns.Add("SpectificationId", typeof(int));
            fillter_cooked.Columns.Add("Value", typeof(string));

            if (fp.filter != null)
            {
                foreach (var item in fp.filter)
                {
                    fillter_cooked.Rows.Add(item.SpectificationId, item.Value);
                }
            }
            var p = new DynamicParameters();
            var commandText = "usp_Web_FilterProductBySpectifications_NotZone";
            //p.Add("@parentId", fp.parentId);
            p.Add("@lang_code", fp.lang_code);
            p.Add("@locationId", fp.locationId);
            p.Add("@manufacture_id", fp.manufacture_id);
            p.Add("@min_price", fp.min_price);
            p.Add("@max_price", fp.max_price);
            p.Add("@sort_price", fp.sort_price);
            p.Add("@sort_rate", fp.sort_rate);
            p.Add("@color_code", fp.color_code == null ? "" : fp.color_code);
            p.Add("@filter_text", fp.filter_text == null ? "" : fp.filter_text);
            p.Add("@material_type", fp.material_type);
            p.Add("@filter", fillter_cooked.AsTableValuedParameter("type_filterProductBySpectification"));
            p.Add("@pageNumber", fp.pageNumber);
            p.Add("@pageSize", fp.pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return result;
        }

        public List<ProductMinify> GetAllProductSortBy(int sort_rate, int locationId, string lang_code, int page_index, int page_size, out int total_row)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetAllProductSortBy";
            p.Add("@sort_rate", sort_rate);
            p.Add("@locationId", locationId);
            p.Add("@lang_code", lang_code);
            p.Add("@pageIndex", page_index);
            p.Add("@pageSize", page_size);
            p.Add("@total_row", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total_row = p.Get<int>("@total_row");
            return result;
        }

        public List<ProductMinify> FilterProductBySpectificationsInZone(FilterProductBySpectification fp, out int total)
        {
            DataTable fillter_cooked = new DataTable();
            fillter_cooked.Columns.Add("SpectificationId", typeof(int));
            fillter_cooked.Columns.Add("Value", typeof(string));




            if (fp.filter != null)
            {
                foreach (var item in fp.filter)
                {
                    if (item.Value != null)
                        fillter_cooked.Rows.Add(item.SpectificationId, item.Value);
                }
            }
            var p = new DynamicParameters();
            var commandText = "usp_Web_FilterProductBySpectifications_v2s";
            p.Add("@parentId", fp.parentId);
            p.Add("@lang_code", fp.lang_code);
            p.Add("@locationId", fp.locationId);
            p.Add("@manufacture_id", fp.manufacture_id == null ? "" : fp.manufacture_id);
            p.Add("@min_price", fp.min_price);
            p.Add("@max_price", fp.max_price);
            p.Add("@sort_price", fp.sort_price);
            p.Add("@sort_rate", fp.sort_rate);
            p.Add("@color_code", fp.color_code == null ? "" : fp.color_code);
            p.Add("@filter_text", fp.filter_text == null ? "" : fp.filter_text);
            p.Add("@material_type", fp.material_type);
            p.Add("@filter", fillter_cooked.AsTableValuedParameter("type_filterProductBySpectification"));
            p.Add("@pageNumber", fp.pageNumber);
            p.Add("@pageSize", fp.pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return result;
        }

        public List<TagViewModel> GetTagsInProduct(string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetAllTag";
            p.Add("@lang_code", lang_code);
            try
            {
                var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<TagViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<TagViewModel>();
            }

        }

        public List<ProductMinify> GetProductInTagMinify(string tag, int locationId, string lang_code, int pageNumber, int pageSize, out int total)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetProductInTag_Minify";
            p.Add("@tag", tag);
            p.Add("@lang_code", lang_code);
            p.Add("@locationId", locationId);
            p.Add("@pageNumber", pageNumber);
            p.Add("@pageSize", pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ProductMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return result;
        }
    }
}
