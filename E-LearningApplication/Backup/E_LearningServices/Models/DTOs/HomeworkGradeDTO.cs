using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Models.DTOs {
    public class HomeworkGradeDTO {
        public int GradeId { get; set; }
        public Nullable<decimal> GradeValue { get; set; }
        public Nullable<System.DateTime> Gradedatetime { get; set; }
        public int HomeworkId { get; set; }
        public int CourseId { get; set; }
        public int CourseModuleId { get; set; }
        public int AnswerId { get; set; }
        public int AssignementId { get; set; }
    }
}