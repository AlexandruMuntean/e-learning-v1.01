using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.DTOs {
    public class MessageDTO {
        public int MessageId { get; set; }

        public string MessageContent { get; set; }

        public DateTime MesageData { get; set; }

        public Nullable<int> UserId { get; set; }

        public Nullable<int> ForumId { get; set; }
        
        public Nullable<int> DiscusionId { get; set; }

        public Nullable<int> ConversationId { get; set; }
    }
}