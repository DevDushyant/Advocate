using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using DocumentFormat.OpenXml.InkML;
using Npgsql.Bulk;
using System.Collections.Generic;

namespace Advocate.Services
{
	public class EGazzetDataService : GenericServiceAsync<BookEntity>, IEGazzetDataServiceAsync
	{
		private readonly AdvocateContext dbContext;
		public EGazzetDataService(AdvocateContext _context) : base(_context)
		{
			this.dbContext = _context;
		}

		public bool BulkUpload(List<EGazzetDataEntity> dataEntity)
		{
			var uploader = new NpgsqlBulkUploader(dbContext);
			uploader.Insert(dataEntity);			
			return true;
		}

		 

		public int DeleteAsync(EGazzetDataEntity obj)
		{
			throw new System.NotImplementedException();
		}

		public int SaveAsync(EGazzetDataEntity obj)
		{
			throw new System.NotImplementedException();
		}

		public int UpdateAsync(EGazzetDataEntity obj)
		{
			throw new System.NotImplementedException();
		}

		EGazzetDataEntity IGenericServiceAsync<EGazzetDataEntity>.FindByIdAsync(int Id)
		{
			throw new System.NotImplementedException();
		}

		IEnumerable<EGazzetDataEntity> IGenericServiceAsync<EGazzetDataEntity>.GetAllAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}
