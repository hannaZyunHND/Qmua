using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Dal.IDbContext;
using MI.Entity.Enums;
using MI.Entity.Models;
using MI.Entity.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        CommentBCL commentBCL;
        CustomerBCL customerBCL;

        ProductInLanguageBCL productBCL;
        ArticleBCL articleCL;
        ZoneInLanguageBCL zoneBCL;
        IDbContext context;

        public CommentController()
        {
            commentBCL = new CommentBCL();
            customerBCL = new CustomerBCL();

            productBCL = new ProductInLanguageBCL();
            articleCL = new ArticleBCL();
            zoneBCL = new ZoneInLanguageBCL();
            context = new IDbContext();
        }

        [HttpPost]
        [Route("FakeComment")]
        public IActionResult CreateFAkeComment(CreateFakeCommnetViewComponent merging)
        {
            Console.WriteLine(merging);
            //Tao commnet o day
            var res = new List<Comment>();
            var objs = merging.objs;
            var cms = merging.comments;
            foreach(var ob in objs)
            {
                foreach(var cm in cms)
                {
                    var r = new Comment();
                    r.ParentId = 0;
                    r.Status = 2;
                    r.ModifiedBy = "";
                    r.ModifiedDate = DateTime.Now;
                    r.CreatedDate = DateTime.Now;
                    r.CreatedByCustomerId = 0;
                    r.CreatedByAdminId = "admin";
                    r.Content = cm.Description;
                    r.ObjectId = ob.productId;
                    r.ObjectType = ob.type;
                    r.Name = cm.Name;
                    r.PhoneOrMail = "";
                    r.Avatar = "";
                    r.Type = cm.Type;
                    if (r.Type.Equals("comment"))
                        r.Rate = 0;
                    if (r.Type.Equals("rating"))
                        r.Rate = cm.Rating == 0 ? 5 : cm.Rating;

                    res.Add(r);
                }
            }
            Console.WriteLine(res);
            try
            {
                context.Comment.AddRange(res);
                context.SaveChanges();
                return Ok("Success");
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }




        [HttpPost("Get")]
        public IActionResult Get([FromBody] Utils.FilterComment filter)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            var total = 0;
            var data = commentBCL.Find(filter, out total);

            var GetChild = commentBCL.FindAll(x => data.Any(d => d.Id == x.ParentId)).GroupBy(x => x.ParentId).ToDictionary(x => x.Key, x => x.Select(d => d).ToList());

            var DicProduct = new Dictionary<int, string>();
            var DicArticle = new Dictionary<int, string>();
            var DicZone = new Dictionary<int, string>();
            if (data.Any(d => d.ObjectType == ((int)CommentType.Product).ToString()))
            {
                var lstId = data.Where(d => d.ObjectType == ((int)CommentType.Product).ToString()).Select(x => x.ObjectId ?? 0).ToList();
                DicProduct = productBCL.GetURLById(lstId.Distinct().ToList());
            }
            if (data.Any(d => d.ObjectType == ((int)CommentType.Article).ToString()))
            {
                var lstId = data.Where(d => d.ObjectType == ((int)CommentType.Article).ToString()).Select(x => x.ObjectId ?? 0).ToList();
                DicArticle = articleCL.GetURLById(lstId.Distinct().ToList());
            }
            if (data.Any(d => d.ObjectType == ((int)CommentType.ArticleZone).ToString() || d.ObjectType == ((int)CommentType.ProductZone).ToString()))
            {
                var lstId = data.Where(d => d.ObjectType == ((int)CommentType.ArticleZone).ToString() || d.ObjectType == ((int)CommentType.ProductZone).ToString()).Select(x => x.ObjectId ?? 0).ToList();
                DicZone = zoneBCL.GetURLById(lstId.Distinct().ToList());
            }
            //var a = (
            //        from d in data
            //        join pil in _context.ProductInLanguage on d.ObjectId equals pil.ProductId
            //        where int.Parse(d.ObjectType) == (int)CommentType.Product
            //        select (new
            //        {
            //            d.Id,
            //            d.ObjectId,
            //            d.ObjectType,
            //            d.ParentId,
            //            d.Status,
            //            d.CreatedDate,
            //            d.CreatedByAdminId,
            //            d.Content,
            //            d.Name,
            //            d.PhoneOrMail,
            //            d.Type,
            //            d.Rate,
            //            Url = Utils.BaseBA.UrlProduct("", pil.Url),
            //            Child = GetChild.ContainsKey(d.Id) ? GetChild[d.Id] : new List<Comment>()
            //        })
            //        )
            //        .Union
            //        (
            //        from d in data
            //        join ail in _context.ArticleInLanguage on d.ObjectId equals ail.ArticleId
            //        where int.Parse(d.ObjectType) == (int)CommentType.Article
            //        select (new
            //        {
            //            d.Id,
            //            d.ObjectId,
            //            d.ObjectType,
            //            d.ParentId,
            //            d.Status,
            //            d.CreatedDate,
            //            d.CreatedByAdminId,
            //            d.Content,
            //            d.Name,
            //            d.PhoneOrMail,
            //            d.Type,
            //            d.Rate,
            //            Url = Utils.BaseBA.UrlProduct("", ail.Url),
            //            Child = GetChild.ContainsKey(d.Id) ? GetChild[d.Id] : new List<Comment>()
            //        })
            //        )
            //        .Union
            //        (
            //            from d in data
            //            join ail in _context.ZoneInLanguage on d.ObjectId equals ail.ZoneId
            //            where (int.Parse(d.ObjectType) == (int)CommentType.ArticleZone) || (int.Parse(d.ObjectType) == (int)CommentType.ProductZone)
            //            select (new
            //            {
            //                d.Id,
            //                d.ObjectId,
            //                d.ObjectType,
            //                d.ParentId,
            //                d.Status,
            //                d.CreatedDate,
            //                d.CreatedByAdminId,
            //                d.Content,
            //                d.Name,
            //                d.PhoneOrMail,
            //                d.Type,
            //                d.Rate,
            //                Url =  Utils.BaseBA.UrlProduct("", ail.Url),
            //                Child = GetChild.ContainsKey(d.Id) ? GetChild[d.Id] : new List<Comment>()
            //            })
            //        ).ToList();
            //response.listData = a;

            var et = Utils.Settings.AppSettings.GetByKey("et") == "1";

            response.listData = data.Select(x => new
            {
                x.Id,
                x.ObjectId,
                x.ObjectType,
                x.ParentId,
                x.Status,
                x.CreatedDate,
                x.CreatedByAdminId,
                x.Content,
                x.Name,
                x.PhoneOrMail,
                x.Type,
                x.Rate,
                Url = et ? BindUrlEnterBuy(x.ObjectId ?? 0, x.ObjectType, DicProduct, DicArticle, DicZone) : BindUrl(x.ObjectId ?? 0, x.ObjectType, DicProduct, DicArticle, DicZone),
                Child = GetChild.ContainsKey(x.Id) ? GetChild[x.Id] : new List<Comment>()
            }).ToList();
            response.total = total;
            return Ok(response);
        }

        public string BindUrl(int id, string objectType, Dictionary<int, string> Product, Dictionary<int, string> Article, Dictionary<int, string> Zone)
        {
            string url = string.Empty;
            string urlBase = string.Empty;
            switch (int.Parse(objectType))
            {
                case (int)CommentType.Product:
                    urlBase = Product.ContainsKey(id) ? Product[id] : "";
                    if (!String.IsNullOrEmpty(urlBase))
                        url = Utils.BaseBA.UrlProductJanhome("", urlBase, Utils.Settings.AppSettings.BaseDomain);
                    break;
                case (int)CommentType.Article:
                    urlBase = Article.ContainsKey(id) ? Article[id] : "";
                    if (!String.IsNullOrEmpty(urlBase))
                        url = Utils.BaseBA.UrlProductJanhome("", urlBase, Utils.Settings.AppSettings.BaseDomain);
                    break;
                case (int)CommentType.ArticleZone:
                    urlBase = Zone.ContainsKey(id) ? Zone[id] : "";
                    if (!String.IsNullOrEmpty(urlBase))
                        url = Utils.BaseBA.UrlNewsJanhome("", urlBase, id, Utils.Settings.AppSettings.BaseDomain);
                    break;
                case (int)CommentType.ProductZone:
                    urlBase = Zone.ContainsKey(id) ? Zone[id] : "";
                    if (!String.IsNullOrEmpty(urlBase))
                        url = Utils.BaseBA.UrlCategoryJanHome(urlBase, Utils.Settings.AppSettings.BaseDomain);
                    break;
            }
            return url;
        }
        public string BindUrlEnterBuy(int id, string objectType, Dictionary<int, string> Product, Dictionary<int, string> Article, Dictionary<int, string> Zone)
        {
            string url = string.Empty;
            string urlBase = string.Empty;
            switch (int.Parse(objectType))
            {
                case (int)CommentType.Product:
                    urlBase = Product.ContainsKey(id) ? Product[id] : "";
                    if (!String.IsNullOrEmpty(urlBase))
                        url = Utils.BaseBA.UrlProduct("", urlBase, Utils.Settings.AppSettings.BaseDomain);
                    break;
                case (int)CommentType.Article:
                    urlBase = Article.ContainsKey(id) ? Article[id] : "";
                    if (!String.IsNullOrEmpty(urlBase))
                        url = Utils.BaseBA.UrlNews(urlBase, Utils.Settings.AppSettings.BaseDomain);
                    break;
                case (int)CommentType.ArticleZone:
                    urlBase = Zone.ContainsKey(id) ? Zone[id] : "";
                    if (!String.IsNullOrEmpty(urlBase))
                        url = Utils.BaseBA.UrlCategoryNews(urlBase, Utils.Settings.AppSettings.BaseDomain);
                    break;
                case (int)CommentType.ProductZone:
                    urlBase = Zone.ContainsKey(id) ? Zone[id] : "";
                    if (!String.IsNullOrEmpty(urlBase))
                        url = Utils.BaseBA.UrlCategory(urlBase, Utils.Settings.AppSettings.BaseDomain);
                    break;
            }
            return url;
        }

        [HttpPut("UpdateStatus")]
        public IActionResult UpdateStatus([FromBody] Comment obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            var data = commentBCL.UpdateStatus(obj);
            if (data == true)
            {
                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Comment obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            obj.ObjectType = obj.ObjectType.ToString();
            var data = commentBCL.Add(obj);
            if (data == true)
            {

                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] Comment obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            var data = commentBCL.Remove(obj);
            if (data == true)
            {
                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
    }
}
