using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.ViewModels;
using Utils;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IArticlesRepository
    {
        Task<int> Create(Articles article);
        Task<int> Update(Articles article);
        Task<int> PublishArticle(int id);
        Task<int> UnPublishArticle(int id);
        Task<ArticlesViewModel> GetById(int id);
        Task<int> Delete(int id);
        Task<PagedResult<ArticlePagingViewModel>> GetAllPaging(FilterArticle article);
        Task<List<TagArticlePagingViewModel>> GetAllTag();
        List<ExportArticleViewCount> ExportExcelArticleViewCount(string ids, int? year);
    }
}