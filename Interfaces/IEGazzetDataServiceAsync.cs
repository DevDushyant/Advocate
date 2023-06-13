using Advocate.Entities;
using System.Collections.Generic;

namespace Advocate.Interfaces
{
	public interface IEGazzetDataServiceAsync : IGenericServiceAsync<EGazzetDataEntity>
	{
		bool BulkUpload(List<EGazzetDataEntity> gazzets);
	}
}
