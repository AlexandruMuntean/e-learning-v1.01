using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class MailViewModel {
        [Required]
        [Display(Name = "Sender address")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Sender { get; set; }

        [Required]
        [Display(Name = "Receiver addresses")]
        public string Receivers { get; set; }

        [Required]
        [Display(Name = "Subject")]
        [StringLength(100)]
        public string MailSubject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string MailBody { get; set; }

    }
}