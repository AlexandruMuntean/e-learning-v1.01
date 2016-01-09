using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Models.DTOs {
    public class HomeworkDTO {
        public int HomeworkId { get; set; }
        public string HomeworkName { get; set; }
        public string HomeworkCode { get; set; }
        public string HomeworkDescription { get; set; }
        public Nullable<System.DateTime> HomeworkDeadline { get; set; }
        public Nullable<decimal> HomeworkPoints { get; set; }
        public Nullable<System.DateTime> HomeworkAccessSpan { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> CourseModuleId { get; set; }
        public Nullable<int> OwnerId { get; set; }
        
    }
}