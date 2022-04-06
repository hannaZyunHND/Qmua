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
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly string _connectionString;

        public ManufacturerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Manufacturers>> GetAllManufacturer()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var result = await conn.QueryAsync<Manufacturers>(
                    "select m.id,mil.name,m.url from Manufacturer m inner join ManufacturerInLanguage mil on m.Id = mil.ManufacturerId where mil.LanguageCode='vi-VN'",
                    null, null,
                    null, CommandType.Text);
                var listManufacturer = result.ToList();
                return listManufacturer;
            }
        }


        public async Task<PagedResult<Manufacturers>> GetAllPagingManufacturer(FilterQuery query)
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
                    parameters.Add("@keyword", query.keyword);
                    parameters.Add("@pageIndex", query.pageIndex);
                    parameters.Add("@pageSize", query.pageSize);
                    parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await conn.QueryAsync<Manufacturers>("Get_Manufacturers_AllPaging",
                        parameters, null, null,
                        CommandType.StoredProcedure);
                    var totalRow = parameters.Get<int>("@totalRow");
                    var manufacturersPaging = new PagedResult<Manufacturers>()
                    {
                        TotalRow = totalRow,
                        Items = result.ToList(),
                        PageIndex = query.pageIndex,
                        PageSize = query.pageSize
                    };
                    return manufacturersPaging;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> AddManufacturer(ManufacturerViewModel manufacturerViewModel)
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
                    parameters.Add("@Avatar", manufacturerViewModel.Avatar);
                    parameters.Add("@Url", manufacturerViewModel.Url);
                    parameters.Add("@Name", manufacturerViewModel.Name);
                    parameters.Add("@UrlInLanguage", manufacturerViewModel.UrlInLanguage);
                    parameters.Add("@LanguageCode", manufacturerViewModel.LanguageCode);
                    parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await connection.ExecuteAsync("Create_Manufacturers", parameters, null, null,
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

        public async Task<int> UpdateManufacturer(ManufacturerViewModel manufacturerViewModel)
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
                    parameters.Add("@id", manufacturerViewModel.Id);
                    parameters.Add("@Avatar", manufacturerViewModel.Avatar);
                    parameters.Add("@Url", manufacturerViewModel.Url);
                    parameters.Add("@Name", manufacturerViewModel.Name);
                    parameters.Add("@UrlInLanguage", manufacturerViewModel.UrlInLanguage);
                    parameters.Add("@LanguageCode", manufacturerViewModel.LanguageCode);
                    var result = await connection.ExecuteAsync("Update_Manufacturers", parameters, null, null,
                        CommandType.StoredProcedure);
                    return manufacturerViewModel.Id;
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
                    var result = await connection.ExecuteAsync("Delete_Manufacturers", parameters, null, null,
                        CommandType.StoredProcedure);
                    return 1;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }

        public async Task<ManufacturerByIdViewModel> GetManufacturerById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var sqlManufacturerById = "select * from Manufacturer where Id=" + id;
                    var sqlManufacturerInLaguageByManufacturerId =
                        "select Name,Url,LanguageCode,ManufacturerId from ManufacturerInLanguage where ManufacturerId=" +
                        id;

                    var resultManufacturerById =
                        await connection.QueryAsync<Manufacturers>(sqlManufacturerById, null, null, null,
                            CommandType.Text);
                    var resultManufacturerInLaguageByManufacturerId =
                        await connection.QueryAsync<ManufacturersInLanguage>(sqlManufacturerInLaguageByManufacturerId,
                            null, null, null,
                            CommandType.Text);
                    var result = new ManufacturerByIdViewModel()
                    {
                        Manufacturers = resultManufacturerById.Single(),
                        ManufacturersInLanguages = resultManufacturerInLaguageByManufacturerId.ToList()
                    };
                    return result;
                }
                catch (Exception e)
                {
                }
            }

            return new ManufacturerByIdViewModel();
        }
    }
}