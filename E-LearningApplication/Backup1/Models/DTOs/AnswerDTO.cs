using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.DTOs {
    public class AnswerDTO {
        public int AnswerId { get; set; }
        public string AnswerType { get; set; }
        public string AnswerValue { get; set; }
    }
}