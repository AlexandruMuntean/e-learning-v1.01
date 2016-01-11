using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class CourseModuleViewModel {
        public int ModuleId { get; set; }

        [Display(Name="Module Name")]
        [StringLength(20)]
        public string ModuleName { get; set; }

        [Display(Name="End date of the module")]
        public Nullable<System.DateTime> Moduledatetime { get; set; }

        [Display(Name = "The course associated(if any)")]
        public Nullable<int> CourseId { get; set; }

        [Display(Name="The grade method associated(if any)")]
        public Nullable<int> GradeId { get; set; }

        [Display(Name = "The previous module associated(if any)")]
        public Nullable<int> PreviousModuleId { get; set; }
    }
}