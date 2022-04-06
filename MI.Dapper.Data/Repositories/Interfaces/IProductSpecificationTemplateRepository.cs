using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.ViewModels;
using Utils;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IProductSpecificationTemplateRepository
    {
        Task<List<ProductSpecificationTemplate>> GetAll();
        Task<PagedResult<ProductSpecificationTemplate>> GetAllPaging(FilterQuery filterQuery);

        Task<int> AddProductSpecificationTemplate(
            ProductSpecificationTemplateViewModel productSpecificationTemplateViewModel);

        Task<int> UpdateProductSpecificationTemplate(
            ProductSpecificationTemplateViewModel productSpecificationTemplateViewModel);

        Task<int> UnPublish(int id);
        Task<ProductSpecificationTemplateByIdViewModel> GetById(int id);
    }
}