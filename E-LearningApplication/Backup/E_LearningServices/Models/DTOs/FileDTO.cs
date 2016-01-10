using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Models.DTOs {
    public class FileDTO {
        public int rootId { get; set; }
        public int parentId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
}