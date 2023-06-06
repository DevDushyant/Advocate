using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public class NotificationTypeServiceAsync : GenericServiceAsync<NotificationTypeEntity>, INotifcationTypeAsyncService
    {
        private readonly AdvocateContext advocateContext;
        public NotificationTypeServiceAsync(AdvocateContext context) : base(context)
        {
            this.advocateContext = context;
        }

        //public new int SaveAsync(NotificationTypeEntity entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }
        //    else
        //    {
        //        int maxSubjectInsertedId = advocateContext.NotificationTypes.MaxAsync(id => id.Id).Result;
        //        entity.Id = maxSubjectInsertedId + 1;
        //        advocateContext.NotificationTypes.Add(entity);
        //        advocateContext.SaveChanges();
        //        return entity.Id;
        //    }
        //}
    }
}
