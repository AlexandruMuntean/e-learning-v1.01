using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class CoursesViewModel {
        public int CourseId { get; set; }

        [Display(Name="Number of credits")]
        public Nullable<int> NumberOfCredits { get; set; }

        [Display(Name = "Course Name")]
        [StringLength(20)]
        public string CourseName { get; set; }

        [Required]
        [Display(Name="Course code")]
        [StringLength(30)]
        public string CourdeCode { get; set; }

        [Display(Name="The associated syllabus(if any)")]
        public Nullable<int> SyllabusId { get; set; }

        [Display(Name="The associated owner(if any)")]
        public Nullable<int> OwnerId { get; set; }

        [Display(Name = "Enrollement key")]
        [StringLength(14)]
        public String EnrollementKey { get; set; }

        public UsersInCourse userInCourse { get; set; }
    }
}