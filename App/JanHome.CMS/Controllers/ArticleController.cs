using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JanHome.CMS.Filter;
using MI.Bo.Bussiness;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Entity.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using MI.Cache;
using StackExchange.Redis;
using Utils;
using System.Text;
using System.Web;
using JanHome.CMS.ExcelHelper;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        ArticleBCL _articleBcl;
        ZoneBCL _zoneBCL;
        IExportUtility _exportUtility;
        private readonly IArticlesRepository _articlesRepository;
        private readonly IZoneArticleRepository _zoneArticleRepository;
        private readonly IProductRepository _productRepository;


        public ArticleController(IArticlesRepository articlesRepository,
            IZoneArticleRepository zoneArticleRepository, IProductRepository productRepository, IExportUtility exportUtility)
        {
            _articlesRepository = articlesRepository;
            _zoneArticleRepository = zoneArticleRepository;
            _productRepository = productRepository;
            _articleBcl = new ArticleBCL();
            _exportUtility = exportUtility;
            _zoneBCL = new ZoneBCL();
        }
        [HttpGet("getalltagv2")]
        public async Task<IActionResult> GetAlltagCache()
        {
            var tags = await _articlesRepository.GetAllTag();

            var keyCache = string.Format("", "param1", "param2", "param3");
            //var result = _distributedCache.GetOrSetCache(keyCache, () => tags, _configuration);
            var result = tags;
            return Ok(result);
        }
        [HttpPost("genIndexing")]
        public IActionResult GenIndexing([FromBody] ViewModel.ArticleIndexing obj)
        {
            try
            {
                obj.Html = HttpUtility.HtmlDecode(obj.Html);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(obj.Html);

                string xpathQuery = "//*[starts-with(name(),'h') and string-length(name()) = 2 and number(substring(name(), 2)) <= 6]";
                var texts = doc.DocumentNode.SelectNodes(xpathQuery);
                if (texts != null)
                {
                    string title = string.Empty;
                    if (obj.Language.Trim() == "vi-VN")
                        title = "Nội dung chính bài Viết ";
                    else
                        title = "The main content of the article ";
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"<div id='toc_container' class='no_bullets'> <p class='toc_title'><span class='toc_toggle'>{title} <a href='#'><i class=\"fa fa-sort-desc\" aria-hidden=\"true\"></i></a></span></p><ul class='toc_list'>");
                    int h2 = 0, h3 = 0, h4 = 0, h5 = 0;
                    StringBuilder strh2 = new StringBuilder();
                    StringBuilder strh3 = new StringBuilder();
                    StringBuilder strh4 = new StringBuilder();
                    StringBuilder strh5 = new StringBuilder();
                    for (int i = 0; i < texts.Count; i++)
                    {
                        string id = Utils.Utility.GenerateSlug(texts[i].InnerText.Replace("\n", "").Replace("  ", " "));
                        texts[i].SetAttributeValue("id", id);
                        switch (texts[i].Name)
                        {
                            case "h2":
                                if (!String.IsNullOrEmpty(strh2.ToString()))
                                {
                                    if (!String.IsNullOrEmpty(strh5.ToString()))
                                        strh4.Replace("[#Child]", "<ul class=\"pl-2\">" + strh5.ToString() + "</ul>");
                                    if (!String.IsNullOrEmpty(strh4.ToString()))
                                        strh3.Replace("[#Child]", "<ul class=\"pl-2\">" + strh4.ToString() + "</ul>");
                                    if (!String.IsNullOrEmpty(strh3.ToString()))
                                        strh2.Replace("[#Child]", "<ul class=\"pl-2\">" + strh3.ToString() + "</ul>");
                                    strh2.Replace("[#Child]", "");
                                    sb.Append(strh2.ToString());
                                    h3 = 0; h4 = 0; h5 = 0;
                                    strh2 = new StringBuilder(); strh3 = new StringBuilder(); strh4 = new StringBuilder(); strh5 = new StringBuilder();
                                }
                                h2++;
                                strh2.Append($"<li><a href='#{id}'><span class='toc_number toc_depth_1'>{h2} </span> {texts[i].InnerText}</a>[#Child]</li>");
                                break;
                            case "h3":
                                h3++;
                                strh3.Append($"<li><a href='#{id}'><span class='toc_number toc_depth_2'>{h2}.{h3} </span> {texts[i].InnerText}</a>[#Child]</li>");
                                break;
                            case "h4":
                                h4++;
                                strh4.Append($"<li><a href='#{id}'><span class='toc_number toc_depth_3'>{h2}.{h3}.{h4} </span> {texts[i].InnerText}</a>[#Child]</li>");
                                break;
                            case "h5":
                                h5++;
                                strh5.Append($"<li><a href='#{id}'><span class='toc_number toc_depth_4'>{h2}.{h3}.{h4}.{h5} </span> {texts[i].InnerText}</a>[#Child]</li>");
                                break;
                        }
                    }
                    if (!String.IsNullOrEmpty(strh2.ToString()))
                    {
                        if (!String.IsNullOrEmpty(strh5.ToString()))
                            strh4.Replace("[#Child]", "<ul class=\"pl-2\">" + strh5.ToString() + "</ul>");
                        if (!String.IsNullOrEmpty(strh4.ToString()))
                            strh3.Replace("[#Child]", "<ul class=\"pl-2\">" + strh4.ToString() + "</ul>");
                        if (!String.IsNullOrEmpty(strh3.ToString()))
                            strh2.Replace("[#Child]", "<ul class=\"pl-2\">" + strh3.ToString() + "</ul>");
                        strh2.Replace("[#Child]", "");
                        sb.Append(strh2.ToString());
                        h3 = 0; h4 = 0; h5 = 0;
                        strh2 = new StringBuilder(); strh3 = new StringBuilder(); strh4 = new StringBuilder(); strh5 = new StringBuilder();
                    }
                    sb.Append("</ul></div>");
                    obj.Indexing = sb.ToString();
                    obj.Html = doc.DocumentNode.OuterHtml;
                }

            }
            catch (Exception ex)
            { }
            return Ok(obj);
        }

        [HttpGet("getalltag")]
        public async Task<IActionResult> GetAlltag()
        {
            var result = await _articlesRepository.GetAllTag();
            return Ok(result);
        }

        [HttpGet("GetAll")]
        [ClaimRequirement(FunctionCode.ARTICLE, ActionCode.VIEW)]
        public ResponseData GetAll()
        {
            var responseData = new ResponseData();
            try
            {
                var data = _articleBcl.FindAll().ToList();
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.ListData = data.ToList<object>();
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPost("publish")]
        [ClaimRequirement(FunctionCode.ARTICLE, ActionCode.PUBLISH)]
        public async Task<ResponseData> Publish(int id)
        {
            var responseData = new ResponseData();
            try
            {
                var data = await _articlesRepository.PublishArticle(id);
                if (data > 0)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = "Không Thành công";
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPost("unpublish")]
        [ClaimRequirement(FunctionCode.ARTICLE, ActionCode.UNPUBLISH)]
        public async Task<ResponseData> UnPublish(int id)
        {
            var responseData = new ResponseData();
            try
            {
                var data = await _articlesRepository.UnPublishArticle(id);
                if (data > 0)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = "Không Thành công";
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }


        [HttpPost("GetPage")]
        //[ClaimRequirement(FunctionCode.ARTICLE, ActionCode.VIEW)]
        public ResponsePaging GetPage([FromBody] FilterArticle filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;

                var data = _articleBcl.GetPage(filter, out total);
                var lstId = data.Select(x => x.ArticlesInZone).SelectMany(d => d).ToList().Select(x => x.ZoneId);
                var dicZone = _zoneBCL.GetById(lstId.Distinct().ToList(), filter.languageCode)
                    .ToDictionary(x => x.Id, z => z.Name);

                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    Name = x.Name,
                    Lang = x.LangCount,
                    x.Avatar,
                    x.Status,
                    x.CreatedDate,
                    x.ViewCount,
                    category = Utils.Utility.BuildZone(x.ArticlesInZone.Select(d => d.ZoneId).ToList(), dicZone),
                    BaseUrl = x.ListUrl.Select(d => new KeyValuePair<string, string>(d.Key, $"{Utils.Utility.BaseDomain(Utils.BaseBA.UrlNewsJanhome("", d.Value, x.Id))}")).ToList()
                }).ToList<object>();
                responseData.Total = total;
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPost("ExportViewCount")]
        public IActionResult ExportExcelViewCoutArticle(FilterArticleWithMonth filter)
        {
            var r = _exportUtility.ExportViewCountArticle(filter);
            return Ok(r);
        }
        
        [HttpGet("GetById")]
        [ClaimRequirement(FunctionCode.ARTICLE, ActionCode.UPDATE)]
        public async Task<ResponseData> GetById(int id)
        {
            var responseData = new ResponseData();
            try
            {
                var result = await _articlesRepository.GetById(id);
                if (result != null)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                    responseData.Data = result;
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPost("Add")]
        [ClaimRequirement(FunctionCode.ARTICLE, ActionCode.CREATE)]
        public async Task<ResponseData> Add([FromBody] Articles article)
        {
            var responseData = new ResponseData();
            try
            {
                var response = Utils.Utility.AutoCheckSlug(article.Title, article.Url);
                if (response.Key)
                {
                    article.Url = response.Value;
                    var exist = _articleBcl.ExistUrl(article.Url, article.Id);
                    if (!exist)
                    {
                        article.Url = UIHelper.ChuyenCoDauThanhKhongCoDau(article.Url);
                        var result = await _articlesRepository.Create(article);
                        if (result > 0)
                        {
                            responseData.Id = result;
                            responseData.Success = true;
                            responseData.Message = "Thành công";
                        }
                    }
                    else
                    {
                        responseData.Message = "Đường dẫn hiện đã tồn tại";
                    }

                }
                else
                {
                    responseData.Message = response.Value;
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPut("Update")]
        [ClaimRequirement(FunctionCode.ARTICLE, ActionCode.UPDATE)]
        public async Task<ResponseData> Update([FromBody] Articles article)
        {
            var responseData = new ResponseData();
            try
            {
                var response = Utils.Utility.AutoCheckSlug(article.Title, article.Url);
                if (response.Key)
                {
                    article.Url = response.Value;
                    var exist = _articleBcl.ExistUrl(article.Url, article.Id);
                    if (!exist)
                    {
                        article.Url = UIHelper.ChuyenCoDauThanhKhongCoDau(article.Url);
                        var result = await _articlesRepository.Update(article);
                        if (result > 0)
                        {

                            responseData.Success = true;
                            responseData.Message = "Thành công";
                        }
                    }
                    else
                    {
                        responseData.Message = "Đường dẫn hiện đã tồn tại";
                    }
                }
                else
                {
                    responseData.Message = response.Value;
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpDelete("Delete")]
        public async Task<ResponseData> Delete(int id)
        {
            var responseData = new ResponseData();
            try
            {
                var ads = await _articlesRepository.Delete(id);
                if (ads > 0)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = "Không Thành công";
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpGet("GetZoneArticle")]
        public async Task<List<Group>> GetZoneArticle()
        {
            try
            {
                var result = await _zoneArticleRepository.GetAllZoneArticles();
                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
            }

            return new List<Group>();
        }

        [HttpGet("GetProductAtArticle")]
        public async Task<List<ProductAtArticle>> GetProductAtArticle()
        {
            try
            {
                var result = await _productRepository.GetAllProduct();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



    }
}
