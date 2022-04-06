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
using Utils;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class ProductSpecificationTemplateRepository : IProductSpecificationTemplateRepository
    {
        private readonly string _connectionString;

        public ProductSpecificationTemplateRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<ProductSpecificationTemplate>> GetAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var result = await conn.QueryAsync<ProductSpecificationTemplate>(
                    "select pst.id, pst.value, pst.zoneid, pst.sortorder, pstil.Name from ProductSpecificationTemplate pst inner join ProductSpecificationTemplateInLanguage pstil on pst.Id=pstil.ProductSpecificationTemplateId where pstil.LanguageCode='vi-VN'",
                    null, null,
                    null, CommandType.Text);
                var listProductSpecificationTemplater = result.ToList();
                return listProductSpecificationTemplater;
            }
        }

        public async Task<PagedResult<ProductSpecificationTemplate>> GetAllPaging(FilterQuery filterQuery)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("@keyword", filterQuery.keyword);
                    parameters.Add("@pageIndex", filterQuery.pageIndex);
                    parameters.Add("@pageSize", filterQuery.pageSize);
                    parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await conn.QueryAsync<ProductSpecificationTemplate>(
                        "Get_ProductSpecificationTemplate_AllPaging",
                        parameters, null, null,
                        CommandType.StoredProcedure);
                    var totalRow = parameters.Get<int>("@totalRow");
                    var productSpecificationTemplatesPaging = new PagedResult<ProductSpecificationTemplate>()
                    {
                        TotalRow = totalRow,
                        Items = result.ToList(),
                        PageIndex = filterQuery.pageIndex,
                        PageSize = filterQuery.pageSize
                    };
                    return productSpecificationTemplatesPaging;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> AddProductSpecificationTemplate(
            ProductSpecificationTemplateViewModel productSpecificationTemplateViewModel)
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
                    parameters.Add("@Unit", productSpecificationTemplateViewModel.Unit);
                    parameters.Add("@Url", productSpecificationTemplateViewModel.Url);
                    parameters.Add("@ZoneIds", productSpecificationTemplateViewModel.ZoneIds);
                    parameters.Add("@Name", productSpecificationTemplateViewModel.Name);
                    parameters.Add("@LanguageCode", productSpecificationTemplateViewModel.LanguageCode);
                    parameters.Add("@IsForAllProduct", productSpecificationTemplateViewModel.IsForAllProduct);
                    parameters.Add("@IsFilter", productSpecificationTemplateViewModel.IsFilter);
                    parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await connection.ExecuteAsync("Create_ProductSpecificationTemplate", parameters, null,
                        null,
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

        public async Task<int> UpdateProductSpecificationTemplate(
            ProductSpecificationTemplateViewModel productSpecificationTemplateViewModel)
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
                    parameters.Add("@id", productSpecificationTemplateViewModel.Id);
                    parameters.Add("@Unit", productSpecificationTemplateViewModel.Unit);
                    parameters.Add("@Url", productSpecificationTemplateViewModel.Url);
                    parameters.Add("@ZoneIds", productSpecificationTemplateViewModel.ZoneIds);
                    parameters.Add("@IsForAllProduct", productSpecificationTemplateViewModel.IsForAllProduct);
                    parameters.Add("@IsFilter", productSpecificationTemplateViewModel.IsFilter);
                    parameters.Add("@Name", productSpecificationTemplateViewModel.Name);
                    parameters.Add("@LanguageCode", productSpecificationTemplateViewModel.LanguageCode);
                    var result = await connection.ExecuteAsync("Update_ProductSpecificationTemplate", parameters, null,
                        null,
                        CommandType.StoredProcedure);
                    return productSpecificationTemplateViewModel.Id;
                }
                catch (Exception e)
                {
                }
            }

            return 0;
        }

        public async Task<int> UnPublish(int id)
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
                    parameters.Add("@id", id);
                    var result = await connection.ExecuteAsync("Delete_ProductSpecificationTemplate", parameters, null,
                        null,
                        CommandType.StoredProcedure);
                    return 1;
                }
                catch (Exception e)
                {
                }

                return 0;
            }
        }

        public async Task<ProductSpecificationTemplateByIdViewModel> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var sqlProductSpecificationTemplateById = "select * from MaintainSpectifications where Id=" + id;
                    var sqlProductSpecificationTemplateInLaguageById =
                        "select * from MaintainSpectificationInLanguage where SpectificationId=" +
                        id;
                    var sqlGetAllZone = "select ZoneId from MaintainSpectificationTemplate where SpectificationId=" +
                                        id;
                    var sqlGetMaintainSpectificationTemplate =
                        "select top 1 IsForAllProduct,IsFilter from  MaintainSpectificationTemplate  where SpectificationId=" +
                        id;
                    var resultGetMaintainSpectificationTemplate =
                        await connection.QueryAsync<ProductSpecificationTemplates>(sqlGetMaintainSpectificationTemplate,
                            null, null, null,
                            CommandType.Text);
                    var resultGetZoneById =
                        await connection.QueryAsync<int>(sqlGetAllZone, null, null, null,
                            CommandType.Text);
                    var resultManufacturerById =
                        await connection.QueryAsync<ProductSpecificationTemplate>(sqlProductSpecificationTemplateById,
                            null, null, null,
                            CommandType.Text);
                    var resultManufacturerInLaguageByManufacturerId =
                        await connection.QueryAsync<ProductSpecificationTemplateInLanguage>(
                            sqlProductSpecificationTemplateInLaguageById,
                            null, null, null,
                            CommandType.Text);

                    var result = new ProductSpecificationTemplateByIdViewModel()
                    {
                        ProductSpecificationTemplate = resultManufacturerById.Single(),
                        ProductSpecificationTemplateInLanguage = resultManufacturerInLaguageByManufacturerId.ToList(),
                        ListZoneIds = resultGetZoneById.ToList(),
                    };
                    if (resultGetMaintainSpectificationTemplate.Count()>0)
                    {
                        result.ProductSpecificationTemplates =
                            resultGetMaintainSpectificationTemplate.FirstOrDefault();
                    }
                    else
                    {
                        result.ProductSpecificationTemplates=new ProductSpecificationTemplates();
                    }
                    return result;
                }
                catch (Exception e)
                {
                }
            }

            return new ProductSpecificationTemplateByIdViewModel();
        }
    }
}