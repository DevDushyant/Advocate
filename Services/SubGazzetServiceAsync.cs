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
    public class SubGazzetServiceAsync : GenericServiceAsync<PartEntity>, ISubGazzetServiceAsync
    {
        private readonly AdvocateContext advocateContext;
        public SubGazzetServiceAsync(AdvocateContext context) : base(context)
        {
            advocateContext = context;
        }

        public IEnumerable<PartEntity> GetAllPartsWithGazette(string userid)
        {
            var result = advocateContext.PartEntities.Where(u=>u.UserID==userid && u.IsActive==true)  //main table
                        .Join(advocateContext.gazetteTypeEntities,  //inner join table
                        gId => gId.GazettId,   // inner join condition
                        sgg => sgg.Id,   //outer joint condition
                        (gid, sgg) => new PartEntity   //retriving the record
                        {
                            Id=gid.Id,
                            PartName = gid.PartName,
                            Description = gid.Description,
                            gazetteTypeEntity = new GazetteTypeEntity()
                            {
                                Id = sgg.Id,
                                GazetteName = sgg.GazetteName
                            }
                        }).ToList();
            return result;
        }

        public PartEntity GetPartDetailByid(string userid, int subgazzetId)
        {
          return  advocateContext.PartEntities.Where(u => u.UserID == userid && u.IsActive == true && u.Id==subgazzetId)  //main table
                        .Join(advocateContext.gazetteTypeEntities,  //inner join table
                        gId => gId.GazettId,   // inner join condition
                        sgg => sgg.Id,   //outer joint condition
                        (gid, sgg) => new PartEntity   //retriving the record
                        {
                            Id = gid.Id,
                            PartName = gid.PartName,
                            Description = gid.Description,
                            gazetteTypeEntity = new GazetteTypeEntity()
                            {
                                Id = sgg.Id,
                                GazetteName = sgg.GazetteName
                            }
                        }).SingleOrDefault();
        }

        public bool isSubGazzetExist(int GazettID, string subGazetteName)
        {
            var result = advocateContext.PartEntities.Where(c => c.GazettId == GazettID && c.PartName.ToLower().Trim() == subGazetteName.ToLower().Trim());
            if (result.Count() != 0)
                return true;
            else
                return false;
        }
    }
}
