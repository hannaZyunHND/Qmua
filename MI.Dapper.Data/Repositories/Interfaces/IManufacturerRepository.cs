using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.ViewModels;
using Utils;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<List<Manufacturers>> GetAllManufacturer();
        Task<PagedResult<Manufacturers>> GetAllPagingManufacturer(FilterQuery query);
        Task<int> AddManufacturer(ManufacturerViewModel manufacturerViewModel);
        Task<int> UpdateManufacturer(ManufacturerViewModel manufacturerViewModel);
        Task<int> UnPublish(int id);
        Task<ManufacturerByIdViewModel> GetManufacturerById(int id);
    }
}