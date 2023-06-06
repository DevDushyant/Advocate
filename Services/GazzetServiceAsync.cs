using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public class GazzetServiceAsync : GenericServiceAsync<GazetteTypeEntity>, IGazzetServiceAsync
    {
        private readonly AdvocateContext advocateContext;
        public GazzetServiceAsync(AdvocateContext context) : base(context)
        {
            advocateContext = context;
        }

        public bool isGazzetExist(string GazzetName)
        {
            var result = advocateContext.gazetteTypeEntities.Where(c => c.GazetteName.ToLower().Trim() == GazzetName.ToLower().Trim());
            if (result.Count() != 0)
                return true;
            else
                return false;
        }
    }
}
