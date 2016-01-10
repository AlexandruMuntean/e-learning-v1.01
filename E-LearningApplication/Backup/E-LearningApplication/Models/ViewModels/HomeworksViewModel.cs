using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class HomeworksViewModel {
        public int HomeworkId { get; set; }

        [Display(Name="Homework name")]
        [StringLength(30)]
        public string HomeworkName { get; set; }

        [Display(Name = "Short description of the homework")]
        [StringLength(30)]
        public string HomeworkDescription { get; set; }

        [Display(Name="Deadline")]
        public Nullable<System.DateTime> HomeworkDeadline { get; set; }

        [Display(Name="Homework points")]
        public Nullable<decimal> HomeworkPoints { get; set; }

        [Display(Name="Homework access time span")]
        public Nullable<System.DateTime> HomeworkAccessSpan { get; set; }

        public string HomeworkCode { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> CourseModuleId { get; set; }
        public Nullable<int> OwnerId { get; set; }

    }
}