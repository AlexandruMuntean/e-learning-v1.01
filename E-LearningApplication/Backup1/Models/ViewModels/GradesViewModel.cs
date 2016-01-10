using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class GradesViewModel {
        public int GradeId { get; set; }

        [Display(Name="Grade value")]
        public Nullable<decimal> GradeValue { get; set; }

        [Display(Name="Grading date")]
        public Nullable<System.DateTime> Gradedatetime { get; set; }

    }
}