using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class ModuleUserViewModel {
        public int ModuleUserId { get; set; }

        [Display(Name="Course module")]
        public CourseModule CourseModule { get; set; }

        [Display(Name="User of the module")]
        public Users Users { get; set; }
    }
}