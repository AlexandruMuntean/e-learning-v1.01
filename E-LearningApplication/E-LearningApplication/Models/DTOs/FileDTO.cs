using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.DTOs {
    public class FileDTO {
        public int rootId { get; set; }
        public int parentId { get; set; }
        public string fileName { get; set; }
    }
}