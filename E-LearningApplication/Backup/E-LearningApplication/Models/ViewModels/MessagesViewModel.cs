using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class MessagesViewModel {
        public int MessageId { get; set; }

        [Display(Name = "Message content")]
        public string MessageContent { get; set; }

        [Display(Name = "Date when the message was sent")]
        public DateTime MesageData { get; set; }

        [Display(Name = "User")]
        public Users user { get; set; }
    }
}