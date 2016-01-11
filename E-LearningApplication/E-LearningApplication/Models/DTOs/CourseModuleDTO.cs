using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.DTOs {
    public class CourseModuleDTO {
        public int ModuleId { get; set; }
        public Nullable<System.DateTime> Moduledatetime { get; set; }
        public Nullable<int> GradeId { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> PreviousModuleId { get; set; }
        public string ModuleName { get; set; }
    
    }
}