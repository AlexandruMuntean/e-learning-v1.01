using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Models.DTOs {
    public class ReceivedGradeDTO {
        public string HomeworkName { get; set; }
        public string HomeworkDescription { get; set; }
        public Nullable<System.DateTime> HomeworkDeadline { get; set; }
        public Nullable<decimal> HomeworkPoints { get; set; }
        public Nullable<int> StudentId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string RecipientName { get; set; }
        public string SubjectCode { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public string AnswerValue { get; set; }
        public Nullable<int> GradeId { get; set; }
        public Nullable<decimal> GradeValue { get; set; }
        public Nullable<System.DateTime> Gradedatetime { get; set; }
    }
}