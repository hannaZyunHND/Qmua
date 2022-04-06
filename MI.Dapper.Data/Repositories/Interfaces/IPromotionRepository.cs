using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.ViewModels;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IPromotionRepository
    {
        Task<List<PromotionGetByProductIdViewModel>> GetListPromotionByProductId(int productId);
        Task<bool> AddPromotionInProduct(List<CreatePromotionWithProduct> createPromotionWithProducts);
    }
}