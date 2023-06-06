using Advocate.Dtos;
using Advocate.Entities;
using System.Collections.Generic;

namespace Advocate.Interfaces
{
    public interface IActServiceAsync:IGenericServiceAsync<ActEntity>
    {
        IEnumerable<ActDto> GetAllActs();
        int SaveAmendedAct(List<AmendedActEntity>selectedActId);
        int SaveRepealedAct(List<RepealedActEntity> selectedActId);
        int SaveActBook(List<ActBookEntity> actBookEntities);
        ActEntity GetActDetailByActId(string userid, int ActId);
        ActEntity LastInsertedData(string userid);
        int GetActIdByActType_Number_year(int TypeId, int ActNumber, int Year);
        List<ActRepealedInfoDto> GetActInfoByRepealedAct(int actId);
        int Update_RepealedActByID(int actId, List<int> repealedAct,string userid);
        int Update_AmendedActByID(int actId, List<int> amendedAct, string userid);
        int Update_ActBookByID(int actId, List<ActBookEntity> actBook, string userid);
        ActDetailDescriptionDto GetActDetailInfoByActId(string userId,int Id);
    }
}
