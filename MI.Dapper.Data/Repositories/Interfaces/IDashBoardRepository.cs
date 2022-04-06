using MI.Dapper.Data.Models;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IDashBoardRepository
    {
        DashBoard GetDashBoard1();
        List<DashBoardOrder> GetDashBoard3();
        List<DashBoard2> GetDashBoard2(DateTime startDate, DateTime endDate);
    }
}
