using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.ViewModels;
using Utils;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface ICouponRepository
    {
        Task<KeyValuePair<int, string>> Create(CouponViewModel coupon);
        Task<KeyValuePair<bool, string>> Update(CouponViewModel coupon);
        Task<PagedResult<CouponViewModel>> GetAllPaging(FilterQuery filterQuery);
        Task<CouponViewModel> GetById(int id);
        Task<bool> Unpublish(int id, int type);
    }
}