using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Dtos
{
    public class ActDto: BaseDto
    {
        public string ActName { get; set; }
        public int ActNumber { get; set; }
        public int ActYear { get; set; }
        public string ActCategory { get; set; }
        public int ActTypeId { get; set; }
        public string ActType { get; set; }
    }
}
