//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_LearningApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Questions
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionDescription { get; set; }
        public Nullable<decimal> QuestionPoints { get; set; }
        public string QuestionType { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> AnswerId { get; set; }
    
        public virtual Answers Answers { get; set; }
        public virtual Courses Courses { get; set; }
    }
}
