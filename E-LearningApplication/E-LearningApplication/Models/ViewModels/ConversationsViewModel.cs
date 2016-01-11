using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class ConversationsViewModel {
        public int ConversationId { get; set; }

        [StringLength(50)]
        public string FilePath { get; set; }

        [StringLength(30)]
        [Display(Name="Conversation name")]
        public string ConversationName { get; set; }
    }
}