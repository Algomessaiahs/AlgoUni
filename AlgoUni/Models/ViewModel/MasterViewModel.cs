using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc; namespace AlgoUni.ViewModel
{
    public class MasterViewModel
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
        public string Degree { get; set; }
        public string ExamCode; public string DepartmentCode { get; set; }
        public string Semester; public IEnumerable<SelectListItem> SubjectCode; public IEnumerable<SelectListItem> Subject; public string Grade;
    }
}

