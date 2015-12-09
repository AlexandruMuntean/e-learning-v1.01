using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class AnswersViewModel {
        public int AnswerId { get; set; }

        [Display(Name="Answer Type")]
        [StringLength(20)]
        public string AnswerType { get; set; }

        [Display(Name="Answer Value")]
        public string AnswerValue { get; set; }
    }
}