using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MI.Bo.Bussiness
{
    public partial class TagInProductBCL : Base<TagInProductLanguage>
    {
        public TagInProductBCL()
        {

        }

        public Dictionary<string, List<string>> FindByProductId(List<string> Id)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var data = _context.TagInProductLanguage.Join(_context.Tag, p => p.TagId, t => t.Id, (pt, tt) => new { Post = pt, Meta = tt }).Where(x => Id.Contains(x.Post.ProductInLanguageId)).Select(x => new { x.Post.ProductInLanguageId, x.Meta.Name }).ToList();

                    return data.GroupBy(x => x.ProductInLanguageId).ToDictionary(x => x.Key, x => x.Select(dx => dx.Name).ToList());
                    //var dic = data.ToDictionary(x => x.Key, x => x.Select(d => d.Tag.Name).ToList());


                }
            }
            catch (Exception ex)
            {

            }
            return new Dictionary<string, List<string>>();
        }
    }
}
