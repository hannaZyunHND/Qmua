using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Models;
using Microsoft.Extensions.Configuration;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly string _connectionString;

        public PromotionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<PromotionGetByProductIdViewModel>> GetListPromotionByProductId(int productId)
        {
            var result = new List<PromotionGetByProductIdViewModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var sqlGetAllPromotion = "select id,name from promotion where status != 3 ";
                    var resultGetAllPromotion =
                        await connection.QueryAsync<Promotion>(sqlGetAllPromotion, null, null, null, CommandType.Text);
                    var sqlGetAllPromotionByProductId =
                        "select CAST(pip.PromotionId as int) from Promotion p inner join ProductInPromotion pip on p.id=pip.PromotionId where p.status != 3 pip.ProductId=" +
                        productId;
                    var resultGetAllPromotionByProductId =
                        await connection.QueryAsync<int>(sqlGetAllPromotionByProductId, null, null, null,
                            CommandType.Text);
                    result.AddRange(resultGetAllPromotion.Select(x => new PromotionGetByProductIdViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ProductId = productId
                    }).ToList());

                    foreach (var item in result)
                    {
                        foreach (var itemj in resultGetAllPromotionByProductId.ToList())
                        {
                            if (item.Id == itemj)
                            {
                                item.IsChoose = true;
                            }
                        }
                    }

                    return result;
                }
                catch (Exception e)
                {
                }

                return result;
            }
        }

        public async Task<bool> AddPromotionInProduct(List<CreatePromotionWithProduct> createPromotionWithProducts)
        {
            var sqlDelete = " delete from ProductInPromotion where PromotionId={0} and ProductId={1}";
            var sqlCreateResult = "";
            var sqlInsert = "insert into ProductInPromotion(PromotionId, ProductId) values ";
            var sqlDeleteResult = "";
            var valueString = new List<string>();
            foreach (var item in createPromotionWithProducts)
            {
                sqlDeleteResult += string.Format(sqlDelete, item.Id, item.ProductId);
                var value = "(" + item.Id + "," + item.ProductId + ")";
                valueString.Add(value);
            }

            sqlCreateResult = sqlInsert + string.Join(",", valueString);
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var resultDelete =
                        await connection.ExecuteAsync(sqlDeleteResult, null, null, null, CommandType.Text);
                    var resultCreate =
                        await connection.ExecuteAsync(sqlCreateResult, null, null, null, CommandType.Text);
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