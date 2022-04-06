using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class ProductSpecificationTemplateInLanguageBCL : Base<ProductSpecificationTemplateInLanguage>
    {
        public ProductSpecificationTemplateInLanguageBCL()
        {

        }

        public List<ProductSpecificationTemplateInLanguage> FindAll(string LanguageCode = "vi-VN")
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.ProductSpecificationTemplateInLanguage.Include(x => x.ProductSpecificationTemplate)
                        .Where(x => x.LanguageCode == LanguageCode).Select(x => new { x.Id, x.Name, x.ProductSpecificationTemplateId, x.ProductSpecificationTemplate.Value, x.ProductSpecificationTemplate.SortOrder, x.ProductSpecificationTemplate.ZoneId }).ToList()
                        .Select(x => new ProductSpecificationTemplateInLanguage { Id = x.Id, ProductSpecificationTemplateId = x.ProductSpecificationTemplateId, Name = x.Name, ProductSpecificationTemplate = new ProductSpecificationTemplate { Value = x.Value, SortOrder = x.SortOrder, ZoneId = x.ZoneId } }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<ProductSpecificationTemplateInLanguage>();
        }
    }
}
