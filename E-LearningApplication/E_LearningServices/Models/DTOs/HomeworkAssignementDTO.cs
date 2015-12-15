using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Models.DTOs {
    public class HomeworkAssignementDTO {
        public int AssignementId { get; set; }
        public Nullable<int> StudentId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> CourseModuleId { get; set; }
        public Nullable<int> HomeworkId { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public Nullable<int> GradeId { get; set; }

        public string RecipientName { get; set; }
    }
}