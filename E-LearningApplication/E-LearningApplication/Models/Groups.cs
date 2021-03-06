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
    
    public partial class Groups
    {
        public Groups()
        {
            this.GroupMembers = new HashSet<GroupMembers>();
            this.Tests = new HashSet<Tests>();
            this.HomeworkAssignements = new HashSet<HomeworkAssignements>();
        }
    
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupType { get; set; }
        public Nullable<int> OwnerId { get; set; }
    
        public virtual ICollection<GroupMembers> GroupMembers { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<Tests> Tests { get; set; }
        public virtual ICollection<HomeworkAssignements> HomeworkAssignements { get; set; }
    }
}
