using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using Microsoft.Extensions.Configuration;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class ArticleInLanguageRepository : IArticleInLanguageRepository
    {
        private readonly string _connectionString;

        public ArticleInLanguageRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> Create(ArticleInLanguageViewModel articleInLanguageViewModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("@Title", articleInLanguageViewModel.Title);
                    parameters.Add("@Description", articleInLanguageViewModel.Description);
                    parameters.Add("@Body", articleInLanguageViewModel.Body);
                    parameters.Add("@Author", articleInLanguageViewModel.Author);
                    parameters.Add("@WordCount", articleInLanguageViewModel.WordCount);
                    parameters.Add("@MetaTitle", articleInLanguageViewModel.MetaTitle);
                    parameters.Add("@MetaKeyword", articleInLanguageViewModel.MetaKeyword);
                    parameters.Add("@MetaDescription", articleInLanguageViewModel.MetaDescription);
                    parameters.Add("@SocialTitle", articleInLanguageViewModel.SocialTitle);
                    parameters.Add("@SocialDescription", articleInLanguageViewModel.SocialDescription);
                    parameters.Add("@SocialImage", articleInLanguageViewModel.SocialImage);
                    parameters.Add("@Url", articleInLanguageViewModel.Url);
                    parameters.Add("@LanguageCode", articleInLanguageViewModel.LanguageCode);
                    parameters.Add("@ArticleId", articleInLanguageViewModel.ArticleId);
                    parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await connection.ExecuteAsync("Create_ArticlesInLanguage", parameters, null, null,
                        CommandType.StoredProcedure);
                    var newId = parameters.Get<int>("@id");
                    return newId;
                }
                catch (Exception e)
                {
                }

                return 0;
            }
        }
    }
}