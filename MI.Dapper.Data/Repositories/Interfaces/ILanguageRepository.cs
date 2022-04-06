using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        Task<List<Languages>> GetAllLanguage();
    }
}