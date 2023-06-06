using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public class ActTypeServiceAsync:GenericServiceAsync<ActTypeEntity>, IActTypeServiceAsync
    {
        private readonly AdvocateContext advocateContext;
        public ActTypeServiceAsync(AdvocateContext context) : base(context)
        {
            advocateContext = context;
        }

        public bool isActExist(string actName)
        {
            var data = advocateContext.ActTyes.Where(w=>w.IsActive==true &&  w.ActType.ToLower() == actName.ToLower());
            if (data.Count() != 0)
                return true;
            else
                return false;
        }
    }
}
