using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class PromotionInLanguageBCL : Base<PromotionInLanguage>
    {

        public PromotionInLanguageBCL()
        {

        }
        public List<ProductInLanguage> Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            total = 0;

            return new List<ProductInLanguage>();
        }
        public List<PromotionInLanguage> GetByPromotionId(int promotionId, string languageCode)
        {
            List<PromotionInLanguage> promotionInLanguages = new List<PromotionInLanguage>();
            try
            {

                using (IDbContext _context = new IDbContext())
                {
                    promotionInLanguages = _context.PromotionInLanguage.Where(n => n.PromotionId == promotionId && n.LanguageCode == languageCode).ToList();
                    return promotionInLanguages;
                }
            }
            catch (Exception ex)
            {

            }
            return new List<PromotionInLanguage>();

        }
        public bool Merge(PromotionInLanguage entity)
        {
            bool respone = false;
            try
            {

                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<PromotionInLanguage>(new List<PromotionInLanguage> { entity });
                    respone = true;
                }
            }
            catch (Exception ex)
            {

            }
            return respone;
        }
    }
}
