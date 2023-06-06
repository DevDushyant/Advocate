using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Advocate.Services
{
    public class SubjectServiceAsync : GenericServiceAsync<SubjectEntity>, ISubjectServiceAsync
    {
        private readonly AdvocateContext advocateContext;
        public SubjectServiceAsync(AdvocateContext context) : base(context)
        {
            this.advocateContext = context;
        }

        public new int SaveAsync(SubjectEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                int maxSubjectInsertedId = advocateContext.Subjects.MaxAsync(id => id.Id).Result;
                entity.Id = maxSubjectInsertedId + 1;
                advocateContext.Subjects.Add(entity);
                advocateContext.SaveChanges();
                return entity.Id;
            }
        }
    }
}
