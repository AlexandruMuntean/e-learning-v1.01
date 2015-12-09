using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class ResourceViewModel {
        public int ResourceId { get; set; }

        [Display(Name="Resource type")]
        [StringLength(30)]
        public string ResourceType { get; set; }

        public string FileId { get; set; }

        [Display(Name="File name")]
        [StringLength(30)]
        public string FileName { get; set; }

        public int CourseId { get; set; }
        
        public int ModuleID { get; set; }
    }
}