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
    
    public partial class Syllabus
    {
        public Syllabus()
        {
            this.Courses = new HashSet<Courses>();
        }
    
        public int SyllabusId { get; set; }
        public string FileLink { get; set; }
    
        public virtual ICollection<Courses> Courses { get; set; }
    }
}
