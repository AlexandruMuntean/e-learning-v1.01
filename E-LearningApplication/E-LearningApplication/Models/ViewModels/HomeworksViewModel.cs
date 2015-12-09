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

        [Display(Name = "Homework description")]
        [StringLength(30)]
        public string HomeworkDescription { get; set; }

        [Display(Name="Deadline")]
        public Nullable<System.DateTime> HomeworkDeadline { get; set; }

        [Display(Name = "Homework type")]
        [StringLength(30)]
        public string HomeworkType { get; set; }

        [Display(Name="Homework points")]
        public Nullable<decimal> HomeworkPoints { get; set; }

        [Display(Name = "Homework submission type")]
        [StringLength(30)]
        public string HomeworkSubmissionType { get; set; }

        [Display(Name="Homework access time span")]
        public Nullable<System.DateTime> HomeworkAccessSpan { get; set; }
    }
}