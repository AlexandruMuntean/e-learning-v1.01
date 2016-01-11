using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class SyllabusViewModel {
        public int SyllabusId { get; set; }

        [Display(Name="Link to file")]
        [StringLength(50)]
        public string FileLink { get; set; }
    }
}