using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace MI.Bo.Bussiness
{
    public partial class ArticleBCL : Base<Article>
    {
        public ArticleBCL()
        {

        }

        public bool ExistUrl(string url, int id)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.ArticleInLanguage.Any(x => x.Url == url && x.ArticleId != id);
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public List<Article> GetPage(FilterArticle filter, out int total)
        {
            using (var _context = new IDbContext())
            {
                IQueryable<Article> items = _context.Article.AsQueryable();
                items = items.Include(x => x.ArticleInLanguage);

                //items = items.Where(x => x.LanguageCode.Trim().Equals(filter.languageCode.Trim()));
                if (!String.IsNullOrEmpty(filter.keyword))
                {
                    filter.keyword = filter.keyword.ToLower();
                    items = items.Where(x => x.Name.ToLower().Contains(filter.keyword) || x.ArticleInLanguage.Any(d => d.Title.Contains(filter.keyword)) || x.Id.ToString() == filter.keyword);
                }

                if (filter.Type != Entity.Enums.TypeArticle.All)
                    items = items.Where(x => x.Type == (byte)filter.Type);
                if (filter.Status != Entity.Enums.StatusArticle.All)
                {
                    items = items.Where(x => x.Status == (byte)filter.Status);
                }
                else
                {
                    if (filter.Status != Entity.Enums.StatusArticle.Remove)
                    {
                        items = items.Where(x => x.Status != (byte)Entity.Enums.StatusArticle.Remove);
                    }
                }
                if (filter.ZoneIds.Count > 0)
                {
                    items = items.Include(x => x.ArticlesInZone);
                    items = items.Where(x => x.ArticlesInZone.Any(d => filter.ZoneIds.Contains(d.ZoneId)));
                }
                else if (filter.ZoneId > 0)
                {
                    items = items.Include(x => x.ArticlesInZone);
                    items = items.Where(x => x.ArticlesInZone.Any(d => d.ZoneId == filter.ZoneId));
                }


                total = items.Count();
                filter.pageIndex = filter.pageIndex < 1 ? 1 : filter.pageIndex;
                return items.OrderByDescending(x => x.CreatedDate).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).Select(x => new
                {
                    x.Id,
                    x.Name,
                    LangCount = x.ArticleInLanguage.Count,
                    x.Avatar,
                    x.ArticlesInZone,
                    x.Status,
                    x.CreatedDate,
                    x.ViewCount,
                    ListUrl = x.ArticleInLanguage.Select(d => new KeyValuePair<string, string>(d.LanguageCode.Trim(), d.Url)).ToList()
                }).ToList().Select(d => new Article
                {
                    Id = d.Id,
                    Name = d.Name,
                    LangCount = d.LangCount,
                    Avatar = d.Avatar,
                    ArticlesInZone = d.ArticlesInZone,
                    Status = d.Status,
                    CreatedDate = d.CreatedDate,
                    ListUrl = d.ListUrl,
                    ViewCount = d.ViewCount ?? 0
                }).ToList();

            }
        }

        public Dictionary<int, string> GetAllName()
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                  return  _context.Article.Where(x => x.Status == (byte)(MI.Entity.Enums.StatusArticle.Public))
                       .Select(x => new { x.Id, x.Name }).ToList().ToDictionary(x => x.Id, x => x.Name);
                }
            }
            catch (Exception ex)
            {

            }
            return new Dictionary<int, string>();
        }
        public Dictionary<int, string> GetURLById(List<int> lstId)
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.ArticleInLanguage.Where(x => lstId.Contains(x.ArticleId) && x.LanguageCode.Trim() == Utils.Utility.DefaultLang).Select(x => new { x.ArticleId, x.Url }).ToList().ToDictionary(d => d.ArticleId, d => d.Url);
            }
        }

    }
}
