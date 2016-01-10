using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class TestsViewModel {
        public int TestId { get; set; }

        [Display(Name="Test name")]
        [StringLength(30)]
        public string TestName { get; set; }

        [Display(Name="Test description")]
        [StringLength(30)]
        public string TestDescription { get; set; }

        [Display(Name="Deadline")]
        public Nullable<System.DateTime> TestDeadline { get; set; }

        [Display(Name="Test type")]
        public string TestType { get; set; }

        [Display(Name="Number of questions")]
        public Nullable<int> NumberOfQuestions { get; set; }

        [Display(Name="Availability span of the test")]
        public Nullable<System.DateTime> TestAvailabilitySpan { get; set; }

        [Display(Name="Attendants section")]
        public string AttendantsSection { get; set; }
    }
}