using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Filter
{
    public class ClaimRequirementAttribute: TypeFilterAttribute
    {
        public ClaimRequirementAttribute(FunctionCode function, ActionCode action) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { function, action };
        }
    }
}
