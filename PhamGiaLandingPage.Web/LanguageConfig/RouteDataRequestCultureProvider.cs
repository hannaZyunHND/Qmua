using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.LanguageConfig
{
    public class RouteDataRequestCultureProvider : RequestCultureProvider
    {
        public int IndexOfCulture;
        public int IndexofUICulture;

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            string culture = null;
            string uiCulture = null;

            var twoLetterCultureName = httpContext.Request.Path.Value.Split('/')[IndexOfCulture]?.ToString();
            var twoLetterUICultureName = httpContext.Request.Path.Value.Split('/')[IndexofUICulture]?.ToString();

            if (twoLetterCultureName == "vi")
                culture = "vi-VN";
            else if (twoLetterCultureName == "en")
                culture = uiCulture = "en-US";

            if (twoLetterUICultureName == "vi")
                culture = "vi-VN";
            else if (twoLetterUICultureName == "en")
                culture = uiCulture = "en-US";

            if (culture == null && uiCulture == null)
                return NullProviderCultureResult;

            if (culture != null && uiCulture == null)
                uiCulture = culture;

            if (culture == null && uiCulture != null)
                culture = uiCulture;

            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

            return Task.FromResult(providerResultCulture);
        }

        //public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
