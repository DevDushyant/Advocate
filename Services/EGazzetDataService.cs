using Advocate.Data;
using Advocate.Dtos;
using Advocate.Entities;
using Advocate.Interfaces;
using DocumentFormat.OpenXml.InkML;
using LinqKit;
using Npgsql.Bulk;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public List<EGazzetDataEntity> EGazzetData(EGazzetSearchDto param)
        {

            var predicate = PredicateBuilder.New<EGazzetDataEntity>(true);
            if (!string.IsNullOrEmpty(param.Category))
                predicate.And(p => p.category.Equals(param.Category));

            if (!string.IsNullOrEmpty(param.Department))
                predicate.And(p => p.department.Equals(param.Department));

            if (!string.IsNullOrEmpty(param.part_section))
                predicate.And(p => p.part_section.Equals(param.part_section));

            var result = dbContext.EGazzetDataEntities.AsNoTracking().AsExpandable();
            result.Where(predicate);
            return result.ToList();
        }

        public List<DdlDto> GetDeaprtmentDataList()
        {
            var ddlDepartment = dbContext.EGazzetDataEntities.Select(s => new DdlDto
            {
                value = s.department,
                text = s.department
            }).Distinct().OrderBy(s => s.text).ToList();
            return ddlDepartment;
        }
        public List<DdlDto> GetCategory()
        {
            var ddlCategory = dbContext.EGazzetDataEntities.Select(s => new DdlDto
            {
                value = s.category,
                text = s.category
            }).Distinct().OrderBy(s => s.text).ToList();
            return ddlCategory;
        }
        public List<DdlDto> GetPart()
        {
            var ddlPart = dbContext.EGazzetDataEntities.Select(s => new DdlDto
            {
                value = s.part_section,
                text = s.part_section
            }).Distinct().OrderBy(s => s.text).ToList();
            return ddlPart;
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
