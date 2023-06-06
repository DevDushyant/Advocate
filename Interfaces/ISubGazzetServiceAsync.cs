using Advocate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
    public interface ISubGazzetServiceAsync : IGenericServiceAsync<PartEntity>
    {
        bool isSubGazzetExist(int GazettID, string subGazetteName);

        IEnumerable<PartEntity> GetAllPartsWithGazette(string userid);

        PartEntity GetPartDetailByid(string userid, int subgazzetId);
    }
}
