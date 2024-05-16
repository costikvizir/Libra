using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace LibraWebApp.Common
{
    public interface IViewRenderService
    {
        Task<string> RenderToString(string viewName, object model, ModelStateDictionary modelStateDictionary, ExpandoObject viewBag);
    }
}
