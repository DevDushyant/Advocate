using Advocate.Dtos;
using Advocate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
    public interface IBookServiceAsync : IGenericServiceAsync<BookEntity>
    {
        public bool isActExist(string bookName);
        public IEnumerable<DropDownDto> GetTallyData(string tallytype, string datetype, string date);
        public int SaveBookEntryDetail(BookEntryDetailEntity bookEntryDetailEntity);
    }
}
