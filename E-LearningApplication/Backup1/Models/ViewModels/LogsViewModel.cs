using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class LogsViewModel {
        public int Id { get; set; }

        [Display(Name="Event datetime")]
        public System.DateTime EventDateTime { get; set; }

        [Display(Name="Event level")]
        [StringLength(100)]
        public string EventLevel { get; set; }

        [Display(Name="Event info")]
        public string EventInfo { get; set; }
    }
}