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
    
    public partial class Conversations
    {
        public Conversations()
        {
            this.Messages = new HashSet<Messages>();
        }
    
        public int ConversationId { get; set; }
        public string FilePath { get; set; }
        public string ConversationName { get; set; }
    
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
