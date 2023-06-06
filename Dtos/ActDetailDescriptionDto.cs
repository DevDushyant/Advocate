using Advocate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Dtos
{
    public class ActDetailDescriptionDto
    {
        public string ActType { get; set; }
        public int ActNumber { get; set; }
        public int ActYear { get; set; }
        public string AssentBy { get; set; }
        public string AssentDate { get; set; }
        public string ActName { get; set; }
        public string PublishedIn { get; set; }
        public string PartName { get; set; }
        public string Nature { get; set; }
        public string GazetteDate { get; set; }
        public string PageNo { get; set; }
        public string ComeInforce { get; set; }
        public string   Subjects { get; set; }        
        public List<RepealedActEntity> RepealedList { get; set; }
        public List<AmendedActEntity> AmanededList { get; set; }

        public List<RepealedActEntity> RepealedByList { get; set; }
        public List<AmendedActEntity> AmanededByList { get; set; }
        public List<SubjectEntity> SubjectList { get; set; }
        public string ActCategory_amended { get; set; }
        public string ActCategory_repealed { get; set; }
        public List<ActBookDto> ActBookList { get; set; }
        public List<ActDto> Repealed_ActList { get; set; }
        public List<ActDto> Amended_ActList { get; set; }

        public List<ActDto> RepealedBy_ActList { get; set; }
        public List<ActDto> AmendedBy_ActList { get; set; }
    }

    public class ActBookDto
    {
        public string BookName { get; set; }
        public string ShortName { get; set; }
        public int Year { get; set; }
        public string PageNo { get; set; }
        public int SerialNo { get; set; }
        public string Volume { get; set; }
    }
}
