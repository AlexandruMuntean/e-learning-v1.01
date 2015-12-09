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
    
    public partial class Courses
    {
        public Courses()
        {
            this.CalendarEvents = new HashSet<CalendarEvents>();
            this.CourseModule = new HashSet<CourseModule>();
            this.Grades = new HashSet<Grades>();
            this.Questions = new HashSet<Questions>();
            this.Tests = new HashSet<Tests>();
            this.Users1 = new HashSet<Users>();
        }
    
        public int CourseId { get; set; }
        public Nullable<int> NumberOfCredits { get; set; }
        public string CourdeCode { get; set; }
        public Nullable<int> OwnerId { get; set; }
        public Nullable<int> SyllabusId { get; set; }
        public string CourseName { get; set; }
    
        public virtual ICollection<CalendarEvents> CalendarEvents { get; set; }
        public virtual ICollection<CourseModule> CourseModule { get; set; }
        public virtual Users Users { get; set; }
        public virtual Syllabus Syllabus { get; set; }
        public virtual ICollection<Grades> Grades { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
        public virtual ICollection<Tests> Tests { get; set; }
        public virtual ICollection<Users> Users1 { get; set; }
    }
}
