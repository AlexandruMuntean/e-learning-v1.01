using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.DTOs {
    public class ResourcesDTO {
        public int ResourceId { get; set; }
        public string ResourceType { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }
        public int CourseId { get; set; }
        public int ModuleID { get; set; }
    }
}