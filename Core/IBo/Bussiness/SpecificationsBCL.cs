using MI.Dal.IDbContext;
using MI.Entity.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class SpecificationsBCL : Base<MaintainSpectifications>
    {
        public SpecificationsBCL()
        {

        }
        public List<MaintainSpectifications> GetMaintainSpectifications()
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var data = _context.MaintainSpectifications.ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return null;
            }
        }
        public Dictionary<int, List<MaintainSpectifications>> GetAll(List<int> lstExist)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var data = _context.MaintainSpectifications.Join(
                        _context.MaintainSpectificationTemplate, spec => spec.Id, specin => specin.SpectificationId,
                        (spec, specin) => new { Post = spec, Meta = specin }).Where(x => !lstExist.Contains(x.Meta.Id)).GroupBy(x => x.Meta.ZoneId.Value).ToDictionary(x => x.Key, x => x.Select(a => a.Post).ToList());

                    return data;
                }
            }
            catch (Exception ex)
            {

            }
            return new Dictionary<int, List<MaintainSpectifications>>();
        }
    }
}
