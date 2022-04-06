using Dapper;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Entity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<DashBoardRepository> _logger;
        public DashBoardRepository(IConfiguration configuration, ILogger<DashBoardRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }
        public DashBoard GetDashBoard1()
        {
            DashBoard dashBoard = new DashBoard();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var parameters = new DynamicParameters();
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    parameters.Add("@totalOrder", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    parameters.Add("@totalNewOrder", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    parameters.Add("@totalProcessingOrder", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    parameters.Add("@totalApprovedOrder", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    parameters.Add("@totalSuccessOrder", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    parameters.Add("@totalCancleOrder", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    var queryResult = connection.QueryFirstOrDefault<DashBoard>("Get_DataDashBoard1", parameters, null, commandTimeout: null, commandType: CommandType.StoredProcedure);

                    dashBoard.TotalOrder = parameters.Get<int>("@totalOrder");
                    dashBoard.TotalNewOrder = parameters.Get<int>("@totalNewOrder");
                    dashBoard.TotalProcessingOrder = parameters.Get<int>("@totalProcessingOrder");
                    dashBoard.TotalApprovedOrder = parameters.Get<int>("@totalApprovedOrder");
                    dashBoard.TotalSuccessOrder = parameters.Get<int>("@totalSuccessOrder");
                    dashBoard.TotalCancleOrder = parameters.Get<int>("@totalCancleOrder");
                }
                catch (Exception ex)
                {
                }
            }

            return dashBoard;
        }

        public List<DashBoardOrder> GetDashBoard3()
        {
            List<DashBoardOrder> lstDashBoardOrder = new List<DashBoardOrder>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var parameters = new DynamicParameters();
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        var queryResult = connection.Query<DashBoardOrder>("Get_DataDashBoard3", parameters, null, commandTimeout: null, commandType: CommandType.StoredProcedure);
                        lstDashBoardOrder = queryResult.ToList();
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return lstDashBoardOrder;
        }

        public List<DashBoard2> GetDashBoard2(DateTime startDate, DateTime endDate)
        {
            List<DashBoard2> orders = new List<DashBoard2>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@start", startDate);
                    parameters.Add("@end", endDate);
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        var queryResult = connection.Query<DashBoard2>("usp_CMS_CalculateOrderInRangeDate", parameters, null, commandTimeout: null, commandType: CommandType.StoredProcedure);
                        orders = queryResult.ToList();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return orders;
        }
    }
}
