using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Entity.Common;
using MI.Entity.Enums;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardRepository _dashBoardRepository;
        CommentBCL commentBCL;
        public DashBoardController(IDashBoardRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
            commentBCL = new CommentBCL();
        }
        [HttpGet("GetDataDashBoard1")]
        public ResponseData GetDataDashBoard1()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var result = _dashBoardRepository.GetDashBoard1();
                if (result != null)
                {
                    responseData.Success = true;
                    responseData.Data = result;
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("GetDataDashBoard2")]
        public ResponseData GetDataDashBoard2(int month, int year)
        {
            ResponseData responseData = new ResponseData();
            List<DashBoard2> orders = new List<DashBoard2>();
            DateTime sDate = new DateTime();
            DateTime eDate = new DateTime();
            string startDate = string.Empty;

            if (month == 0)
            {
                month = DateTime.Now.Month;
            }

            if (year == 0)
            {
                year = DateTime.Now.Year;
            }

            try
            {

                startDate = month + "/01/" + year;

                sDate = DateTime.Parse(startDate);
                eDate = DateTime.Parse(startDate).AddMonths(1).AddDays(-1);
                orders = _dashBoardRepository.GetDashBoard2(sDate, eDate);
                if (orders.Count() > 0)
                {
                    foreach (var item in orders)
                    {
                        item.CreatedDate = DateTime.Parse(item.CreatedDate).ToString("dd/MM/yyyy");
                    }
                    responseData.Success = true;
                    responseData.ListData = orders.ToList<object>();
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("GetDataDashBoard3")]
        public ResponseData GetDataDashBoard3()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var result = _dashBoardRepository.GetDashBoard3().OrderByDescending(r => r.CreatedDate).Skip(0).Take(10);
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        item.StatusName = StatusOrders.GetStatusName(item.Status);
                        item.CreatedDateString = DateTime.Parse(item.CreatedDate.ToString()).ToString("dd/MM/yyyy");
                        item.Note = !String.IsNullOrEmpty(item.Note) ? ((item.Note.Length > 20) ? item.Note.Substring(0, 20) + " ..." : item.Note) : "";
                    }
                    responseData.Success = true;
                    responseData.ListData = result.ToList<object>();
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("GetCommentData")]
        public ResponseData GetCommentData()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var result = commentBCL.ListCommentDashBoard();
                if (result.Count() > 0)
                {
                    responseData.Success = true;
                    responseData.ListData = result.ToList<object>();
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

    }
}
