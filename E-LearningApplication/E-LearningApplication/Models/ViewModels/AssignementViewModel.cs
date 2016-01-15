using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class AssignementViewModel {
        public int HomeworkId { get; set; }

        [Display(Name="Homework name")]
        public string HomeworkName { get; set; }

        [Display(Name = "Short description")]
        public string HomeworkDescription { get; set; }

        [Display(Name = "Deadline")]
        public Nullable<System.DateTime> HomeworkDeadline { get; set; }

        [Display(Name = "Maximum awarded points")]
        public Nullable<decimal> HomeworkPoints { get; set; }

        public string HomeworkCode { get; set; }
        public int AssignementId { get; set; }
        public Nullable<int> RecipientId { get; set; }

        [Display(Name = "Subject code")]
        public string SubjectCode { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public Nullable<int> GradeId { get; set; }
    }
}