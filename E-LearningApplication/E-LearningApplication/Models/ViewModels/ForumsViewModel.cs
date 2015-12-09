using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class ForumsViewModel {
        public int ForumId { get; set; }

        [StringLength(30)]
        [Display(Name="Forum category")]
        public string Category { get; set; }
    }
}