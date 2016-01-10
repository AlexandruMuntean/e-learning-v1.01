using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.DTOs {
    public class CoursesDTO {
        public int CourseId { get; set; }
        public Nullable<int> NumberOfCredits { get; set; }
        public string CourdeCode { get; set; }
        public Nullable<int> OwnerId { get; set; }
        public Nullable<int> SyllabusId { get; set; }
        public string CourseName { get; set; }
    }
}