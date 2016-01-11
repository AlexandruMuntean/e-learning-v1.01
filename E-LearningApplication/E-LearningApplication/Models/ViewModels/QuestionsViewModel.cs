using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class QuestionsViewModel {
        public int QuestionId { get; set; }

        [Display(Name="Question name")]
        [StringLength(30)]
        public string QuestionName { get; set; }

        [Display(Name="Description")]
        [StringLength(30)]
        public string QuestionDescription { get; set; }

        [Display(Name="Points")]
        public Nullable<decimal> QuestionPoints { get; set; }

        [Display(Name="Question type")]
        [StringLength(30)]
        public string QuestionType { get; set; }

        [Display(Name="The answer associated(if any)")]
        public Answers Answers { get; set; }

        [Display(Name = "The course associated(if any)")]
        public Courses Courses { get; set; }
    }
}