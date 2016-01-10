using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class CalendarEventsViewModel {
        public int EventId { get; set; }

        [StringLength(30)]
        [Display(Name="Short description of the event")]
        public string EventDescription { get; set; }

        [Display(Name="Start date of the event")]
        public Nullable<System.DateTime> EventStartdatetime { get; set; }

        [Display(Name="End date of the event")]
        public Nullable<System.DateTime> EventEnddatetime { get; set; }

        [Display(Name="Course associated with the event(if any)")]
        public Courses Courses { get; set; }
    }
}