using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.ViewModels;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductAtArticle>> GetAllProduct();
        Task<int> ProductAdd(Product product);

        List<ExportExcelProductPriceInLocation> ExportExcel(string idList, int loc_id);
        List<ExportExcelMaintainSpectificationInProduct> ExportExcelMaintainSpectification(string idList, int spec_id, string lang_code);
        string ImportExcelProductPriceInLocation(DataTable mergeProduct);
        string ImportExcelMaintainSpectificatinInProduct(DataTable mergeProduct);
        


    }
}