using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class ReceivedGradeViewModel {
        [Display(Name="Homework name")]
        public string HomeworkName { get; set; }
        
        [Display(Name = "Homework description")]
        public string HomeworkDescription { get; set; }
        
        [Display(Name = "Homework deadline")]
        public Nullable<System.DateTime> HomeworkDeadline { get; set; }
        
        [Display(Name = "Maximum points awarded")]
        public Nullable<decimal> HomeworkPoints { get; set; }

        public Nullable<int> StudentId { get; set; }
        public Nullable<int> GroupId { get; set; }
        
        [Display(Name="Recipient name")]
        public string RecipientName { get; set; }

        [Display(Name = "Subject")]
        public string SubjectCode { get; set; }
        public Nullable<int> AnswerId { get; set; }

        [Display(Name = "Answer given")]
        public string AnswerValue { get; set; }
        public Nullable<int> GradeId { get; set; }

        [Display(Name = "Grade received")]
        public Nullable<decimal> GradeValue { get; set; }

        [Display(Name = "Grading time")]
        public Nullable<System.DateTime> Gradedatetime { get; set; }
    }
}