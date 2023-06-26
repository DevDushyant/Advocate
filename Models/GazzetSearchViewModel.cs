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
        public string  GNatureId { get; set; }
        public int GazzetId { get; set; }
        public int PartId { get; set; }
        public int Year { get; set; }
        public string DepartmentId { get; set; }
    }
}
