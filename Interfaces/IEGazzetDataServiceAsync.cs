using Advocate.Dtos;
using Advocate.Entities;
using System.Collections.Generic;

namespace Advocate.Interfaces
{
	public interface IEGazzetDataServiceAsync : IGenericServiceAsync<EGazzetDataEntity>
	{
		bool BulkUpload(List<EGazzetDataEntity> gazzets);
		List<DdlDto> GetDeaprtmentDataList();
		List<DdlDto>  GetPart();
		List<DdlDto> GetCategory();
		List<EGazzetDataEntity> EGazzetData(EGazzetSearchDto param);


	}
}
