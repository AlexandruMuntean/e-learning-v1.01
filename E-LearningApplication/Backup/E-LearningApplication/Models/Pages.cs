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
    
    public partial class Pages
    {
        public Pages()
        {
            this.Resources = new HashSet<Resources>();
        }
    
        public int PageId { get; set; }
        public string FilePath { get; set; }
        public string PageLink { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ICollection<Resources> Resources { get; set; }
    }
}
