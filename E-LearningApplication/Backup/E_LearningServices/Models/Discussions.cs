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
    
    public partial class Discussions
    {
        public Discussions()
        {
            this.Messages = new HashSet<Messages>();
        }
    
        public int DiscusionId { get; set; }
        public string DiscussionSubject { get; set; }
    
        public virtual ICollection<Messages> Messages { get; set; }
    }
}