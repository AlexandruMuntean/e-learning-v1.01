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
    
    public partial class CourseModule
    {
        public CourseModule()
        {
            this.ModuleUser = new HashSet<ModuleUser>();
        }
    
        public int ModuleId { get; set; }
        public Nullable<System.DateTime> Moduledatetime { get; set; }
        public Nullable<int> GradeId { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> PreviousModuleId { get; set; }
        public string ModuleName { get; set; }
    
        public virtual Courses Courses { get; set; }
        public virtual Grades Grades { get; set; }
        public virtual ICollection<ModuleUser> ModuleUser { get; set; }
    }
}
