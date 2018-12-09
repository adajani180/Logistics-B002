using Logistics.Areas.Config.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Logistics.Helpers
{
    public class MiscHelper
    {
        public static IEnumerable<SelectListItem> GetLookupSelectListItems(string lookupType, long? selectedId)
        {
            LookupRepository repo = new LookupRepository();
            return repo.Find(lookup => lookup.Type.ToLower().Equals(lookupType.ToLower()))
                .Select(lookup => new SelectListItem
                {
                    Value = lookup.Id.ToString(),
                    Text = lookup.Name,
                    Selected = (lookup.Id == selectedId)
                });
        }
    }
}