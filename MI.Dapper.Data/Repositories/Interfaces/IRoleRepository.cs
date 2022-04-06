using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.ViewModels;
using Utils;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<AppRole>> GetAll();
        Task<PagedResult<AppRole>> GetAllPaging(FilterQuery filterQuery);
        Task<List<FunctionViewModel>> GetAllFunction();
        Task<List<ActionViewModel>> GetAllAction();
    }
}