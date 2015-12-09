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
    
    public partial class Homeworks
    {
        public int HomeworkId { get; set; }
        public string HomeworkName { get; set; }
        public string HomeworkDescription { get; set; }
        public Nullable<System.DateTime> HomeworkDeadline { get; set; }
        public string HomeworkType { get; set; }
        public Nullable<decimal> HomeworkPoints { get; set; }
        public string HomeworkSubmissionType { get; set; }
        public Nullable<System.DateTime> HomeworkAccessSpan { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> GradeId { get; set; }
    
        public virtual Grades Grades { get; set; }
        public virtual Groups Groups { get; set; }
    }
}
