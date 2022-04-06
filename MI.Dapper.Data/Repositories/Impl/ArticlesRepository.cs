using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Helpers;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using MI.Entity.CustomClass;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoreLinq.Extensions;
using Newtonsoft.Json;
using Utils;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ArticlesRepository> _logger;

        public ArticlesRepository(IConfiguration configuration,
            ILogger<ArticlesRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> Create(Articles articles)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var jsonString = string.Empty;
                if (!string.IsNullOrEmpty(articles.Position) || !string.IsNullOrEmpty(articles.Address) ||
                    !string.IsNullOrEmpty(articles.Salary) || !string.IsNullOrEmpty(articles.Count))
                {
                    ArticleRecruitment employmentInformation = new ArticleRecruitment()
                    {
                        Address = articles.Address,
                        Count = articles.Count,
                        Position = articles.Position,
                        Salary = articles.Salary,
                        StartDate = articles.StartDate,
                        EndDate = articles.EndDate,
                        ttkh = articles.ttkh ?? "",
                        ttdc = articles.ttdc ?? "",
                        ttlv = articles.ttlv ?? "",
                        ttdv = articles.ttdv ?? "",
                        AvatarArray = articles.avatarArray ?? "",
                        phanMoRong = articles.phanMoRong
                        
                    };
                    jsonString = JsonConvert.SerializeObject(employmentInformation);
                }

                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var sqlGetAllTag = "select t.Name,t.LanguageCode from tag t";
                    var resultGetAllTag =
                        await connection.QueryAsync<TagArticleViewModel>(sqlGetAllTag, null, null, null,
                            CommandType.Text);
                    var listTag = articles.TagNames.Split(',');
                    var listResultTag = new List<string>();
                    foreach (var item in listTag)
                    {
                        if (!resultGetAllTag.Any(x => x.Name.Equals(item)))
                        {
                            listResultTag.Add(item);
                        }
                    }

                    if (listResultTag.Count > 0)
                    {
                        var tagNames = string.Join(",", listResultTag);
                        var parametersTag = new DynamicParameters();
                        parametersTag.Add("@TagNames", tagNames);
                        parametersTag.Add("@LanguageCode", articles.LanguageCode);
                        var resultAddTag = await connection.ExecuteAsync("Create_Tag", parametersTag, null, null,
                            CommandType.StoredProcedure);
                    }


                    var parameters = new DynamicParameters();
                    parameters.Add("@Avatar", articles.Avatar);
                    parameters.Add("@Name", articles.Name);
                    parameters.Add("@Status", articles.Status);
                    parameters.Add("@Type", articles.Type);
                    parameters.Add("@LastModifiedDate", articles.LastModifiedDate);
                    parameters.Add("@DistributionDate", articles.DistributionDate);
                    parameters.Add("@CreatedBy", articles.CreatedBy);
                    parameters.Add("@LastModifiedBy", articles.LastModifiedBy);
                    parameters.Add("@PublishedBy", articles.PublishedBy);
                    parameters.Add("@Url", articles.Url);
                    parameters.Add("@UrlOld", articles.UrlOld);
                    parameters.Add("@ZoneIds", articles.ZoneIds);
                    parameters.Add("@ProductIds", articles.ProductIds);
                    parameters.Add("@Title", articles.Title);
                    parameters.Add("@Description", articles.Description);
                    parameters.Add("@Body", articles.Body);
                    parameters.Add("@Author", articles.Author);
                    parameters.Add("@WordCount", articles.WordCount);
                    parameters.Add("@MetaTitle ", articles.MetaTitle);
                    parameters.Add("@MetaKeyword", articles.MetaKeyword);
                    parameters.Add("@MetaDescription ", articles.MetaDescription);
                    parameters.Add("@MetaNoIndex ", articles.MetaNoIndex);
                    parameters.Add("@MetaCanonical ", articles.MetaCanonical);
                    parameters.Add("@MetaDescription ", articles.MetaDescription);
                    parameters.Add("@SocialTitle ", articles.SocialTitle);
                    parameters.Add("@SocialDescription", articles.SocialDescription);
                    parameters.Add("@SocialImage ", articles.SocialImage);
                    parameters.Add("@LanguageCode ", articles.LanguageCode);
                    parameters.Add("@Metadata", jsonString);
                    parameters.Add("@IsShowHome", articles.IsShowHome);
                    parameters.Add("@TagNames", articles.TagNames, DbType.String);
                    parameters.Add("@Indexing", articles.Indexing);
                    parameters.Add("@FileAttachment", articles.FileAttachment, DbType.String);
                    parameters.Add("@FileAttachmentUrl", articles.FileAttachmentUrl, DbType.String);
                    parameters.Add("@MetaWebPage", articles.MetaWebPage, DbType.String);
                    parameters.Add("@ThiTruong", articles.ThiTruong, DbType.String);
                    parameters.Add("@MoHinh", articles.MoHinh, DbType.String);
                    parameters.Add("@DuAn", articles.DuAn, DbType.String);
                    parameters.Add("@MucDichDauTu", articles.MucDichDauTu, DbType.String);
                    parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await connection.ExecuteAsync("Create_Articles", parameters, null, null,
                        CommandType.StoredProcedure);
                    var newId = parameters.Get<int>("@id");

                    Log.Error(newId.ToString());
                    return newId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Log.Error(ex.Message);
                }
            }

            return 0;
        }

        public async Task<int> Update(Articles article)
        {
            var jsonString = string.Empty;

            ArticleRecruitment employmentInformation = new ArticleRecruitment
            {
                Address = article.Address ?? "",
                Count = article.Count ?? "0",
                Position = article.Position ?? "",
                Salary = article.Salary ?? "0",
                StartDate = article.StartDate,
                EndDate = article.EndDate,
                ttkh = article.ttkh ?? "",
                ttdc = article.ttdc ?? "",
                ttlv = article.ttlv ?? "",
                ttdv = article.ttdv ?? "",
                AvatarArray = article.avatarArray ?? "",
                phanMoRong = article.phanMoRong
            };
            jsonString = JsonConvert.SerializeObject(employmentInformation);

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var sqlGetAllTag = "select t.Name,t.LanguageCode from tag t";
                    var resultGetAllTag =
                        await connection.QueryAsync<TagArticleViewModel>(sqlGetAllTag, null, null, null,
                            CommandType.Text);
                    var listTag = article.TagNames.Split(',');
                    var listResultTag = new List<string>();
                    foreach (var item in listTag)
                    {
                        if (!resultGetAllTag.Any(x => x.Name.Equals(item)))
                        {
                            listResultTag.Add(item);
                        }
                    }

                    if (listResultTag.Count > 0)
                    {
                        var tagNames = string.Join(",", listResultTag);
                        var parametersTag = new DynamicParameters();
                        parametersTag.Add("@TagNames", tagNames);
                        parametersTag.Add("@LanguageCode", article.LanguageCode);
                        var resultAddTag = await connection.ExecuteAsync("Create_Tag", parametersTag, null, null,
                            CommandType.StoredProcedure);
                    }


                    var parameters = new DynamicParameters();
                    parameters.Add("@id", article.Id);
                    parameters.Add("@Avatar", article.Avatar);
                    parameters.Add("@Name", article.Name);
                    parameters.Add("@Status ", article.Status);
                    parameters.Add("@Type ", article.Type);
                    parameters.Add("@LastModifiedDate ", article.LastModifiedDate);
                    parameters.Add("@DistributionDate ", article.DistributionDate);
                    parameters.Add("@CreatedBy ", article.CreatedBy);
                    parameters.Add("@LastModifiedBy ", article.LastModifiedBy);
                    parameters.Add("@PublishedBy ", article.PublishedBy);
                    parameters.Add("@Url ", article.Url);
                    parameters.Add("@UrlOld ", article.UrlOld);
                    parameters.Add("@ZoneIds ", article.ZoneIds);
                    parameters.Add("@ProductIds ", article.ProductIds);
                    parameters.Add("@Title ", article.Title);
                    parameters.Add("@Description", article.Description);
                    parameters.Add("@Body", article.Body ?? "");
                    parameters.Add("@Author", article.Author ?? "");
                    parameters.Add("@WordCount", article.WordCount);
                    parameters.Add("@MetaTitle ", article.MetaTitle ?? "");
                    parameters.Add("@MetaKeyword", article.MetaKeyword ?? "");
                    parameters.Add("@MetaDescription ", article.MetaDescription ?? "");
                    parameters.Add("@MetaNoIndex ", article.MetaNoIndex ?? "");
                    parameters.Add("@MetaCanonical ", article.MetaCanonical ?? "");
                    parameters.Add("@SocialTitle ", article.SocialTitle?? "");
                    parameters.Add("@SocialDescription", article.SocialDescription?? "");
                    parameters.Add("@SocialImage ", article.SocialImage?? "");
                    parameters.Add("@LanguageCode ", article.LanguageCode);
                    parameters.Add("@Metadata", jsonString);
                    parameters.Add("@IsShowHome", article.IsShowHome ?? false);
                    parameters.Add("@Indexing", article.Indexing ?? "");
                    parameters.Add("@TagNames", article.TagNames);
                    parameters.Add("@FileAttachment", article.FileAttachment ?? "");
                    parameters.Add("@FileAttachmentMinify", article.FileAttachmentMinify ?? "");
                    parameters.Add("@FileAttachmentUrl", article.FileAttachmentUrl ?? "");
                    parameters.Add("@MetaWebPage", article.MetaWebPage);
                    parameters.Add("@ThiTruong", article.ThiTruong, DbType.String);
                    parameters.Add("@MoHinh", article.MoHinh, DbType.String);
                    parameters.Add("@DuAn", article.DuAn, DbType.String);
                    parameters.Add("@MucDichDauTu", article.MucDichDauTu, DbType.String);
                    var result = await connection.ExecuteAsync("Update_Articles", parameters, null, null,
                        CommandType.StoredProcedure);

                    Log.Error(article.Id.ToString());
                    return article.Id;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return 0;
        }

        public async Task<int> PublishArticle(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var result = await connection.ExecuteAsync("update article set status=2 where id=" + id, null, null,
                        null,
                        CommandType.Text);
                    return 1;
                }
                catch (Exception ex)
                {
                }
            }

            return 0;
        }

        public async Task<int> UnPublishArticle(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var result = await connection.ExecuteAsync("update article set status=1 where id=" + id, null, null,
                        null,
                        CommandType.Text);
                    return 1;
                }
                catch (Exception ex)
                {
                }
            }

            return 0;
        }

        public async Task<ArticlesViewModel> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var sqlGetById = "select * from article where id=" + id;
                    var sqlZoneIdByArticleId =
                        "select ZoneId from ArticlesInZone aiz inner join article a on a.id= aiz.articleid where a.id=" +
                        id;
                    var sqlProductIdByArticleId =
                        "select pia.productid as id,Title as name from ProductInLanguage p inner join ProductInArticle pia on p.productid = pia.productid where p.LanguageCode='vi-VN' and pia.ArticleId=" +
                        id;
                    var sqlArticleInLanguage = "select * from articleinlanguage where ArticleId=" + id;
                    var resultGetById = await
                        connection.QueryAsync<Articles>(sqlGetById, null, null, null, CommandType.Text);
                    var resultGetZoneByArticleId =
                        await connection.QueryAsync<int>(sqlZoneIdByArticleId, null, null, null,
                            CommandType.Text);
                    var resultGetProductIdByArticleId = await
                        connection.QueryAsync<ProductAtArticle>(sqlProductIdByArticleId, null, null, null,
                            CommandType.Text);
                    var resultArticleInLanguage = await connection.QueryAsync<ArticleInLanguageViewModel>(
                        sqlArticleInLanguage, null, null, null,
                        CommandType.Text);
                    var articleInLanguageViewModels = resultArticleInLanguage.ToList();
                    foreach (var item in articleInLanguageViewModels)
                    {
                        if (!string.IsNullOrEmpty(item.Tags))
                        {
                            var resultTag = item.Tags.Split(',').ToList();
                            foreach (var itemj in resultTag)
                            {
                                item.TagsViewModels.Add(new TagsViewModel() { Text = itemj });
                            }
                        }

                        if (string.IsNullOrEmpty(item.MetaData)) continue;
                        var employmentInfomation =
                            JsonConvert.DeserializeObject<ArticleRecruitment>(item.MetaData);
                        item.Address = employmentInfomation.Address;
                        item.Count = employmentInfomation.Count;
                        item.Position = employmentInfomation.Position;
                        item.Salary = employmentInfomation.Salary;
                        item.StartDate = employmentInfomation.StartDate;
                        item.EndDate = employmentInfomation.EndDate;
                        item.phanMoRong = employmentInfomation.phanMoRong;
                    }

                    var articleGetById = new ArticlesViewModel();
                    articleGetById.Articles = resultGetById.SingleOrDefault();
                    articleGetById.ListZoneIds = resultGetZoneByArticleId.ToList();
                    articleGetById.ProductAtArticles = resultGetProductIdByArticleId.ToList();
                    articleGetById.ArticleInLanguageViewModels = articleInLanguageViewModels.ToList();
                    return articleGetById;
                }
                catch (Exception ex)
                {
                }
            }

            return null;
        }

        public async Task<int> Delete(int id)
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
                    parameters.Add("@id", id);
                    var result = await connection.ExecuteAsync("Delete_Articles", parameters, null, null,
                        CommandType.StoredProcedure);
                    return 1;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }

        public async Task<PagedResult<ArticlePagingViewModel>> GetAllPaging(FilterArticle article)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var sql = BuildQuerySearch.CountTotalAndGetSearchRecord(article);
                    var result = connection.QueryMultiple(sql);

                    var resultArticlePagingViewModel = new List<ArticlePagingViewModel>();
                    var totalRow = result.Read<int>().Single();
                    var articles = result.Read<ArticlePagingViewModel>().ToList();
                    foreach (var item in articles)
                    {
                        var sqlCategoryName =
                            "select z.Name from Zone z inner join ArticlesInZone aiz on z.Id=aiz.ZoneId where aiz.ArticleId=" +
                            item.Id;
                        var resultCategoryName = await connection.QueryAsync<string>(
                            sqlCategoryName, null, null, null,
                            CommandType.Text);
                        var categoryNames = string.Join(",", resultCategoryName);
                        item.CategoryName = categoryNames;
                        resultArticlePagingViewModel.Add(item);
                    }

                    resultArticlePagingViewModel =
                        resultArticlePagingViewModel.DistinctBy(x => x.Id).ToList();


                    var articlesPaging = new PagedResult<ArticlePagingViewModel>()
                    {
                        TotalRow = totalRow,
                        Items = resultArticlePagingViewModel,
                        PageIndex = article.pageIndex,
                        PageSize = article.pageSize
                    };

                    return articlesPaging;
                }
                catch (Exception e)
                {
                }
            }

            return new PagedResult<ArticlePagingViewModel>();
        }

        public async Task<List<TagArticlePagingViewModel>> GetAllTag()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var sqlGetAllTag = "select t.Name as Text,t.LanguageCode from tag t";
                    var resultGetAllTag =
                        await connection.QueryAsync<TagArticlePagingViewModel>(sqlGetAllTag, null, null, null,
                            CommandType.Text);
                    return resultGetAllTag.ToList();
                }
                catch (Exception exception)
                {
                }

                return new List<TagArticlePagingViewModel>();
            }
        }

        public List<ExportArticleViewCount> ExportExcelArticleViewCount(string ids, int? year)
        {
            year = year ?? DateTime.Now.Year;
            var result = new List<ExportArticleViewCount>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("@idList", ids);
                    parameters.Add("@objectType", 2);
                    parameters.Add("@year_picker", year);
                    result = connection.Query<ExportArticleViewCount>("usp_Tools_ExportExcelViewCountV1", parameters, null, false, null, CommandType.StoredProcedure).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return result;
            }
        }
    }
}