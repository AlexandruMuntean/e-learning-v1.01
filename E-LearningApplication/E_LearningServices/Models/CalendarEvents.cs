//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_LearningServices.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CalendarEvents
    {
        public int EventId { get; set; }
        public string EventDescription { get; set; }
        public Nullable<System.DateTime> EventStartdatetime { get; set; }
        public Nullable<System.DateTime> EventEnddatetime { get; set; }
        public Nullable<int> CourseId { get; set; }
    
        public virtual Courses Courses { get; set; }
    }
}
