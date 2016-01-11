using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class PagesViewModel {
        public int PageId { get; set; }

        [Display(Name="Path to file")]
        [StringLength(50)]
        public string FilePath { get; set; }

        [Display(Name="Link to the page with the file")]
        [StringLength(50)]
        public string PageLink { get; set; }
    }
}