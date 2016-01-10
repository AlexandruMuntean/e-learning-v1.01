using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class DiscussionsViewModel {
        public int DiscusionId { get; set; }

        [StringLength(30)]
        [Display(Name="The topic of the discussion(if any)")]
        public string DiscussionSubject { get; set; }
    }
}