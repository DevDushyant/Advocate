using Microsoft.AspNetCore.Mvc.Rendering;

namespace Advocate.Models
{
    public class GazzetSearchViewModel
    {
        public SelectList LstNature { get; set; }
        public SelectList LstGazzets { get; set; }
        public SelectList LstParts { get; set; }
        public SelectList LstYear { get; set; }
        public SelectList LstDepartment{ get; set; }
        public int GNatureId { get; set; }
        public string Category { get; set; }
        public string part_section { get; set; }
        public int Year { get; set; }
        public string Department { get; set; }
    }
}
