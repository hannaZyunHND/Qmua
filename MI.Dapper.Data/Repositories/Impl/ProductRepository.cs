using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using Microsoft.Extensions.Configuration;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ExportExcelProductPriceInLocation> ExportExcel(string idList, int loc_id)
        {
            //convert
            if (idList.Length > 0 && loc_id > 0)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        var parameters = new DynamicParameters();
                        parameters.Add("@idList", idList);
                        parameters.Add("@loc_id", loc_id);
                        var result = connection.Query<ExportExcelProductPriceInLocation>("usp_CMS_ExportExcelProductPrice", parameters, null, true, null, CommandType.StoredProcedure);
                        return result.ToList();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return null;
                    }
                }
            }
            return null;
        }

        public List<ExportExcelMaintainSpectificationInProduct> ExportExcelMaintainSpectification(string idList, int spec_id, string lang_code)
        {
            //usp_CMS_ExportExcelMaintainSpectificationInProduct
            if (idList.Length > 0 && spec_id > 0)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        var parameters = new DynamicParameters();
                        parameters.Add("@idList", idList);
                        parameters.Add("@spec_id", spec_id);
                        parameters.Add("@lang_code", lang_code);
                        var result = connection.Query<ExportExcelMaintainSpectificationInProduct>("usp_CMS_ExportExcelMaintainSpectificationInProduct", parameters, null, true, null, CommandType.StoredProcedure);
                        return result.ToList();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return null;
                    }
                }
            }
            return null;
            //throw new NotImplementedException();
        }

        public async Task<List<ProductAtArticle>> GetAllProduct()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var result = await conn.QueryAsync<ProductAtArticle>(
                    "select p.id,Title as name from product p inner join ProductInLanguage pil on p.id=pil.ProductId where pil.LanguageCode='vi-VN'",
                    null, null, null,
                    CommandType.Text);
                var listProduct = result.ToList();
                return listProduct;
            }
        }

        public string ImportExcelProductPriceInLocation(DataTable mergeProduct)
        {
            if (mergeProduct != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        var parameters = new DynamicParameters();
                        parameters.Add("@mergeTbl", mergeProduct.AsTableValuedParameter("dbo.type_ProductInLocation"));
                        //parameters.Add("@loc_id", loc_id);
                        //var result = connection.Execute("usp_CMS_ImportExcelProductPriceInLocation", parameters, null, true, null, CommandType.StoredProcedure);
                        var result1 = connection.Execute("usp_CMS_ImportExcelProductPriceInLocation", parameters, null, null, CommandType.StoredProcedure);
                        return "success";
                        //return result.ToList();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return e.Message;
                    }
                }
            }
            return null;
        }
        public string ImportExcelMaintainSpectificatinInProduct(DataTable mergeProduct)
        {
            if (mergeProduct != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        var parameters = new DynamicParameters();
                        parameters.Add("@mergeTbl", mergeProduct.AsTableValuedParameter("dbo.type_MaintainSpectificationInProduct_v1"));
                        parameters.Add("@lang_code", "vi-VN");
                        //parameters.Add("@loc_id", loc_id);    
                        //var result = connection.Execute("usp_CMS_ImportExcelProductPriceInLocation", parameters, null, true, null, CommandType.StoredProcedure);
                        var result1 = connection.Execute("usp_CMS_ImportExcelMaintainSpectificationInProduct", parameters, null, null, CommandType.StoredProcedure);
                        return "success";
                        //return result.ToList();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return e.Message;
                    }
                }
            }
            return null;
        }

        //Add by AnhNV start
        public async Task<int> ProductAdd(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("@Status", product.Status);
                    parameters.Add("@Url", product.Url);
                    parameters.Add("@Avatar", product.Avatar);
                    parameters.Add("@AvatarArray", product.AvatarArray);
                    parameters.Add("@Price", product.Price);
                    parameters.Add("@DiscountPrice", product.DiscountPrice);
                    parameters.Add("@Warranty", product.Warranty);
                    parameters.Add("@ManufacturerId", product.ManufacturerId);
                    parameters.Add("@Code", product.Code);
                    parameters.Add("@CreatedBy", product.CreatedBy);
                    parameters.Add("@ModifyBy", product.ModifyBy);
                    parameters.Add("@Unit", product.Unit);
                    parameters.Add("@Quantity", product.Quantity);
                    parameters.Add("@PropertyId", product.PropertyId);
                    parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await connection.ExecuteAsync("ProductAdd", parameters, null, null,
                        CommandType.StoredProcedure);
                    var newId = parameters.Get<int>("@id");
                    return newId;
                }
                catch (Exception e)
                {
                }
            }

            return 0;
        }

        //Add by AnhNV end
    }
}


