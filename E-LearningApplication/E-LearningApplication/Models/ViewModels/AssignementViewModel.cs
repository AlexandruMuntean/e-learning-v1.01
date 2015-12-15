using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class AssignementViewModel {
        public int HomeworkId { get; set; }
        public string HomeworkName { get; set; }
        public string HomeworkDescription { get; set; }
        public Nullable<System.DateTime> HomeworkDeadline { get; set; }
        public Nullable<decimal> HomeworkPoints { get; set; }
        public string HomeworkSubmissionType { get; set; }
        public int AssignementId { get; set; }
        public Nullable<int> RecipientId { get; set; }
        public string SubjectCode { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public Nullable<int> GradeId { get; set; }
    }
}