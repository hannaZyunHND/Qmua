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
    public class CouponRepository : ICouponRepository
    {
        private readonly string _connectionString;

        public CouponRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<KeyValuePair<int, string>> Create(CouponViewModel coupon)
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
                    parameters.Add("@Name", coupon.Name, DbType.String);
                    parameters.Add("@Code", coupon.Code);
                    parameters.Add("@DiscountOption", coupon.DiscountOption);
                    parameters.Add("@NumberOfUsed", coupon.NumberOfUsed);
                    parameters.Add("@QuantityDiscount", coupon.QuantityDiscount);
                    parameters.Add("@ValueDiscount", coupon.ValueDiscount);
                    parameters.Add("@StartDate", coupon.StartDate);
                    parameters.Add("@EndDate", coupon.EndDate);
                    parameters.Add("@Category", String.Join(",", coupon.Category));
                    parameters.Add("@IsCategory", coupon.IsCategory);
                    parameters.Add("@ListProduct", String.Join(",", coupon.ListProduct));
                    parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await connection.ExecuteAsync("Create_Coupon", parameters, null, null,
                        CommandType.StoredProcedure);
                    var newId = parameters.Get<int>("@id");
                    return new KeyValuePair<int, string>(newId, "Thành công");
                }
                catch (Exception e)
                {
                }
            }

            return new KeyValuePair<int, string>(0, "Thất bại");
        }

        public async Task<KeyValuePair<bool, string>> Update(CouponViewModel coupon)
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
                    parameters.Add("@Name", coupon.Name, DbType.String);
                    parameters.Add("@Code", coupon.Code);
                    parameters.Add("@DiscountOption", coupon.DiscountOption);
                    parameters.Add("@NumberOfUsed", coupon.NumberOfUsed);
                    parameters.Add("@QuantityDiscount", coupon.QuantityDiscount);
                    parameters.Add("@ValueDiscount", coupon.ValueDiscount);
                    parameters.Add("@StartDate", coupon.StartDate);
                    parameters.Add("@EndDate", coupon.EndDate);
                    parameters.Add("@Category", String.Join(",", coupon.Category));
                    parameters.Add("@IsCategory", coupon.IsCategory);
                    parameters.Add("@ListProduct", String.Join(",", coupon.ListProduct));
                    parameters.Add("@id", coupon.Id);
                    var result = await connection.ExecuteAsync("Update_Coupon", parameters, null, null,
                        CommandType.StoredProcedure);
                    return new KeyValuePair<bool, string>(true, "Thành công");
                }
                catch (Exception e)
                {
                }
            }

            return new KeyValuePair<bool, string>(false, "Thất bại");
        }

        public async Task<PagedResult<CouponViewModel>> GetAllPaging(FilterQuery filterQuery)
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
                    var result = await conn.QueryAsync<CouponViewModel>("Get_Coupon_AllPaging",
                        parameters, null, null,
                        CommandType.StoredProcedure);
                    var totalRow = parameters.Get<int>("@totalRow");
                    var manufacturersPaging = new PagedResult<CouponViewModel>()
                    {
                        TotalRow = totalRow,
                        Items = result.ToList(),
                        PageIndex = filterQuery.pageIndex,
                        PageSize = filterQuery.pageSize
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

        public async Task<CouponViewModel> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    var sqlCouponById = "select * from Coupon where Id=" + id;

                    var resultCouponById =
                        await connection.QueryAsync<CouponViewModel>(sqlCouponById, null, null, null,
                            CommandType.Text);
                    var result = resultCouponById.FirstOrDefault();

                    return result;
                }
                catch (Exception e)
                {
                }
            }
            return new CouponViewModel();
        }



        public async Task<bool> Unpublish(int id, int type)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    string sqlUnpublishCouponById = "";
                    if (type != 2)
                        sqlUnpublishCouponById = " update Coupon SET Locked=1 where Id=" + id;
                    else
                        sqlUnpublishCouponById = " delete Coupon where Id=" + id;

                    var resultCouponById =
                        await connection.ExecuteAsync(sqlUnpublishCouponById, null, null, null,
                            CommandType.Text);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}