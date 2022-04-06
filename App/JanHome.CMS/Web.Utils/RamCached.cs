using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanHome.CMS.Web.Utils
{
    public class RamCached
    {
        public static int timeCache = 2;
        #region Mail Camp

        public static Dictionary<string, MI.Entity.Models.Language> _DicLanguages { get; set; }
        public static DateTime LastestLoadLanguages = new DateTime(2000, 01, 01);

        public static Dictionary<string, MI.Entity.Models.Language> DicLanguages
        {
            get
            {
                if (_DicLanguages == null || _DicLanguages.Count <= 0 || LastestLoadLanguages.AddDays(timeCache) < DateTime.Now)
                {
                    var languages = new MI.Bo.Bussiness.LanguageBCL();

                    _DicLanguages = languages.FindAll().ToDictionary(x => x.LanguageCode, x => x);
                    if (_DicLanguages == null)
                    {
                        _DicLanguages = new Dictionary<string, MI.Entity.Models.Language>();
                    }
                    LastestLoadLanguages = DateTime.Now;
                }

                return _DicLanguages;
            }
        }

        #endregion Mail Camp

    }
}
