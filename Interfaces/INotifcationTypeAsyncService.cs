using Advocate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
   public interface INotifcationTypeAsyncService : IGenericServiceAsync<NotificationTypeEntity>
    {
       //new int SaveAsync(NotificationTypeEntity obj);
    }
}
