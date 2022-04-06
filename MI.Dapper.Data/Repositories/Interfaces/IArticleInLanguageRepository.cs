using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.ViewModels;
using MI.Entity.Models;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IArticleInLanguageRepository
    {
        Task<int> Create(ArticleInLanguageViewModel articleInLanguageViewModel);

        
    }
}