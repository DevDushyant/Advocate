using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public class AppMenuService : GenericServiceAsync<NavigationEntity>, INavigationAsync
    {        
        public AppMenuService(AdvocateContext context):base(context)
        {
            
        }        
    }
}
