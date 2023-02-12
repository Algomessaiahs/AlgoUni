using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace AlgoUni.ViewModel
{
    public class StudentViewModel
    {
        public string studentID { get; set; }
        public string StudentName { get; set; }
        public string DepartmentCode { get; set; }
        public string Department { get; set; }
        public string Degree { get; set; }
        public string Semester { get; set; }
        //public string ExamCode { get; set; }
        //public int UnivCode { get; set; }
        //public int CollegeCode { get; set; }
        public List<StudentMarksViewModel> ListstudentMarksViewModels { get; set; }
    }
}